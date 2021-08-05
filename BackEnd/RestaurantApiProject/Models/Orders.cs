using RestaurantApiProject.Contracts;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class Orders: IEntity
    {
        public int Id { get; set; }
        public string OrderDescription { get; set; }
        public int DeliveryPhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime CreateTime { get; set; }
        public string OrderNote { get; set; }
    }
}
