using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models;
using Supermarket.Models.Response;
using Supermarket.Services;
using System.ComponentModel.DataAnnotations;
using Supermarket.Extensions;

namespace Supermarket.Controllers
{

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
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<NhanVienDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<NhanVienDto>>>> GetAllNhanVien()
        {
            try
            {
                var nhanViens = await _nhanVienService.GetAllAsync();
                var nhanVienDtos = nhanViens.ToDtos();

                return Ok(ApiResponse.Ok(nhanVienDtos, "Lấy danh sách nhân viên thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
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
                    return NotFound(ApiResponse.Fail<object>($"Không tìm thấy nhân viên với mã: {id}"));
                }

                var nhanVienDto = nhanVien.ToDto();

                return Ok(ApiResponse.Ok(nhanVienDto, "Lấy thông tin nhân viên thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
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

                var nhanVienDto = createdNhanVien.ToDto();

                return CreatedAtAction(nameof(GetNhanVienById), new { id = createdNhanVien.MaNhanVien }, ApiResponse.Ok(nhanVienDto, "Tạo nhân viên thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
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
                if (id != updateDto.MaNhanVien)
                {
                    return BadRequest(ApiResponse.Fail<object>("Mã nhân viên trong URL không khớp với mã trong dữ liệu"));
                }

                var existingNhanVien = await _nhanVienService.GetByIdAsync(id);
                if (existingNhanVien == null)
                {
                    return NotFound(ApiResponse.Fail<object>($"Không tìm thấy nhân viên với mã: {id}"));
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

                var nhanVienDto = updatedNhanVien.ToDto();

                return Ok(ApiResponse.Ok(nhanVienDto, "Cập nhật nhân viên thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
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
                var nhanVienDtos = nhanViens.ToDtos();

                return Ok(ApiResponse.Ok(nhanVienDtos, $"Tìm thấy {nhanVienDtos.Count()} nhân viên"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
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
                    return NotFound(ApiResponse.Fail<object>($"Không tìm thấy nhân viên với mã: {id}"));
                }

                return Ok(ApiResponse.Ok<object>(default, "Xóa nhân viên thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }
    }
}
