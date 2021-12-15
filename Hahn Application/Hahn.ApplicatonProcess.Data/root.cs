using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace Hahn.ApplicatonProcess.Data
{
    public class root
    {
        public static void injectDependencies(IServiceCollection services)
        {
            services.AddDbContext<HahnAppContext>(opts => opts.UseInMemoryDatabase("HahnDB"));
            services.AddScoped<HahnAppContext>();           
        }
    }
}
