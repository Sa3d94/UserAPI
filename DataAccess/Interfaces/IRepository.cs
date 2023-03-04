using System;
namespace DataAccess.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    IQueryable<TEntity> Get();
    Task<TEntity> InsertAsync(TEntity entity);

  }

}

