using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EFCore;

namespace E_Commerce_WebAPI
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>  
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                prj => prj.MigrationsAssembly("Web_API_eCommerce"));

            return new RepositoryContext(builder.Options);
        }
    }
}
