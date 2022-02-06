using GringottsBankingApp.API.Extensions;
using GringottsBankingApp.API.Filters;
using GringottsBankingApp.Core.Repositories;
using GringottsBankingApp.Core.Services;
using GringottsBankingApp.Core.UnitOfWorks;
using GringottsBankingApp.Data;
using GringottsBankingApp.Data.Repositories;
using GringottsBankingApp.Data.UnitOfWorks;
using GringottsBankingApp.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBankingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<AccountNotFoundFilter>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITransferService, TransferService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString"), configure =>
                {
                    configure.MigrationsAssembly("GringottsBankingApp.Data");
                });
            });

            services.AddControllers(x =>
            {
                x.Filters.Add(new ValidationFilter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GringottsBankingApp.API", Version = "v1" });
            });

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GringottsBankingApp.API v1"));
            }

            app.UseCustomExceptionHandler();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
