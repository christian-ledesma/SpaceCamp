using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Domain.Entities;
using SpaceCamp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceCamp.API.Controllers
{
    public class ActivitiesController : BaseController
    {
        private readonly SpaceCampContext _context;

        public ActivitiesController(SpaceCampContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Activity>>> GetList()
        {
            var res = await _context.Activities.ToListAsync();
            return res;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Activity>> Get(Guid id)
        {
            var res = await _context.Activities.FindAsync(id);
            return res;
        }
    }
}
