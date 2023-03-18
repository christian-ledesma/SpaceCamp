using Microsoft.AspNetCore.Mvc;
using SpaceCamp.Application.Features.Followers;
using System.Threading.Tasks;

namespace SpaceCamp.API.Controllers
{
    public class FollowController : BaseController
    {
        [HttpPost]
        [Route("{username}")]
        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await Mediator.Send(new FollowToggle.Command { TargetUsername = username }));
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetFollowings(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new List.Query { Username = username, Predicate = predicate }));
        }
    }
}
