using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Blog:BaseEntity
    {
        [Key]
        public int BlogId { get; set; }
        public string ImageUrl { get; set; }
        public string Heading { get; set; }
        public string Details { get; set; }
        public int BloodDonorHeaderId { get; set; }
    }
}
