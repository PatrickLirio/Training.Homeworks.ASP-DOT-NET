using Microsoft.AspNetCore.Mvc;
using MyShop_v2.Application.DTOs.Common;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities.Base.Interface;

namespace MyShop_v2.Api.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController<T, TId, TRequest, TResponse> : ControllerBase
        where T : class, IEntity<TId>
        where TResponse : class
    {
        protected readonly GenericService<T, TId, TRequest, TResponse> _service;

        protected GenericController(GenericService<T, TId, TRequest, TResponse> service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResponse>> GetById(TId id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // [HttpPost("search")]
        // public virtual async Task<ActionResult<PagedResult<TResponse>>> GetPaged([FromBody] FilterGroup filter, [FromQuery] int page = 1, [FromQuery] int size = 10)
        // {
        //     var result = await _service.GetPagedAsync(filter, page, size);
        //     return Ok(result);
        // }

        [HttpPost]
        public virtual ActionResult<TResponse> Create([FromBody] TRequest request)
        {
            var result = _service.Add(request);
            // TResponse is expected to have an Id property (inherited from BaseResponse)
            return CreatedAtAction(nameof(GetById), new { id = (result as dynamic).Id }, result);
        }

        [HttpPut("{id}")]
        public virtual ActionResult<TResponse> Update(TId id, [FromBody] TRequest request)
        {
            var result = _service.Update(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(TId id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}