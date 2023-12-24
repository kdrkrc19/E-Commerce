using AutoMapper;
using Entities.Models;
using Entities.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IModelService> _modelService;
        private readonly Lazy<IBrandService> _brandService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryMananger, IMapper mapper, IDataShaper<ProductsDto> _shaper,
                              IConfiguration configuration, UserManager<User> userManager)
        {
            _modelService = new Lazy<IModelService>(() => new ModelManager(repositoryMananger, mapper));
            _brandService = new Lazy<IBrandService>(() => new BrandManager(repositoryMananger, mapper));
            _productService = new Lazy<IProductService>(() => new ProductManager(repositoryMananger, mapper, _shaper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(userManager, mapper, configuration));
        }
        public IModelService ModelService => _modelService.Value;
        public IBrandService BrandService => _brandService.Value;
        public IProductService ProductService => _productService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
