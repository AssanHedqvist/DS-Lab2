using ProjectApp.Persistence;

namespace ProjectApp.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntityDb
{
    public void Add(T entity);
    public void Remove(T entity);
    public void Update(T entity);
    public List<T> GetAll();
    public T GetById(int id);
}