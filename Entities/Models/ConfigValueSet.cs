using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ConfigValueSet:BaseEntity
    {
        [Key]
        public int ConfigValueId { get; set; }
        [Required]
        public string ConfigValue { get; set; }
        public string ConfigShortValue { get; set; }
        public int ConfigValueSetId { get; set; }
        public int? ParentConfigValueId { get; set; }
        public int? ParentConfigValueSetId { get; set; }
        public virtual ConfigMaster ConfigMaster { get; set; }
    }
}
