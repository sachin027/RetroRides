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
    public class LoginService : ILoginInterface
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();

        public UserModel Login(LoginModel loginModel)
        {
            try
            {
                UserModel userModel = new UserModel();
                //UserRideTable user = _DBContext.UserRideTable.FirstOrDefault(x => loginModel.EmailID == "admin@gmail.com" && loginModel.Password == "admin452");
                UserRideTable user = _DBContext.UserRideTable.FirstOrDefault(x =>  x.Email == loginModel.EmailID  && x.Password == loginModel.Password);
                if(user != null)
                {
                    userModel.Email = user.Email;
                    userModel.FirstName = user.FirstName;
                    userModel.MiddleName = user.MiddleName;
                    userModel.LastName = user.LastName;
                    userModel.Password = user.Password;
                    userModel.UserID = user.UserID;
                }
                return userModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserModel RegisterUser(UserModel userModel)
        {
            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@fname" , userModel.FirstName),
                    new SqlParameter("@mname" , userModel.MiddleName),
                    new SqlParameter("@lname" , userModel.LastName),
                    new SqlParameter("@email" , userModel.Email),
                    new SqlParameter("@contactNumber" , userModel.PhoneNumber),
                    new SqlParameter("@address1" , userModel.AddressLine1),
                    new SqlParameter("@address2" , userModel.AddressLine2),
                    new SqlParameter("@country" , userModel.Country),
                    new SqlParameter("@state" , userModel.State),
                    new SqlParameter("@city" , userModel.City),
                    new SqlParameter("@zipcode" , userModel.ZipCode),
                    new SqlParameter("@dob" , userModel.DOB),
                    new SqlParameter("@password" , userModel.Password),
                    new SqlParameter("@profilephoto" , userModel.ProfilePhoto),
                };

                UserModel user = _DBContext.Database.SqlQuery<UserModel>("SP_RegisterUser @fname, @mname , @lname , @email, @contactNumber , @address1 , @address2, @country , @state, @city , @zipcode , @dob,@password,@profilephoto", sqlParameters).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
