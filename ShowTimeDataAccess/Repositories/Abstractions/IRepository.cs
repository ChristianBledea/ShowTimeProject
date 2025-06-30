using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Repositories.Abstractions
{
    
public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> Delete(int id);
        Task<IEnumerable<TEntity>> GetAll(); 
        Task<TEntity> GetById(int id);
    }
}
