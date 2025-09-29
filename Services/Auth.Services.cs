using Microsoft.IdentityModel.Tokens;
using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Repositories.AuthRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Supermarket.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request);
        Task<UserInfoDto?> GetUserInfoAsync(int userId);
        Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<bool> LogoutAsync(int userId);
        Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request);
    }

    public class AuthService : IAuthService
    {
        private readonly IAuthRepositories _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepositories authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                // Tìm tài khoản theo email hoặc số điện thoại
                var taiKhoan = await _authRepository.GetTaiKhoanByEmailAsync(request.EmailOrPhone) ??
                              await _authRepository.GetTaiKhoanByPhoneAsync(request.EmailOrPhone);

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
                if (await _authRepository.CheckEmailExistsAsync(request.Email))
                {
                    return new RegisterResponseDto
                    {
                        Success = false,
                        Message = "Email đã được sử dụng"
                    };
                }

                // Kiểm tra số điện thoại đã tồn tại
                if (await _authRepository.CheckPhoneExistsAsync(request.SoDienThoai))
                {
                    return new RegisterResponseDto
                    {
                        Success = false,
                        Message = "Số điện thoại đã được sử dụng"
                    };
                }

                // Tạo tài khoản mới - mặc định là khách hàng
                var taiKhoan = new TaiKhoan
                {
                    TenNguoiDung = request.TenNguoiDung,
                    Email = request.Email,
                    SoDienThoai = request.SoDienThoai,
                    MatKhau = HashPassword(request.Password),
                    MaQuyen = request.MaQuyen ?? 2, // Sử dụng MaQuyen từ request hoặc mặc định là khách hàng
                    TrangThai = "Active"
                };

                // Lưu tài khoản vào database
                var createdTaiKhoan = await _authRepository.CreateTaiKhoanAsync(taiKhoan);

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
                        TenQuyen = createdTaiKhoan.MaQuyenNavigation?.TenQuyen,
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
                var taiKhoan = await _authRepository.GetTaiKhoanByIdAsync(userId);
                if (taiKhoan == null) return false;

                // Kiểm tra mật khẩu hiện tại
                if (!VerifyPassword(request.CurrentPassword, taiKhoan.MatKhau ?? ""))
                {
                    return false;
                }

                // Cập nhật mật khẩu mới
                var hashedPassword = HashPassword(request.NewPassword);
                return await _authRepository.UpdatePasswordAsync(userId, hashedPassword);
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
                var taiKhoan = await _authRepository.GetTaiKhoanByIdAsync(userId);
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

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            // TODO: Implement refresh token logic
            return new RefreshTokenResponseDto
            {
                Success = false,
                Message = "Refresh token chưa được implement"
            };
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            try
            {
                // Revoke refresh token if exists
                await _authRepository.RevokeRefreshTokenAsync(userId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            try
            {
                // Tìm tài khoản theo email
                var taiKhoan = await _authRepository.GetTaiKhoanByEmailAsync(request.Email);

                if (taiKhoan == null)
                {
                    return new ForgotPasswordResponseDto
                    {
                        Success = false,
                        Message = "Email không tồn tại trong hệ thống"
                    };
                }

                // Kiểm tra trạng thái tài khoản
                if (taiKhoan.TrangThai != "Active")
                {
                    return new ForgotPasswordResponseDto
                    {
                        Success = false,
                        Message = "Tài khoản đã bị khóa, không thể reset mật khẩu"
                    };
                }

                // Tạo reset token (đơn giản - trong thực tế nên dùng JWT với thời hạn ngắn)
                var resetToken = GenerateResetToken(taiKhoan);

                // TODO: Gửi email với reset token
                // Trong demo này, chúng ta sẽ trả về token để test

                return new ForgotPasswordResponseDto
                {
                    Success = true,
                    Message = "Reset token đã được tạo. Vui lòng kiểm tra email để reset mật khẩu.",
                    ResetToken = resetToken // Trong thực tế không nên trả về token này
                };
            }
            catch (Exception ex)
            {
                return new ForgotPasswordResponseDto
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }

        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            try
            {
                // Validate reset token (đơn giản - trong thực tế nên verify JWT)
                var userId = ValidateResetToken(request.ResetToken);

                if (userId == null)
                {
                    return new ResetPasswordResponseDto
                    {
                        Success = false,
                        Message = "Reset token không hợp lệ hoặc đã hết hạn"
                    };
                }

                // Lấy thông tin tài khoản
                var taiKhoan = await _authRepository.GetTaiKhoanByIdAsync(userId.Value);

                if (taiKhoan == null)
                {
                    return new ResetPasswordResponseDto
                    {
                        Success = false,
                        Message = "Tài khoản không tồn tại"
                    };
                }

                // Kiểm tra trạng thái tài khoản
                if (taiKhoan.TrangThai != "Active")
                {
                    return new ResetPasswordResponseDto
                    {
                        Success = false,
                        Message = "Tài khoản đã bị khóa"
                    };
                }

                // Cập nhật mật khẩu mới
                var hashedPassword = HashPassword(request.NewPassword);
                var success = await _authRepository.UpdatePasswordAsync(userId.Value, hashedPassword);

                if (success)
                {
                    return new ResetPasswordResponseDto
                    {
                        Success = true,
                        Message = "Reset mật khẩu thành công"
                    };
                }
                else
                {
                    return new ResetPasswordResponseDto
                    {
                        Success = false,
                        Message = "Có lỗi xảy ra khi reset mật khẩu"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResetPasswordResponseDto
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
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

        private string GenerateResetToken(TaiKhoan taiKhoan)
        {
            // Tạo reset token đơn giản (trong thực tế nên dùng JWT với thời hạn ngắn)
            var tokenData = $"{taiKhoan.MaTaiKhoan}:{DateTime.UtcNow.AddHours(1):yyyyMMddHHmmss}";
            var tokenBytes = Encoding.UTF8.GetBytes(tokenData);
            return Convert.ToBase64String(tokenBytes);
        }

        private int? ValidateResetToken(string resetToken)
        {
            try
            {
                var tokenBytes = Convert.FromBase64String(resetToken);
                var tokenData = Encoding.UTF8.GetString(tokenBytes);

                var parts = tokenData.Split(':');
                if (parts.Length != 2) return null;

                var userId = int.Parse(parts[0]);
                var expiryTime = DateTime.ParseExact(parts[1], "yyyyMMddHHmmss", null);

                if (DateTime.UtcNow > expiryTime) return null;

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
