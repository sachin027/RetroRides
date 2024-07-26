using Sachin_452.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452.Repository.Interface
{
    public interface IUserInterface
    {
        List<BikeDetailModel> BikeList();
        BikeDetailModel GetBikeById(int BikeId);
    }
}
