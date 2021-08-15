using RestaurantApiProject.Contracts;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class OrdersFood: IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }

        public virtual Food Food { get; set; }
    }
}
