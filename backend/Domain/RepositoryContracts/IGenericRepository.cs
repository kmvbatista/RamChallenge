using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IList<TEntity>> GetAll();
    Task<TEntity> GetById(Guid id);
    Task<TEntity> Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);
}
