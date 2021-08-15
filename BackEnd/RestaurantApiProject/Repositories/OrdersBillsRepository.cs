using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Repositories
{
    public class OrdersBillsRepository : BaseRepository<OrdersBills>, IOrderBillRepository
    {
        public OrdersBillsRepository(RestaurantProjectContext restaurantProjectContext)
           : base(restaurantProjectContext)
        {
        }
    }
}
