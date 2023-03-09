using Microsoft.AspNetCore.Mvc;
using SpaceCamp.Application.Features.Profiles;
using System.Threading.Tasks;

namespace SpaceCamp.API.Controllers
{
    public class ProfilesController : BaseController
    {
        [HttpGet]
        [Route("{username}")]
        public  async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Username = username }));
        }
    }
}
