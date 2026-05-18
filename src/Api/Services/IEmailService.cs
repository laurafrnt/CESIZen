namespace Api.Services
{
    public interface IEmailService
    {
        Task SendConfirmationMail(string receiverMail, string username, string token);
        Task SendMailUsed(string receiverMail, string username);
    }
}
