using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System;
using System.Threading.Tasks;

namespace RestaurantApiProject.Repositories
{
    public class OrderFoodRepository : BaseRepository<OrdersFood>, IOrderFoodRepository
    {
        public OrderFoodRepository(RestaurantProjectContext restaurantProjectContext)
            : base(restaurantProjectContext)
        {
        }
    }
}

