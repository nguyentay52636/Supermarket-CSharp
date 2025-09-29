using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models.Response;
using Supermarket.Services;
using System.Security.Claims;

namespace Supermarket.Controllers
{
    /// <summary>
    /// TaiKhoan Management Controller - Quản lý CRUD tài khoản
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Tất cả API đều cần authentication
    [Tags("TaiKhoan Management")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanManagementService;

        public TaiKhoanController(ITaiKhoanService taiKhoanManagementService)
        {
            _taiKhoanManagementService = taiKhoanManagementService;
        }

        /// <summary>
        /// Lấy danh sách tài khoản với phân trang và tìm kiếm
        /// </summary>
        /// <param name="searchDto">Thông tin tìm kiếm và phân trang</param>
        /// <returns>Danh sách tài khoản</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanListResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanListResponseDto>), 400)]
        public async Task<ActionResult<ApiResponse<TaiKhoanListResponseDto>>> GetAllTaiKhoans([FromQuery] TaiKhoanSearchDto searchDto)
        {
            try
            {
                var result = await _taiKhoanManagementService.GetAllTaiKhoansAsync(searchDto);

                return Ok(new ApiResponse<TaiKhoanListResponseDto>
                {
                    Success = true,
                    Message = "Lấy danh sách tài khoản thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TaiKhoanListResponseDto>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Lấy thông tin tài khoản theo ID
        /// </summary>
        /// <param name="id">ID tài khoản</param>
        /// <returns>Thông tin tài khoản</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 404)]
        public async Task<ActionResult<ApiResponse<TaiKhoanDto>>> GetTaiKhoanById(int id)
        {
            try
            {
                var taiKhoan = await _taiKhoanManagementService.GetTaiKhoanByIdAsync(id);

                if (taiKhoan == null)
                {
                    return NotFound(new ApiResponse<TaiKhoanDto>
                    {
                        Success = false,
                        Message = "Không tìm thấy tài khoản",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<TaiKhoanDto>
                {
                    Success = true,
                    Message = "Lấy thông tin tài khoản thành công",
                    Data = taiKhoan
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Tạo tài khoản mới
        /// </summary>
        /// <param name="createDto">Thông tin tài khoản mới</param>
        /// <returns>Tài khoản đã tạo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 400)]
        public async Task<ActionResult<ApiResponse<TaiKhoanDto>>> CreateTaiKhoan([FromBody] CreateTaiKhoanDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            try
            {
                var taiKhoan = await _taiKhoanManagementService.CreateTaiKhoanAsync(createDto);

                return CreatedAtAction(nameof(GetTaiKhoanById), new { id = taiKhoan.MaTaiKhoan }, new ApiResponse<TaiKhoanDto>
                {
                    Success = true,
                    Message = "Tạo tài khoản thành công",
                    Data = taiKhoan
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="id">ID tài khoản</param>
        /// <param name="updateDto">Thông tin cập nhật</param>
        /// <returns>Tài khoản đã cập nhật</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 400)]
        [ProducesResponseType(typeof(ApiResponse<TaiKhoanDto>), 404)]
        public async Task<ActionResult<ApiResponse<TaiKhoanDto>>> UpdateTaiKhoan(int id, [FromBody] UpdateTaiKhoanDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ",
                    Data = null
                });
            }

            try
            {
                var taiKhoan = await _taiKhoanManagementService.UpdateTaiKhoanAsync(id, updateDto);

                if (taiKhoan == null)
                {
                    return NotFound(new ApiResponse<TaiKhoanDto>
                    {
                        Success = false,
                        Message = "Không tìm thấy tài khoản",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<TaiKhoanDto>
                {
                    Success = true,
                    Message = "Cập nhật tài khoản thành công",
                    Data = taiKhoan
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TaiKhoanDto>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="id">ID tài khoản</param>
        /// <returns>Kết quả xóa</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTaiKhoan(int id)
        {
            try
            {
                var success = await _taiKhoanManagementService.DeleteTaiKhoanAsync(id);

                if (!success)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy tài khoản",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Xóa tài khoản thành công",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="id">ID tài khoản</param>
        /// <param name="status">Trạng thái mới</param>
        /// <returns>Kết quả cập nhật</returns>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<ActionResult<ApiResponse<object>>> UpdateTaiKhoanStatus(int id, [FromBody] string status)
        {
            try
            {
                var success = await _taiKhoanManagementService.UpdateTaiKhoanStatusAsync(id, status);

                if (!success)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Cập nhật trạng thái thất bại",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Cập nhật trạng thái thành công",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Reset mật khẩu tài khoản
        /// </summary>
        /// <param name="id">ID tài khoản</param>
        /// <param name="resetDto">Thông tin mật khẩu mới</param>
        /// <returns>Kết quả reset</returns>
        [HttpPatch("{id}/reset-password")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<ActionResult<ApiResponse<object>>> ResetPassword(int id, [FromBody] ResetPasswordDto resetDto)
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

            try
            {
                var success = await _taiKhoanManagementService.ResetPasswordAsync(id, resetDto);

                if (!success)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Reset mật khẩu thất bại",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Reset mật khẩu thành công",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <param name="excludeId">ID tài khoản loại trừ</param>
        /// <returns>Kết quả kiểm tra</returns>
        [HttpGet("check-email")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        public async Task<ActionResult<ApiResponse<object>>> CheckEmailExists([FromQuery] string email, [FromQuery] int? excludeId = null)
        {
            try
            {
                var exists = await _taiKhoanManagementService.CheckEmailExistsAsync(email, excludeId);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Kiểm tra email thành công",
                    Data = new { Exists = exists }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại
        /// </summary>
        /// <param name="phone">Số điện thoại cần kiểm tra</param>
        /// <param name="excludeId">ID tài khoản loại trừ</param>
        /// <returns>Kết quả kiểm tra</returns>
        [HttpGet("check-phone")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        public async Task<ActionResult<ApiResponse<object>>> CheckPhoneExists([FromQuery] string phone, [FromQuery] int? excludeId = null)
        {
            try
            {
                var exists = await _taiKhoanManagementService.CheckPhoneExistsAsync(phone, excludeId);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Kiểm tra số điện thoại thành công",
                    Data = new { Exists = exists }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
