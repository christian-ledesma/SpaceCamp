using MediatR;
using SpaceCamp.Persistence.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var activity = await _context.Activities.FindAsync(request.Id);
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
