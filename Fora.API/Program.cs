
using Fora.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Fora
{
    public class Program
    {
        private static readonly string _edgarURLDefault = "https://data.sec.gov/api/xbrl/companyfacts/";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(Program));

            var config = builder.Configuration;

            string? connectionString = config.GetConnectionString("ForaEdgarDbLocalConn");
            builder.Services.AddDbContext<EdgarCompanyDataContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            builder.Services.AddSingleton<ICrudDbService, CrudDbService>();

            var edgarUrlConfig = config.GetValue<string>("EdgarUrl");
            if (string.IsNullOrEmpty(edgarUrlConfig)) edgarUrlConfig = _edgarURLDefault;

            builder.Services.AddHttpClient("Edgar", httpClient =>
            {
                httpClient.BaseAddress = new Uri(edgarUrlConfig);

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // TODO: verify accept header
                // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.42.0");
            });

            builder.Services.AddSingleton<ICallEdgarService, CallEdgarService>();

            // Background service depends on other services and should be added last
            builder.Services.AddHostedService<UpdateDbBackgroundService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(d =>
            {
                d.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fora Edgar API",
                    Version = "v1.0",
                    Description = "Fora coding challenge V2"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                d.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // TODO: Do we we need auth?
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
