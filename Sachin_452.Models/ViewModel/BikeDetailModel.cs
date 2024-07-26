using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452.Models.ViewModel
{
    public class BikeDetailModel
    {
        public int BikeId { get; set; }
        public int SellerId { get; set; }
        [Required(ErrorMessage ="Please enter brand Name")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Please enter Model Name")]
        public string BikeModel { get; set; }
        [Required(ErrorMessage = "Please enter Years")]
        public Nullable<int> Years { get; set; }
        [Required(ErrorMessage = "Please enter driven Kilometres ")]
        public Nullable<int> KilometresDriven { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        public Nullable<int> Price { get; set; }
        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter BikeImages")]
        public string BikeImages { get; set; }
        [Required(ErrorMessage = "Please enter Seller First Name")]
        public string SellerFirstName { get; set; }
        [Required(ErrorMessage = "Please enter Seller Last Name")]
        public string SellerlastName { get; set; }
        [Required(ErrorMessage = "Please enter Seller EmailId")]
        public string SellerEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [MaxLength(10, ErrorMessage = "Enter valid Number")]
        public string SellerPhoneNumber { get; set; }

        public string[] BikeImagesPath { get; set; }

        public int IsFavourite { get; set; }
    }
}
