namespace ECommerce.API.ECommerce.Application.Interfaces
{
    public interface IMailRepo
    {
        Task SendEmailAsync(string mailTo, string subject, string body);
    }
}
