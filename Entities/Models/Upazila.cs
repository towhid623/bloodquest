using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Upazila:BaseEntity
    {
        [Key]
        public int UpazilaHeaderId { get; set; }
        public int DistrictHeaderId { get; set; }
        public string UpazilaName { get; set; }
        public District District { get; set; }
    }
}
