using RestaurantApiProject.Models;
using System.Threading.Tasks;

namespace RestaurantApiProject.Contracts
{
  public  interface IOrderBillRepository: IBaseRepository<OrdersBills>
    {
        int GetTotalOrders();
    }
}
