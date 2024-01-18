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
    public class RepositoryModel : RepositoryBase<Model>, IRepositoryModel
    {
		private readonly RepositoryContext _context;
		public RepositoryModel(RepositoryContext context) : base(context)
        {
			_context = context;
        }

        public Model GetModel(int id, bool trackChanges) => GenericReadExpression(x => x.ModelId.Equals(id), trackChanges).SingleOrDefault();

		public IEnumerable<Model> GetPagedModels(RequestParameters parameters, bool trackChanges)
		{
			var veriler = _context.Models.AsQueryable().Skip((parameters.PageNumber - 1) * parameters.PageSize)
							 .Take(parameters.PageSize)
							 .ToList();

			return veriler;
		}
	}
}
