using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models.Response;
using Supermarket.Services;
using System.Security.Claims;
using System.Linq;
using Supermarket.Extensions;

namespace Supermarket.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    [Tags("TaiKhoan")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanManagementService;

        public TaiKhoanController(ITaiKhoanService taiKhoanManagementService)
        {
            _taiKhoanManagementService = taiKhoanManagementService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TaiKhoanListDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaiKhoanListDto>>>> GetAllTaiKhoans()
        {
            try
            {
                var taiKhoans = await _taiKhoanManagementService.GetAllTaiKhoansAsync();
                var taiKhoanDtos = taiKhoans.Select(t => t.ToListDto());

                return Ok(ApiResponse.Ok(taiKhoanDtos, "Lấy danh sách tài khoản thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }




        [HttpGet("{id:int}")]
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


        [HttpPut("{id:int}")]
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

        [HttpDelete("{id:int}")]
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


        [HttpPatch("{id:int}/status")]
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


        [HttpPatch("{id:int}/reset-password")]
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


    }


}
