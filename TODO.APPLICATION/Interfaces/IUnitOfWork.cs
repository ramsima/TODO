using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Interfaces
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        IDbTransaction? Transaction { get; }

        Task BeginAsync(CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellatinToken = default);

        Task RollbackAsync(CancellationToken cancellatinToken = default);
    }
}
