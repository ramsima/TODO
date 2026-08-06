using Dapper;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public TagRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TagDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();


        var command = new CommandDefinition(
                commandText:@"Select Id,Name from tags order by name",
                cancellationToken:cancellationToken
            );

        return await connection.QueryAsync<TagDto>(command);
    }
}