using System;

namespace ag.DbData.Abstraction
{
    /// <summary>
    /// Represents custom DbData exception.
    /// </summary>
    public class DbDataException : Exception
    {
        /// <summary>
        /// Gets text of SQL command caused the exception.
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// Creates new instance of DbDataException.
        /// </summary>
        /// <param name="ex">Original exception.</param>
        /// <param name="commandText">SQL command text.</param>
        public DbDataException(Exception ex, string commandText) : base(ex.Message, ex)
        {
            CommandText = commandText;
        }
    }
}
