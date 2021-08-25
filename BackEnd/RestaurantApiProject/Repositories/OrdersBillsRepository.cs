using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Repositories
{
    public class OrdersBillsRepository : IOrderBillRepository
    {
        private RestaurantProjectContext context;

        public OrdersBillsRepository(RestaurantProjectContext restaurantProjectContext)
        {
            this.context = restaurantProjectContext;
        }

        public async Task create(OrdersBills obj)
        {
             context.OrdersBills.Add(obj);
            await context.SaveChangesAsync();
        }

        public Task<bool> edit(OrdersBills obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrdersBills>> getAll()
        {
            try
            {
                return await context.OrdersBills.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<OrdersBills> getById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetTotalOrders()
        {
            try
            {
                return context.OrdersBills.Count();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public bool isExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> removeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

