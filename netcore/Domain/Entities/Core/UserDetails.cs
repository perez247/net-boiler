using System;

namespace Domain.Entities.Core
{
    /// <summary>
    /// Other details about the user
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// Id of the user detail
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// User this additional infor is connected to
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Id of the User it is connected to
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        /// <summary>
        /// About me field
        /// </summary>
        /// <value>string</value>
        public string About { get; set; }

        /// <summary>
        /// First name of the Individual or staff
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Individual or staff
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }

        /// <summary>
        /// Other name of the Individual or staff
        /// </summary>
        /// <value></value>
        public string OtherName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gender of the User
        /// m = Male, f = Female, anyother = other
        /// </summary>
        /// <value></value>
        public string Gender { get; set; }

        /// <summary>
        /// Date this user detail
        /// </summary>
        /// <value></value>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Date last the user detail was modified
        /// </summary>
        /// <value></value>
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public UserDetails()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}