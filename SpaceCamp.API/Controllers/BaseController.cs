using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SpaceCamp.API.Extensions;
using SpaceCamp.Application.Core;

namespace SpaceCamp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value is not null)
            {
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value is null)
            {
                return NotFound();
            }
            return BadRequest(result.Error);
        }

        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value is not null)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize, result.Value.Count, result.Value.TotalPages);
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value is null)
            {
                return NotFound();
            }
            return BadRequest(result.Error);
        }
    }
}
