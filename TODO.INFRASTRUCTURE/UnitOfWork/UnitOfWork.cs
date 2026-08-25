using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Infrastructure.Data;
using TODO.APPLICATION.Data_Interface;
using TODO.APPLICATION.Interfaces;

namespace TODO.INFRASTRUCTURE.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly IDbConnection _connection;
        public UnitOfWork(DbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
        }
        public IDbConnection Connection => _connection;

        public IDbTransaction? Transaction {get; private set;}

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            if(_connection is DbConnection dbConnectin)
            {
                await dbConnectin.OpenAsync(cancellationToken);
            }
            else
            {
                _connection.Open();
            }

            Transaction = _connection.BeginTransaction();
        }

        public Task CommitAsync(CancellationToken cancellatinToken = default)
        {
            Transaction?.Commit();
            return Task.CompletedTask;
        }

        public Task RollbackAsync(CancellationToken cancellatinToken = default)
        {
            Transaction?.Rollback();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            _connection.Dispose();
        }
    }
}
