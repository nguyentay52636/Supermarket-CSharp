using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Models;
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
        [ProducesResponseType(typeof(IEnumerable<NhanVienDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<NhanVienDto>>> GetAllNhanVien()
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

                return Ok(nhanVienDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
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
        [ProducesResponseType(typeof(NhanVienDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<NhanVienDto>> GetNhanVienById(string id)
        {
            try
            {
                var nhanVien = await _nhanVienService.GetByIdAsync(id);
                if (nhanVien == null)
                {
                    return NotFound($"Không tìm thấy nhân viên với mã: {id}");
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

                return Ok(nhanVienDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
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
        [ProducesResponseType(typeof(NhanVienDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<NhanVienDto>> CreateNhanVien([FromBody] CreateNhanVienDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
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

                return CreatedAtAction(nameof(GetNhanVienById), new { id = createdNhanVien.MaNhanVien }, nhanVienDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
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
        [ProducesResponseType(typeof(NhanVienDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<NhanVienDto>> UpdateNhanVien(string id, [FromBody] UpdateNhanVienDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != updateDto.MaNhanVien)
                {
                    return BadRequest("Mã nhân viên trong URL không khớp với mã trong dữ liệu");
                }

                var existingNhanVien = await _nhanVienService.GetByIdAsync(id);
                if (existingNhanVien == null)
                {
                    return NotFound($"Không tìm thấy nhân viên với mã: {id}");
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

                return Ok(nhanVienDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Không có nội dung trả về</returns>
        /// <response code="204">Xóa thành công</response>
        /// <response code="404">Không tìm thấy nhân viên</response>
        /// <response code="500">Lỗi server</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteNhanVien(string id)
        {
            try
            {
                var result = await _nhanVienService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Không tìm thấy nhân viên với mã: {id}");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
