using System;

namespace Asmt.Main.Common;

/// <summary>
/// Options for the controller.
/// TODO: Make this to use the appsettings.json file with Options pattern.
/// </summary>
public static class AsmtOpts
{
    /// <summary>
    /// Options for the authentication.
    /// </summary>
    public class Authentication 
    {
        /// <summary>
        /// The secret key for the authentication.
        /// </summary>
        public static readonly string Secret = "FBDF8E5A62DB963A188D742C246B8";

        /// <summary>
        /// The issuer for the authentication.
        /// </summary>
        public static readonly string Issuer = "asmt-issuer";

        /// <summary>
        /// The audience for the authentication.
        /// </summary>
        public static readonly string Audience = "asmt-audience";
    }

    /// <summary>
    /// Options for the cache.
    /// </summary>  
    public class Cache 
    {
        /// <summary>
        /// The absolute duration of the cache.
        /// </summary>
        public static readonly TimeSpan AbsoluteDuration = TimeSpan.FromHours(1);

        /// <summary>
        /// The sliding duration of the cache.
        /// </summary>
        public static readonly TimeSpan SlidingDuration = TimeSpan.FromMinutes(30);
    }
}
