using Dapper;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;
using TODO.APPLICATION.Data_Interface;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Interfaces;

namespace Todo.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly IUnitOfWork _uow;

    public TagRepository(IUnitOfWork uow)
    {
        _uow = uow;
    }

   

    public async Task<IEnumerable<TagDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        //using var connection = _connectionFactory.CreateConnection();


        var command = new CommandDefinition(
                commandText:@"Select Id,Name from tags order by name",
                cancellationToken:cancellationToken,
                transaction : _uow.Transaction
            );

        return await _uow.Connection.QueryAsync<TagDto>(command);
    }
}