using RestaurantApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApiProject.Contracts
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        string login(string email, string password);
        string generateLoginToken();
    }
}

