using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecules.infrastructure.data
{
    public class MoleculesDbContext : DbContext
    {
        public DbSet<CalcOrderDbEntity> CalcOrders { get; set; }

        private readonly IConfiguration _configuration;

        public MoleculesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration["ConnectionString"]?.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalcOrderDbEntity>()
                    .ToTable("CalcOrder")
                    .HasKey(o => o.Id);

            modelBuilder.Entity<CalcOrderDbEntity>()
                    .Property(o => o.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<CalcOrderDbEntity>()
                   .Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            modelBuilder.Entity<CalcOrderDbEntity>()
                   .Property(o => o.CustomerName)
                   .IsRequired();

        }
    }
}
