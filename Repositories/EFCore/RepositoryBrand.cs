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
    public class RepositoryBrand : RepositoryBase<Brand>, IRepositoryBrand
    {
		private readonly RepositoryContext _context;
        public RepositoryBrand(RepositoryContext context) : base(context)
        {
			_context = context;
        }

        public Brand GetBrand(int id, bool trackChanges) => GenericReadExpression(x => x.BrandId.Equals(id), trackChanges).SingleOrDefault();

		public IEnumerable<Brand> GetPagedBrands(RequestParameters parameters, bool trackChanges)
		{
			var veriler = _context.Brands.AsQueryable().Skip((parameters.PageNumber - 1) * parameters.PageSize)
							 .Take(parameters.PageSize)
							 .ToList();

			return veriler;
		}
	}
}
