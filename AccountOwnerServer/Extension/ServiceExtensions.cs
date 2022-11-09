using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Contracts;
using LoggerService;
//using Microsoft.EntityFrameworkCore;
using Entities;
using Repository;
using Entities.Helpers;
using Entities.Models;

namespace AccountOwnerServer.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CrosPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });
        }

       

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => {

            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        //check for update 
        public static void ConfigureAccountOwner(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:AccountOwner"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));    
      
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<ISortHelper<Owner>, SortHelper<Owner>>();
            services.AddScoped<ISortHelper<Account>,SortHelper<Account>>();

            services.AddScoped<IDataShaper<Owner>, DataShaper<Owner>>();
            //services.AddScoped<IDataShaper<Account>, DataShaper<Account>();


            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
         
        
    }

    
   
}
