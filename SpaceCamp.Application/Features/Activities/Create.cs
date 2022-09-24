using MediatR;
using SpaceCamp.Domain.Entities;
using SpaceCamp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly SpaceCampContext _context;

            public Handler(SpaceCampContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
