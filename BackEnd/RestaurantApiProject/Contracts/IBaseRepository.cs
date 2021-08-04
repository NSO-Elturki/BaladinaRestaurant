using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Contracts
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task create(T obj);
        Task<IEnumerable<T>> getAll();
        // Task edit(T obj);
        Task<bool> edit(T obj);
         Task<bool> removeById(int id);
        Task<T> getById(int id);
        bool isExists(int id);





    }
}
