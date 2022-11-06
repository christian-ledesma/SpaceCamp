using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
using SpaceCamp.Application.Interfaces;
using SpaceCamp.Domain.Entities;
using SpaceCamp.Persistence.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Activities
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly SpaceCampContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(SpaceCampContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.Include(x => x.Attendees).ThenInclude(x => x.User).SingleOrDefaultAsync(x => x.Id == request.Id);

                if (activity is null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user is null) return null;

                var hostUername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.User?.UserName;

                var attendance = activity.Attendees.FirstOrDefault(x => x.User.UserName == user.UserName);

                if (attendance is not null && hostUername == user.UserName)
                {
                    activity.IsCancelled = !activity.IsCancelled;
                }

                if (attendance is not null && hostUername != user.UserName)
                {
                    activity.Attendees.Remove(attendance);
                }

                if (attendance is null)
                {
                    attendance = new ActivityAttendee
                    {
                        User = user,
                        Activity = activity,
                        IsHost = false
                    };

                    activity.Attendees.Add(attendance);
                }

                var result = await _context.SaveChangesAsync() > 0;
                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error on UpdateAttendance");
            }
        }
    }
}
