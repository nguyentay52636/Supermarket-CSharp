using Supermarket.Extensions;
using Supermarket.Data;
using Microsoft.EntityFrameworkCore;

using Supermarket.Services;
using Supermarket.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.ConfigureSwagger();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.RegisterAllServices();

var app = builder.Build();

app.ConfigureSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
