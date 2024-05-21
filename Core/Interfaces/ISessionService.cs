using Core.Models.Session;

namespace Core.Services;

public interface ISessionService
{
    List<Session> GetAll();
    Session? GetById(Guid id);
    Session Update(Session session);
    SessionDto Update(SessionDto session);
    Session Create(Session session);
    SessionDto Create(SessionDto sessionDto);
    bool Delete(Session session);
    bool DeleteById(Guid id);
    bool DeleteByMovieId(Guid movieId);
    bool Delete(int id);
}