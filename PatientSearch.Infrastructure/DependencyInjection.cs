using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientSearch.Application.Interfaces;
using PatientSearch.Infrastructure.Repositories;
using PatientSearch.Infrastructure.Services;

namespace PatientSearch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<PatientSearchDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserManagementRepo, UserManagementRepo>();
            services.AddScoped<IPatientRepo, PatientRepo>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

    }
}
