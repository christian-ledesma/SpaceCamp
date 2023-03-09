using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Application.Core;
using SpaceCamp.Persistence.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceCamp.Application.Features.Profiles
{
    public class Details
    {
        public class Query : IRequest<Result<Profile>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Profile>>
        {
            private readonly SpaceCampContext _spaceCampContext;
            private readonly IMapper _mapper;

            public Handler(SpaceCampContext spaceCampContext, IMapper mapper)
            {
                _spaceCampContext = spaceCampContext ?? throw new ArgumentNullException(nameof(spaceCampContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _spaceCampContext.Users
                                                    .ProjectTo<Profile>(_mapper.ConfigurationProvider)
                                                    .SingleOrDefaultAsync(x => x.Username == request.Username);

                return Result<Profile>.Success(user);
            }
        }
    }
}
