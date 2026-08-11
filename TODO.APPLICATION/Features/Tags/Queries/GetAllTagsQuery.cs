using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Tags.Queries
{
    public record GetAllTagsQuery(
        ) : ICacheableQuery<IEnumerable<TagDto>>
    {
        public string CacheKey => "tags:all";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
    
}
