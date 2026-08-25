using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;
using Dapper;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Data_Interface;
using TODO.APPLICATION.Interfaces;

namespace Todo.Infrastructure.Repositories
{
    public class CategoryRepository(IUnitOfWork _uow) : ICategoryRepository
    {
        
        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            //using var connection = _connectionFactory.CreateConnection();
            string sql = @"SELECT Id, Name
              FROM Categories
              ORDER BY Name";

            var command = new CommandDefinition(
                    commandText:sql,
                    cancellationToken:cancellationToken,
                    transaction: _uow.Transaction
                );
            var a = await _uow.Connection.QueryAsync<CategoryDto>(command);
            return a;
        }
    }
}
