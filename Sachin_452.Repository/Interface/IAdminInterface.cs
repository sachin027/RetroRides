using Sachin_452.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452.Repository.Interface
{
    public interface IAdminInterface
    {
        bool AddBikeDetail(BikeDetailModel bikeDetail);
        bool EditBike(BikeDetailModel bikeModel);
        bool DeleteBike(int BikeId);

        BikeDetailModel GetBikeDetailsById(int id);
    }
}
