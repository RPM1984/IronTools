//-----------------------------------------------------------------------
// <copyright file="ConfigurationFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    /// <summary>
    /// Base class for all configuration factories
    /// </summary>
    internal abstract class ConfigurationFactory
    {
        /// <summary>
        /// Factory method for creating configurations
        /// </summary>
        /// <returns>A configuration for use with IronClient</returns>
        public abstract Configuration GetConfiguartion();
    }
}