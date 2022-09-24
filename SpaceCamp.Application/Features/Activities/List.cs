using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Domain.Entities;
using SpaceCamp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly SpaceCampContext _context;

            public Handler(SpaceCampContext context)
            {
                _context = context;
            }
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Activities.ToListAsync();
            }
        }
    }
}
