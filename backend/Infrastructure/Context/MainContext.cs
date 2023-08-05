using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Context
{
    public class MainContext
      : DbContext
    {
        public MainContext(DbContextOptions options)
            : base(options)
        {

        }

        public MainContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.Use
                // optionsBuilder.Use
                // .UseLazyLoadingProxies()
                // .UseSqlServer("DefaultConnection");
            }
        }
    }
}