using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Models.Response;
using Supermarket.Services;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Controllers
{
    /// <summary>
    /// API quản lý nhân viên
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NhanVienController : ControllerBase
    {
        private readonly NhanVienService _nhanVienService;

        public NhanVienController(NhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// <response code="200">Trả về danh sách nhân viên thành công</response>
        /// <response code="500">Lỗi server</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<NhanVienDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanVienDto>>>> GetAllNhanVien()
        {
            try
            {
                var nhanViens = await _nhanVienService.GetAllAsync();
                var nhanVienDtos = nhanViens.Select(nv => new NhanVienDto
                {
                    MaNhanVien = nv.MaNhanVien,
                    TenNhanVien = nv.TenNhanVien,
                    GioiTinh = nv.GioiTinh,
                    NgaySinh = nv.NgaySinh,
                    SoDienThoai = nv.SoDienThoai,
                    VaiTro = nv.vaiTro,
                    CreatedAt = nv.CreatedAt,
                    UpdatedAt = nv.UpdatedAt
                });

                return Ok(new ApiResponse<IEnumerable<NhanVienDto>>
                {
                    Success = true,
                    Message = "Lấy danh sách nhân viên thành công",
                    Data = nhanVienDtos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo mã
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên</returns>
        /// <response code="200">Trả về thông tin nhân viên thành công</response>
        /// <response code="404">Không tìm thấy nhân viên</response>
        /// <response code="500">Lỗi server</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<NhanVienDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<NhanVienDto>>> GetNhanVienById(string id)
        {
            try
            {
                var nhanVien = await _nhanVienService.GetByIdAsync(id);
                if (nhanVien == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy nhân viên với mã: {id}",
                        Data = null
                    });
                }

                var nhanVienDto = new NhanVienDto
                {
                    MaNhanVien = nhanVien.MaNhanVien,
                    TenNhanVien = nhanVien.TenNhanVien,
                    GioiTinh = nhanVien.GioiTinh,
                    NgaySinh = nhanVien.NgaySinh,
                    SoDienThoai = nhanVien.SoDienThoai,
                    VaiTro = nhanVien.vaiTro,
                    CreatedAt = nhanVien.CreatedAt,
                    UpdatedAt = nhanVien.UpdatedAt
                };

                return Ok(new ApiResponse<NhanVienDto>
                {
                    Success = true,
                    Message = "Lấy thông tin nhân viên thành công",
                    Data = nhanVienDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Tạo nhân viên mới
        /// </summary>
        /// <param name="createDto">Thông tin nhân viên mới</param>
        /// <returns>Thông tin nhân viên đã tạo</returns>
        /// <response code="201">Tạo nhân viên thành công</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        /// <response code="500">Lỗi server</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<NhanVienDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<NhanVienDto>>> CreateNhanVien([FromBody] CreateNhanVienDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Dữ liệu đầu vào không hợp lệ",
                        Data = ModelState
                    });
                }

                var nhanVien = new NhanVien
                {
                    TenNhanVien = createDto.TenNhanVien,
                    GioiTinh = createDto.GioiTinh,
                    NgaySinh = createDto.NgaySinh,
                    SoDienThoai = createDto.SoDienThoai,
                    vaiTro = createDto.VaiTro,
                    TrangThai = "Active"
                };

                var createdNhanVien = await _nhanVienService.AddAsync(nhanVien);

                var nhanVienDto = new NhanVienDto
                {
                    MaNhanVien = createdNhanVien.MaNhanVien,
                    TenNhanVien = createdNhanVien.TenNhanVien,
                    GioiTinh = createdNhanVien.GioiTinh,
                    NgaySinh = createdNhanVien.NgaySinh,
                    SoDienThoai = createdNhanVien.SoDienThoai,
                    VaiTro = createdNhanVien.vaiTro,
                    CreatedAt = createdNhanVien.CreatedAt,
                    UpdatedAt = createdNhanVien.UpdatedAt
                };

                return CreatedAtAction(nameof(GetNhanVienById), new { id = createdNhanVien.MaNhanVien }, new ApiResponse<NhanVienDto>
                {
                    Success = true,
                    Message = "Tạo nhân viên thành công",
                    Data = nhanVienDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <param name="updateDto">Thông tin cập nhật</param>
        /// <returns>Thông tin nhân viên đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        /// <response code="404">Không tìm thấy nhân viên</response>
        /// <response code="500">Lỗi server</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<NhanVienDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<NhanVienDto>>> UpdateNhanVien(string id, [FromBody] UpdateNhanVienDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Dữ liệu đầu vào không hợp lệ",
                        Data = ModelState
                    });
                }

                if (id != updateDto.MaNhanVien)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Mã nhân viên trong URL không khớp với mã trong dữ liệu",
                        Data = null
                    });
                }

                var existingNhanVien = await _nhanVienService.GetByIdAsync(id);
                if (existingNhanVien == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy nhân viên với mã: {id}",
                        Data = null
                    });
                }

                existingNhanVien.TenNhanVien = updateDto.TenNhanVien;
                existingNhanVien.GioiTinh = updateDto.GioiTinh;
                existingNhanVien.NgaySinh = updateDto.NgaySinh;
                existingNhanVien.SoDienThoai = updateDto.SoDienThoai;
                existingNhanVien.vaiTro = updateDto.VaiTro;

                var updatedNhanVien = await _nhanVienService.UpdateAsync(existingNhanVien);

                var nhanVienDto = new NhanVienDto
                {
                    MaNhanVien = updatedNhanVien.MaNhanVien,
                    TenNhanVien = updatedNhanVien.TenNhanVien,
                    GioiTinh = updatedNhanVien.GioiTinh,
                    NgaySinh = updatedNhanVien.NgaySinh,
                    SoDienThoai = updatedNhanVien.SoDienThoai,
                    VaiTro = updatedNhanVien.vaiTro,
                    CreatedAt = updatedNhanVien.CreatedAt,
                    UpdatedAt = updatedNhanVien.UpdatedAt
                };

                return Ok(new ApiResponse<NhanVienDto>
                {
                    Success = true,
                    Message = "Cập nhật nhân viên thành công",
                    Data = nhanVienDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Tìm kiếm nhân viên theo tên hoặc vai trò
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách nhân viên tìm được</returns>
        /// <response code="200">Tìm kiếm thành công</response>
        /// <response code="500">Lỗi server</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<NhanVienDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanVienDto>>>> SearchNhanVien([FromQuery] string searchTerm)
        {
            try
            {
                var nhanViens = await _nhanVienService.GetAllAsync();
                var filteredNhanViens = nhanViens.Where(nv =>
                    nv.TenNhanVien.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    nv.vaiTro.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    nv.SoDienThoai.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                );

                var nhanVienDtos = filteredNhanViens.Select(nv => new NhanVienDto
                {
                    MaNhanVien = nv.MaNhanVien,
                    TenNhanVien = nv.TenNhanVien,
                    GioiTinh = nv.GioiTinh,
                    NgaySinh = nv.NgaySinh,
                    SoDienThoai = nv.SoDienThoai,
                    VaiTro = nv.vaiTro,
                    CreatedAt = nv.CreatedAt,
                    UpdatedAt = nv.UpdatedAt
                });

                return Ok(new ApiResponse<IEnumerable<NhanVienDto>>
                {
                    Success = true,
                    Message = $"Tìm thấy {nhanVienDtos.Count()} nhân viên",
                    Data = nhanVienDtos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Kết quả xóa nhân viên</returns>
        /// <response code="200">Xóa thành công</response>
        /// <response code="404">Không tìm thấy nhân viên</response>
        /// <response code="500">Lỗi server</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteNhanVien(string id)
        {
            try
            {
                var result = await _nhanVienService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy nhân viên với mã: {id}",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Xóa nhân viên thành công",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
