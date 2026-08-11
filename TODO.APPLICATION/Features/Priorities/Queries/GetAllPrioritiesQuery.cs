using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Priorities.Queries
{
    public record GetAllPrioritiesQuery(
        ) : ICacheableQuery<IEnumerable<PriorityDto>>
    {
        public string CacheKey => "priorities:all";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
