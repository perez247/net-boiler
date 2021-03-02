using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Entities.Core;
using MimeKit;

namespace Infrastructure.Implementation.Email
{
    /// <summary>
    /// Sending a mail json properties
    /// </summary>
    public class EmailObjects
    {

        /// <summary>
        /// Flag to indicate that the message can be send after configuratuin
        /// </summary>
        /// <value></value>
        public bool CanSend { get; set; }

        /// <summary>
        /// Sender email
        /// </summary>
        /// <value></value>
        public string Sender { get; set; }

        /// <summary>
        /// Password of the sender
        /// </summary>
        /// <value></value>
        public string Password { get; set; }

        /// <summary>
        /// Smtp server
        /// </summary>
        /// <value></value>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Port for sending the mail
        /// </summary>
        /// <value></value>
        public int Port { get; set; }

        /// <summary>
        /// List of people to send to
        /// </summary>
        /// <value></value>
        public ICollection<User> To { get; set; }

        /// <summary>
        /// Subject of the mail
        /// </summary>
        /// <value></value>
        public string Subject { get; set; }

        /// <summary>
        /// COntent of the message
        /// </summary>
        /// <value></value>
        public string Content { get; set; }

        /// <summary>
        /// Mime message
        /// </summary>
        /// <value></value>
        public MimeMessage MimeMessage { get; set; }

        /// <summary>
        /// COnstrcutor
        /// </summary>
        public EmailObjects()
        {
            To = new HashSet<User>();
            CanSend = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VerifyEmailObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }


}