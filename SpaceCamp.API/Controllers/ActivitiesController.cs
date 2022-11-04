using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceCamp.Application.Features.Activities;
using SpaceCamp.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SpaceCamp.API.Controllers
{
    [AllowAnonymous]
    public class ActivitiesController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var res = await Mediator.Send(new List.Query());
            return HandleResult(res);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await Mediator.Send(new Details.Query { Id = id });
            return HandleResult(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Activity activity)
        {
            var res = await Mediator.Send(new Create.Command { Activity = activity });
            return HandleResult(res);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Activity activity)
        {
            activity.Id = id;
            var res = await Mediator.Send(new Edit.Command { Activity = activity });
            return HandleResult(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await Mediator.Send(new Delete.Command { Id = id });
            return HandleResult(res);
        }
    }
}
