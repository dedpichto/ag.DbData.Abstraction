using System.Diagnostics;
using ag.DbData.Abstraction.Services;

namespace ag.DbData.Abstraction
{
    /// <summary>
    /// Represents <see cref="ag.DbData"/> settings.
    /// </summary>
    public class DbDataSettings
    {
        private readonly DbDataStringProvider _stringProvider;

        /// <summary>
        /// Creates new instance of DbDataSettings.
        /// </summary>
        public DbDataSettings()
        {
            _stringProvider = new DbDataStringProvider();
        }

        /// <summary>
        /// Specifies whether exceptions logging is allowed.
        /// </summary>
        public bool AllowExceptionLogging { get; set; } = true;

        /// <summary>
        /// Database connection string.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string ConnectionString
        {
            protected internal get => _stringProvider.ConnectionString;
            set => _stringProvider.ConnectionString = value;
        }
    }
}
