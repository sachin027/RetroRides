using Sachin_452.Models.DBContext;
using Sachin_452.Models.ViewModel;
using Sachin_452.Repository.Interface;
using Sachin_452.Repository.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sachin_452.APIs.Controllers
{
    public class UserAPIController : ApiController
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        IUserInterface _userInterface = new UserService();
        // GET: UserAPI
        [HttpGet]
        [Route("api/UserAPI/BikeListForUser")]
        public List<BikeDetailModel> BikeListForUser()
        {
            return _userInterface.BikeList();
        }

        [HttpGet]
        [Route("api/UserAPI/GetBikeById")]
        public BikeDetailModel GetBikeById(int BikeId)
        {
            return _userInterface.GetBikeById(BikeId);
        }


        [HttpGet]
        [Route("api/UserAPI/GetUserProfile")]
        public UserModel GetUserProfile(int UserId)
        {
            
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@UserId",UserId)
            };
            UserModel userModel = _DBContext.Database.SqlQuery<UserModel>("exec SP_GetUserProfile @UserId", sqlParameters).FirstOrDefault();

            return userModel;
        }

        [Route("api/UserAPI/GetCountryList")]
        public List<CountryModel> GetCountryList()
        {
            List<CountryModel> countryModels = _DBContext.Database.SqlQuery<CountryModel>("exec SP_GetCountryList").ToList();
            return countryModels;
        }


        [Route("api/UserAPI/EditUserProfile")]
        public bool EditUserProfile(UserModel userModel)
        {
            try
            {
                int editId = 0;
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                new SqlParameter("@UserId",userModel.UserID),
                new SqlParameter("@FirstName",userModel.FirstName),
                new SqlParameter("@LastName",userModel.LastName),
                new SqlParameter("@MiddleName",(object)userModel.MiddleName ?? DBNull.Value),
                new SqlParameter("@Email",userModel.Email),
                new SqlParameter("@MobileNumber",userModel.PhoneNumber),
                new SqlParameter("@DOB",(object)userModel.DOB ?? DBNull.Value),
                new SqlParameter("@Address1",userModel.AddressLine1),
                new SqlParameter("@Address2",(object)userModel.AddressLine2 ?? DBNull.Value),
                new SqlParameter("@CountryId",Convert.ToString(userModel.Country)),
                new SqlParameter("@StateId",Convert.ToString(userModel.State)),
                new SqlParameter("@CityId",Convert.ToString( userModel.City)),
                new SqlParameter("@ZipCode",userModel.ZipCode),
                new SqlParameter("@Image",(object)userModel.ProfilePhoto ?? DBNull.Value)
                };

                editId = _DBContext.Database.SqlQuery<int>("exec SP_EditUserProfile  @UserId,@FirstName, @LastName, @MiddleName, @Email, @MobileNumber,@DOB, @Address1, @Address2, @CountryId, @StateId, @CityId,@ZipCode,@Image", sqlParameters).FirstOrDefault();

                return editId > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}