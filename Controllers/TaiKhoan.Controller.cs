using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models.Response;
using Supermarket.Services;
using System.Security.Claims;

namespace Supermarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;

        public TaiKhoanController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            var result = await _taiKhoanService.LoginAsync(request);

            if (result.Success)
            {
                return Ok(new ApiResponse<LoginResponseDto>
                {
                    Success = true,
                    Message = result.Message,
                    Data = result
                });
            }

            return BadRequest(new ApiResponse<LoginResponseDto>
            {
                Success = false,
                Message = result.Message,
                Data = null
            });
        }

        /// <summary>
        /// Đăng ký tài khoản mới
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterResponseDto>>> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<RegisterResponseDto>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            var result = await _taiKhoanService.RegisterAsync(request);

            if (result.Success)
            {
                return Ok(new ApiResponse<RegisterResponseDto>
                {
                    Success = true,
                    Message = result.Message,
                    Data = result
                });
            }

            return BadRequest(new ApiResponse<RegisterResponseDto>
            {
                Success = false,
                Message = result.Message,
                Data = null
            });
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Không thể xác định người dùng",
                    Data = null
                });
            }

            var success = await _taiKhoanService.ChangePasswordAsync(userId, request);

            if (success)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Đổi mật khẩu thành công",
                    Data = null
                });
            }

            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "Mật khẩu hiện tại không chính xác hoặc có lỗi xảy ra",
                Data = null
            });
        }

        /// <summary>
        /// Lấy thông tin người dùng hiện tại
        /// </summary>
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserInfoDto>>> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new ApiResponse<UserInfoDto>
                {
                    Success = false,
                    Message = "Không thể xác định người dùng",
                    Data = null
                });
            }

            var userInfo = await _taiKhoanService.GetUserInfoAsync(userId);

            if (userInfo == null)
            {
                return NotFound(new ApiResponse<UserInfoDto>
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin người dùng",
                    Data = null
                });
            }

            return Ok(new ApiResponse<UserInfoDto>
            {
                Success = true,
                Message = "Lấy thông tin thành công",
                Data = userInfo
            });
        }

        /// <summary>
        /// Kiểm tra token có hợp lệ không
        /// </summary>
        [HttpGet("validate-token")]
        [Authorize]
        public ActionResult<ApiResponse<object>> ValidateToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userNameClaim = User.FindFirst(ClaimTypes.Name);
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Token hợp lệ",
                Data = new
                {
                    UserId = userIdClaim?.Value,
                    UserName = userNameClaim?.Value,
                    Role = userRoleClaim?.Value
                }
            });
        }
    }
}
