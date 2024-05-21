using Core.Models.Ticket;

namespace Core.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, List<Ticket> tickets);
}