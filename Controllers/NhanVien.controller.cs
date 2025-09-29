using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Models.Response;
using Supermarket.Services;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NhanVienController : ControllerBase
    {
        private readonly NhanVienService _nhanVienService;
        private static NhanVienDto ToDto(NhanVien nv) => new NhanVienDto
        {
            MaNhanVien = nv.MaNhanVien,
            TenNhanVien = nv.TenNhanVien ?? string.Empty,
            GioiTinh = nv.GioiTinh ?? string.Empty,
            NgaySinh = nv.NgaySinh,
            SoDienThoai = nv.SoDienThoai ?? string.Empty,
            Email = nv.Email,
            VaiTro = nv.vaiTro ?? string.Empty,
            MaCuaHang = nv.MaCuaHang,
            TrangThai = nv.TrangThai
        };

        public NhanVienController(NhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<NhanVienDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanVienDto>>>> GetAllNhanVien()
        {
            try
            {
                var nhanViens = await _nhanVienService.GetAllAsync();
                var nhanVienDtos = nhanViens.Select(ToDto);

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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<NhanVienDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<NhanVienDto>>> GetNhanVienById(int id)
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

                var nhanVienDto = ToDto(nhanVien);

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
                    Email = createDto.Email,
                    vaiTro = createDto.VaiTro,
                    MaCuaHang = createDto.MaCuaHang,
                    TrangThai = "Active"
                };

                var createdNhanVien = await _nhanVienService.AddAsync(nhanVien);

                var nhanVienDto = ToDto(createdNhanVien);

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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<NhanVienDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<NhanVienDto>>> UpdateNhanVien(int id, [FromBody] UpdateNhanVienDto updateDto)
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
                existingNhanVien.Email = updateDto.Email;
                existingNhanVien.vaiTro = updateDto.VaiTro;
                existingNhanVien.MaCuaHang = updateDto.MaCuaHang;
                existingNhanVien.TrangThai = updateDto.TrangThai;

                var updatedNhanVien = await _nhanVienService.UpdateAsync(existingNhanVien);

                var nhanVienDto = ToDto(updatedNhanVien);

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


        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<NhanVienDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanVienDto>>>> SearchNhanVien([FromQuery] string searchTerm)
        {
            try
            {
                var nhanViens = await _nhanVienService.SearchAsync(searchTerm);
                var nhanVienDtos = nhanViens.Select(ToDto);

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

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteNhanVien(int id)
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
