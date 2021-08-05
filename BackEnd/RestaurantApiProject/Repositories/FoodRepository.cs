
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApiProject.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {

        public FoodRepository(RestaurantProjectContext restaurantProjectContext)
            :base(restaurantProjectContext)
        {
        }

        /// <summary>
        /// Get specfic type of food
        /// </summary>
        /// <param name="type">
        /// The wanted type of food
        /// </param>
        /// <returns>
        /// All food that have the same wanted type
        /// </returns>
        public List<Food> getFoodByType(string type)
        {
            try
            {
                List<Food> listOfFood = base.context.Food.Where(s => s.FoodType == type).ToList();

                if (listOfFood == null)
                {
                    return null;
                }

                return listOfFood;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

    }
}
