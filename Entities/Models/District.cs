using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class District:BaseEntity
    {
        [Key]
        public int DistrictHeaderId { get; set; }
        public int DivisionHeaderId { get; set; }
        public string DistrictName { get; set; }
        public ICollection<Upazila> Upazilas { get; set; }
        public Division Division { get; set; }
    }
}
