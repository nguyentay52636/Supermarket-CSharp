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
    [Tags("Authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), 400)]
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

            var result = await _authService.LoginAsync(request);

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


        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<RegisterResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<RegisterResponseDto>), 400)]
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

            var result = await _authService.RegisterAsync(request);

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
        /// <param name="request">Thông tin đổi mật khẩu</param>
        /// <returns>Kết quả đổi mật khẩu</returns>
        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 401)]
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

            var success = await _authService.ChangePasswordAsync(userId, request);

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
        /// <returns>Thông tin người dùng</returns>
        [HttpGet("profile")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<UserInfoDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<UserInfoDto>), 401)]
        [ProducesResponseType(typeof(ApiResponse<UserInfoDto>), 404)]
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

            var userInfo = await _authService.GetUserInfoAsync(userId);

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

        [HttpGet("validate-token")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 401)]
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
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ApiResponse<RefreshTokenResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<RefreshTokenResponseDto>), 400)]
        public async Task<ActionResult<ApiResponse<RefreshTokenResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request);

            if (result.Success)
            {
                return Ok(new ApiResponse<RefreshTokenResponseDto>
                {
                    Success = true,
                    Message = result.Message,
                    Data = result
                });
            }

            return BadRequest(new ApiResponse<RefreshTokenResponseDto>
            {
                Success = false,
                Message = result.Message,
                Data = null
            });
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns>Kết quả đăng xuất</returns>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 401)]
        public async Task<ActionResult<ApiResponse<object>>> Logout()
        {
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

            var success = await _authService.LogoutAsync(userId);

            if (success)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Đăng xuất thành công",
                    Data = null
                });
            }

            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "Có lỗi xảy ra khi đăng xuất",
                Data = null
            });
        }

        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ApiResponse<ForgotPasswordResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<ForgotPasswordResponseDto>), 400)]
        public async Task<ActionResult<ApiResponse<ForgotPasswordResponseDto>>> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<ForgotPasswordResponseDto>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            var result = await _authService.ForgotPasswordAsync(request);

            if (result.Success)
            {
                return Ok(new ApiResponse<ForgotPasswordResponseDto>
                {
                    Success = true,
                    Message = result.Message,
                    Data = result
                });
            }

            return BadRequest(new ApiResponse<ForgotPasswordResponseDto>
            {
                Success = false,
                Message = result.Message,
                Data = null
            });
        }

        /// <summary>
        /// Reset mật khẩu bằng reset token
        /// </summary>
        /// <param name="request">Thông tin reset password</param>
        /// <returns>Kết quả reset</returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ApiResponse<ResetPasswordResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<ResetPasswordResponseDto>), 400)]
        public async Task<ActionResult<ApiResponse<ResetPasswordResponseDto>>> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<ResetPasswordResponseDto>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            var result = await _authService.ResetPasswordAsync(request);

            if (result.Success)
            {
                return Ok(new ApiResponse<ResetPasswordResponseDto>
                {
                    Success = true,
                    Message = result.Message,
                    Data = result
                });
            }

            return BadRequest(new ApiResponse<ResetPasswordResponseDto>
            {
                Success = false,
                Message = result.Message,
                Data = null
            });
        }
    }
}
