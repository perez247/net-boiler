using System;
using System.Collections.Generic;

namespace Application.Infrastructure.Token
{
    /// <summary>
    /// Token Data
    /// </summary>
    public class UserTokenData
    {
        /// <summary>
        /// Id of the user that signed in
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Roles of the signed in user
        /// </summary>
        /// <value></value>
        public IEnumerable<string> Roles { get; set; }

    }
}