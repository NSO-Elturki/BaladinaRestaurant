using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;

namespace RestaurantApiProject.Repositories
{
    public class OrderDrinksRepository : BaseRepository<OrdersDrinks>, IOrderDrinkRepository
    {
        public OrderDrinksRepository(RestaurantProjectContext restaurantProjectContext)
            : base(restaurantProjectContext)
        {
        }
    }
}
