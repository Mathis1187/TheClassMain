using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TheClassMain.Service
{
    class EmailOutlook
    {
        public void EnvoyerAvecOutlook()
        {
            var message = new MailMessage();
            message.From = new MailAddress("tonadresse@outlook.com");
            message.To.Add("destinataire@example.com");
            message.Subject = "Test Outlook";
            message.Body = "Bonjour, ceci est un test via Outlook.";

            var smtp = new SmtpClient("smtp.office365.com", 587);
            smtp.Credentials = new NetworkCredential("tonadresse@outlook.com", "tonMotDePasse");
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
    }
}
