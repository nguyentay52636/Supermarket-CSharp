using Microsoft.AspNetCore.Mvc;
using Supermarket.Data;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ApplicationDbContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("test-db")]
    public async Task<IActionResult> TestDatabaseConnection()
    {
        try
        {
            // Test database connection
            var canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                return BadRequest(new { message = "Cannot connect to database", success = false });
            }

            // Test query
            var productCount = await _context.Products.CountAsync();
            var nhanVienCount = await _context.NhanViens.CountAsync();

            return Ok(new
            {
                message = "Database connection successful",
                success = true,
                productCount = productCount,
                nhanVienCount = nhanVienCount,
                timestamp = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Database connection failed",
                success = false,
                error = ex.Message,
                innerException = ex.InnerException?.Message,
                stackTrace = ex.StackTrace,
                timestamp = DateTime.Now
            });
        }
    }
}
