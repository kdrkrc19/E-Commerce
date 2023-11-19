using Entities;
using Entities.Models;
using Entities.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<ExpandoObject> GetAllProductsList(RequestParameters parameters, bool trackChanges);
        IEnumerable<Product> GetAllProductsPagination(RequestParameters parameters, bool trackChanges);
        Product GetProduct(int id, bool trackChanges);
        ProductsDto CreateProduct(ProductsDto product);
        void UpdateProduct(ProductsDto product);// ÖNCE GET YAPILACAK Sonra Set     
        void DeleteProduct(int id);// ÖNCE GET YAPILACAK Sonra Set
    }
}
