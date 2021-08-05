using FakeItEasy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Controllers;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Restaurant.Test
{
    public class FoodsControllerTest
    {
        FoodsController sut;
        RestaurantProjectContext context;
        List<Food> listOfFood;
        int ValidFoodId = 1;

        public FoodsControllerTest()
        {
            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            context = new RestaurantProjectContext(options);
            context.Database.EnsureCreated();

            listOfFood = new List<Food>
            {
                new Food { Id = 1, FoodType = "Main dish", FoodName = "Chease burger", FoodPrice = 10, FoodDescription = "The burger contains beef, onion, mashroom, ketchup and mayo", CreateDate = DateTime.Now },
                new Food { Id = 2, FoodType = "Main dish", FoodName = "Sea food", FoodPrice = 12, FoodDescription = "Pasta contains salmon, shrims, calamari and tomato sous", CreateDate = DateTime.Now }
            };

            context.Food.AddRange(listOfFood);
            context.SaveChanges();
        }

        [Fact]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async Task GetAll_returnTheCorrectNumberOfFoodInDatabase()
        {
            //Arrange
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.GetAll();
            //Assert
            var result = response as OkObjectResult;
            var returnedListOfFood = result.Value as IEnumerable<Food>;
            Assert.Equal(2, returnedListOfFood.Count());
        }

        [Fact]
        public async Task Post_shouldAddNewFoodToDatabase()
        {
            //Arrangee5
            var newFood = new Food { Id = 3, FoodType = "Pizza", FoodName = "Margarita", FoodPrice = 8, FoodDescription = "Pizza contains olive, mazorila and tomato sous"};
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Post(newFood);
            //Assert
            Assert.Equal(3, context.Food.Count());
        }

        [Theory]
        [InlineData("Main dish", " Pizza fungi", 9, "machrom and mazzorala")]
        [InlineData("Main dish", "hanburger", 9, "Beef and chease")]
        [InlineData("Main dish", "Pizza with salami", 10, "machrom and mazzorala")]
        public async Task Post_addManyNewFoodToDatabase(string foodType, string foodName, decimal foodPrice, string foodDescription)
        {
            //Arrange
            var newFood = new Food { FoodType = foodType, FoodName = foodName, FoodPrice = foodPrice, FoodDescription = foodDescription};
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Post(newFood);
            //Assert
            Assert.Equal(3, context.Food.Count());
        }

        [Fact]
        public async Task Post_returnBadRequestInCaseTheNewDrinkObjectIdIsAlreadyExsistInDatabase()
        {
            //Arrangee
            var newFood = new Food { Id = 1, FoodType = "Pizza", FoodName = "Margarita", FoodPrice = 8, FoodDescription = "Pizza contains olive, mazorila and tomato sous" };
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Post(newFood);
            //Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Theory]
        [InlineData(null,"Fungi", 9, "machrom and mazzorala")]
        [InlineData("Main dish", "hanburger", 9, null)]
        [InlineData("Main dish", "Pizza with salami", -10 , "machrom and mazzorala")]
        [InlineData("Main dish", "Rice with kip", 0, "rice with kip, vegatables and cheases")]
        [InlineData(null, null, 0, null)]

        public async Task Post_shouldReturnBadRequestInCaseNewAddedFoodHasInvaildValueOrIsEqualToNull(string foodType, string foodName, decimal foodPrice, string foodDescription)
        {
            //Arrange
            var newFood = new Food { Id = 4, FoodType = foodType, FoodName = foodName, FoodPrice = foodPrice, FoodDescription = foodDescription };
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Post(newFood);
            //Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async Task getByID_shouldReturnObjectOfTypeFoodHasSameInsertId()
        {
            //Arrange
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.GetById(ValidFoodId);
            //Assert
            Assert.IsType<Food>(response.Value);
            Assert.Equal(1, response.Value.Id);

        }

        [Theory]
        [InlineData(777)]
        [InlineData(300)]
        [InlineData(99)]
        public async Task getByID_shouldReturnNullInCaseTheFoodIdIsNotFound(int id)
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.GetById(id);
            var result = response.GetType();
            //Assert
            Assert.Null(response.Value);
        }

        [Fact]
        public async Task Delete_shouldDeleteFoodWhichHasTheInsertIdAndReturnOkObject()
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Delete(ValidFoodId);
            //Assert
            Assert.Null(context.Food.SingleOrDefault(s => s.Id == ValidFoodId));
            
        }

        [Fact]
        public async Task Delete_MakeSureTheMethodIsAuthorize()
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            var actualAttribute = sut.GetType().GetMethod("Delete").GetCustomAttributes(typeof(AuthorizeAttribute), true);
            //Act
            var response = await sut.Delete(ValidFoodId);
            var result = response as ActionResult;
            //Assert
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Delete_shouldReturnBadRequestInCaseTheInsertIdNumberIsEqualToZeroOrBelow(int id)
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Delete(id);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Delete_shouldReturnNotFoundInCaseThereIsNoFoodWithTheInsertId(int id)
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Delete(id);
            //Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Put_shouldEditFoodWhichHasTheInsertIdAndReturnOkObject()
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            var editFoodType = "Fast food";
            //Act
            listOfFood[0].FoodType = editFoodType;
            var response = await sut.Put(listOfFood[0].Id, listOfFood[0]);
            var foodAfterEdit = context.Food.SingleOrDefault(s => s.Id == ValidFoodId) as Food;
            //Assert
            Assert.Equal(editFoodType, foodAfterEdit.FoodType);
            Assert.IsType<OkResult>(response);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Put_shouldReturnBadRequestInCaseTheInsertIdNumberIsEqualToZeroOrBelow(int id)
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Put(id, listOfFood[0]);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Put_shouldReturnBadRequestInCaseTheInsertIdAndTheIdOfTheInsertFoodObjectAreNotEqual(int id)
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
            //Act
            var response = await sut.Put(id, listOfFood[0]);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Put_returnNotFoundInCaseTheInsertIdOfTheFoodThatNeedToEditIsNotFoundInDatabase(int id)
        {
            //Arrangee
            var foodRepo = new FoodRepository(context);
            sut = new FoodsController(foodRepo);
          var food =  new Food { Id = id, FoodType = "Main dish", FoodName = "Sea food", FoodPrice = 12, FoodDescription = "Pasta contains salmon, shrims, calamari and tomato sous", CreateDate = DateTime.Now };
            //Act
            var response = await sut.Put(id, food);
            //Assert
            Assert.IsType<NotFoundResult>(response);
        }

    }
}
