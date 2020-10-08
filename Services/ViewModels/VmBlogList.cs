using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class VmBlogList
    {
        public int BlogId { get; set; }
        public string ImageUrl { get; set; }
        public string Heading { get; set; }
        public string Details { get; set; }
        public int BloodDonorHeaderId { get; set; }
    }
}
