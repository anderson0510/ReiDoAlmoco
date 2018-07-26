using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ReiDoAlmoco.RegrasDeNegocio.Utils
{
    public class EnviarEmail
    {
        private SmtpClient client;

        public EnviarEmail()
        {
            client = new SmtpClient("hostname");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("username", "password");
        }

        public void Enviar(MailMessage message)
        {
            message.From = new MailAddress("remetente");            
            client.Send(message);
        }
    }
}
