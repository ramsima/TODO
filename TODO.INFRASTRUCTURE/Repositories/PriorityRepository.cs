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
    public class PriorityRepository : IPriorityRepository
    {
        private readonly IUnitOfWork _uow;

        public PriorityRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<PriorityDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            //using var connection = _connectionFactory.CreateConnection();

            var command = new CommandDefinition(
                    commandText: @"SELECT Id, Name FROM Priorities ORDER BY Name",
                    cancellationToken:cancellationToken,
                    transaction: _uow.Transaction
                );

            return await _uow.Connection.QueryAsync<PriorityDto>(command);
        }
    }
}
