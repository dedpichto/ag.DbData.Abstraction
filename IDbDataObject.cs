using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ag.DbData.Abstraction
{
    /// <summary>
    /// Represents IDbDataObject interface.
    /// </summary>
    public interface IDbDataObject : IDisposable
    {
        /// <summary>
        /// Begins transaction on database specified in connection string.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>True if transaction has been started, false otherwise.</returns>
        bool BeginTransaction(string connectionString);

        /// <summary>
        /// Begins transaction on current database
        /// </summary>
        /// <returns></returns>
        bool BeginTransaction();

        /// <summary>
        /// Commits transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Fills <see cref="DataSet"/> accordingly to specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSet(string query);

        /// <summary>
        /// Fills <see cref="DataSet"/> accordingly to specified SQL query with command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSet(string query, int timeout);

        /// <summary>
        /// Fills <see cref="DataSet"/> accordingly to specified SQL query, storing results in tables with names specified in <paramref name="tables"/> parameter.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="tables">List of tables names tbe used in DataSet.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSet(string query, IEnumerable<string> tables);

        /// <summary>
        /// Fills <see cref="DataSet"/> accordingly to specified SQL query with command timeout, storing results in tables with names specified in <paramref name="tables"/> parameter.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="tables">List of tables names tbe used in DataSet.</param>
        /// <param name="timeout">Command timeout</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSet(string query, IEnumerable<string> tables, int timeout);

        /// <summary>
        /// Fills <see cref="DataSet"/> in transaction accordingly to specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSetInTransaction(string query);

        /// <summary>
        /// Fills <see cref="DataSet"/> in transaction accordingly to specified SQL query with command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSetInTransaction(string query, int timeout);

        /// <summary>
        /// Fills <see cref="DataSet"/> in transaction accordingly to specified SQL query, storing results in tables with names specified in <paramref name="tables"/> parameter.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="tables">List of tables names tbe used in DataSet.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSetInTransaction(string query, IEnumerable<string> tables);

        /// <summary>
        /// Fills <see cref="DataSet"/> in transaction accordingly to specified SQL query with command timeout, storing results in tables with names specified in <paramref name="tables"/> parameter.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="tables">List of tables names tbe used in DataSet.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="DataSet"/>.</returns>
        DataSet FillDataSetInTransaction(string query, IEnumerable<string> tables, int timeout);

        /// <summary>
        /// Fills <see cref="DataTable"/> accordingly to specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable FillDataTable(string query);

        /// <summary>
        /// Fills <see cref="DataTable"/> accordingly to specified SQL query with command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable FillDataTable(string query, int timeout);

        /// <summary>
        /// Fills <see cref="DataTable"/> in transaction accordingly to specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable FillDataTableInTransaction(string query);

        /// <summary>
        /// Fills <see cref="DataTable"/> in transaction accordingly to specified SQL query with command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable FillDataTableInTransaction(string query, int timeout);

        /// <summary>
        /// Executes specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns>Numbers of rows affected by execution.</returns>
        int Execute(string query);

        /// <summary>
        /// Executes specified SQL query with specified command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns>Numbers of rows affected by execution.</returns>
        int Execute(string query, int timeout);

        /// <summary>
        /// Executes specified SQL query in transaction.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns>Numbers of rows affected by execution.</returns>
        int ExecuteInTransaction(string query);

        /// <summary>
        /// Executes specified SQL query in transaction with specified command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns>Numbers of rows affected by execution.</returns>
        int ExecuteInTransaction(string query, int timeout);

        /// <summary>
        /// Gets <see cref="DbDataReader"/> for specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="DbDataReader"/>.</returns>
        DbDataReader GetDataReader(string query);

        /// <summary>
        /// Gets <see cref="DbDataReader"/> for specified SQL query with specified command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="DbDataReader"/>.</returns>
        DbDataReader GetDataReader(string query, int timeout);

        /// <summary>
        /// Gets <see cref="DbDataReader"/> for specified SQL query, using one of the <see cref="CommandBehavior"/> values
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="commandBehavior">CommandBehavior value.</param>
        /// <returns><see cref="DbDataReader"/>.</returns>
        DbDataReader GetDataReader(string query, CommandBehavior commandBehavior);

        /// <summary>
        /// Gets <see cref="DbDataReader"/> for specified SQL query with specified command timeout, using one of the <see cref="CommandBehavior"/> values
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandBehavior"></param>
        /// <param name="timeout"></param>
        /// <returns><see cref="DbDataReader"/>.</returns>
        DbDataReader GetDataReader(string query, CommandBehavior commandBehavior, int timeout);

        /// <summary>
        /// Executes <see cref="DbCommand"/>.
        /// </summary>
        /// <param name="cmd"><see cref="DbCommand"/>.</param>
        /// <returns>Number of rows affected by execution.</returns>
        int ExecuteCommand(DbCommand cmd);

        /// <summary>
        /// Executes <see cref="DbCommand"/> with specified command timeout.
        /// </summary>
        /// <param name="cmd"><see cref="DbCommand"/>.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns>Number of rows affected by execution.</returns>
        int ExecuteCommand(DbCommand cmd, int timeout);

        /// <summary>
        /// Executes <see cref="DbCommand"/> in transaction.
        /// </summary>
        /// <param name="cmd"><see cref="DbCommand"/>.</param>
        /// <returns>Number of rows affected by execution.</returns>
        int ExecuteCommandInTransaction(DbCommand cmd);

        /// <summary>
        /// Executes <see cref="DbCommand"/> in transaction with specified command timeout.
        /// </summary>
        /// <param name="cmd"><see cref="DbCommand"/>.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns>Number of rows affected by execution.</returns>
        int ExecuteCommandInTransaction(DbCommand cmd, int timeout);
        
        /// <summary>
        /// Gets schema information for the data source of DbDataObject connection .
        /// </summary>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable GetSchema();

        /// <summary>
        /// Gets schema information for the data source of DbDataObject connection using the specified string for the schema name.
        /// </summary>
        /// <param name="collectionName">Specifies the name of the schema to return.</param>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable GetSchema(string collectionName);

        /// <summary>
        /// Gets schema information for the data source of DbDataObject connection using the specified string for the schema name and the specified string array for the restriction values.
        /// </summary>
        /// <param name="collectionName">Specifies the name of the schema to return.</param>
        /// <param name="restrictedValues">Specifies a set of restriction values for the requested schema.</param>
        /// <returns><see cref="DataTable"/>.</returns>
        DataTable GetSchema(string collectionName, string[] restrictedValues);

        /// <summary>
        /// Gets scalar value for specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="object"/>.</returns>
        object GetScalar(string query);

        /// <summary>
        /// Gets scalar value for specified SQL query with command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="object"/>.</returns>
        object GetScalar(string query, int timeout);

        /// <summary>
        /// Gets scalar value for specified SQL query in transaction.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns><see cref="object"/>.</returns>
        object GetScalarInTransaction(string query);

        /// <summary>
        /// Gets scalar value for specified SQL query with command timeout in transaction.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns><see cref="object"/>.</returns>
        object GetScalarInTransaction(string query, int timeout);

        #region Async methods
        /// <summary>
        /// Asynchronously executes specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<int> ExecuteAsync(string query);

        /// <summary>
        /// Asynchronously executes specified SQL query with specified command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<int> ExecuteAsync(string query, int timeout);

        /// <summary>
        /// Asynchronously executes specified SQL query with cancellation token.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<int> ExecuteAsync(string query, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously executes specified SQL query with command timeout and cancellation token.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<int> ExecuteAsync(string query, int timeout, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously gets scalar value for specified SQL query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<object> GetScalarAsync(string query);

        /// <summary>
        /// Asynchronously gets scalar value for specified SQL query with command timeout.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<object> GetScalarAsync(string query, int timeout);

        /// <summary>
        /// Asynchronously gets scalar value for specified SQL query with cancellation token.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="cancellationToken">>A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<object> GetScalarAsync(string query, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously gets scalar value for specified SQL query with command timeout and cancellation token.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <param name="timeout">Command timeout.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<object> GetScalarAsync(string query, int timeout, CancellationToken cancellationToken);
        #endregion
    }
}
