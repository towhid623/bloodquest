using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ConfigMaster:BaseEntity
    {
        [Key]
        public int ConfigId { get; set; }
        public string ConfigName { get; set; }
        public string ConfigShortName { get; set; }
    }
}
