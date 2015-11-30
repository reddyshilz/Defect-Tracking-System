using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace DefectTracker.DataSource
{
    /// <summary>
    /// Hold methods to send mail-Prabasini
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// Method to setup smtp server & send mail to given user
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="messgeBody"></param>
        public static void SendMail(string toEmail, string messgeBody)
        {
            string fromAddress = ConfigurationManager.AppSettings["fromEmail"].ToString(); //"pvamudtsinfo@gmail.com";
            string mailPassword = ConfigurationManager.AppSettings["smtpPassword"].ToString();// "pvamudts";   
            try
            {
                // Create smtp connection.
                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(ConfigurationManager.AppSettings["outgoingPort"]); ;//outgoing port for the mail.
                client.Host = ConfigurationManager.AppSettings["gmailSMTP"].ToString();//"smtp.gmail.com";
                client.EnableSsl = true;                
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);
                
                // Fill the mail form.
                var send_mail = new MailMessage();

                send_mail.IsBodyHtml = true;
                //address from where mail will be sent.
                send_mail.From = new MailAddress(fromAddress);
                //address to which mail will be sent.           
                send_mail.To.Add(new MailAddress(toEmail));
                //subject of the mail.
                send_mail.Subject = "Defect Tracking System";

                send_mail.Body = messgeBody;
                client.Send(send_mail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
   
}