using AuthAPI.Services.IServices;
using System.Net;
using System.Net.Mail;

namespace AuthAPI.Services.SendEmailService
{
    public class FropsiEmailSender : IEmailSenderService
    {
        public void sendMailWithFropsiEmailServer(string mailAddressTo, string subject, string body)
        {
            /*IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

           string host = configuration.GetSection("SendEmailSettings:Host").ToString()!;
           string password = configuration.GetSection("SendEmailSettings:Password").ToString()!;*/

            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient("smtp.forpsi.com");

            mail.From = new MailAddress("postmaster@vitya-dev.hu");
            mail.To.Add(mailAddressTo);
            mail.Subject = subject;
            mail.Body = body;
            smtpServer.Credentials = new NetworkCredential("postmaster@vitya-dev.hu", "dk-Xn2Qurq");

            smtpServer.Port = 587;
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);

        }
    }
}
