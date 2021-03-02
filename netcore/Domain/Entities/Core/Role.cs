using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Core
{
    /// <summary>
    /// Roles class of the application
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
        /// <summary>
        /// List of roles of the application
        /// </summary>
        /// <value></value>
        public ICollection<UserRole> UserRoles { get; private set; }

        /// <summary>
        /// COnstructor
        /// </summary>
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
    }

    /// <summary>
    /// Roles of users in this Application
    /// </summary>
    public enum AppRoles {
        /// Basic
        user,

        /// Basic staff
        admin,

        /// Super staff
        superAdmin,
    }
}