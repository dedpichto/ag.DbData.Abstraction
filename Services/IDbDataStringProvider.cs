namespace ag.DbData.Abstraction.Services
{
    /// <summary>
    /// Represents IDbDataStringProvider interface.
    /// </summary>
    public interface IDbDataStringProvider
    {
        /// <summary>
        /// Encrypts connection string on set and decrypts it on get.
        /// </summary>
        string ConnectionString { get; set; }
    }
}
