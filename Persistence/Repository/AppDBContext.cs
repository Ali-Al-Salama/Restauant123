using Microsoft.EntityFrameworkCore;
using Restaurant.Persistence.Entity;
namespace Restaurant.Persistence
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items {get;set;}
        public DbSet<WeeklyMenu> WeeklyMenu{get;set;}
        public DbSet<Order> Order{get;set;}
        public DbSet<User> User{get;set;}
        public DbSet<ResetPassword> ResetPasswords{get;set;}
        public DbSet<Payment> Payment{get;set;}
        public DbSet<pendingUser>pendingUser{get;set;}


    }
}