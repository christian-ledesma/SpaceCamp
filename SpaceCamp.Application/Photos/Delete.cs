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
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly SpaceCampContext _spaceCampContext;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;

            public Handler(SpaceCampContext spaceCampContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                _spaceCampContext = spaceCampContext ?? throw new ArgumentNullException(nameof(spaceCampContext));
                _photoAccessor = photoAccessor ?? throw new ArgumentNullException(nameof(photoAccessor));
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

                if (photo.IsMain)
                    return Result<Unit>.Failure("You cannot delete your main photo");

                var result = await _photoAccessor.DeletePhotoAsync(photo.Id);

                if (result is null)
                    return Result<Unit>.Failure("Problem deleting photo from Cloudinary");

                user.Photos.Remove(photo);

                var success = await _spaceCampContext.SaveChangesAsync() > 0;

                if (success)
                    return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem deleting photo from Api");
            }
        }
    }
}
