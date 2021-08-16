using RestaurantApiProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantApiProject.Contracts
{
    public interface IOrderFoodRepository: IBaseOrderRepository<OrdersFood>
    {
    }
}
