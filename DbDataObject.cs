using ag.DbData.Abstraction.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ag.DbData.Abstraction
{
    /// <summary>
    /// Abstract class implemented IDbDataObject interface.
    /// </summary>
    public abstract class DbDataObject : IDbDataObject
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DbConnection _connection;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DbDataSettings _dbDataSettings;

        #region ctor

        /// <summary>
        /// Creates new instance of <see cref="DbDataObject"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> object.</param>
        /// <param name="options"><see cref="DbDataSettings"/> options.</param>
        /// <param name="stringProvider"><see cref="IDbDataStringProvider"/>.</param>
        protected DbDataObject(ILogger<IDbDataObject> logger, IOptions<DbDataSettings> options, IDbDataStringProvider stringProvider) : this(logger, options)
        {
            StringProvider = stringProvider;
            var connectionString = _dbDataSettings.ConnectionString;
            if (!string.IsNullOrEmpty(connectionString))
                StringProvider.ConnectionString = connectionString;
        }

        /// <summary>
        /// Creates new instance of <see cref="DbDataObject"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> object.</param>
        /// <param name="options"><see cref="DbDataSettings"/> options.</param>
        protected DbDataObject(ILogger<IDbDataObject> logger, IOptions<DbDataSettings> options)
        {
            _dbDataSettings = options?.Value;
            Logger = _dbDataSettings == null || _dbDataSettings.AllowExceptionLogging ? logger : null;
        }

        /// <summary>
        /// Creates new instance of <see cref="DbDataObject"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> object.</param>
        [Obsolete("Used for backward compatibility with versions prior to 2.0.4.1")]
        protected DbDataObject(ILogger<IDbDataObject> logger)
        {
            Logger = logger;
        }
        #endregion

        #region Public properties

        /// <summary>
        /// Represents a connection to a database.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DbConnection Connection
        {
            protected internal get
            {
                return _connection;
            }
            set
            {
                if (value == null)
                {
                    _connection?.Dispose();
                    _connection = null;
                    return;
                }
                _connection?.Dispose();
                _connection = value;
                if (StringProvider != null)
                    StringProvider.ConnectionString = _connection.ConnectionString;
            }
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// Represents logger object.
        /// </summary>
        protected ILogger<IDbDataObject> Logger { get; }

        /// <summary>
        /// Represents <see cref="DbDataStringProvider"/> object.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected IDbDataStringProvider StringProvider { get; }

        /// <summary>
        /// Represents a connection to a database used for transactions.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected DbConnection TransConnection { get; set; }

        /// <summary>
        /// Represents transaction.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected DbTransaction Transaction { get; set; }
        #endregion

        #region Protected methods
        /// <summary>
        /// Checks whether valid timeout is set and if it is valid sets <see cref="DbCommand.CommandTimeout"/> property.
        /// </summary>
        /// <param name="command"><see cref="DbCommand"/>.</param>
        /// <param name="timeout">Timeout value.</param>
        /// <returns>True iff timeout is valid, false otherwise.</returns>
        protected static bool IsValidTimeout(DbCommand command, int timeout)
        {
            if (timeout == -1) return true;
            if (timeout < 0) return false;
            command.CommandTimeout = timeout;
            return true;
        }
        #endregion

        #region Abstract methods
        /// <inheritdoc />
        public abstract DataSet FillDataSet(string query);

        /// <inheritdoc />
        public abstract DataSet FillDataSet(string query, int timeout);

        /// <inheritdoc />
        public abstract DataSet FillDataSet(string query, IEnumerable<string> tables);

        /// <inheritdoc />
        public abstract DataSet FillDataSet(string query, IEnumerable<string> tables, int timeout);

        /// <inheritdoc />
        public abstract DataSet FillDataSetInTransaction(string query);

        /// <inheritdoc />
        public abstract DataSet FillDataSetInTransaction(string query, int timeout);

        /// <inheritdoc />
        public abstract DataSet FillDataSetInTransaction(string query, IEnumerable<string> tables);

        /// <inheritdoc />
        public abstract DataSet FillDataSetInTransaction(string query, IEnumerable<string> tables, int timeout);

        /// <inheritdoc />
        public abstract DataTable FillDataTable(string query);

        /// <inheritdoc />
        public abstract DataTable FillDataTable(string query, int timeout);

        /// <inheritdoc />
        public abstract DataTable FillDataTableInTransaction(string query);

        /// <inheritdoc />
        public abstract DataTable FillDataTableInTransaction(string query, int timeout);

        /// <inheritdoc />
        public abstract DataTable FillDataTable(DbCommand dbCommand);

        /// <inheritdoc />
        public abstract DataTable FillDataTable(DbCommand dbCommand, int timeout);

        /// <inheritdoc />
        public abstract DataTable FillDataTableInTransaction(DbCommand dbCommand);

        /// <inheritdoc />
        public abstract DataTable FillDataTableInTransaction(DbCommand dbCommand, int timeout);

        /// <inheritdoc />
        public abstract int ExecuteCommand(DbCommand cmd);

        /// <inheritdoc />
        public abstract int ExecuteCommand(DbCommand cmd, int timeout);

        /// <inheritdoc />
        public abstract int ExecuteCommandInTransaction(DbCommand cmd);

        /// <inheritdoc />
        public abstract int ExecuteCommandInTransaction(DbCommand cmd, int timeout);

        /// <inheritdoc />
        public abstract bool BeginTransaction(string connectionString);

        /// <inheritdoc />
        public abstract bool BeginTransaction();

        /// <inheritdoc />
        public abstract Task<int> ExecuteAsync(string query);

        /// <inheritdoc />
        public abstract Task<int> ExecuteAsync(string query, int timeout);

        /// <inheritdoc />
        public abstract Task<int> ExecuteAsync(string query, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task<int> ExecuteAsync(string query, int timeout, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task<object> GetScalarAsync(string query);

        /// <inheritdoc />
        public abstract Task<object> GetScalarAsync(string query, int timeout);

        /// <inheritdoc />
        public abstract Task<object> GetScalarAsync(string query, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task<object> GetScalarAsync(string query, int timeout, CancellationToken cancellationToken);

        #endregion

        #region Public methods

        /// <inheritdoc />
        public DataTable GetSchema()
        {
            try
            {
                Connection.Open();
                return Connection.GetSchema();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at GetSchema");
                throw new DbDataException(ex, "Error at GetSchema");
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }

        /// <inheritdoc />
        public DataTable GetSchema(string collectionName)
        {
            try
            {
                Connection.Open();
                return Connection.GetSchema(collectionName);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at GetSchema");
                throw new DbDataException(ex, "Error at GetSchema");
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }

        /// <inheritdoc />
        public DataTable GetSchema(string collectionName, string[] restrictedValues)
        {
            try
            {
                Connection.Open();
                return Connection.GetSchema(collectionName, restrictedValues);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at GetSchema");
                throw new DbDataException(ex, "Error at GetSchema");
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
        }

        /// <inheritdoc />
        public object GetScalar(string query) => innerGetScalar(query, -1, false);

        /// <inheritdoc />
        public object GetScalar(string query, int timeout) => innerGetScalar(query, timeout, false);

        /// <inheritdoc />
        public int Execute(string query) => innerExecute(query, -1, false);

        /// <inheritdoc />
        public int Execute(string query, int timeout) => innerExecute(query, timeout, false);

        /// <inheritdoc />
        public DbDataReader GetDataReader(string query) => innerGetDataReader(query, CommandBehavior.CloseConnection, -1);

        /// <inheritdoc />
        public DbDataReader GetDataReader(string query, int timeout) => innerGetDataReader(query, CommandBehavior.CloseConnection, timeout);

        /// <inheritdoc />
        public DbDataReader GetDataReader(string query, CommandBehavior commandBehavior) => innerGetDataReader(query, commandBehavior, -1);

        /// <inheritdoc />
        public DbDataReader GetDataReader(string query, CommandBehavior commandBehavior, int timeout) => innerGetDataReader(query, commandBehavior, timeout);

        /// <inheritdoc />
        public void CommitTransaction()
        {
            try
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
                if (TransConnection.State == ConnectionState.Open)
                    TransConnection.Close();
                TransConnection.Dispose();
                TransConnection = null;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at CommitTransaction");
                throw new DbDataException(ex, "Error at CommitTransaction");
            }
        }

        /// <inheritdoc />
        public void RollbackTransaction()
        {
            try
            {
                Transaction.Rollback();
                Transaction.Dispose();
                Transaction = null;
                if (TransConnection.State == ConnectionState.Open)
                    TransConnection.Close();
                TransConnection.Dispose();
                TransConnection = null;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at RollbackTransaction");
                throw new DbDataException(ex, "Error at RollbackTransaction");
            }
        }

        /// <inheritdoc />
        public int ExecuteInTransaction(string query) => innerExecute(query, -1, true);

        /// <inheritdoc />
        public int ExecuteInTransaction(string query, int timeout) => innerExecute(query, timeout, true);

        /// <inheritdoc />
        public object GetScalarInTransaction(string query) => innerGetScalar(query, -1, true);

        /// <inheritdoc />
        public object GetScalarInTransaction(string query, int timeout) => innerGetScalar(query, timeout, true);
        #endregion

        #region IDisposable implementation

        private bool _disposed;

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Disposed flag.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                StringProvider?.Dispose();
                Connection?.Dispose();
                Connection = null;
                TransConnection?.Dispose();
                TransConnection = null;
            }

            _disposed = true;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~DbDataObject()
        {
            Dispose(false);
        }
        #endregion

        #region Private methods
        private int innerExecute(string query, int timeout, bool inTransaction)
        {
            try
            {
                using (var cmd = inTransaction
                    ? TransConnection.CreateCommand()
                    : Connection.CreateCommand())
                {
                    if (timeout != -1)
                    {
                        if (timeout >= 0)
                            cmd.CommandTimeout = timeout;
                        else
                            throw new ArgumentException("Invalid CommandTimeout value", nameof(timeout));
                    }
                    cmd.CommandText = query;
                    if (inTransaction)
                        cmd.Transaction = Transaction;
                    else
                        Connection.Open();
                    var rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, $"Error at Execute; command text: {query}");
                throw new DbDataException(ex, query);
            }
            finally
            {
                if (!inTransaction)
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
            }
        }

        private DbDataReader innerGetDataReader(string query, CommandBehavior commandBehavior, int timeout)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = query;
            try
            {
                if (timeout != -1)
                {
                    if (timeout >= 0)
                        cmd.CommandTimeout = timeout;
                    else
                        throw new ArgumentException("Invalid CommandTimeout value", nameof(timeout));
                }
                Connection.Open();
                return cmd.ExecuteReader(commandBehavior);
            }
            catch (DbException dex)
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Logger?.LogError(dex, $"Error at GetDataReader; command text: {query}");
                throw new DbDataException(dex, query);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, $"Error at GetDataReader; command text: {query}");
                throw new DbDataException(ex, query);
            }
        }

        private object innerGetScalar(string query, int timeout, bool inTransaction)
        {
            try
            {
                using (var cmd = inTransaction ? TransConnection.CreateCommand() : Connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    if (timeout != -1)
                    {
                        if (timeout >= 0)
                            cmd.CommandTimeout = timeout;
                        else
                            throw new ArgumentException("Invalid CommandTimeout value", nameof(timeout));
                    }
                    if (inTransaction)
                        cmd.Transaction = Transaction;
                    else
                        Connection.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, $"Error at GetScalar; command text: {query}");
                throw new DbDataException(ex, query);
            }
            finally
            {
                if (!inTransaction)
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
            }
        }
        #endregion
    }
}
