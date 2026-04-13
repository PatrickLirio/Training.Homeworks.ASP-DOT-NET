using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Application.Mappings;
using MyShop_v2.Application.Services;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Infrastructure.Data;
using MyShop_v2.Infrastructure.Repositories;
using MyShop_v2.Infrastructure.Repositories.Base;


var builder = WebApplication.CreateBuilder(args);

// Configure Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services and Repositories
builder.Services.AddScoped<FilterService>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));

// Register the open generic service
builder.Services.AddScoped(typeof(GenericService<,,,>));

// Register concrete services (Update your Repository registrations similarly)
builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
builder.Services.AddScoped<ICategoryRpository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<StockMovementService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>()); // automapper v16 on .net v10

var app = builder.Build();

// Enable Swagger for all environments during initial development/testing
// Or ensure your IIS Environment is set to 'Development'
if (app.Environment.IsDevelopment() || true) 
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
