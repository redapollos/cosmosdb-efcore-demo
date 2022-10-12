using efcore_cosmosdb.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace efcore_cosmosdb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // get cosmos vars
            var connectionString = builder.Configuration.GetConnectionString("CosmosConnection");
            var container = builder.Configuration.GetValue<string>("CosmosDbName");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // setup cosmos db to local
            builder.Services.AddDbContext<CosmosContext>(options => options.UseCosmos(connectionString, container));

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

            // handle DB seeding and run test
            SeedDb.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider, builder.Configuration).GetAwaiter().GetResult();


            app.Run();
        }
    }
}