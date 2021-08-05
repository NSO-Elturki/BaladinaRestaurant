using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApiProject.Services
{
    public class DrinksRepository : BaseRepository<Drinks>, IDrinksRepository
    {

        public DrinksRepository(RestaurantProjectContext restaurantProjectContext)
            : base(restaurantProjectContext)
        {
        }

        /// <summary>
        /// Get specfic type of drink
        /// </summary>
        /// <param name="type">
        /// The wanted type of drink
        /// </param>
        /// <returns>
        /// All drinks that have the same wanted type
        /// </returns>
        public List<Drinks> getDrinksByType(string type)
        {
            try
            {
                List<Drinks> listOfDrinks = base.context.Drinks.Where(s => s.DrinkType == type).ToList();

                if (listOfDrinks == null)
                {
                    return null;
                }

                return listOfDrinks;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
    }
}
