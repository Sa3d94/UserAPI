using System;
using DataAccess.Interfaces;
using Entity.Models;

namespace DataAccess.Services
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

    protected readonly dbContext _context;

    public Repository(dbContext context)
    {
      _context = context;

    }

    public  IQueryable<TEntity> Get()
    {
      return  _context.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
      {
      if (entity == null)
      {
        throw new ArgumentNullException($"{nameof(InsertAsync)} Data Shouldn't Be Null");
      }

      try
      {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
      }
      catch 
      {
        throw;
      }
    }
    }

}

