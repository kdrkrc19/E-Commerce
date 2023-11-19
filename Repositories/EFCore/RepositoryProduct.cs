using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
    {
        private readonly RepositoryContext _context;
        public RepositoryProduct(RepositoryContext context) : base (context)
        {
            _context = context;
        }
        public Product GetProduct(int id, bool trackChanges) => GenericReadExpression(x => x.ProductId.Equals(id), trackChanges).SingleOrDefault();

        public IEnumerable<Product> GetPagedProducts(RequestParameters parameters, bool trackChanges)
        {
            var veriler = _context.Products.AsQueryable().Skip((parameters.PageNumber - 1) * parameters.PageSize)
                             .Take(parameters.PageSize)
                             .ToList();

            return veriler;
        }
    }
}
