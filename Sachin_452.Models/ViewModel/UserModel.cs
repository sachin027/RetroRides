using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sachin_452.Models.ViewModel
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage ="Please enter firstName!")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter lastName!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter EmailId")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number!")]
        public string PhoneNumber { get; set; }

        public Nullable<System.DateTime> DOB { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public Nullable<int> Country { get; set; }
        [Required(ErrorMessage = "Please Select State")]
        public Nullable<int> State { get; set; }
        [Required(ErrorMessage = "Please Select City")]
        public Nullable<int> City { get; set; }
        [Required(ErrorMessage = "Please Select ZipCode")]
        public Nullable<int> ZipCode { get; set; }

        [Required(ErrorMessage = "Please upload Image")]
        public string ProfilePhoto { get; set; }
        //public HttpPostedFileBase ProfilePhotoPath { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
