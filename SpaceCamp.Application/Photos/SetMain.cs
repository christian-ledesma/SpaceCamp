using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
using SpaceCamp.Application.Interfaces;
using SpaceCamp.Persistence.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Photos
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
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
                var user = await _spaceCampContext.Users.Include(x => x.Photos)
                                                        .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user is null)
                    return null;

                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

                if (photo is null)
                    return null;

                var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

                if (currentMain is not null)
                    currentMain.IsMain = false;

                photo.IsMain = true;

                var success = await _spaceCampContext.SaveChangesAsync() > 0;

                if (success)
                    return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Problem setting main photo");
            }
        }
    }
}
