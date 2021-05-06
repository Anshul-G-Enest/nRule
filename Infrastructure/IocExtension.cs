using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rule.WebAPI.Context;
using Rule.WebAPI.Services;
using Rule.WebAPI.Services.Interface;

namespace Rule.WebAPI.Infrastructure
{
    public static class IocExtension
    {
        public static IServiceCollection BuildContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RuleDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("RuleDB")));
            services.AddHttpContextAccessor();

            services.AddScoped<IRuleData, RuleData>();
            services.AddScoped<IPersonData, PersonData>();
            services.AddScoped<IExecuteData, ExecuteData>();
            return services;
        }
    }
}
