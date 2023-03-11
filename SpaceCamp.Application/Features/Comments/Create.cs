using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
using SpaceCamp.Application.Interfaces;
using SpaceCamp.Domain.Entities;
using SpaceCamp.Persistence.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Comments
{
    public class Create
    {
        public class  Command : IRequest<Result<CommentDto>>
        {
            public string Body { get; set; }
            public Guid ActivityId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Body).NotEmpty();
                RuleFor(x => x.ActivityId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<CommentDto>>
        {
            private readonly SpaceCampContext _spaceCampContext;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(SpaceCampContext spaceCampContext, IMapper mapper, IUserAccessor userAccessor)
            {
                _spaceCampContext = spaceCampContext ?? throw new ArgumentNullException(nameof(spaceCampContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            }

            public async Task<Result<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _spaceCampContext.Activities.FindAsync(request.ActivityId);
                if (activity is null)
                    return null;

                var user = await _spaceCampContext.Users
                                                    .Include(x => x.Photos)
                                                    .SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                var comment = new Comment
                {
                    Author = user,
                    Activity = activity,
                    Body = request.Body
                };

                activity.Comments.Add(comment);

                var success = await _spaceCampContext.SaveChangesAsync() > 0;

                if (success)
                    return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
                return Result<CommentDto>.Failure("Failed to add comment");
            }
        }
    }
}
