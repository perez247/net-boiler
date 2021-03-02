using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.Implementation.Email
{
    /// <summary>
    /// EmailService implementation of IEmailService
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Constructor for EmailService
        /// </summary>
        public EmailService()
        {
            _hostname = Environment.GetEnvironmentVariable("HOSTNAME");
            _emailObjects = EmailFunctions.GenerateEmailObject();

        }

        private readonly string _hostname;
        private SmtpClient smtpClient;
        private EmailObjects _emailObjects;

        private readonly string _templatePath = "../Infrastructure/Implementation/Email/Templates";

        // ------------------------------------------------

        /// <summary>
        /// Send Email verification link to use and verify email 
        /// </summary>
        /// <param name="verifyEmailData"></param>
        /// <returns></returns>
        public async Task SendVerifyEmailAsync(EmailData verifyEmailData)
        {

            string token = WebUtility.UrlEncode(verifyEmailData.Token);
            string id = WebUtility.UrlEncode(verifyEmailData.User.Id.ToString());

            _emailObjects.To.Add(verifyEmailData.User);
            _emailObjects.Subject = "Verify Email";
            var url = $"{_hostname}/public/verify-email?token={token}&userId={id}";

            using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/VerifyEmail.html"))
            {
                var content = SourceReader.ReadToEnd();
                content = content.Replace("{1}", url);
                _emailObjects.Content = content;

                EmailFunctions.CreateMimeMessage(_emailObjects);

                await EmailFunctions.SendData(_emailObjects);
            }
        }

        /// <summary>
        /// Send a link for user to use and change password
        /// </summary>
        /// <param name="verifyEmailData"></param>
        /// <returns></returns>
        public async Task SendForgotPasswordEmailAsync(EmailData verifyEmailData)
        {

            string token = WebUtility.UrlEncode(verifyEmailData.Token);
            string id = WebUtility.UrlEncode(verifyEmailData.User.Id.ToString());

            _emailObjects.To.Add(verifyEmailData.User);
            _emailObjects.Subject = "Reset Password";
            var url = $"{_hostname}/public/reset-password?token={token}&userId={id}";

            using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/ForgotPassword.html"))
            {
                var content = SourceReader.ReadToEnd();
                content = content.Replace("{1}", url);
                _emailObjects.Content = content;

                EmailFunctions.CreateMimeMessage(_emailObjects);

                await EmailFunctions.SendData(_emailObjects);
            }

        }

        /// <summary>
        /// Send a notification to the user of newly changed password..
        /// </summary>
        /// <param name="VerifyEmailData"></param>
        /// <returns></returns>
        public async Task SendChangePasswordNotificationAsync(EmailData VerifyEmailData)
        {
            string token = WebUtility.UrlEncode(VerifyEmailData.Token);
            string id = WebUtility.UrlEncode(VerifyEmailData.User.Id.ToString());

            _emailObjects.To.Add(VerifyEmailData.User);
            _emailObjects.Subject = "Password Changed";

            using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/ConfirmPasswordChanged.html"))
            {
                var content = SourceReader.ReadToEnd();
                _emailObjects.Content = content;

                EmailFunctions.CreateMimeMessage(_emailObjects);

                await EmailFunctions.SendData(_emailObjects);
            }
        }

        // /// <summary>
        // /// Send a notification for rejection of post
        // /// </summary>
        // /// <param name="VerifyEmailData"></param>
        // /// <returns></returns>
        // public async Task PostRejected(EmailData VerifyEmailData)
        // {
        //     string token = WebUtility.UrlEncode(VerifyEmailData.Token);
        //     string id = WebUtility.UrlEncode(VerifyEmailData.User.Id.ToString());

        //     _emailObjects.To.Add(VerifyEmailData.User);
        //     _emailObjects.Subject = "Post Rejected";

        //     using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/PostRejected.html"))
        //     {
        //         var content = SourceReader.ReadToEnd();
        //         content = content.Replace("{1}", VerifyEmailData.Post.Title);
        //         content = content.Replace("{2}", VerifyEmailData.Post.Description);
        //         content = content.Replace("{3}", VerifyEmailData.Post.Statement);
        //         _emailObjects.Content = content;

        //         EmailFunctions.CreateMimeMessage(_emailObjects);

        //         await EmailFunctions.SendData(_emailObjects);
        //     }
        // }

        // /// <summary>
        // /// Send a notification for rejection of comment
        // /// </summary>
        // /// <param name="VerifyEmailData"></param>
        // /// <returns></returns>
        // public async Task CommentRejected(EmailData VerifyEmailData)
        // {
        //     string token = WebUtility.UrlEncode(VerifyEmailData.Token);
        //     string id = WebUtility.UrlEncode(VerifyEmailData.User.Id.ToString());

        //     _emailObjects.To.Add(VerifyEmailData.User);
        //     _emailObjects.Subject = "Comment Rejected";

        //     using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/CommentRejected.html"))
        //     {
        //         var content = SourceReader.ReadToEnd();
        //         content = content.Replace("{1}", VerifyEmailData.Comment.Message);
        //         content = content.Replace("{2}", VerifyEmailData.Comment.Statement);
        //         _emailObjects.Content = content;

        //         EmailFunctions.CreateMimeMessage(_emailObjects);

        //         await EmailFunctions.SendData(_emailObjects);
        //     }
        // }

        /// <summary>
        /// Send a notification for rejection of comment
        /// </summary>
        /// <param name="VerifyEmailData"></param>
        /// <returns></returns>
        public async Task UserDeactivated(EmailData VerifyEmailData)
        {
            string token = WebUtility.UrlEncode(VerifyEmailData.Token);
            string id = WebUtility.UrlEncode(VerifyEmailData.User.Id.ToString());

            _emailObjects.To.Add(VerifyEmailData.User);
            _emailObjects.Subject = "Account Deactivated";

            using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/AccountDeactivation.html"))
            {
                var content = SourceReader.ReadToEnd();
                // content = content.Replace("{1}", VerifyEmailData.User.Statement);
                content = content.Replace("{2}", VerifyEmailData.User.LockoutEnd.Value.LocalDateTime.ToShortDateString());
                _emailObjects.Content = content;

                EmailFunctions.CreateMimeMessage(_emailObjects);

                await EmailFunctions.SendData(_emailObjects);
            }
        }


        // /// <summary>
        // /// Send a notification to the new newly created staff with password
        // /// </summary>
        // /// <param name="VerifyEmailData"></param>
        // /// <returns></returns>
        // public async Task WelcomeStaff(NewStaffData VerifyEmailData)
        // {
        //     _emailObjects.To.Add(VerifyEmailData.Staff);
        //     _emailObjects.Subject = "Welcome";

        //     using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/WelcomeStaff.html"))
        //     {
        //         var content = SourceReader.ReadToEnd();
        //         content = content.Replace("{1}", VerifyEmailData.Staff.Email);
        //         content = content.Replace("{2}", VerifyEmailData.Password);
        //         content = content.Replace("{3}", _hostname);
        //         _emailObjects.Content = content;

        //         EmailFunctions.CreateMimeMessage(_emailObjects);

        //         await EmailFunctions.SendData(_emailObjects);
        //     }
        // }



    }
}


