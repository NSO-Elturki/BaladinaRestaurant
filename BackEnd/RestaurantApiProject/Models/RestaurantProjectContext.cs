using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RestaurantApiProject.Models
{
    public partial class RestaurantProjectContext : DbContext
    {
       

        public RestaurantProjectContext(DbContextOptions<RestaurantProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drinks> Drinks { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<OrdersBills> OrdersBills { get; set; }
        public virtual DbSet<OrdersDrinks> OrdersDrinks { get; set; }
        public virtual DbSet<OrdersFood> OrdersFood { get; set; }
        public virtual DbSet<Users> Users { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drinks>(entity =>
            {
                entity.Property(e => e.DrinkPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.FoodPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<OrdersBills>(entity =>
            {
                entity.ToTable("Orders_Bills");

                entity.HasIndex(e => e.OrderId)
                    .HasName("UQ__Orders_B__C3905BCE2833B110")
                    .IsUnique();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.OrdersBills)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Orders_Bi__Clien__68D28DBC");
            });

            modelBuilder.Entity<OrdersDrinks>(entity =>
            {
                entity.ToTable("Orders_Drinks");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.OrdersDrinks)
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders_Dr__Drink__55BFB948");
            });

            modelBuilder.Entity<OrdersFood>(entity =>
            {
                entity.ToTable("Orders_Food");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrdersFood)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders_Fo__FoodI__42ACE4D4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
