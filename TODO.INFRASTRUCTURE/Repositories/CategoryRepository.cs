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
    public class CategoryRepository(DbConnectionFactory connectionFactory) : ICategoryRepository
    {
        private readonly DbConnectionFactory _connectionFactory = connectionFactory;
        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            string sql = @"SELECT Id, Name
              FROM Categories
              ORDER BY Name";

            var command = new CommandDefinition(
                    commandText:sql,
                    cancellationToken:cancellationToken
                );
            var a = await connection.QueryAsync<CategoryDto>(command);
            return a;
        }
    }
}
