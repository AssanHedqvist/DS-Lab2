using Microsoft.EntityFrameworkCore;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;

namespace ProjectApp.Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntityDb {
    
    public readonly AuctionDbContext _context;
    private readonly DbSet<T> _entities;
    
    public GenericRepository(AuctionDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    
    public void Add(T entity)
    {
        _entities.Add(entity);
        _context.SaveChanges();
    }

    public void Remove(T entity)
    {
        _entities.Remove(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
        _context.SaveChanges();
    }

    public List<T> GetAll()
    {
        return _entities.ToList();
    }

    public T GetById(int id)
    {
       var entity = _context.Find<T>(id);
       return entity;
    }
}