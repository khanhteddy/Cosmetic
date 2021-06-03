using Microsoft.Extensions.Configuration;
using System;

namespace EC.SecurityService
{
    /// <summary>
    /// Options for the service 2f.
    /// </summary>
    public class SecurityServiceOptions
    {
        /// <summary>
        /// Creates a new default instance of the options.
        /// </summary>
        public SecurityServiceOptions()
        {

        }

        internal SecurityServiceOptions(IConfiguration config)
            : this()
        {

        }

        /// <summary>
        /// The cache identifier of the service 2f (can be any string).
        /// Change this property to force the service 2f to reload in browsers.
        /// </summary>
        public string ServiceTokenKey { get; set; }   


    }
}