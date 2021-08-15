using RestaurantApiProject.Models;

namespace RestaurantApiProject.Contracts
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        string login(string email, string password);
        string generateLoginToken();
    }
}

