namespace AuthAPI.Services.IServices
{
    public interface IEmailSenderService
    {
        public void sendMailWithFropsiEmailServer(string mailAddressTo, string subject, string body);
        
    }
}
