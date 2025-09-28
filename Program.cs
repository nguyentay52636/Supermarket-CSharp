using SupermarketAPI.Extensions;
using Supermarket.Data;
using Microsoft.EntityFrameworkCore;
using Supermarket.Repositories;
using Supermarket.Repositories.ProductRepository;
using Supermarket.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerDocumentation();

// Register repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();

// Register services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<NhanVienService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
