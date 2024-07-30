using HospitalManagmentSystem.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace HospitalManagmentSystem.Services.Implementations
{
    internal class EmailService : IMessageSender
    {
        public void Send(string toAddress, string subject, string contents)
        {
            string fromEmail = "oliver.warrick2@gmail.com";
            string password = Constants.EmailPassword;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = contents,
                IsBodyHtml = false
            };
            mailMessage.To.Add(toAddress);

            client.Send(mailMessage);
        }
    }
}
