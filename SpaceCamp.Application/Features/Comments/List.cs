using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
using SpaceCamp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Comments
{
    public class List
    {
        public class Query : IRequest<Result<List<CommentDto>>>
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<CommentDto>>>
        {
            private readonly SpaceCampContext _spaceCampContext;
            private readonly IMapper _mapper;

            public Handler(SpaceCampContext spaceCampContext, IMapper mapper)
            {
                _spaceCampContext = spaceCampContext ?? throw new ArgumentNullException(nameof(spaceCampContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Result<List<CommentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var comments = await _spaceCampContext.Comments
                                                        .Where(x => x.Activity.Id == request.ActivityId)
                                                        .OrderByDescending(x => x.CreatedAt)
                                                        .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                                                        .ToListAsync(CancellationToken.None);

                return Result<List<CommentDto>>.Success(comments);
            }
        }
    }
}
