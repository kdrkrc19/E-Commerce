using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryBrand : IRepositoryBase<Brand>
    {
        Brand GetBrand(int id, bool trackChanges);
		IEnumerable<Brand> GetPagedBrands(RequestParameters parameters, bool trackChanges);
	}
}
