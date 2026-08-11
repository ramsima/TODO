using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Interfaces
{
    public interface ICachingService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken);

        Task RemoveAsync(string key, CancellationToken cancellationToken);

        Task SetAsync<T>(string key,T value,TimeSpan? expiration = null ,CancellationToken cancellationToken = default);

        Task<T?> GetOrCreateAsync<T>(
                string key,
                Func<Task<T>> factory,
                TimeSpan? expiration = null,
                CancellationToken cancellationToken = default
            );
    }
}
