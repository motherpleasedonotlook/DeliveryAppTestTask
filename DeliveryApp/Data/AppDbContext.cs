namespace DeliveryApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    //таблицы
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //связь 1 к 1 между Customer и ApplicationUser
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Customer)
            .WithOne(c => c.User)
            .HasForeignKey<Customer>(c => c.UserId);
        
        modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.SenderAddress)
            .WithMany()
            .HasForeignKey(o => o.SenderAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.ReceiverAddress)
            .WithMany()
            .HasForeignKey(o => o.ReceiverAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        //ускорение поиска адресов по типу
        modelBuilder.Entity<Address>()
            .HasIndex(a => new { a.CustomerId, a.IsSender });

        //заказов клиента
        modelBuilder.Entity<Order>()
            .HasIndex(o => o.CustomerId);

        //заказов по дате
        modelBuilder.Entity<Order>()
            .HasIndex(o => o.CreationDate);
        
        base.OnModelCreating(modelBuilder);
    }
}