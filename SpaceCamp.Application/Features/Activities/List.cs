using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
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
        public class Query : IRequest<Result<List<ActivityDto>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
        {
            private readonly SpaceCampContext _context;
            private readonly IMapper _mapper;

            public Handler(SpaceCampContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _context.Activities.ProjectTo<ActivityDto>(_mapper.ConfigurationProvider).ToListAsync();
                    //.Include(x => x.Attendees).ThenInclude(x => x.User).ToListAsync();
                return Result<List<ActivityDto>>.Success(activities);
            }
        }
    }
}
