using backendtest.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;

namespace backendtest.API
{
    public class Startup
    {
        public IConfiguration configuration { get; }
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            //if (hostEnvironment.IsDevelopment())
            //{
            //    builder.AddUserSecrets<Startup>();
            //}

            configuration = builder.Build();
        }
         
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(configuration);
            services.AddIdentityConfig(configuration);
            services.RegisterServices();
            services.AddSwaggerConfiguration();
        }
         
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration(); 
            app.UseApiConfiguration(env); 
        }
    }
}
