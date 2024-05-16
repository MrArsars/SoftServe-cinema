// using BusinessLogic.Interfaces;
// using Core.Models;
// using Core.Models.Session;
// using Core.Models.Ticket;
//
// namespace Core.Services;
//
// public class TicketService
// {
//     private readonly IRepository<TicketDto> _tickeRepository;
//     private readonly IRepository<Hall> _hallRepository;
//     private readonly IRepository<SessionDto> _sessionRepository;
//     private readonly IRepository<Place> _placesRepository;
//
//     public TicketService(IRepository<TicketDto> tickeRepository, IRepository<Hall> hallRepository, IRepository<SessionDto> sessionRepository, IRepository<Place> placesRepository)
//     {
//         _tickeRepository = tickeRepository;
//         _hallRepository = hallRepository;
//         _sessionRepository = sessionRepository;
//         _placesRepository = placesRepository;
//     }
//     
//     public List<Ticket> GetAll()
//     {
//         var res = _tickeRepository.GetAll().ToList();
//         return res;
//     }
//
//     public Ticket? GetById(Guid id)
//     {
//         var res = _tickeRepository.GetById(id);
//         return res;
//     }
//
//     public List<Ticket>? GetAllByUser(Guid userId)
//     {
//         var res = _tickeRepository.GetAll().Where(x => x.UserId.Equals(userId)).ToList();
//         return res;
//     }
//
//     public List<Ticket>? GetAllBySessionId(Guid sessionId)
//     {
//         var res = _tickeRepository.GetAll().Where(x => x.SessionId.Equals(sessionId)).ToList();
//         return res;
//     }
//
//     public Ticket Update(Ticket actor)
//     {
//         try
//         {
//             _tickeRepository.Update(actor);
//             _tickeRepository.Save();
//             return actor;
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public Ticket Create(Ticket ticket)
//     {
//         try
//         {   
//             _tickeRepository.Insert(ticket);
//             _tickeRepository.Save();
//             return ticket;
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public bool Delete(Ticket ticket)
//     {
//         try
//         {
//             _tickeRepository.Delete(ticket);
//             _tickeRepository.Save();
//             return true;
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//     
//     public bool Delete(int id)
//     {
//         try
//         {
//             _tickeRepository.Delete(id);
//             _tickeRepository.Save();
//             return true;
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
// }