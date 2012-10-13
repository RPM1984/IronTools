//-----------------------------------------------------------------------
// <copyright file="DefaultConfigurationFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    /// <summary>
    /// Default configuration factory
    /// </summary>
    internal class DefaultConfigurationFactory : ConfigurationFactory
    {
        /// <summary>
        /// Creates the most basic configuration for use with IronClient
        /// </summary>
        /// <returns>A configuration for use with IronClient</returns>
        public override Configuration GetConfiguartion()
        {
            return new Configuration()
            {
                Protocol = "https",
                Port = 443,
            };
        }
    }
}