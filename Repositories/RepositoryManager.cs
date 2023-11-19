using Repositories.Contracts;
using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IRepositoryModel> _repositoryModel;
        private readonly Lazy<IRepositoryBrand> _repositoryBrand;
        private readonly Lazy<IRepositoryProduct> _repositoryProduct;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _repositoryModel = new Lazy<IRepositoryModel>(() => new RepositoryModel(_context));
            _repositoryBrand = new Lazy<IRepositoryBrand>(() => new RepositoryBrand(_context));
            _repositoryProduct = new Lazy<IRepositoryProduct>(() => new RepositoryProduct(_context));
        }

        public IRepositoryBrand Brand => _repositoryBrand.Value; // LazyLoading Kullanımı 
        public IRepositoryModel Model => _repositoryModel.Value;
        public IRepositoryProduct Product => _repositoryProduct.Value;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
