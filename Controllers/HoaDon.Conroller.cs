using Microsoft.AspNetCore.Mvc;
using Supermarket.DTOs;
using Supermarket.Extensions;
using Supermarket.Models.Response;
using Supermarket.Services;

namespace Supermarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService _service;

        public HoaDonController(IHoaDonService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<HoaDonDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<HoaDonDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(ApiResponse.Ok(result.ToDtos(), "Lấy danh sách hóa đơn thành công"));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<HoaDonDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public async Task<ActionResult<ApiResponse<HoaDonDto>>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound(ApiResponse.Fail<object>("Không tìm thấy hóa đơn"));
            return Ok(ApiResponse.Ok(entity.ToDto(), "Lấy hóa đơn thành công"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<HoaDonDto>), 201)]
        public async Task<ActionResult<ApiResponse<HoaDonDto>>> Create([FromBody] CreateHoaDonDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MaHoaDon }, ApiResponse.Ok(created, "Tạo hóa đơn thành công"));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<HoaDonDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public async Task<ActionResult<ApiResponse<HoaDonDto>>> Update(int id, [FromBody] UpdateHoaDonDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound(ApiResponse.Fail<object>("Không tìm thấy hóa đơn"));
            return Ok(ApiResponse.Ok(updated, "Cập nhật hóa đơn thành công"));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return Ok(ApiResponse.Ok(ok, ok ? "Xóa hóa đơn thành công" : "Hóa đơn không tồn tại"));
        }
    }
}


