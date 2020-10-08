using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Division:BaseEntity
    {
        [Key]
        public int DivisionHeaderId { get; set; }
        public string DivisionName { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
