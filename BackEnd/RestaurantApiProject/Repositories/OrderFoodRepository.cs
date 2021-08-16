using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantApiProject.Repositories
{
    public class OrderFoodRepository : BaseOrderRepository<OrdersFood>, IOrderFoodRepository
    {
        public OrderFoodRepository(RestaurantProjectContext restaurantProjectContext)
           : base(restaurantProjectContext)
        {
        }

    }
}

