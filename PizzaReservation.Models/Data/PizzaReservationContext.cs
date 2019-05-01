using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PizzaReservation.Models.Data
{
    public class PizzaReservationContext : IdentityDbContext<IdentityUser>
    {
        public PizzaReservationContext(DbContextOptions<PizzaReservationContext> options) : base(options)
        {

        }

        public DbSet<Pizza> tblPizzas { get; set; }
        public DbSet<Size> tblSizes { get; set; }
        public DbSet<Topping> tblToppings { get; set; }
        public DbSet<PizzaTopping> tblPizzaToppings { get; set; }
        public DbSet<PizzaSize> tblPizzaSizes { get; set; }
        public DbSet<PizzaOrder> tblPizzaOrders { get; set; }
        public DbSet<Order> tblOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // FOREIGN KEYS TUSSENTABELLEN
            modelBuilder.Entity<PizzaTopping>()
                .HasKey(pt => new { pt.PizzaId, pt.ToppingId });

            modelBuilder.Entity<PizzaOrder>()
                .HasKey(po => new { po.PizzaId, po.OrderId, po.SizeId });

            modelBuilder.Entity<PizzaSize>()
                .HasKey(ps => new { ps.PizzaId, ps.SizeId });

            // RELATIES TUSSENTABELLEN
            modelBuilder.Entity<PizzaTopping>() //Pizza -> PizzaTopping één-op-veel
                .HasOne(pt => pt.Pizza)
                .WithMany(p => p.PizzaToppings)
                .HasForeignKey(pt => pt.PizzaId);
            modelBuilder.Entity<PizzaTopping>() //Topping -> PizzaTopping één-op-veel
                .HasOne(pt => pt.Topping)
                .WithMany(t => t.PizzaToppings)
                .HasForeignKey(pt => pt.ToppingId);


            modelBuilder.Entity<PizzaSize>() //Pizza -> PizzaSize één-op-veel
                .HasOne(ps => ps.Pizza)
                .WithMany(p => p.PizzaSizes)
                .HasForeignKey(ps => ps.PizzaId);
            modelBuilder.Entity<PizzaSize>() //Size -> PizzaSize één-op-veel
                .HasOne(ps => ps.Size)
                .WithMany(s => s.PizzaSizes)
                .HasForeignKey(ps => ps.SizeId);


            modelBuilder.Entity<PizzaOrder>() //Pizza -> PizzaOrder één-op-veel 
                .HasOne(po => po.Pizza)
                .WithMany(p => p.PizzaOrders)
                .HasForeignKey(po => po.PizzaId);
            modelBuilder.Entity<PizzaOrder>() //Order -> PizzaOrder één-op-veel
                .HasOne(po => po.Order)
                .WithMany(o => o.PizzaOrders)
                .HasForeignKey(po => po.OrderId);


            modelBuilder.Entity<PizzaOrder>() // PizzaOrder -> Size één-op-veel
               .HasOne(po => po.Order)
               .WithMany(p => p.PizzaOrders)
               .HasForeignKey(po => po.OrderId);
            modelBuilder.Entity<PizzaOrder>() // PizzaOrder -> Size één-op-veel
                .HasOne(po => po.Size)
                .WithMany(s => s.PizzaOrders)
                .HasForeignKey(po => po.SizeId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
