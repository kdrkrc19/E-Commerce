using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryProduct : IRepositoryBase<Product>
    {
        Product GetProduct(int id, bool trackChanges);
        IEnumerable<Product> GetPagedProducts(RequestParameters parameters, bool trackChanges);
    }
}
