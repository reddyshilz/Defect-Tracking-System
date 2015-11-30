using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using DefectTracker.Notification;


namespace DefectTracker.SMSUtility
{
    public class Sms : INotifyEngine
    {
        public void SendNotification(Message message)
        {
            var fromAddress = ConfigurationManager.AppSettings["fromEmail"].ToString(); //"pvamudtsinfo@gmail.com";
            var mailPassword = ConfigurationManager.AppSettings["smtpPassword"].ToString(); // "pvamudts";   
            // Create smtp connection.
            var client = new SmtpClient { Port = int.Parse(ConfigurationManager.AppSettings["outgoingPort"]) };
            ; //outgoing port for the mail.
            client.Host = ConfigurationManager.AppSettings["gmailSMTP"].ToString(); //"smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);

            // Fill the mail form.
            var sendMail = new MailMessage { IsBodyHtml = true, From = new MailAddress(fromAddress) };

            //address from where mail will be sent.
            //address to which mail will be sent.           
            sendMail.To.Add(new MailAddress(message.ToAddress));
            //subject of the mail.
            sendMail.Subject = message.Subject;

            sendMail.Body = message.Body;
            client.Send(sendMail);
        }
    }
}
