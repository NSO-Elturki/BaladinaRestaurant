using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Controllers;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Test
{
  public  class DrinksControllerTest
    {
        DrinksController sut;
        RestaurantProjectContext context;
        List<Drinks> listOfDrinks;
        int ValidDrinkId = 1;

        public DrinksControllerTest()
        {
            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            context = new RestaurantProjectContext(options);
            context.Database.EnsureCreated();

            listOfDrinks = new List<Drinks>
            {
                new Drinks { Id = 1, DrinkType = "Energy", DrinkName = "Redball", DrinkPrice = 3, Quantity = 30, CreateDate = DateTime.Now },
                new Drinks { Id = 2, DrinkType = "Cold", DrinkName = "Mix milk shake", DrinkPrice = 4, Quantity = 10, CreateDate = DateTime.Now, DrinkDescription = "Milk shake with apple, banana, avocado and grape" },
            };

            context.Drinks.AddRange(listOfDrinks);
            context.SaveChanges();
        }

        [Fact]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async Task GetAll_returnTheCorrectNumberOfDrinksInDatabase()
        {
            //Arrange
            var drinksRepo = new DrinksRepository(context);
            sut = new DrinksController(drinksRepo);
            //Act
            var response = await sut.GetAll();
            var result = response as OkObjectResult;
            //Assert
            var returnedListOfDrinks = result.Value as IEnumerable<Drinks>;
            Assert.Equal(2, returnedListOfDrinks.Count());
        }

        [Fact]
        public async Task Post_shouldAddNewDrinkToDatabase()
        {
            //Arrangee5
           var newDrink = new Drinks { Id = 3, DrinkType = "Cold", DrinkName = "Ice coffe", DrinkPrice = 3, Quantity = 20, CreateDate = DateTime.Now };
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Post(newDrink);
            //Assert
            Assert.Equal(3, context.Drinks.Count());
        }

        [Theory]
        [InlineData("Cold", "ice tea", 5, 5)]
        [InlineData("Fizzy", "7Up", 1, 10)]
        [InlineData("Energy", "Monster", 4, 15)]
        public async Task Post_addManyNewDrinksToDatabase(string drinkType, string drinkName, decimal drinkPrice, int quantity)
        {
            //Arrangee5
            var newDrink = new Drinks {DrinkType = drinkType, DrinkName = drinkName, DrinkPrice = drinkPrice, Quantity = quantity};
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Post(newDrink);
            //Assert
            Assert.Equal(3, context.Drinks.Count());
        }


        [Fact]
        public async Task Post_returnBadRequestInCaseTheNewDrinkObjectIdIsAlreadyExsistInDatabase()
        {
            //Arrangee5
            var newDrink = new Drinks { Id = 1, DrinkType = "Cold", DrinkName = "Ice coffe", DrinkPrice = 3, Quantity = 20, CreateDate = DateTime.Now };
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Post(newDrink);
            //Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Theory]
        [InlineData(null, "ice tea", 5, 5)]
        [InlineData("Fizzy", null, 2, 0)]
        [InlineData("Energy", "Monster", -4, 10)]
        [InlineData("Fizzy", "fanta", -4, 0)]
        [InlineData(null, null, 0, 0)]
        public async Task Post_shouldReturnBadRequestInCaseNewAddedDrinkHasInvaildValueOrIsEqualToNull(string drinkType, string drinkName, decimal drinkPrice, int quantity)
        {
            //Arrange
            var newDrink = new Drinks { Id = 55, DrinkType = drinkType, DrinkName = drinkName, DrinkPrice = drinkPrice, Quantity = quantity, CreateDate = DateTime.Now };
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Post(newDrink);
            //Assert
            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async Task getByID_shouldReturnObjectOfTypeDrinkHasSameInsertId()
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.GetById(ValidDrinkId);
            //Assert
            Assert.IsType<Drinks>(response.Value);
            Assert.Equal(ValidDrinkId, response.Value.Id);

        }

        [Theory]
        [InlineData(777)]
        [InlineData(300)]
        [InlineData(99)]
        public async Task getByID_shouldReturnNullInCaseTheDrinkIdIsNotFound(int id)
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.GetById(id);
            var result = response.GetType();
            //Assert
            Assert.Null(response.Value);
        }

        [Fact]
        public async Task Delete_shouldDeleteDrinkWhichHasTheInsertIdAndReturnOkObject()
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Delete(ValidDrinkId);
            //Assert
            Assert.Null(context.Food.SingleOrDefault(s => s.Id == ValidDrinkId));
        }

        [Fact]
        public async Task Delete_MakeSureTheMethodIsAuthorize()
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            var actualAttribute = sut.GetType().GetMethod("Delete").GetCustomAttributes(typeof(AuthorizeAttribute), true);
            //Act
            var response = await sut.Delete(ValidDrinkId);
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
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
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
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Delete(id);
            //Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Put_shouldEditDrinkWhichHasTheInsertIdAndReturnOkObject()
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            var newDrinkPrice = 5;
            //Act
            listOfDrinks[0].DrinkPrice = newDrinkPrice;
            var response = await sut.Put(listOfDrinks[0].Id, listOfDrinks[0]);
            var drinkAfterEdit = context.Drinks.SingleOrDefault(s => s.Id == ValidDrinkId) as Drinks;
            //Assert
            Assert.Equal(newDrinkPrice, drinkAfterEdit.DrinkPrice);
            Assert.IsType<OkResult>(response);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Put_shouldReturnBadRequestInCaseTheInsertIdNumberIsEqualToZeroOrBelow(int id)
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Put(id, listOfDrinks[0]);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Put_shouldReturnBadRequestInCaseTheInsertIdAndTheIdOfTheInsertDrinkObjectAreNotEqual(int id)
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            //Act
            var response = await sut.Put(id, listOfDrinks[0]);
            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(9)]
        [InlineData(233)]
        public async Task Put_returnNotFoundInCaseTheInsertIdOfTheDrinkThatNeedToEditIsNotFoundInDatabase(int id)
        {
            //Arrangee
            var drinkRepo = new DrinksRepository(context);
            sut = new DrinksController(drinkRepo);
            var drink = new Drinks { Id = id, DrinkType = "Cold", DrinkName = "Ice coffe", DrinkPrice = 3, Quantity = 20, CreateDate = DateTime.Now };
            //Act
            var response = await sut.Put(id, drink);
            //Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
