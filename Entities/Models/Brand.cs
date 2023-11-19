using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Brand
    {
        [Key]
        
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
