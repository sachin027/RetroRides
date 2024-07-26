using Newtonsoft.Json;
using Sachin_452.Models.DBContext;
using Sachin_452.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sachin_452.Controllers
{
    public class UserController : Controller
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        // GET: User
        public async Task<ActionResult> UserDashboard()
        {
            string res = await WebHelper.WebHelper.HttpRequestResponce($"api/UserAPI/BikeListForUser");
            List<BikeDetailModel> list = new List<BikeDetailModel>();
            UserRideTable user = _DBContext.UserRideTable.FirstOrDefault(u => u.UserID == SessionHelper.SessionHelpers.UserID);
            ViewBag.profile = user.ProfilePhoto;
            list = JsonConvert.DeserializeObject<List<BikeDetailModel>>(res);
            foreach (var item in list)
            {
                item.BikeImagesPath = item.BikeImages.Split(',');
            }
            if (list != null && list.Count() > 0)
            {
                List<string> BikeBrandList = list.Select(m => m.BrandName).ToList();
                ViewBag.BikeBrandList = BikeBrandList;
            }
            return View(list);
        }

        public async Task<ActionResult> UserProfile()
        {
            try
            {
                int UserId = SessionHelper.SessionHelpers.UserID;
                string res = await WebHelper.WebHelper.HttpRequestResponce($"api/UserAPI/GetUserProfile?UserId={UserId}");
                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(res);

                string res1 = await WebHelper.WebHelper.HttpRequestResponce("api/UserAPI/GetCountryList");
                ViewBag.CountryList = JsonConvert.DeserializeObject<List<CountryModel>>(res1);
                return View(userModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> GetBike(int BikeId)
        {
            BikeDetailModel bikeModel = new BikeDetailModel();
            string res = await WebHelper.WebHelper.HttpRequestResponce($"api/UserAPI/GetBikeById?BikeId={BikeId}");
            bikeModel = JsonConvert.DeserializeObject<BikeDetailModel>(res);
            bikeModel.BikeImagesPath = bikeModel.BikeImages.Split(',');
            return View(bikeModel);
        }

        public async Task<ActionResult> AddFavourite(int BikeId)
        {
            int UserId1 = SessionHelper.SessionHelpers.UserID;
            UserRideTable _UsersRide = _DBContext.UserRideTable.FirstOrDefault(u => u.UserID == UserId1);
            ViewBag.Name = _UsersRide.FirstName + " " + _UsersRide.LastName;
            ViewBag.ProfilePicture = _UsersRide.ProfilePhoto;
            int UserId = SessionHelper.SessionHelpers.UserID;
            SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@BikeId",BikeId),
                    new SqlParameter("@UserId",UserId)
                };
            int a = _DBContext.Database.SqlQuery<int>("exec AddToFavourite @BikeId,@UserId", sqlParameters).FirstOrDefault();
            if (a == 0)
            {
                TempData["Error"] = " Already in Favourite";
                return RedirectToAction("UserDashboard");
            }
            else
            {
                TempData["Success"] = "Bike Added to Favourite";
                return RedirectToAction("UserDashboard");
            }

        }

        [HttpGet]
        public async Task<ActionResult> FavouriteBike()
        {
            try
            {
                int UserId1 = SessionHelper.SessionHelpers.UserID;
                UserRideTable _UsersRide = _DBContext.UserRideTable.FirstOrDefault(u => u.UserID == UserId1);
                //ViewBag.Name = _UsersRide.FirstName + " " + _UsersRide.LastName;
                int UserId = SessionHelper.SessionHelpers.UserID;
                ViewBag.ProfilePicture = _UsersRide.ProfilePhoto;
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("@UserId",UserId)
                    };
                List<BikeDetailModel> _BikeModel = _DBContext.Database.SqlQuery<BikeDetailModel>("exec SP_GetFavouriteList @UserId", sqlParameters).ToList();
                return View(_BikeModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UserProfile(UserModel userModel)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                if (userModel.ProfilePhoto != null)
                {
                    userModel.ProfilePhoto = GetUniqueImage(Request.Files[0]);
                }
                else
                {

                    userModel.ProfilePhoto = null;
                }
                string res = await WebHelper.WebHelper.HttpRequestResponce("api/UserAPI/EditUserProfile", JsonConvert.SerializeObject(userModel));
                bool edit = JsonConvert.DeserializeObject<bool>(res);
                if (edit)
                {
                    TempData["success"] = "Edit Profile Successfully";
                    Session["ProfileImage"] = userModel.ProfilePhoto;
                    return RedirectToAction("UserDashboard");
                }
                else
                {
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);
            }
        }
        private string GetUniqueImage(HttpPostedFileBase file)
        {
            string uniqueFilename = DateTime.Now.ToString("ddmmyyyyss") + "-" + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Images/ProfilePicture/") + uniqueFilename);
            return uniqueFilename;
        }

        public ActionResult RemoveBikeFromFavourite(int BikeId, int UserId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BikeId" , BikeId),
                new SqlParameter("@UserId" , UserId)
            };
            _DBContext.Database.ExecuteSqlCommand("exec SP_removeBikeFromFavourite @BikeId , @UserId" , sqlParameters);
            return RedirectToAction("UserDashboard");
        }
    }
}