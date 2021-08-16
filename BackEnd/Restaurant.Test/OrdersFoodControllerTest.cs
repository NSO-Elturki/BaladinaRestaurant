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
   public class OrdersFoodControllerTest
    {
        OrdersFoodController sut;
        RestaurantProjectContext context;
        List<OrdersFood> listOfOrdersFood;
        int ValidOrderFoodId = 1;

        public OrdersFoodControllerTest()
        {
            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            context = new RestaurantProjectContext(options);
            context.Database.EnsureCreated();

            listOfOrdersFood = new List<OrdersFood>
            {
                new OrdersFood { Id = 1, OrderId = 1, FoodId = 1},
                new OrdersFood { Id = 2, OrderId = 2, FoodId = 2}
,
            };

            context.OrdersFood.AddRange(listOfOrdersFood);

            context.SaveChanges();
        }

        [Fact]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public async Task GetAll_returnTheCorrectNumberOfOrderFoodInDatabase()
        {
            //Arrange
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.GetAll();
            var result = response as OkObjectResult;
            //Assert
            var returnedListOfOrdersFood = result.Value as IEnumerable<OrdersFood>;
            Assert.Equal(2, returnedListOfOrdersFood.Count());
        }

        [Fact]
        public async Task Post_shouldAddNewOrderFoodDatabase()
        {
            //Arrangee5
            var newOrderFood = new OrdersFood { Id = 3, OrderId = 1, FoodId = 2 };
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.Post(newOrderFood);
            //Assert
            Assert.Equal(3, context.OrdersFood.Count());
        }

        [Theory]
        [InlineData(5, 1 )]
        [InlineData(8,2)]
        [InlineData(20, 1)]

        public async Task Post_addManyOrderFoodToDatabase(int orderId , int foodId)
        {
            //Arrangee5
            var newOrderFood = new OrdersFood { OrderId = orderId, FoodId = foodId };
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.Post(newOrderFood);
            //Assert
            Assert.Equal(3, context.OrdersFood.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Post_shouldReturnBadRequestInCaseTheNewOrderFoodHasFoodIdEqualToZeroOrLess(int foodId)
        {
            //Arrange
            var newOrderFood = new OrdersFood { OrderId = 1, FoodId = foodId };
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.Post(newOrderFood);
            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Post_shouldReturnBadRequestInCaseTheNewOrderFoodHasFoodIdEqualToNull()
        {
            //Arrange
            var newOrderFood = new OrdersFood { OrderId = 1};
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.Post(newOrderFood);
            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Delete_shouldDeleteTheOrderFoodObjectFromDatabaseThatHasTheSameInsertId()
        {
            //Arrange
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.Delete(ValidOrderFoodId);
            //Assert
            Assert.Null(context.OrdersFood.SingleOrDefault(s => s.Id == ValidOrderFoodId));
        }

        [Fact]
        public async Task Delete_afterDeleteTheOrderFoodThatHasTheSameInsertIdTheTotalOrderFoodInDatabaseShouldEqualToOne()
        {
            //Arrange
            var OrderFoodRepo = new OrderFoodRepository(context);
            sut = new OrdersFoodController(OrderFoodRepo);
            //Act
            var response = await sut.Delete(ValidOrderFoodId);
            //Assert
            Assert.Equal(1,context.OrdersFood.Count());
        }

    }
}
