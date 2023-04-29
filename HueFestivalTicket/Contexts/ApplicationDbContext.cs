using HueFestivalTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ManagerToken> ManagerTokens { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventLocation> EventLocations { get; set; }
        public DbSet<ImageEvent> ImageEvents { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PriceTicket> PriceTickets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TypeLocation> TypeLocations { get; set; }
        public DbSet<TypeTicket> TypeTickets { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
