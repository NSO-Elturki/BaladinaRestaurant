using Microsoft.EntityFrameworkCore;
using RestaurantApiProject.Controllers;
using RestaurantApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Test
{
   public class OrdersBillsControllerTest
    {
        OrdersBillsController sut;
        RestaurantProjectContext context;
        List<OrdersBills> listOfOrdersBills;
        int ValidOrderBillId = 1;

        public OrdersBillsControllerTest()
        {
            var options = new DbContextOptionsBuilder<RestaurantProjectContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            context = new RestaurantProjectContext(options);
            context.Database.EnsureCreated();

            listOfOrdersBills = new List<OrdersBills>
            {
                new OrdersBills { Id = 1, ClientName = "Nagi Elturki", ClientAddress = "Tue La Luna",  ClientPhoneNumber = 1234567, Email = "nagielturki@gmail.com", City = "Eindhoven", Note = "Please ring bill 1234", OrderId = 1, ToPickUp = 0, TotalCost = 20, CreateDate = DateTime.Now },
                new OrdersBills { Id = 1, ClientName = "Redwan Elturki", ClientAddress = "Vactorai park",  ClientPhoneNumber = 1234567, Email = "redwanelturki@gmail.com", City = "Eindhoven", Note = "Please ring bill 333", OrderId = 1, ToPickUp = 0, TotalCost = 25, CreateDate = DateTime.Now },
            };

            context.OrdersBills.AddRange(listOfOrdersBills);
            context.SaveChanges();
        }

        [Fact]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

    }
}
