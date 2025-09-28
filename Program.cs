using Supermarket.Extensions;
using Supermarket.Data;
using Microsoft.EntityFrameworkCore;
using Supermarket.Repositories;
using Supermarket.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();

// Register services
// builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<NhanVienService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
