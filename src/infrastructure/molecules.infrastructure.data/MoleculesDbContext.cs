using Microsoft.EntityFrameworkCore;
using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecules.infrastructure.data
{
    public class MoleculesDbContext : DbContext
    {
        public DbSet<CalcOrderDbEntity> CalcOrders { get; set; }

        public MoleculesDbContext(DbContextOptions<MoleculesDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("moleculesapp");

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
