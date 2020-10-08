using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class VmDonorList
    {
        public int BloodDonorHeaderId { get; set; }
        public string BloodDonorName { get; set; }
        public string BloodDonorId { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? ThanaId { get; set; }
        public string ThanaName { get; set; }
        public string Address { get; set; }
        public int BloodgroupId { get; set; }
        public string BloodgroupName { get; set; }
        public string ImageUrl { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string Occupation { get; set; }
        public bool ReadyForDonate { get; set; }
        public string ReadyForDonateString { get; set; }
        public string LastDonated { get; set; }
        public string FbUrl { get; set; }
        public string IsActive { get; set; }
    }
}
