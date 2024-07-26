using Sachin_452.Models.DBContext;
using Sachin_452.Models.ViewModel;
using Sachin_452.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452.Repository.Services
{
    public class UserService : IUserInterface
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        public List<BikeDetailModel> BikeList()
        {
            try
            {
                List<BikeDetailModel> bikeDetails = _DBContext.Database.SqlQuery<BikeDetailModel>("exec SP_GetBikeListForUser").ToList();
                return bikeDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BikeDetailModel GetBikeById(int BikeId)
        {
            try
            {
                BikeDetailModel bikeModel = new BikeDetailModel();
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                new SqlParameter("@BikeId",BikeId)
                };

                bikeModel = _DBContext.Database.SqlQuery<BikeDetailModel>("exec SP_GetBikeById @BikeId", sqlParameters).FirstOrDefault();
                return bikeModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
