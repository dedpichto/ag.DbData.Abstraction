using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Options;

namespace ag.DbData.Abstraction
{
    /// <summary>
    /// Abstract class implemented IDbDataObject interface.
    /// </summary>
    public abstract class DbDataObject : IDbDataObject
    {
        #region ctor
        /// <summary>
        /// Creates new instance of <see cref="DbDataObject"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> object.</param>
        /// <param name="options"><see cref="DbDataSettings"/> options.</param>
        protected DbDataObject(ILogger<IDbDataObject> logger, IOptions<DbDataSettings> options)
        {
            var dbDataSettings = options?.Value;
            Logger = dbDataSettings == null || dbDataSettings.AllowExceptionLogging ? logger : null;
        }

        /// <summary>
        /// Creates new instance of <see cref="DbDataObject"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> object.</param>
        [ObsoleteAttribute("Used for backward compatibility with versions prior to 2.0.4.1")]
        protected DbDataObject(ILogger<IDbDataObject> logger)
        {
            Logger = logger;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Represents a connection to a database.
        /// </summary>
        public DbConnection Connection { protected internal get; set; }
        #endregion

        #region Protected properties

        /// <summary>
        /// Represents logger object.
        /// </summary>
        protected ILogger<IDbDataObject> Logger { get; set; }

        /// <summary>
        /// Represents a connection to a database used for transactions.
        /// </summary>
        protected DbConnection TransConnection { get; set; }

        /// <summary>
        /// Represents transaction.
        /// </summary>
        protected DbTransaction Transaction { get; set; }
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
        public abstract int ExecuteCommand(DbCommand cmd);

        /// <inheritdoc />
        public abstract int ExecuteCommand(DbCommand cmd, int timeout);

        /// <inheritdoc />
        public abstract int ExecuteCommandInTransaction(DbCommand cmd);

        /// <inheritdoc />
        public abstract int ExecuteCommandInTransaction(DbCommand cmd, int timeout);

        /// <inheritdoc />
        public abstract bool BeginTransaction(string connectionString);

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
        public DbDataReader GetDataReader(string query) => innerGetDataReader(query, -1);

        /// <inheritdoc />
        public DbDataReader GetDataReader(string query, int timeout) => innerGetDataReader(query, timeout);

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
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Connection?.Dispose();
            TransConnection?.Dispose();
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
                    cmd.CommandText = query;
                    if (inTransaction)
                        cmd.Transaction = Transaction;
                    else
                        Connection.Open();
                    if (timeout != -1)
                    {
                        if (timeout >= 0)
                            cmd.CommandTimeout = timeout;
                        else
                            throw new ArgumentException("Invalid CommandTimeout value", nameof(timeout));
                    }
                    var rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at Execute");
                throw new DbDataException(ex, query);
            }
            finally
            {
                if (!inTransaction)
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
            }
        }

        private DbDataReader innerGetDataReader(string query, int timeout)
        {
            DbDataReader reader = null;
            var cmd = Connection.CreateCommand();
            cmd.CommandText = query;
            try
            {
                Connection.Open();
                if (timeout != -1)
                {
                    if (timeout >= 0)
                        cmd.CommandTimeout = timeout;
                    else
                        throw new ArgumentException("Invalid CommandTimeout value", nameof(timeout));
                }

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (DbException dex)
            {
                Logger?.LogError(dex, "Error at GetDataReader");
                throw new DbDataException(dex, query);
            }
            catch (Exception ex)
            {
                cmd.Cancel();
                if (reader == null)
                    Connection.Close();
                Logger?.LogError(ex, "Error at GetDataReader");
                throw new DbDataException(ex, query);
            }
            finally
            {
                cmd.Dispose();
            }
        }

        private object innerGetScalar(string query, int timeout, bool inTransaction)
        {
            try
            {
                using (var cmd = inTransaction ? TransConnection.CreateCommand() : Connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    if (inTransaction)
                        cmd.Transaction = Transaction;
                    else
                        Connection.Open();
                    if (timeout == -1) return cmd.ExecuteScalar();
                    if (timeout >= 0)
                        cmd.CommandTimeout = timeout;
                    else
                        throw new ArgumentException("Invalid CommandTimeout value", nameof(timeout));
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error at GetScalar");
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
