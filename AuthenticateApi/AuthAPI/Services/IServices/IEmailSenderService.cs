namespace AuthAPI.Services.IServices
{
    public interface IEmailSenderService
    { 

        public bool sendMailWithFropsiEmailServer(string mailAddressTo, string subject, string body);

        public bool isValidEmail(string email);
        
    }
}
