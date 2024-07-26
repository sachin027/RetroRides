using Newtonsoft.Json;
using Sachin_452.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sachin_452.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public async Task<ActionResult> AdminDashboard()
        {
            string res = await WebHelper.WebHelper.HttpRequestResponce("api/AdminAPI/BikeList");
            List<BikeDetailModel> _Bikelist = new List<BikeDetailModel>();
            _Bikelist = JsonConvert.DeserializeObject<List<BikeDetailModel>>(res);
            foreach (var item in _Bikelist)
            {
                item.BikeImagesPath = item.BikeImages.Split(',');
            }
            return View(_Bikelist);
        }

        public ActionResult AddBikeDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddBikeDetails(BikeDetailModel bikeDetailModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0 || bikeDetailModel.BikeImages.EndsWith("JPG") || bikeDetailModel.BikeImages.EndsWith("PNG") || bikeDetailModel.BikeImages.EndsWith("JPEG"))
                    {
                        string a = "";
                        for (var i = 0; i < Request.Files.Count; i++)
                        {
                            a += GetFileUniqueName(Request.Files[i]);
                            if (i < Request.Files.Count - 1)
                                a += ",";
                        }
                        bikeDetailModel.BikeImages = a;
                    }

                    string url = "api/AdminAPI/AddBike";
                    string content = JsonConvert.SerializeObject(bikeDetailModel);
                    string res = await WebHelper.WebHelper.HttpRequestResponce(url, content);
                    bool save = JsonConvert.DeserializeObject<bool>(res);
                    if (save)
                    {
                        TempData["success"] = "Bike Added Successfully";
                        return RedirectToAction("AdminDashboard");
                    }
                    else
                    {
                        TempData["error"] = "Something went wrong!";
                        return View(bikeDetailModel);
                    }
                }
                else
                {
                    TempData["error"] = "Something went wrong!";
                    return View(bikeDetailModel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> EditDetail(int BikeId)
        {
            string res = await WebHelper.WebHelper.HttpRequestResponce($"api/AdminAPI/GetBikeDetailById?BikeId={BikeId}");
            BikeDetailModel bikeModel = new BikeDetailModel();
            bikeModel = JsonConvert.DeserializeObject<BikeDetailModel>(res);
            Session["BikeImages"] = bikeModel.BikeImages;
            bikeModel.BikeImagesPath = bikeModel.BikeImages.Split(',');
            return View(bikeModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditDetail(BikeDetailModel bikeModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0 && Request.Files.Count < 5)
                {
                    string a = "";
                    for (var i = 0; i < Request.Files.Count; i++)
                    {
                        a += GetFileUniqueName(Request.Files[i]);
                        if (i < Request.Files.Count - 1)
                            a += ",";
                    }
                    bikeModel.BikeImages = a;
                }
                else
                {
                    bikeModel.BikeImages = Session["BikeImages"].ToString();
                    bikeModel.BikeImagesPath = Session["BikeImages"].ToString().Split(',');
                    return View(bikeModel);
                }


                string res = await WebHelper.WebHelper.HttpRequestResponce("api/AdminAPI/EditBikeDetails", JsonConvert.SerializeObject(bikeModel));
                bool save = JsonConvert.DeserializeObject<bool>(res);
                if (save)
                {
                    TempData["success"] = "Edit Successfully";
                    return RedirectToAction("Index");
                }
                else
                {

                    bikeModel.BikeImagesPath = Session["BikeImages"].ToString().Split(',');
                    return View(bikeModel);
                }
            }
            else
            {

                bikeModel.BikeImagesPath = Session["BikeImages"].ToString().Split(',');

                return View(bikeModel);
            }

        }

        public async Task<ActionResult> Delete(int BikeId)
        {
            string res = await WebHelper.WebHelper.HttpRequestResponce($"api/AdminAPI/DeleteBike?BikeId={BikeId}");
            bool save = JsonConvert.DeserializeObject<bool>(res);
            return RedirectToAction("Index");
        }

        public string GetFileUniqueName(HttpPostedFileBase file)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yyyy-ss") + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Images/BikeImage/") + fileName);
            return fileName;
        }


    }
}