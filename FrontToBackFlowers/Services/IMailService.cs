using FrontToBackFlowers.Data;

namespace FrontToBackFlowers.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(RequestEmail requestEmail);
    }
}
