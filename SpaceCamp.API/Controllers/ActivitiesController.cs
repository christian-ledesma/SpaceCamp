using Microsoft.AspNetCore.Mvc;
using SpaceCamp.Application.Features.Activities;
using SpaceCamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceCamp.API.Controllers
{
    public class ActivitiesController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetList()
        {
            var res = await Mediator.Send(new List.Query());
            return res;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Activity>> Get(Guid id)
        {
            var res = await Mediator.Send(new Details.Query { Id = id });
            return res;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Activity activity)
        {
            var res = await Mediator.Send(new Create.Command { Activity = activity });
            return Ok(res);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Activity activity)
        {
            activity.Id = id;
            var res = await Mediator.Send(new Edit.Command { Activity = activity });
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await Mediator.Send(new Delete.Command { Id = id });
            return Ok(res);
        }
    }
}
