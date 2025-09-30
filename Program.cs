using Supermarket.Extensions;
using Supermarket.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

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

var httpPort = 5295;
var httpsPort = 7000;

KillProcessOnPort(httpPort);
KillProcessOnPort(httpsPort);

// Cấu hình URL cố định
builder.WebHost.UseUrls($"http://localhost:{httpPort};https://localhost:{httpsPort}");

var app = builder.Build();

app.ConfigureSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"🚀 Server started successfully!");
Console.WriteLine($"📱 HTTP:  http://localhost:{httpPort}");
Console.WriteLine($"🔒 HTTPS: https://localhost:{httpsPort}");
Console.WriteLine($"📚 Swagger: https://localhost:{httpsPort}/swagger");
Console.WriteLine($"Press Ctrl+C to stop the server");

app.Run();

static void KillProcessOnPort(int port)
{
    try
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "lsof",
            Arguments = $"-ti:{port}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        if (process != null)
        {
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
            {
                var pids = output.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var pid in pids)
                {
                    if (int.TryParse(pid, out var processId))
                    {
                        try
                        {
                            var killProcess = Process.GetProcessById(processId);
                            killProcess.Kill();
                            Console.WriteLine($"✅ Killed process {processId} on port {port}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"⚠️  Could not kill process {processId}: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️  Error checking port {port}: {ex.Message}");
    }
}
