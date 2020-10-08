using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class VmDonorDetails
    {
        public int BloodDonorHeaderId { get; set; }
        public string BloodDonorName { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Thana { get; set; }
        public string Address { get; set; }
        public string Bloodgroup { get; set; }
        public string ImageUrl { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string ReadyForDonate { get; set; }
        public string LastDonated { get; set; }
        public string FbUrl { get; set; }
    }
}
