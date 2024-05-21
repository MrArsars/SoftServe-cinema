using System.Net;
using System.Net.Mail;
using System.Text;
using Core.Models.Session;
using Core.Models.Ticket;

namespace Core.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string toEmail, List<Ticket> tickets)
    {
        var fromAddress = new MailAddress("arseniu437@gmail.com", "SoftCinema");
        var toAddress = new MailAddress(toEmail);
        const string fromPassword = "vhqp cmgq vvhl mpke";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        var emailBody = GenerateTickets(tickets);

        using (var message = new MailMessage(fromAddress, toAddress)
               {
                   Subject = "SoftCinema tickets",
                   Body = emailBody,
                   IsBodyHtml = true
               })
        {
            await smtp.SendMailAsync(message);
        }
    }

    private string GenerateTickets(List<Ticket> tickets)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"UTF-8\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("<title>Cinema Ticket</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; background-color: #f4f4f4; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }");
            sb.AppendLine(".ticket { background: #fff; border: 2px dashed #333; padding: 20px; width: 300px; text-align: center; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); margin-bottom: 20px; }");
            sb.AppendLine(".ticket h1 { margin: 0; font-size: 24px; }");
            sb.AppendLine(".ticket p { margin: 5px 0; }");
            sb.AppendLine(".ticket .film-name { font-size: 20px; font-weight: bold; }");
            sb.AppendLine(".ticket .details { margin-top: 10px; border-top: 1px dashed #333; padding-top: 10px; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            foreach (var ticket in tickets)
            {
                sb.AppendLine("<div class=\"ticket\">");
                sb.AppendLine("<h1>Cinema Ticket</h1>");
                sb.AppendFormat("<p class=\"film-name\">{0}</p>", ticket.Session.Movie.Name);
                sb.AppendLine("<div class=\"details\">");
                sb.AppendFormat("<p><strong>Day:</strong> {0}</p>", ticket.Session.DateTimeStart.ToString("dddd, MMMM dd, yyyy"));
                sb.AppendFormat("<p><strong>Time:</strong> {0}</p>", ticket.Session.DateTimeStart.ToString("hh:mm tt"));
                sb.AppendFormat("<p><strong>Hall:</strong> {0}</p>", ticket.Session.Hall.Name);
                sb.AppendFormat("<p><strong>Seat row:</strong> {0}</p>", ticket.Place.RowNumber);
                sb.AppendFormat("<p><strong>Seat number:</strong> {0}</p>", ticket.Place.SeatNumber);
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
}