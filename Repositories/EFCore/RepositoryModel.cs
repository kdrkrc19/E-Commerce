using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryModel : RepositoryBase<Model>, IRepositoryModel
    {
        public RepositoryModel(RepositoryContext context) : base(context)
        {

        }

        public Model GetModel(int id, bool trackChanges) => GenericReadExpression(x => x.ModelId.Equals(id), trackChanges).SingleOrDefault();  
    }
}
