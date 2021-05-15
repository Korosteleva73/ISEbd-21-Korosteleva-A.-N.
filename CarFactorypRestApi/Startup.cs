using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryDatabaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace CarFactorypRestApi
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
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<ICarStorage, CarStorage>();
            services.AddTransient<IWarehouseStorage, WarehouseStorage>();
            services.AddTransient<IDetailStorage, DetailStorage>();
            services.AddTransient<OrderLogic>();
            services.AddTransient<ClientLogic>();
            services.AddTransient<CarLogic>();
            services.AddTransient<WarehouseLogic>();
            services.AddTransient<DetailLogic>();
            services.AddControllers().AddNewtonsoftJson();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}