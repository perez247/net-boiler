using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using MimeKit;
namespace Infrastructure.Implementation.Email
{
    /// <summary>
    /// Email functions
    /// </summary>
    public static class EmailFunctions
    {

        // public static SendGridMessage GenerateMsg(string from, string fromName, string templateId, string emailAddress) {

        //     var msg = new SendGridMessage();

        //     msg.SetFrom(new EmailAddress(from, fromName));
        //     msg.AddTo(new EmailAddress(
        //         emailAddress
        //     ));

        //     msg.SetTemplateId(templateId);

        //     return msg;
        // }

        /// <summary>
        /// Set information into the template
        /// </summary>
        /// <param name="verifyEmailData"></param>
        /// <param name="_hostname"></param>
        /// <returns></returns>
        public static VerifyEmailObject GenerateJsonVariables(EmailData verifyEmailData, string _hostname)
        {

            string token = WebUtility.UrlEncode(verifyEmailData.Token);
            string id = WebUtility.UrlEncode(verifyEmailData.User.Id.ToString());

            return new VerifyEmailObject()
            {
                FirstName = verifyEmailData.User.GetFullName(),
                Url = $"{_hostname}/public/verify-email?token={token}&userId={id}"
            };


        }

        /// <summary>
        /// Generate the neccessary information from the environment variable
        /// </summary>
        /// <returns></returns>
        public static EmailObjects GenerateEmailObject()
        {
            var envAsString = Environment.GetEnvironmentVariable("EMAIL_SERVER");

            if (envAsString == null)
                return new EmailObjects() { CanSend = false };

            // throw new CustomMessageException(envAsString);

            var envAsArray = envAsString.Split('|');

            // throw new CustomMessageException(string.Join(",", envAsArray));


            return new EmailObjects()
            {
                Sender = envAsArray[0],
                Password = envAsArray[1],
                SmtpServer = envAsArray[2],
                Port = Int32.Parse(envAsArray[3])
            };

        }

        /// <summary>
        /// Generate the neccessary information from the environment variable
        /// </summary>
        /// <returns></returns>
        public static void CreateMimeMessage(EmailObjects emailObjects)
        {

            var mime = new MimeMessage();

            if (!emailObjects.CanSend) { return; }

            mime.From.Add(new MailboxAddress("fortheECO", emailObjects.Sender));
            mime.Sender = new MailboxAddress("fortheECO", emailObjects.Sender);
            mime.To.AddRange(emailObjects.To.Select(x => new MailboxAddress(x.GetFullName(), x.Email)));

            mime.Subject = emailObjects.Subject;
            mime.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { Text = emailObjects.Content };

            emailObjects.MimeMessage = mime;

        }

        /// <summary>
        /// Method for calling third party application to send mail
        /// </summary>
        /// <param name="_emailObjects"></param>
        /// <returns></returns>
        public static async Task SendData(EmailObjects _emailObjects)
        {
            if (!_emailObjects.CanSend)
                throw new CustomMessageException(_emailObjects.Content);

            // await Task.Delay(100);
            // SmtpMail oMail = new SmtpMail("Try It");

            // oMail.From = _emailObjects.Sender;
            // oMail.To = _emailObjects.To.Select(x => x.Email).FirstOrDefault();
            // oMail.Subject = _emailObjects.Subject;
            // oMail.HtmlBody = _emailObjects.Content;

            // SmtpServer oServer = new SmtpServer("");

            // EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            // oSmtp.SendMail(oServer, oMail);


            using (MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                // await Task.Delay(100);
                // smtpClient.LocalDomain = Environment.GetEnvironmentVariable("HOSTNAME");
                await smtpClient.ConnectAsync(_emailObjects.SmtpServer, 465, true);
                await smtpClient.AuthenticateAsync(_emailObjects.Sender, _emailObjects.Password);
                await smtpClient.SendAsync(_emailObjects.MimeMessage);
                await smtpClient.DisconnectAsync(true);

            }
        }
    }
}