using Sachin_452.Models.DBContext;
using Sachin_452.Models.ViewModel;
using Sachin_452.Repository.Interface;
using Sachin_452.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sachin_452.APIs.Controllers
{
    public class AdminAPIController : ApiController
    {
        IAdminInterface _adminInterface = new AdminService();
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        // GET: AdminAPI

        [Route("api/AdminAPI/AddBike")]
        public bool AddBike(BikeDetailModel bikeDetailModel)
        {
            try
            {
                return _adminInterface.AddBikeDetail(bikeDetailModel); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/AdminAPI/BikeList")]
        public List<BikeDetailModel> BikeList()
        {
            try
            {
                List<BikeDetailModel> list = new List<BikeDetailModel>();
                list = _DBContext.Database.SqlQuery<BikeDetailModel>("exec SP_GetBikeList").ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/AdminAPI/EditBikeDetails")]
        public bool EditBikeDetails(BikeDetailModel bikeModel)
        {
            return _adminInterface.EditBike(bikeModel);
        }

        [Route("api/AdminAPI/DeleteBike")]
        [HttpGet]
        public bool DeleteBike(int BikeId)
        {
            return _adminInterface.DeleteBike(BikeId);
        }


        [HttpGet]
        [Route("api/AdminAPI/GetBikeDetailById")]
        public BikeDetailModel GetBikeDetailById(int BikeId)
        {
            return _adminInterface.GetBikeDetailsById(BikeId);
        }
    }
}