using Hackaton.Application.Interfaces.Persistence;
using Hackaton.Application.Interfaces.Persistence.Helper;
using Hackaton.Domain.Entities;
using Hackaton.Persistence.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hackaton.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<ISortHelper<Employee>, SortHelper<Employee>>();
            services.AddScoped<IDataShaper<Employee>, DataShaper<Employee>>();
            services.AddDbContext<RepositoryContext>(options
                        => options.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b
                        => b.MigrationsAssembly("Hackaton.Persistence")));
            return services;
        }
    }
}