using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.ModelsDTO
{
    public class ProductsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string ProductImagePath { get; set; }
    }

}
