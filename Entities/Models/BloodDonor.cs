using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BloodDonor:BaseEntity
    {
        [Key]
        public int BloodDonorHeaderId { get; set; }
        public string BloodDonorName { get; set; }
        public string BloodDonorId { get; set; }
        public int? Country { get; set; }
        public int Division { get; set; }
        public int District { get; set; }
        public int? Thana { get; set; }
        public string Address { get; set; }
        public int Bloodgroup { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Occupation { get; set; }
        public bool ReadyForDonate { get; set; }
        public DateTime? LastDonated { get; set; }
        public string FbUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}
