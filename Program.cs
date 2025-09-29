using Supermarket.Extensions;
using Supermarket.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;

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

// Dynamically choose an available HTTP port (prefers 5295)
var preferredPort = 5295;
var selectedPort = preferredPort;
bool portBusy = false;
try
{
    var probe = new TcpListener(IPAddress.Loopback, preferredPort);
    probe.Start();
    probe.Stop();
}
catch
{
    portBusy = true;
}

if (portBusy)
{
    var fallback = new TcpListener(IPAddress.Loopback, 0);
    fallback.Start();
    selectedPort = ((IPEndPoint)fallback.LocalEndpoint).Port;
    fallback.Stop();
}

builder.WebHost.UseUrls($"http://localhost:{selectedPort}");

var app = builder.Build();

app.ConfigureSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
Console.WriteLine($"Server listening at http://localhost:{selectedPort}");
app.Run();
