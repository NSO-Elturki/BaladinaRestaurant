using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Repositories
{
    public class BaseOrderRepository<T> : IBaseOrderRepository<T> where T : class, IEntity
    {

        private RestaurantProjectContext context;

        public BaseOrderRepository(RestaurantProjectContext context)
        {
            this.context = context;
        }
        // public async Task create(T order)
        public virtual async Task create(List<T> orders)
        {
            try
            {
                // context.Set<T>().Add(order);
                context.Set<T>().AddRange(orders);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<T>> getAll()
        {
            try
            {
                return await context.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> removeById(int id)
        {
            try
            {
                var getObject = await context.Set<T>().FindAsync(id);
                if (getObject == null)
                {
                    return false;
                }

                context.Set<T>().Remove(getObject);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public bool isExists(int id)
        {
            return context.Set<T>().Any(e => e.Id == id);
        }
    }
}
