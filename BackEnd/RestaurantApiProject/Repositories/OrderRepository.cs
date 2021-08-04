using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Services
{
    public class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        public OrderRepository(RestaurantProjectContext restaurantProjectContext)
            : base(restaurantProjectContext)
        {
        }
    }
}
