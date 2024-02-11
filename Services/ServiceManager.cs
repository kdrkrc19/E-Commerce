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
        private readonly Lazy<IEmailService> _mailService;
        public ServiceManager(IRepositoryManager repositoryMananger, IMapper mapper, IDataShaper<ModelsDto> _shaperModel, IDataShaper<ProductsDto> _shaperProduct,
                              IDataShaper<BrandsDto> _shaperBrand, IDataShaper<EmailUsDto> _shaperMail, IConfiguration configuration, UserManager<User> userManager)
        {
            _modelService = new Lazy<IModelService>(() => new ModelManager(repositoryMananger, mapper, _shaperModel));
            _brandService = new Lazy<IBrandService>(() => new BrandManager(repositoryMananger, mapper, _shaperBrand));
            _productService = new Lazy<IProductService>(() => new ProductManager(repositoryMananger, mapper, _shaperProduct));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(userManager, mapper, configuration));
            _mailService = new Lazy<IEmailService>(() => new EmailManager(repositoryMananger, mapper, _shaperMail));
        }
        public IModelService ModelService => _modelService.Value;
        public IBrandService BrandService => _brandService.Value;
        public IProductService ProductService => _productService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IEmailService EmailService => _mailService.Value;
    }
}
