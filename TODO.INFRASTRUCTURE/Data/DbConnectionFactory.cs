using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TODO.APPLICATION.Data_Interface;

namespace Todo.Infrastructure.Data;

public class DbConnectionFactory :IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(
                "DefaultConnection connection string is not configured."));
    }

}