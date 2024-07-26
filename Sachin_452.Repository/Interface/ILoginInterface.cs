using Sachin_452.Models.DBContext;
using Sachin_452.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452.Repository.Interface
{
    public interface ILoginInterface
    {
        UserModel RegisterUser(UserModel userModel);

        UserModel Login(LoginModel loginModel);
    }
}
