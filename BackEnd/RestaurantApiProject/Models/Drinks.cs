using RestaurantApiProject.Contracts;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class Drinks: IEntity
    {
        public int Id { get; set; }
        public string DrinkType { get; set; }
        public string DrinkName { get; set; }
        public decimal DrinkPrice { get; set; }
        public int Quantity { get; set; }
        public string DrinkDescription { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
