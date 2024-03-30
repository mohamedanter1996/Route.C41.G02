using Microsoft.Extensions.DependencyInjection;
using Route.C41.G02.BLL;
using Route.C41.G02.BLL.Interfaces;

namespace Route.C41.G02.PL.Helpers
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
