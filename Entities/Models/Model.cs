using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }

		[ForeignKey("BrandId")] // BrandId sütunu ile ilişkilendirildi
		public int BrandId { get; set; }
		public Brand Brand { get; set; }
	}
}
