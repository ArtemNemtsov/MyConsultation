using ConsultationService.Services;
using DBContext.Connect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consultation.Core
{
    public static class ServiceProviderExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            string connectionString = Configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<d6tp5on2hao81vContext>(options => options.UseNpgsql(connectionString));
        }

        public static void AddPageServices(this IServiceCollection services)
        {
            services.AddTransient<PatientService>();
            services.AddTransient<СonsultationService>();
        }
    }
}
