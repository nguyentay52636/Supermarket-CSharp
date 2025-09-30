using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Extensions;
using Supermarket.Models;
using Supermarket.Models.Response;
using Supermarket.Services;

namespace Supermarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KhachHangController : ControllerBase
    {
        private readonly KhachHangService _service;

        public KhachHangController(KhachHangService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<KhachHangDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<KhachHangDto>>>> GetAll()
        {
            try
            {
                var list = await _service.GetAllAsync();
                return Ok(ApiResponse.Ok(list.ToDtos(), "Lấy danh sách khách hàng thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<KhachHangDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<KhachHangDto>>> GetById(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound(ApiResponse.Fail<object>($"Không tìm thấy khách hàng với mã: {id}"));
                }
                return Ok(ApiResponse.Ok(entity.ToDto(), "Lấy thông tin khách hàng thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<KhachHangDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<KhachHangDto>>> Create([FromBody] CreateKhachHangDto createDto)
        {
            try
            {
                var entity = new KhachHang
                {
                    TenKhachHang = createDto.TenKhachHang,
                    DiaChi = createDto.DiaChi,
                    DiemTichLuy = createDto.DiemTichLuy ?? 0,
                    HangThanhVien = createDto.HangThanhVien,
                    MaTaiKhoan = createDto.MaTaiKhoan,
                    TrangThai = "Active"
                };

                var created = await _service.AddAsync(entity);
                var dto = created.ToDto();
                return CreatedAtAction(nameof(GetById), new { id = created.MaKhachHang }, ApiResponse.Ok(dto, "Tạo khách hàng thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<KhachHangDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<KhachHangDto>>> Update(int id, [FromBody] UpdateKhachHangDto updateDto)
        {
            try
            {
                if (id != updateDto.MaKhachHang)
                {
                    return BadRequest(ApiResponse.Fail<object>("Mã khách hàng trong URL không khớp với mã trong dữ liệu"));
                }

                var existing = await _service.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(ApiResponse.Fail<object>($"Không tìm thấy khách hàng với mã: {id}"));
                }

                existing.TenKhachHang = updateDto.TenKhachHang;
                existing.DiaChi = updateDto.DiaChi;
                existing.DiemTichLuy = updateDto.DiemTichLuy;
                existing.HangThanhVien = updateDto.HangThanhVien;
                existing.MaTaiKhoan = updateDto.MaTaiKhoan;
                existing.TrangThai = updateDto.TrangThai;

                var updated = await _service.UpdateAsync(existing);
                return Ok(ApiResponse.Ok(updated.ToDto(), "Cập nhật khách hàng thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<KhachHangDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<ActionResult<ApiResponse<IEnumerable<KhachHangDto>>>> Search([FromQuery] string searchTerm)
        {
            try
            {
                var list = await _service.SearchAsync(searchTerm);
                return Ok(ApiResponse.Ok(list.ToDtos(), $"Tìm thấy {list.Count()} khách hàng"));
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
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(ApiResponse.Fail<object>($"Không tìm thấy khách hàng với mã: {id}"));
                }
                return Ok(ApiResponse.Ok<object>(default, "Xóa khách hàng thành công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail<object>($"Lỗi server: {ex.Message}"));
            }
        }
    }
}


