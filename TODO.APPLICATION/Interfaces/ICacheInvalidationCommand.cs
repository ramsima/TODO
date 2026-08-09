using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Interfaces
{
    public interface ICacheInvalidationCommand<T> : IRequest<T>
    {
        public IReadOnlyCollection<string> CacheKey { get; } 
    }
}
