using Dapper;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;
using TODO.APPLICATION.Data_Interface;
using TODO.APPLICATION.DTOs;

namespace Todo.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TagRepository(IDbConnectionFactory connectionFactory)
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