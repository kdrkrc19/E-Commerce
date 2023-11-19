using Entities.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace E_Commerce_WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>(); //AddScoped yeni bir response oluşturuyo // Interface somutlaştırma
            //Transient parçalanıp kendi oluşturabiliyo
            //singleton hangi requesti alırsa alsın aynı responsu döner
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureDataShaper(this IServiceCollection services) 
        {
            services.AddScoped<IDataShaper<ProductsDto>, DataShaper<ProductsDto>>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config => 
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.acunmedyaakademi.apiroot+json");
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.acunmedyaakademi.hateoas+json");
                }
            });
        }
    }
}
