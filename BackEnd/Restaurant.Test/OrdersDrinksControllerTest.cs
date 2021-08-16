using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Controllers;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Test
{
  public  class OrdersDrinksControllerTest
    {
        OrdersDrinksController sut;
        RestaurantProjectContext context;
        List<OrdersDrinks> listOfOrdersDrinks;
        int ValidOrderDrinkId = 1;

        public OrdersDrinksControllerTest()
        {
            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            context = new RestaurantProjectContext(options);
            context.Database.EnsureCreated();

            listOfOrdersDrinks = new List<OrdersDrinks>
            {
                new OrdersDrinks { Id = 1, OrderId = 1, DrinkId = 1},
                new OrdersDrinks { Id = 2, OrderId = 2, DrinkId = 2}
,
            };

            context.OrdersDrinks.AddRange(listOfOrdersDrinks);

            context.SaveChanges();
        }

        [Fact]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async Task GetAll_returnTheCorrectNumberOfOrderDrinksInDatabase()
        {
            //Arrange
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.GetAll();
            var result = response as OkObjectResult;
            //Assert
            var returnedListOfOrdersDrinks = result.Value as IEnumerable<OrdersDrinks>;
            Assert.Equal(2, returnedListOfOrdersDrinks.Count());
        }

        [Fact]
        public async Task Post_shouldAddNewOrderDrinkDatabase()
        {
            //Arrangee5
            var newOrderDrink = new OrdersDrinks { Id = 3, OrderId = 1, DrinkId = 2 };
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.Post(newOrderDrink);
            //Assert
            Assert.Equal(3, context.OrdersDrinks.Count());
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(8, 2)]
        [InlineData(20, 1)]

        public async Task Post_addManyOrderDrinkToDatabase(int orderId, int drinkId)
        {
            //Arrangee5
            var newOrderDrink = new OrdersDrinks { OrderId = orderId, DrinkId = drinkId };
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.Post(newOrderDrink);
            //Assert
            Assert.Equal(3, context.OrdersDrinks.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Post_shouldReturnBadRequestInCaseTheNewOrderDrinkHasFoodIdEqualToZeroOrLess(int drinkId)
        {
            //Arrange
            var newOrderDrink = new OrdersDrinks { OrderId = 1, DrinkId = drinkId };
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.Post(newOrderDrink);
            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Post_shouldReturnBadRequestInCaseTheNewOrderDrinkHasFoodIdEqualToNull()
        {
            //Arrange
            var newOrderDrink = new OrdersDrinks { OrderId = 1 };
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.Post(newOrderDrink);
            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Delete_shouldDeleteTheOrderDrinkObjectFromDatabaseThatHasTheSameInsertId()
        {
            //Arrange
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.Delete(ValidOrderDrinkId);
            //Assert
            Assert.Null(context.OrdersFood.SingleOrDefault(s => s.Id == ValidOrderDrinkId));
        }

        [Fact]
        public async Task Delete_afterDeleteTheOrderDrinkThatHasTheSameInsertIdTheTotalOrderDrinksInDatabaseShouldEqualToOne()
        {
            //Arrange
            var OrderDrinksRepo = new OrderDrinksRepository(context);
            sut = new OrdersDrinksController(OrderDrinksRepo);
            //Act
            var response = await sut.Delete(ValidOrderDrinkId);
            //Assert
            Assert.Equal(1, context.OrdersDrinks.Count());
        }


    }
}
