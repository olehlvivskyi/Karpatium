using System.Data;
using Microsoft.Data.SqlClient;
using Serilog;

namespace Karpatium.Core.Database;

/// <summary>
/// Manages and interacts with a SQL Server database by providing functionality such as executing queries and managing database connections.
/// </summary>
public static class MsSqlManager
{
    private static string? _connectionString;

    /// <summary>
    /// Initializes the SQL Server manager with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string used to connect to the SQL Server database.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided <paramref name="connectionString"/> is null or empty.
    /// </exception>
    public static void Initialize(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
        }
        
        Log.Verbose("MsSqlManager: Initializing with `{ConnectionString}` connection string.", connectionString);
        
        _connectionString = connectionString;
    }
    
    /// <summary>
    /// Executes the specified SQL query without returning any results.
    /// </summary>
    /// <param name="query">The SQL query to execute against the database.</param>
    public static void ExecuteNonReturningQuery(string query)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand command = new SqlCommand(query, connection);
        
        connection.Open();
        command.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Executes the specified SQL query and returns a DataTable containing the results.
    /// </summary>
    /// <param name="query">The SQL query to execute against the database.</param>
    /// <returns>A DataTable containing the rows and columns returned by the query.</returns>
    public static DataTable ExecuteQuery(string query)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataAdapter adapter = new SqlDataAdapter(command);
        
        DataTable dataTable = new DataTable();
        connection.Open();
        adapter.Fill(dataTable);

        return dataTable;
    }

    /// <summary>
    /// Executes the specified SQL query and returns the first column of the first row in the result set.
    /// </summary>
    /// <param name="query">The SQL query to execute against the database.</param>
    /// <returns>
    /// The first column of the first row in the result set, or null if the result set is empty.
    /// </returns>
    public static object? ExecuteQueryScalar(string query)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand command = new SqlCommand(query, connection);
        
        connection.Open();
        object? result = command.ExecuteScalar();
        
        return result;
    }

    /// <summary>
    /// Executes a stored procedure with the specified name and parameters against the SQL Server database.
    /// </summary>
    /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
    /// <param name="sqlParameters">A list of SQL parameters to pass to the stored procedure.</param>
    /// <returns>The number of rows affected by the stored procedure execution.</returns>
    public static int ExecuteStoredProcedure(string storedProcedureName, List<SqlParameter> sqlParameters)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand command = new SqlCommand(storedProcedureName, connection);
        
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(sqlParameters.ToArray());
        connection.Open();
        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected;
    }
}