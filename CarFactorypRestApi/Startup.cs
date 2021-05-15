using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryDatabaseImplement.Implements;
using CarFactoryBusinessLogic.HelperModels;
using System.Threading;
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
        // This method gets called by the runtime. Use this method to add services to the        container.
   
    public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<ICarStorage, CarStorage>();
            services.AddTransient<IMessageInfoStorage, MessageInfoStorage>();
            services.AddTransient<OrderLogic>();
            services.AddTransient<ClientLogic>();
            services.AddTransient<CarLogic>();
            services.AddTransient<MailLogic>();
            services.AddControllers().AddNewtonsoftJson();
            MailLogic.MailConfig(new MailConfig
            {
                SmtpClientHost = "smtp.gmail.com",
                SmtpClientPort = 587,
                MailLogin = "korostelevaa729@gmail.com",
                MailPassword = "korosteleva2001"
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageInfoStorage messageInfoStorage)
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