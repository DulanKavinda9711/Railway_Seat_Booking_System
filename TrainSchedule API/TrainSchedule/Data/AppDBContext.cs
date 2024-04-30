using Microsoft.EntityFrameworkCore;
using TrainSchedule.Model;
namespace TrainSchedule.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        { 
        
        }

        public DbSet<Train>trains { get; set; }
        public DbSet<Booking> bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>().Property(p=> p.Name).IsRequired();
            modelBuilder.Entity<Booking>().Property(p=>p.TrainName).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "Data Source=DESKTOP-21SIMOF\\DULAN;Initial Catalog=Dulan62;User ID=sa;Password=sql123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=Yes;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(conn);
        }
    }
}
