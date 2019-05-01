using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PizzaReservation.API.Models;
using PizzaReservation.Models;
using PizzaReservation.Models.Data;
using PizzaReservation.Models.Repositories;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace PizzaReservation.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            #region Connection database

            var connString = Configuration.GetConnectionString("DefaultConnString");
            services.AddDbContext<PizzaReservationContext>(options => options.UseSqlServer(connString));

            #endregion

            #region Logger

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.RollingFile($"Serilogs/PizzaReservation-{DateTime.Now.ToShortDateString()}.txt")
                .CreateLogger();

            #endregion

            #region Swagger

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new Info { Title = "Pizza Reservation API", Description = "API om pizza's te bestellen" });
            });

            #endregion

            #region Automapper

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<Pizza, PizzaDTO>();
                cfg.CreateMap<Size, SizeDTO>();
                cfg.CreateMap<Topping, ToppingDTO>();                
            });

            #endregion

            #region Service Registration

            services.AddScoped<ISizeRepo, SizeRepo>();
            services.AddScoped<IToppingRepo, ToppingRepo>();
            services.AddScoped<IPizzaRepo, PizzaRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddAutoMapper(typeof(Startup));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Logger

            loggerFactory.AddSerilog();
            Log.Logger.Warning("Serilog running");

            #endregion

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Reservation API");
                cfg.RoutePrefix = string.Empty;
            });

            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
