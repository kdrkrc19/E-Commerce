using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryModel : IRepositoryBase<Model>
    {
        Model GetModel(int id, bool trackChanges);
		IEnumerable<Model> GetPagedModels(RequestParameters parameters, bool trackChanges);
	}
}
