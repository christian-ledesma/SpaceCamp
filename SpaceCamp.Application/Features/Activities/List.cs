using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SpaceCamp.Application.Core;
using SpaceCamp.Application.Interfaces;
using SpaceCamp.Persistence.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Activities
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ActivityDto>>>
        {
            public ActivityParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ActivityDto>>>
        {
            private readonly SpaceCampContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(SpaceCampContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }
            public async Task<Result<PagedList<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Activities
                    .Where(x => x.Date >= request.Params.StartDate)
                    .OrderBy(x => x.Date)
                    .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();
                //.Include(x => x.Attendees).ThenInclude(x => x.User).ToListAsync();

                if (request.Params.IsGoing && !request.Params.IsHost)
                {
                    query = query.Where(x => x.Attendees.Any(y => y.Username == _userAccessor.GetUsername()));
                }

                if (request.Params.IsHost && !request.Params.IsGoing)
                {
                    query = query.Where(x => x.HostUsername == _userAccessor.GetUsername());
                }

                var pagedItems = await PagedList<ActivityDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);
                return Result<PagedList<ActivityDto>>.Success(pagedItems);
            }
        }
    }
}
