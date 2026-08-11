using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;
using Dapper;
using TODO.APPLICATION.DTOs;

namespace Todo.Infrastructure.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public PriorityRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<PriorityDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            var command = new CommandDefinition(
                    commandText: @"SELECT Id, Name FROM Priorities ORDER BY Name",
                    cancellationToken:cancellationToken
                );

            return await connection.QueryAsync<PriorityDto>(command);
        }
    }
}
