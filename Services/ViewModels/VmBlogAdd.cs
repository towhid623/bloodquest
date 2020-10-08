using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.ViewModels
{
    public class VmBlogAdd
    {
        public int BlogHeaderId { get; set; }
        public string ImageUrl { get; set; }
        public string BlogTitle { get; set; }
        public string Description { get; set; }
        public bool IsFeedback { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
