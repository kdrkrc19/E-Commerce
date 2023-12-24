using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IBrandService BrandService { get; }
        IModelService ModelService { get; }
        IProductService ProductService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
