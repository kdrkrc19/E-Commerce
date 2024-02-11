using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }

        [ForeignKey("BrandId")] // BrandId sütunu ile ilişkilendirildi
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("ModelId")] // ModelId sütunu ile ilişkilendirildi
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public string ProductImagePath { get; set; }

    }
}
