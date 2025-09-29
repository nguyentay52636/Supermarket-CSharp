using Microsoft.IdentityModel.Tokens;
using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Repositories.TaiKhoanRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Supermarket.Services
{
    public interface ITaiKhoanService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request);
        Task<UserInfoDto?> GetUserInfoAsync(int userId);
    }

    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepositories _taiKhoanRepository;
        private readonly IConfiguration _configuration;

        public TaiKhoanService(ITaiKhoanRepositories taiKhoanRepository, IConfiguration configuration)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                // Tìm tài khoản theo email hoặc số điện thoại
                var taiKhoan = await _taiKhoanRepository.GetTaiKhoanByEmailAsync(request.EmailOrPhone) ??
                              await _taiKhoanRepository.GetTaiKhoanByPhoneAsync(request.EmailOrPhone);

                if (taiKhoan == null)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Tài khoản không tồn tại"
                    };
                }

                // Kiểm tra mật khẩu
                if (!VerifyPassword(request.Password, taiKhoan.MatKhau ?? ""))
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Mật khẩu không chính xác"
                    };
                }

                // Kiểm tra trạng thái tài khoản
                if (taiKhoan.TrangThai != "Active")
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Tài khoản đã bị khóa"
                    };
                }

                // Tạo JWT token
                var token = GenerateJwtToken(taiKhoan);

                return new LoginResponseDto
                {
                    Success = true,
                    Message = "Đăng nhập thành công",
                    Token = token,
                    UserInfo = new UserInfoDto
                    {
                        MaTaiKhoan = taiKhoan.MaTaiKhoan,
                        TenNguoiDung = taiKhoan.TenNguoiDung ?? "",
                        Email = taiKhoan.Email ?? "",
                        SoDienThoai = taiKhoan.SoDienThoai ?? "",
                        MaQuyen = taiKhoan.MaQuyen,
                        TenQuyen = taiKhoan.MaQuyenNavigation?.TenQuyen,
                        TrangThai = taiKhoan.TrangThai ?? ""
                    }
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                // Kiểm tra email đã tồn tại
                if (await _taiKhoanRepository.CheckEmailExistsAsync(request.Email))
                {
                    return new RegisterResponseDto
                    {
                        Success = false,
                        Message = "Email đã được sử dụng"
                    };
                }

                // Kiểm tra số điện thoại đã tồn tại
                if (await _taiKhoanRepository.CheckPhoneExistsAsync(request.SoDienThoai))
                {
                    return new RegisterResponseDto
                    {
                        Success = false,
                        Message = "Số điện thoại đã được sử dụng"
                    };
                }

                // Tạo tài khoản mới
                var taiKhoan = new TaiKhoan
                {
                    TenNguoiDung = request.TenNguoiDung,
                    Email = request.Email,
                    SoDienThoai = request.SoDienThoai,
                    MatKhau = HashPassword(request.Password),
                    MaQuyen = request.MaQuyen ?? 2, 
                    TrangThai = "Active"
                };

                var createdTaiKhoan = await _taiKhoanRepository.CreateTaiKhoanAsync(taiKhoan);

                return new RegisterResponseDto
                {
                    Success = true,
                    Message = "Đăng ký thành công",
                    UserInfo = new UserInfoDto
                    {
                        MaTaiKhoan = createdTaiKhoan.MaTaiKhoan,
                        TenNguoiDung = createdTaiKhoan.TenNguoiDung ?? "",
                        Email = createdTaiKhoan.Email ?? "",
                        SoDienThoai = createdTaiKhoan.SoDienThoai ?? "",
                        MaQuyen = createdTaiKhoan.MaQuyen,
                        TrangThai = createdTaiKhoan.TrangThai ?? ""
                    }
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request)
        {
            try
            {
                var taiKhoan = await _taiKhoanRepository.GetTaiKhoanByIdAsync(userId);
                if (taiKhoan == null) return false;

                // Kiểm tra mật khẩu hiện tại
                if (!VerifyPassword(request.CurrentPassword, taiKhoan.MatKhau ?? ""))
                {
                    return false;
                }

                // Cập nhật mật khẩu mới
                taiKhoan.MatKhau = HashPassword(request.NewPassword);
                return await _taiKhoanRepository.UpdateTaiKhoanAsync(taiKhoan);
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserInfoDto?> GetUserInfoAsync(int userId)
        {
            try
            {
                var taiKhoan = await _taiKhoanRepository.GetTaiKhoanByIdAsync(userId);
                if (taiKhoan == null) return null;

                return new UserInfoDto
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    TenNguoiDung = taiKhoan.TenNguoiDung ?? "",
                    Email = taiKhoan.Email ?? "",
                    SoDienThoai = taiKhoan.SoDienThoai ?? "",
                    MaQuyen = taiKhoan.MaQuyen,
                    TenQuyen = taiKhoan.MaQuyenNavigation?.TenQuyen,
                    TrangThai = taiKhoan.TrangThai ?? ""
                };
            }
            catch
            {
                return null;
            }
        }

        private string GenerateJwtToken(TaiKhoan taiKhoan)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, taiKhoan.MaTaiKhoan.ToString()),
                new Claim(ClaimTypes.Name, taiKhoan.TenNguoiDung ?? ""),
                new Claim(ClaimTypes.Email, taiKhoan.Email ?? ""),
                new Claim(ClaimTypes.MobilePhone, taiKhoan.SoDienThoai ?? ""),
                new Claim(ClaimTypes.Role, taiKhoan.MaQuyenNavigation?.TenQuyen ?? "Customer"),
                new Claim("MaQuyen", taiKhoan.MaQuyen?.ToString() ?? "2")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24), // Token hết hạn sau 24 giờ
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}
