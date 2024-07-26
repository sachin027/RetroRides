using Sachin_452.APIs.JWTAuthentication;
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
    public class LoginAPIController : ApiController
    {
        //Sachin_452Entities _DBContext = new Sachin_452Entities();
        ILoginInterface _LoginInterface = new LoginService();

        [HttpPost]
        [Route("api/LoginAPI/RegisterUser")]
        public UserModel RegisterUser(UserModel userModel)
        {
            try
            {
                UserModel user = _LoginInterface.RegisterUser(userModel);
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("api/LoginAPI/Login")]
        public UserModel Login(LoginModel loginModel)
        {
            try
            {
                UserModel user = _LoginInterface.Login(loginModel);
                user.Token = Authentication.GenerateJWTAuthetication(user.FirstName);
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}