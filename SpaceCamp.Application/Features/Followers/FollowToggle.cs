using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
using SpaceCamp.Application.Interfaces;
using SpaceCamp.Persistence.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Followers
{
    public class FollowToggle
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string TargetUsername { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly SpaceCampContext _spaceCampContext;
            private readonly IUserAccessor _userAccessor;

            public Handler(SpaceCampContext spaceCampContext, IUserAccessor userAccessor)
            {
                _spaceCampContext = spaceCampContext ?? throw new ArgumentNullException(nameof(spaceCampContext));
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var observer = await _spaceCampContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                var target = await _spaceCampContext.Users.FirstOrDefaultAsync(x => x.UserName == request.TargetUsername);

                if (target is null)
                    return null;

                var following = await _spaceCampContext.UserFollowings.FindAsync(observer.Id, target.Id);

                if (following is null)
                {
                    following = new Domain.Entities.UserFollowing
                    {
                        Observer = observer,
                        Target = target
                    };

                    _spaceCampContext.UserFollowings.Add(following);
                }
                else
                {
                    _spaceCampContext.UserFollowings.Remove(following);
                }

                var success = await _spaceCampContext.SaveChangesAsync() > 0;

                if (success)
                    return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Failed to update following");
            }
        }
    }
}
