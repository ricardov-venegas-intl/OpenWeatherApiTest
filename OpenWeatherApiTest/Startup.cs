using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using OpenWeatherApiTest.Mappers;
using OpenWeatherApiTest.OpenWeatherMapApi;
using OpenWeatherApiTest.OpenWeatherMapApi.Implementation;

namespace OpenWeatherApiTest
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenWeatherApiTest", Version = "v1" });
            });
            services.AddSingleton<OpenWeatherMapClientConfiguration>(
                new OpenWeatherMapClientConfiguration
                {
                    Urlbase = Configuration.GetValue<string>("OpenWeatherMapClientConfiguration_Urlbase"),
                    ApiKey = Configuration.GetValue<string>("OpenWeatherMapClientConfiguration_ApiKey")
                });
            services.AddTransient<IOpenWeatherMapClient, OpenWeatherMapClient>();
            services.AddTransient<IOneCallResponseToWeatherForecastMapper, OneCallResponseToWeatherForecastMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenWeatherApiTest v1"));
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
