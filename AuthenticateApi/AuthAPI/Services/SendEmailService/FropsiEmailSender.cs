using AuthAPI.Services.IServices;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AuthAPI.Services.SendEmailService
{
    public class FropsiEmailSender : IEmailSenderService
    {

        public bool isValidEmail(string email)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");

            if(validateEmailRegex.IsMatch(email))
            {
                Console.WriteLine("valid");
                return true;
            }
            return false;
        }

        public bool sendMailWithFropsiEmailServer(string mailAddressTo, string subject, string body)
        {
            /*IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

           string host = configuration.GetSection("SendEmailSettings:Host").ToString()!;
           string password = configuration.GetSection("SendEmailSettings:Password").ToString()!;*/

            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient("smtp.forpsi.com");

            try
            {
                mail.From = new MailAddress("postmaster@vitya-dev.hu");
                mail.To.Add(mailAddressTo);
                mail.Subject = subject;
                mail.Body = body;
                smtpServer.Credentials = new NetworkCredential("postmaster@vitya-dev.hu", "dk-Xn2Qurq");

                smtpServer.Port = 587;
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }
    }
}
