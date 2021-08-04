
using RestaurantApiProject.Models;
using System.Collections.Generic;

namespace RestaurantApiProject.Contracts
{
    public interface IFoodRepository: IBaseRepository<Food>
    {
        List<Food> getFoodByType(string type);
    }
}
