using AuthAPI.Services.IServices;
using Google.Protobuf.WellKnownTypes;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AuthAPI.Services.SendEmailService
{
    public class FropsiEmailSender : IEmailSenderService
    {

        private IConfiguration _configuration;

        public FropsiEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

           string host = _configuration.GetSection("SendEmailSettings:Host").Value!;
           string username = _configuration.GetSection("SendEmailSettings:Username").Value!;
           string password = _configuration.GetSection("SendEmailSettings:Password").Value!;

            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient(host);

            try
            {
                mail.From = new MailAddress(username);
                mail.To.Add(mailAddressTo);
                mail.Subject = subject;
                mail.Body = body;
                smtpServer.Credentials = new NetworkCredential(username, password);

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
