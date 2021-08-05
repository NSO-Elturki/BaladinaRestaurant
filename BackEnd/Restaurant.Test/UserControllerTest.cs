using Microsoft.Extensions.Configuration;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Controllers;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

namespace Restaurant.Test
{
    public class UserControllerTest
    {
        UsersController sut;
        RestaurantProjectContext context;
        List<Users> listOfUsers;
        int ValidUserId = 1;

        public UserControllerTest() 
        {
            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            context = new RestaurantProjectContext(options);
            context.Database.EnsureCreated();

             listOfUsers = new List<Users>
            {
                new Users { Id = 1, FullName = "Wax Nuata", Email = "wax.nuata@outlook.com", Password = "wax123", PhoneNumber = 123456, UserType = 0, CreateDate = DateTime.Now },
                new Users { Id = 2, FullName = "Alex Nuata", Email = "alex.nuata@outlook.com", Password = "alex123", PhoneNumber = 123456, UserType = 0, CreateDate = DateTime.Now }
            };

            context.Users.AddRange(listOfUsers);
            context.SaveChanges();
        }

        [Fact]
        public void Dispose() 
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async Task GetAll_returnTheCorrectNumberOfUsersInDatabase()
        {
            //Arrange
            var fakeConfigure = A.Fake<IConfiguration>();
            var fakeRepo = A.Fake<UserRepository>(x => x.WithArgumentsForConstructor(() => new UserRepository(context, fakeConfigure)));
            sut = new UsersController(fakeRepo);
            //Act
            var response = await sut.GetAll();
            //Assert
            var result = response as OkObjectResult;
            var returnedListOfUsers = result.Value as IEnumerable<Users>;
            Assert.Equal(2, returnedListOfUsers.Count());
        }

        [Fact]
        public void loginUser_returnOkObjectResultInCaseTheUserLoginSuccesfully()
        {
            //Arrange
            var fakeConfigure = A.Fake<IConfiguration>();
            var fakeRepo = A.Fake<UserRepository>(x => x.WithArgumentsForConstructor(() => new UserRepository(context, fakeConfigure)));
            A.CallTo(() => fakeRepo.login(this.listOfUsers[0].Email, this.listOfUsers[0].Password)).Returns("bsjhbcscbskdcbsdkjcbksjdbc");
            sut = new UsersController(fakeRepo);
            //Act
            var response = sut.loginUser(listOfUsers[0]);
            //Assert
            Assert.IsType<OkObjectResult>(response);

        }

        [Theory]
        [InlineData(" ", "nagi123444")]
        [InlineData("mo.turki@gmail.com", " ")]
        [InlineData(" ", " ")]
        public void loginUser_returnBadRequestResultInCaseTheUserInsertInvaildInput(string email, string password)
        {
            //Arrange
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            var user = new Users() { Email = email, Password = password };
            //Act
            var response = sut.loginUser(user);
            //Assert
            Assert.IsType<BadRequestResult>(response);

        }

        [Theory]
        [InlineData("mark@gmail.com", "mark123444")]
        [InlineData("mo.turki@gmail.com", "1234567")]
        [InlineData("faroz@outlook.com", "faroz123")]
        public void loginUser_returnNotFoundResultInCaseTheUserEmailIsNotExsistInDatabase(string email, string password)
        {
            //Arrange
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            var user = new Users() { Email = email, Password = password };
            //Act
            var response = sut.loginUser(user);
            //Assert
            Assert.IsType<NotFoundResult>(response);

        }

        [Fact]
        public async Task Post_shouldAddNewUserToDatabase()
        {
            //Arrange
            var fakeConfigure = A.Fake<IConfiguration>();
            var user = new Users() { Email = "Omar.elturki@outlook.com", Password = "omar123" };
            var fakeRepo = A.Fake<UserRepository>(x => x.WithArgumentsForConstructor(() => new UserRepository(context, fakeConfigure)));
            sut = new UsersController(fakeRepo);
            //Act
            var response = await sut.Post(user);
            //Assert
            Assert.Equal(3,context.Users.Count());
        }

        [Theory]
        [InlineData("nagielturki", " ")]
        [InlineData(null, "nagi12345")]
        [InlineData("kiko@gmail.com", "1234")]
        [InlineData("", "")]
        public async Task Post_shouldReturnBadRequestInCaseNewUserEmailIsNullOrTheEmailIsInvalidOrTheUserPasswordIsLessThan6Characters(string email, string password)
        {
            //Arrange
            var fakeConfigure = A.Fake<IConfiguration>();
            var newUser = new Users() { Id = 33, FullName = "Sean pual", Email = email, Password = password, PhoneNumber = 212345667, UserType = 0 };
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.Post(newUser);
            //Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async Task getByID_shouldReturnNullBecauseIsNotAuthorize()
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.GetById(ValidUserId);
            //Assert
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task getByID_MakeSureTheMethodIsAuthorize()
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var fakeRepo = A.Fake<UserRepository>(x => x.WithArgumentsForConstructor(() => new UserRepository(context, fakeConfigure)));
            sut = new UsersController(fakeRepo);
            var actualAttribute = sut.GetType().GetMethod("GetById").GetCustomAttributes(typeof(AuthorizeAttribute), true);
            //Act
            var response = await sut.GetById(ValidUserId);
            //Assert
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());
        }

        [Fact]
        public async Task getByID_shouldReturnObjectOfTypeUserHasSameInsertId()
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var fakeRepo = A.Fake<UserRepository>(x => x.WithArgumentsForConstructor(() => new UserRepository(context, fakeConfigure)));
            sut = new UsersController(fakeRepo);
            //Act
            var response = await sut.GetById(ValidUserId);
            //Assert
            Assert.IsType<Users>(response.Value);
            Assert.Equal(1, response.Value.Id);

        }

        [Theory]
        [InlineData(777)]
        [InlineData(300)]
        [InlineData(99)]
        public async Task getByID_shouldReturnNullInCaseTheUserIdIsNotFound(int id)
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var fakeRepo = A.Fake<UserRepository>(x => x.WithArgumentsForConstructor(() => new UserRepository(context, fakeConfigure)));
            sut = new UsersController(fakeRepo);
            //Act
            var response = await sut.GetById(id);
            var result = response.GetType();
            //Assert
            Assert.Null(response.Value);
        }

        [Fact]
        public async Task Delete_shouldDeleteUserWhichHasTheInsertIdAfterAuthorizeAndReturnOkObject()
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.Delete(ValidUserId);
            //Assert
            Assert.Null(context.Users.SingleOrDefault(s => s.Id == ValidUserId));
            Assert.IsType<OkResult>(response);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Delete_shouldReturnBadRequestInCaseTheInsertIdNumberIsEqualToZeroOrBelow(int id)
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.Delete(id);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Delete_shouldReturnNotFoundInCaseThereIsNoUserWithTheInsertId(int id)
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.Delete(id);
            //Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Put_shouldEditUserWhichHasTheInsertIdAndReturnOkObject()
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            var newPhoneNumber = 12121212;
            //Act
            listOfUsers[0].PhoneNumber = newPhoneNumber;
            var response = await sut.Put(listOfUsers[0].Id, listOfUsers[0]);
            var userAfterEdit = context.Users.SingleOrDefault(s => s.Id == ValidUserId) as Users;
            //Assert
            Assert.Equal(newPhoneNumber, userAfterEdit.PhoneNumber);
            Assert.IsType<OkResult>(response);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Put_shouldReturnBadRequestInCaseTheInsertIdNumberIsEqualToZeroOrBelow(int id)
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.Put(id, listOfUsers[0]);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Put_shouldReturnBadRequestInCaseTheInsertIdAndTheIdOfTheInsertUserObjectAreNotEqual(int id)
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            //Act
            var response = await sut.Put(id, listOfUsers[0]);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task  Put_returnNotFoundResultInCaseTheInsertIdOfTheUserThatNeedToEditIsNotFoundInDatabase(int id)
        {
            //Arrangee
            var fakeConfigure = A.Fake<IConfiguration>();
            var userRepo = new UserRepository(context, fakeConfigure);
            sut = new UsersController(userRepo);
            var user = new Users() {Id = id, FullName = "Sean pual", Email = "seanpual@gmail.com", Password = "1234567", PhoneNumber = 212345667, UserType = 0 };
            //Act
            var response = await sut.Put(id, user);
            //Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
