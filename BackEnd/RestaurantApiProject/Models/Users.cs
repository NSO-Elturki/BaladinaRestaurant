using RestaurantApiProject.Contracts;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class Users: IEntity
    {
        public Users()
        {
            OrdersBills = new HashSet<OrdersBills>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public int UserType { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<OrdersBills> OrdersBills { get; set; }
    }
}
