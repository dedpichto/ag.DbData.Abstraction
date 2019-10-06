namespace ag.DbData.Abstraction.Factories
{
    /// <summary>
    /// Represents IDbDataFactory interface.
    /// </summary>
    public interface IDbDataFactory
    {
        /// <summary>
        /// Creates object of type <see cref="IDbDataObject"/>.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns><see cref="IDbDataObject"/>.</returns>
        IDbDataObject Create(string connectionString);
    }
}
