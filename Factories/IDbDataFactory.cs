﻿namespace ag.DbData.Abstraction.Factories
{
    /// <summary>
    /// Represents IDbDataFactory interface.
    /// </summary>
    public interface IDbDataFactory
    {
        /// <summary>
        /// Creates object of type <see cref="IDbDataObject"/>.
        /// </summary>
        /// <returns><see cref="IDbDataObject"/>.</returns>
        IDbDataObject Create();
        /// <summary>
        /// Creates object of type <see cref="IDbDataObject"/>.
        /// </summary>
        /// <param name="defaultCommandTimeOut">Replaces default coommand timeout of provider</param>
        /// <returns></returns>
        IDbDataObject Create(int defaultCommandTimeOut);
        /// <summary>
        /// Creates object of type <see cref="IDbDataObject"/>.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns><see cref="IDbDataObject"/>.</returns>
        IDbDataObject Create(string connectionString);
    }
}
