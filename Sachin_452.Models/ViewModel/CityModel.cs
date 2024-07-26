using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452.Models.ViewModel
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> StateId { get; set; }
    }
}
