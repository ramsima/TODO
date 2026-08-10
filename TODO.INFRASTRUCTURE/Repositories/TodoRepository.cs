using Dapper;
using System.Data;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public TodoRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CreateAsync(CreateTodoDto dto, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
        INSERT INTO Todos
        (
            Title,
            Description,
            CategoryId,
            PriorityId,
            DueDate,
            IsCompleted,
            CreatedAt
        )
        VALUES
        (
            @Title,
            @Description,
            @CategoryId,
            @PriorityId,
            @DueDate,
            0,
            GETUTCDATE()
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";

        var command = new CommandDefinition(
                commandText: sql,
                cancellationToken:cancellationToken,
                parameters: dto
            );

        return await connection.ExecuteScalarAsync<int>(command);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = @"
            Delete from Todos where Id = @Id
        ";

        var command = new CommandDefinition(
                commandText:sql,
                commandType:CommandType.Text,
                parameters: new {Id = id},
                cancellationToken: cancellationToken
            );

        int success = await connection.ExecuteAsync(command);

        return success > 0;
    }

    public async Task<IEnumerable<TodoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
        SELECT
            t.Id,
            t.Title,
            t.Description,
            c.Name AS CategoryName,
            p.Name AS PriorityName,
            t.DueDate,
            t.IsCompleted
        FROM Todos t
        INNER JOIN Categories c
            ON t.CategoryId = c.Id
        INNER JOIN Priorities p
            ON t.PriorityId = p.Id";

        var command = new CommandDefinition(
                commandText : sql,
                cancellationToken: cancellationToken
             );
        return await connection.QueryAsync<TodoDto>(command);
    }

    public async Task<TodoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
        SELECT
            t.Id,
            t.Title,
            t.Description,
            c.Name AS CategoryName,
            p.Name AS PriorityName,
            t.DueDate,
            t.IsCompleted
        FROM Todos t
        INNER JOIN Categories c
            ON t.CategoryId = c.Id
        INNER JOIN Priorities p
            ON t.PriorityId = p.Id
        WHERE t.Id = @Id";

        var command = new CommandDefinition(
                commandText: sql,
                cancellationToken: cancellationToken,
                parameters: new {Id = id}
            );

        return await connection.QueryFirstOrDefaultAsync<TodoDto>(command);
    }

    public async Task<bool> UpdateAsync(UpdateTodoDto dto, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
        UPDATE Todos
        SET
            Title = @Title,
            Description = @Description,
            CategoryId = @CategoryId,
            PriorityId = @PriorityId,
            DueDate = @DueDate,
            IsCompleted = @IsCompleted,
            UpdatedAt = GETUTCDATE()
        WHERE Id = @Id";

        var command = new CommandDefinition(
                commandText:sql,
                parameters:dto,
                cancellationToken:cancellationToken
            );

        int success = await connection.ExecuteAsync(command);

        return success > 0;
    }
}