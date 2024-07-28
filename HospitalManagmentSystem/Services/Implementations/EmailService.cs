using HospitalManagmentSystem.Services.Interfaces;
using System.Net.Mail;
using System.Text;

namespace HospitalManagmentSystem.Services.Implementations
{
    internal class EmailService : IMessageService
    {
        public void Send(string toAddress, string contents)
        {
            // Copied from https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=net-7.0#remarks

            SmtpClient client = new SmtpClient("aspmx.l.google.com");//smtp.gmail.com");
            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("oliver.warrick2@gmail.com",
               "Jane " + (char)0xD8 + " Clayton",
            Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(toAddress);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test email message sent by an application. ";
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "test message 1" + someArrows;
            message.SubjectEncoding = Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            var mailSent = false;
            client.SendCompleted += new SendCompletedEventHandler((_, _) =>
            {
                mailSent = true;
                Console.WriteLine("finished sending email");
            });
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";
            client.SendAsync(message, userState);
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            string answer = Console.ReadLine();
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            if (answer.StartsWith("c") && mailSent == false)
            {
                client.SendAsyncCancel();
            }
            // Clean up.
            message.Dispose();
            Console.WriteLine("Goodbye.");
        }
    }
}
