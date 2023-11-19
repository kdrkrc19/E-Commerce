using Entities.Models;
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
        public RepositoryBrand(RepositoryContext context) : base(context)
        {

        }

        public Brand GetBrand(int id, bool trackChanges) => GenericReadExpression(x => x.BrandId.Equals(id), trackChanges).SingleOrDefault();
    }
}
