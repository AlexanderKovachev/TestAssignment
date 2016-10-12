using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAssignment.DAL
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> FindById(int id);

        Task<int> Insert(TEntity emp);

        Task<int> Update(TEntity emp);

        Task<int> Delete(int id);
    }
}
