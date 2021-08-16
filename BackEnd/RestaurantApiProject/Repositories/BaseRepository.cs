 using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Services
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {

        public RestaurantProjectContext context;

        public BaseRepository(RestaurantProjectContext context) 
        {
            this.context = context;
        }

        /// <summary>
        /// add new item to a specfic table in the database 
        /// </summary>
        /// <param name="obj">
        /// The item need to add to the database.
        /// </param>
        /// <returns></returns>
        public async Task create(T obj)
        {
            try
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Edit specfic item in database
        /// </summary>
        /// <param name="obj">
        /// The item need to edit to the database.
        /// </param>
        /// <returns>
        /// True if the specfic item in database is edit it, and return false in case the item is not edit it.
        /// </returns>
        public async Task<bool> edit(T obj)
        {
            try
            {
                if (context.Set<T>().SingleOrDefault(s => s.Id == obj.Id) == null) { return false; }
                context.Entry(obj).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get all items of specfic table in the database
        /// </summary>
        /// <returns>
        /// List of items of a specfic table in database
        /// </returns>
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

        /// <summary>
        /// Get specfic item from database based of item Id
        /// </summary>
        /// <param name="id">
        /// The Id of the wanted item
        /// </param>
        /// <returns>
        /// Item from a specfic table
        /// </returns>
        public async Task<T> getById(int id)
        {
            try
            {
                return await context.Set<T>().FindAsync(id);
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Remove item from a specfic table in database
        /// </summary>
        /// <param name="id">
        /// The Id of the  item that need to be delete it
        /// </param>
        /// <returns>
        /// True if the specfic item in database is removed, and return false in case the item is not remove it.
        /// </returns>
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

        /// <summary>
        /// Check if a specfic item is exsist in a specfic table
        /// </summary>
        /// <param name="id">
        /// The Id of wanted item
        /// </param>
        /// <returns>
        /// True if the item exsist in database and false if it not
        /// </returns>
        public bool isExists(int id)
        {
            return context.Set<T>().Any(e => e.Id == id);
        }
    }

}
