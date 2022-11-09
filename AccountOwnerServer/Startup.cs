using AccountOwnerServer.Extension;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace AccountOwnerServer
{
    public class Startup
    {
       public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)//To inject our services in application,
            //by using the ConfigureServices we need to Call IServiceCollection in ConfigureServices
        {
            services.ConfigureCors();
           services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureAccountOwner(Configuration);
            services.ConfigureRepositoryWrapper();
            services.AddControllers();
            //services.AddAutoMapper(typeof(Startup));

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
            .AddNewtonsoftJson();
                }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment eve)
        {
            
            if (eve.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
             
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            }
            );
            //Khow as Middleware 
            //The Middleware must have in
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            
            });

                
            
            
        }

    }
}
