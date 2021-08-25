using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Contracts
{
   public interface IBaseOrderRepository<T> where T : class, IEntity
    {
        Task create(List<T> order);
        Task<bool> removeById(int id);
        Task<IEnumerable<T>> getAll();
        bool isExists(int id);

    }
}
