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
    public class AdminService : IAdminInterface
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();

        public bool AddBikeDetail(BikeDetailModel bikeModel)
        {
            try
            {
                int save = 0;
                Seller seller = new Seller()
                {
                    SellerFirstName = bikeModel.SellerFirstName,
                    SellerlastName = bikeModel.SellerlastName,
                    SellerEmail = bikeModel.SellerEmail,
                    SellerPhoneNumber = bikeModel.SellerPhoneNumber
                };

                seller = _DBContext.Seller.Add(seller);
                save = _DBContext.SaveChanges();

                BikeDetails bike = new BikeDetails()
                {
                    BrandName = bikeModel.BrandName,
                    BikeModel = bikeModel.BikeModel,
                    KilometresDriven = bikeModel.KilometresDriven,
                    Price = bikeModel.Price,
                    SellerId = seller.SellerId,
                    Years = bikeModel.Years,
                    Description = bikeModel.Description,
                    BikeImages = bikeModel.BikeImages,
                    //IsDelete = false
                };

                _DBContext.BikeDetails.Add(bike);
                save = _DBContext.SaveChanges();

                return save > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteBike(int BikeId)
        {

            int save = 0;
            BikeDetails bike = _DBContext.BikeDetails.Where(m => m.BikeId == BikeId).FirstOrDefault();
            _DBContext.BikeDetails.Remove(bike);

            save = _DBContext.SaveChanges();

            return save > 0 ? true : false;
        }

        public bool EditBike(BikeDetailModel bikeModel)
        {
            int save = 0;
            BikeDetails bike = _DBContext.BikeDetails.Where(m => m.BikeId == bikeModel.BikeId).FirstOrDefault();
            Seller seller = _DBContext.Seller.Where(m => m.SellerId == bikeModel.SellerId).FirstOrDefault();

            bike.BrandName = bikeModel.BrandName;
            bike.BikeModel = bikeModel.BikeModel;
            bike.KilometresDriven = bikeModel.KilometresDriven;
            bike.Price = bikeModel.Price;
            bike.SellerId = seller.SellerId;
            bike.Years = bikeModel.Years;
            bike.Description = bikeModel.Description;
            bike.BikeImages = bikeModel.BikeImages;
            _DBContext.Entry(bike).State = System.Data.Entity.EntityState.Modified;
            save = _DBContext.SaveChanges();

            _DBContext.Entry(seller).State = System.Data.Entity.EntityState.Modified;
            seller.SellerFirstName = bikeModel.SellerFirstName;
            seller.SellerlastName = bikeModel.SellerlastName;
            seller.SellerEmail = bikeModel.SellerEmail;
            seller.SellerPhoneNumber = bikeModel.SellerPhoneNumber;
            save = _DBContext.SaveChanges();

            return save > 0 ? true : false;
        }

        public BikeDetailModel GetBikeDetailsById(int BikeId)
        {
            BikeDetailModel bikeModel = new BikeDetailModel();
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BikeId",BikeId)
            };

            bikeModel = _DBContext.Database.SqlQuery<BikeDetailModel>("exec SP_GetBikeDetailById @BikeId", sqlParameters).FirstOrDefault();
            return bikeModel;
        }
    }
}
