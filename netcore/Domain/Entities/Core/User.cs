using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Core
{
    /// <summary>
    /// User Of the application
    /// </summary>
    public class User: IdentityUser<Guid>
    {

        /// <summary>
        /// Details about this user
        /// </summary>
        /// <value></value>
        public UserDetails UserDetails { get; set; }

        /// <summary>
        /// If activated or not by the staff of ECO
        /// </summary>
        /// <value></value>
        public DateTime? DeactivatedUntil { get; set; }

        /// <summary>
        /// Accept terms and conditions
        /// Must accept condition else cannot use platform
        /// </summary>
        /// <value></value>
        public bool AgreeToTermsAndCondition { get; set; }

        /// <summary>
        /// Return Full Name of the User
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            if (UserDetails == null)
                return "";

            var other = string.IsNullOrEmpty(UserDetails.OtherName) ? "" : UserDetails.OtherName;
            var name = $"{UserDetails.LastName} {UserDetails.FirstName} {other}".Trim();

            return name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public User()
        {
            AgreeToTermsAndCondition = true;
            DeactivatedUntil = null;
        }

    }
}