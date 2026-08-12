using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Interfaces
{
    public interface ICacheInvalidationPublisher
    {
        Task PublishAsync(IReadOnlyCollection<string> cacheKey, CancellationToken cancellationToken = default);
    }
}
