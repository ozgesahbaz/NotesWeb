using System.Collections.Generic;
using WebApplication_Notes.Entities;

namespace WebApplication_Notes.DataAccess
{
    public interface IRepository<TEntity>
    {
        int Delete(int id);
        List<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Insert(TEntity entity);
        TEntity Update(int id, TEntity entity);
    }
}
