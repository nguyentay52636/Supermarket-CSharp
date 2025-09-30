using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Supermarket.DTOs;
using Supermarket.Extensions;
using Supermarket.Models.Response;
using Supermarket.Services;

namespace Supermarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamService _service;

        public SanPhamController(ISanPhamService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<SanPhamDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SanPhamDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(ApiResponse.Ok(result.ToDtos(), "Lấy danh sách sản phẩm thành công"));
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<SanPhamDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<SanPhamDto>>>> Search([FromQuery] string? q, [FromQuery] int? loaiId, [FromQuery] int? thuongHieuId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var result = await _service.SearchAsync(q, loaiId, thuongHieuId, minPrice, maxPrice);
            return Ok(ApiResponse.Ok(result.ToDtos(), "Tìm kiếm sản phẩm thành công"));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<SanPhamDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public async Task<ActionResult<ApiResponse<SanPhamDto>>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound(ApiResponse.Fail<object>("Không tìm thấy sản phẩm"));
            return Ok(ApiResponse.Ok(entity.ToDto(), "Lấy sản phẩm thành công"));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<SanPhamDto>), 201)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<ApiResponse<SanPhamDto>>> Create([FromForm] CreateSanPhamDto dto, IFormFile? image)
        {
            var created = await _service.CreateAsync(dto, image);
            return CreatedAtAction(nameof(GetById), new { id = created.MaSanPham }, ApiResponse.Ok(created, "Tạo sản phẩm thành công"));
        }

        [HttpPost("json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<SanPhamDto>), 201)]
        public async Task<ActionResult<ApiResponse<SanPhamDto>>> CreateJson([FromBody] CreateSanPhamDto dto)
        {
            var created = await _service.CreateAsync(dto, null);
            return CreatedAtAction(nameof(GetById), new { id = created.MaSanPham }, ApiResponse.Ok(created, "Tạo sản phẩm thành công"));
        }

        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<SanPhamDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<ApiResponse<SanPhamDto>>> Update(int id, [FromForm] UpdateSanPhamDto dto, IFormFile? image)
        {
            var updated = await _service.UpdateAsync(id, dto, image);
            if (updated == null) return NotFound(ApiResponse.Fail<object>("Không tìm thấy sản phẩm"));
            return Ok(ApiResponse.Ok(updated, "Cập nhật sản phẩm thành công"));
        }

        [HttpPut("{id:int}/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<SanPhamDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public async Task<ActionResult<ApiResponse<SanPhamDto>>> UpdateJson(int id, [FromBody] UpdateSanPhamDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto, null);
            if (updated == null) return NotFound(ApiResponse.Fail<object>("Không tìm thấy sản phẩm"));
            return Ok(ApiResponse.Ok(updated, "Cập nhật sản phẩm thành công"));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return Ok(ApiResponse.Ok(ok, ok ? "Xóa sản phẩm thành công" : "Sản phẩm không tồn tại"));
        }
    }
}

