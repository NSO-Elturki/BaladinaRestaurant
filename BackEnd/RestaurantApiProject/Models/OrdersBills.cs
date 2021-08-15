using RestaurantApiProject.Contracts;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class OrdersBills: IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public int ClientPhoneNumber { get; set; }
        public string ClientAddress { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalCost { get; set; }
        public int ToPickUp { get; set; }

        public virtual Users Client { get; set; }
    }
}
