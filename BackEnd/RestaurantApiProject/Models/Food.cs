using RestaurantApiProject.Contracts;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class Food: IEntity
    {
        public Food()
        {
            OrdersFood = new HashSet<OrdersFood>();
        }

        public int Id { get; set; }
        public string FoodType { get; set; }
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<OrdersFood> OrdersFood { get; set; }
    }
}
