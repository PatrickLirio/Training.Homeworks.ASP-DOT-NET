
using Microsoft.EntityFrameworkCore;
using MyShop.Configurations.Seeders;
using MyShop.Data;
using MyShop.Services;
using MyShop.Services.Interfaces;

namespace MyShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             // Configure Database Context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));   

            // Add services to the container.
            builder.Services.AddScoped<IStorageService, StorageService>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Seed the database with initial data to run this by "dotnet run" command in the terminal
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate(); // Apply any pending migrations
                ProductSeeder.Seed(context); // Seed the database with initial data


                #region -- Debugging Seeding Process--
                // try
                // {
                //     context.Database.Migrate();
                //     ProductSeeder.Seed(context);
                //     Console.WriteLine("✅ Seeding completed.");
                // }
                // catch (Exception ex)
                // {
                //     Console.WriteLine($"❌ Seeding failed: {ex.Message}");
                //     throw;
                // }

                #endregion 
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
