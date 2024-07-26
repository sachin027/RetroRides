using Newtonsoft.Json;
using Sachin_452.Models.DBContext;
using Sachin_452.Models.ViewModel;
using Sachin_452.WebHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sachin_452.Controllers
{
    public class HomeController : Controller
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        private static Random random = new Random();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel user)
        {
            try
            {
                string url = "api/LoginAPI/Login";
                string content = JsonConvert.SerializeObject(user);
                string response = await WebHelper.WebHelper.HttpRequestResponce(url, content);

                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(response);
                var cookie = new HttpCookie("jwt", userModel.Token)
                {
                    HttpOnly = true,
                    // Secure = true, // Uncomment this line if your application is running over HTTPS
                };
                Response.Cookies.Add(cookie);
                var usr = _DBContext.UserRideTable.Where(u => u.Email == user.EmailID && u.Password == user.Password).FirstOrDefault();
                if (user != null && user.EmailID == "admin@gmail.com")
                {
                    SessionHelper.SessionHelpers.EmailID = userModel.Email;
                    SessionHelper.SessionHelpers.UserName = userModel.FirstName;
                    Session["AdminUsername"] = SessionHelper.SessionHelpers.UserName;

                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (user != null )
                {
                    SessionHelper.SessionHelpers.UserID = usr.UserID;
                    SessionHelper.SessionHelpers.EmailID = userModel.Email;
                    SessionHelper.SessionHelpers.UserName = userModel.FirstName;

                    Session["photo"] = usr.ProfilePhoto;
                    Session["AdminUsername"] = SessionHelper.SessionHelpers.UserName;
                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("UserDashboard", "User");
                }
                else
                {
                    TempData["error"] = "something went wrong!";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Register()
        {
            ViewBag.Countries = _DBContext.Country.ToList(); 
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        if (!IsValidImageFile(file))
                        {
                            return View(user); // Return to view with model containing errors
                        }

                        // If valid, convert image and assign to user model
                        string base64Image = convertImage(file);
                        user.ProfilePhoto = base64Image;
                    }
                    string password = GenerateRandomNumericPassword(6);
                    user.Password = password;

                    string url = "api/LoginAPI/RegisterUser";
                    string content = JsonConvert.SerializeObject(user);
                    string response = await WebHelper.WebHelper.HttpRequestResponce(url, content);
                    UserModel userTable = JsonConvert.DeserializeObject<UserModel>(response);

                    if (userTable.UserID > 0)
                    {
                        TempData["success"] = "Register Successfully";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["error"] = "Email Already exists!";
                        return View(userTable);
                    }
                }
                else
                {
                    TempData["error"] = "something went wrong";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string convertImage(HttpPostedFileBase file)
        {
            string uniqueFilename = DateTime.Now.ToString("ddmmyyyyss") + "-" + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Images/ProfilePicture/") + uniqueFilename);
            return uniqueFilename;
        }
        private bool IsValidImageFile(HttpPostedFileBase file)
        {
            // Check file extension and size
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ProfilePhoto", "Invalid file type. Please upload a JPG, JPEG, or PNG file.");
                return false;
            }
            if (file.ContentLength > 3 * 1024 * 1024) // 3 MB
            {
                ModelState.AddModelError("ProfilePhoto", "File size exceeds the limit of 3 MB.");
                return false;
            }
            return true; // Indicates validation passed
        }

        private string GenerateRandomNumericPassword(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public JsonResult GetStateByCountry(int countryId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@CountryId " , countryId)
                };
                List<StateModel> _StateList = _DBContext.Database.SqlQuery<StateModel>("SP_GetStateByCountry @CountryId", sqlParameter).ToList();
                return Json(_StateList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetCityByState(int stateId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@StateId " , stateId)
                };
                List<CityModel> _CityList = _DBContext.Database.SqlQuery<CityModel>("SP_GetCityByState @StateId", sqlParameter).ToList();
                return Json(_CityList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetZipCodeByCity(int cityId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@CityId " , cityId)
                };
                List<ZipModel> _ZipCodeList = _DBContext.Database.SqlQuery<ZipModel>("SP_GetZipCodeByCity @CityId", sqlParameter).ToList();
                return Json(_ZipCodeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                DeleteAllCookies();
                TempData["success"] = "Logout successfully.";
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void DeleteAllCookies()
        {
            foreach (var cookie in Request.Cookies.AllKeys)
            {
                var expiredCookie = new HttpCookie(cookie)
                {
                    Expires = DateTime.UtcNow.AddDays(-1)
                };
                Response.Cookies.Add(expiredCookie);
            }
        }


    }
}