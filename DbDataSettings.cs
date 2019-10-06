namespace ag.DbData.Abstraction
{
    /// <summary>
    /// Represents <see cref="ag.DbData"/> settings.
    /// </summary>
    public class DbDataSettings
    {
        /// <summary>
        /// Specifies whether exceptions logging is allowed.
        /// </summary>
        public bool AllowExceptionLogging { get; set; } = true;
    }
}
