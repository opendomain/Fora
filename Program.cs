
using Fora.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Fora
{
    public class Program
    {
        private static readonly string _edgarURLDefault = "https://data.sec.gov/api/xbrl/companyfacts/";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<HostOptions>(x =>
            {
                x.ServicesStartConcurrently = false;
                x.ServicesStopConcurrently = false;
            });

            string? connectionString = builder.Configuration.GetConnectionString("ForaEdgarDbServerConn");
            builder.Services.AddDbContext<EdgarCompanyDataContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            builder.Services.AddSingleton<ICrudDbService, CrudDbService>();
            var config = builder.Configuration;

            var edgarUrlConfig = config.GetValue<string>("EdgarUrl");
            if (string.IsNullOrEmpty(edgarUrlConfig)) edgarUrlConfig = _edgarURLDefault;

            builder.Services.AddHttpClient("Edgar", httpClient =>
            {
                httpClient.BaseAddress = new Uri(edgarUrlConfig);

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.42.0");
            });

            builder.Services.AddSingleton<ICallEdgarService, CallEdgarService>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddHostedService<UpdateDbBackgroundService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
