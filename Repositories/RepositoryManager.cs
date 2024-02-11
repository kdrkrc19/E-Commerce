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
        private readonly Lazy<IRepositoryEmailUs> _repositoryEmailUs;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _repositoryModel = new Lazy<IRepositoryModel>(() => new RepositoryModel(_context));
            _repositoryBrand = new Lazy<IRepositoryBrand>(() => new RepositoryBrand(_context));
            _repositoryProduct = new Lazy<IRepositoryProduct>(() => new RepositoryProduct(_context));
            _repositoryEmailUs = new Lazy<IRepositoryEmailUs>(() => new RepositoryEmailUs(_context));
        }

        public IRepositoryBrand Brand => _repositoryBrand.Value; // LazyLoading Kullanımı 
        public IRepositoryModel Model => _repositoryModel.Value;
        public IRepositoryProduct Product => _repositoryProduct.Value;
        public IRepositoryEmailUs EmailUs => _repositoryEmailUs.Value;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
