namespace BusinessLogic.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByIds(List<Guid> guids);
        TEntity? GetById(Guid id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        void Delete(Guid id);
        void Delete(TEntity entity);
        void Save();
    }
}
