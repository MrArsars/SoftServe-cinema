using Core.Models.Ticket;

namespace Core.Interfaces;

public interface ITicketService
{
    List<Ticket> GetAll();
    Ticket? GetById(Guid id);
    List<Ticket>? GetAllBySessionId(Guid sessionId);
    List<Guid>? GetAllIdsBySessionId(Guid sessionId);
    Ticket Update(Ticket ticket);
    Ticket Create(Ticket ticket);
    bool Create(List<TicketDto> ticketDtos);
    bool Delete(Ticket ticket);
    bool Delete(int id);
}