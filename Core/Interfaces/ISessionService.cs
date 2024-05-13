using Core.Models.Session;

namespace Core.Services;

public interface ISessionService
{
    List<Session> GetAll();
    Session? GetById(Guid id);
    Session Update(Session session);
    Session Create(Session session);
    bool Delete(Session session);
    bool Delete(int id);
}