using Microsoft.IdentityModel.Tokens;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models; 
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;
using BC = BCrypt.Net.BCrypt;


namespace RestaurantApiProject.Services
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public IConfiguration configuration;

        public UserRepository(RestaurantProjectContext context, IConfiguration configuration)
           : base(context)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Create the token that is mandatory to use in the headers of some http requests
        /// </summary>
        /// <returns>
        /// token string
        /// </returns>
        public string generateLoginToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       /// <summary>
       /// Get a info of specfic user by email in the database
       /// </summary>
       /// <param name="email">
       /// The email of the user which help database to get the user
       /// </param>
       /// <returns>
       /// The wanted user if the user exsist in the database or return null if the user is not exsist
       /// </returns>
        public Users getUser(string email)
        {
            try
            {
                Users user = null;
                user = context.Users.SingleOrDefault(s => s.Email == email);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Help to know if the insert email and password of a user is exsist in the database. 
        /// </summary>
        /// <param name="email">
        /// The email of the user
        /// </param>
        /// <param name="password">
        /// The password of the user
        /// </param>
        /// <returns>
        /// In case the user email and password is correct then the method will return the token string 
        /// that user need for some features in the application. In case the user email or password are wrong 
        /// then the method will return null
        /// </returns>
        public virtual string login(string email, string password)
        {
            try
            {
                    var loginUser = getUser(email);

                    if (loginUser != null && BC.Verify(password, loginUser.Password))
                    {
                        return this.generateLoginToken();
                    }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
