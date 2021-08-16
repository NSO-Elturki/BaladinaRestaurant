using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;

namespace RestaurantApiProject.Repositories
{
    public class OrderDrinksRepository : BaseOrderRepository<OrdersDrinks>, IOrderDrinkRepository
    {
        public OrderDrinksRepository(RestaurantProjectContext restaurantProjectContext)
           : base(restaurantProjectContext)
        {
        }

    }
}
