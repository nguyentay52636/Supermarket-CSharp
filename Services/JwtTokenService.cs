using Microsoft.IdentityModel.Tokens;
using Supermarket.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Supermarket.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(TaiKhoan taiKhoan);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(TaiKhoan taiKhoan)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? string.Empty;
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, taiKhoan.MaTaiKhoan.ToString()),
                new Claim(ClaimTypes.Name, taiKhoan.TenNguoiDung ?? string.Empty),
                new Claim(ClaimTypes.Email, taiKhoan.Email ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, taiKhoan.SoDienThoai ?? string.Empty),
                new Claim(ClaimTypes.Role, taiKhoan.MaQuyenNavigation?.TenQuyen ?? "Customer"),
                new Claim("MaQuyen", taiKhoan.MaQuyen?.ToString() ?? "2")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}


