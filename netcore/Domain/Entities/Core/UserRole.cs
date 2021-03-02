using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Core
{
    /// <summary>
    /// Connection between user and their role
    /// </summary>
    public class UserRole: IdentityUserRole<Guid>
    {
        /// <summary>
        /// User
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        /// <value></value>
        public Role Role { get; set; }
    }
}