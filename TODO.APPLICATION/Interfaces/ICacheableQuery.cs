using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Interfaces
{
    public interface ICacheableQuery<TResponse> : IRequest<TResponse>
    {
        public string CacheKey { get; }
        public TimeSpan? Expiration { get; }
    }
}
