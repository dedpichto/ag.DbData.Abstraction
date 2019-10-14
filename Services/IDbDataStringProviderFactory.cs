namespace ag.DbData.Abstraction.Services
{
    /// <summary>
    /// RRepresents IDbDataStringProviderFactory interface.
    /// </summary>
    /// <typeparam name="T">The type used for creation of <see cref="DbDataStringProvider"/> object.</typeparam>
    public interface IDbDataStringProviderFactory<out T> where T : DbDataStringProvider
    {
        /// <summary>
        /// Creates <see cref="DbDataStringProvider"/> of type T.
        /// </summary>
        /// <returns><see cref="DbDataStringProvider"/> of type T.</returns>
        T Get();
    }
}
