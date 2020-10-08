using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.ViewModels
{
    public class VmDonorAdd
    {
        public int BloodDonorHeaderId { get; set; }
        [Required]
        public string BloodDonorName { get; set; }
        public string BloodDonorId { get; set; }
        public int? Country { get; set; }
        [Required]
        public int Division { get; set; }
        [Required]
        public int District { get; set; }
        public int? Thana { get; set; }
        public string Address { get; set; }
        [Required]
        public int Bloodgroup { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public int Gender { get; set; }
        public string Occupation { get; set; }
        [Required]
        public bool ReadyForDonate { get; set; }
        public string LastDonated { get; set; }
        public string FbUrl { get; set; }
        public bool? IsActive { get; set; }
        public HttpPostedFileBase ProPic { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
