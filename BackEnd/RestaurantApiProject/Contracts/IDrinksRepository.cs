using RestaurantApiProject.Models;
using System.Collections.Generic;

namespace RestaurantApiProject.Contracts
{
    public interface IDrinksRepository: IBaseRepository<Drinks>
    { 
        List<Drinks> getDrinksByType(string type);
    }
}
