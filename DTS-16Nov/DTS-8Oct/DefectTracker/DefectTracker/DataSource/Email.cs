using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace DefectTracker.DataSource
{
    public static class Email
    {
        public static void SendMail(string toEmail, string messgeBody)
        {
            string fromAddress = "pvamudtsinfo@gmail.com";
            string mailPassword = "pvamudts";       // Mail id password from where mail will be sent.
            
            // Create smtp connection.
            SmtpClient client = new SmtpClient();
            client.Port = 587;//outgoing port for the mail.
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);


            // Fill the mail form.
            var send_mail = new MailMessage();

            send_mail.IsBodyHtml = true;
            //address from where mail will be sent.
            send_mail.From = new MailAddress("pvamudtsinfo@gmail.com");
            //address to which mail will be sent.           
            send_mail.To.Add(new MailAddress(toEmail));
            //subject of the mail.
            send_mail.Subject = "Defect Tracking System";

            send_mail.Body = messgeBody;
            client.Send(send_mail);
        }
    }
   
}