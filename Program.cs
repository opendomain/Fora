
using Fora.Services;
using Microsoft.EntityFrameworkCore;

namespace Fora
{
    public class Program
    {
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
