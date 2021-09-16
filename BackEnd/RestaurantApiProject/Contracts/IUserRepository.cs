using RestaurantApiProject.Models;

namespace RestaurantApiProject.Contracts
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        TokenClass login(string email, string password);
        string generateLoginToken();
    }
}

