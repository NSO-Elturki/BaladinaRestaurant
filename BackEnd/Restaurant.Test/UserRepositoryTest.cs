//using FakeItEasy;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using RestaurantApiProject.Models;
//using RestaurantApiProject.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Restaurant.Test
//{
//  public  class UserRepositoryTest
//    {
//        UserRepository sut;
//        RestaurantProjectContext context;
//        List<Users> listOfUsers;
//        IConfiguration configuration;

//        public UserRepositoryTest()
//        {
//            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
//           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//           .Options;

//            context = new RestaurantProjectContext(options);
//            context.Database.EnsureCreated();

//            listOfUsers = new List<Users>
//            {
//                new Users { Id = 1, FullName = "Wax Nuata", Email = "wax.nuata@outlook.com", Password = "wax123", PhoneNumber = 123456, UserType = 0, CreateDate = DateTime.Now },
//                new Users { Id = 2, FullName = "Alex Nuata", Email = "alex.nuata@outlook.com", Password = "alex123", PhoneNumber = 123456, UserType = 0, CreateDate = DateTime.Now }

//            };

//            context.Users.AddRange(listOfUsers);
//            context.SaveChanges();

//            var inMemorySettings = new Dictionary<string, string>
//            {
//              {"Key", "AhmedRestaurant2021EindhovenEgyptionFood94InCenterOfTheCity"},
//              {"Issuer", "NagiApi"},
//              {"Audience", "adminsOfRestaurantProject"},
//              {"Subject", "LoginApiTokenOfRestaurantProject"},


//            };

//             configuration = new ConfigurationBuilder()
//           .AddInMemoryCollection(inMemorySettings)
//           .Build();
//        }

//        [Fact]
//        public void Dispose()
//        {
//            context.Database.EnsureDeleted();
//            context.Dispose();
//        }

//        [Fact]
//        public void Constroctur_DbContextAndIConfigureShouldNotEqualToNull()
//        {
//            //Arrange
//            var fakeConfigure = A.Fake<IConfiguration>();
//            sut = new UserRepository(context, fakeConfigure);
//            //Act//Assert
//            Assert.NotNull(sut.context);
//            Assert.NotNull(sut.configuration);
//        }

//        [Fact]
//        public void generateLoginToken_shouldNotReturnNullString()
//        {
//            //Arrange
//          //  var fakeConfigure = A.Fake<IConfiguration>();

//            sut = new UserRepository(context, configuration);
//            //Act//Assert
//            Assert.NotNull(sut.generateLoginToken());
//        }

//        [Fact]
//        public void login_shouldNotReturnNullString()
//        {
//            //Arrange
//            //  var fakeConfigure = A.Fake<IConfiguration>();

//            sut = new UserRepository(context, configuration);
//            //Act//Assert
//            Assert.NotNull(sut.generateLoginToken());
//        }

//    }
//}
