using Microsoft.EntityFrameworkCore;
using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecules.infrastructure.data
{
    public class MoleculesDbContext : DbContext
    {
        public DbSet<CalcOrderDbEntity> CalcOrders { get; set; }

        public DbSet<CalcOrderItemDbEntity> CalcOrderItems { get; set; }

        public MoleculesDbContext(DbContextOptions<MoleculesDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("moleculesapp");

            // CalcOrderDvEntity

            modelBuilder.Entity<CalcOrderDbEntity>()
                    .ToTable("CalcOrder")
                    .HasKey(o => o.Id);

            modelBuilder.Entity<CalcOrderDbEntity>()
                    .Property(o => o.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<CalcOrderDbEntity>()
                .HasMany(o => o.CalcOrderItems)
                .WithOne(i => i.CalcOrder)
                .HasForeignKey(i => i.CalcOrderId)
                .HasPrincipalKey(o => o.Id);

            modelBuilder.Entity<CalcOrderDbEntity>()
                   .Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            modelBuilder.Entity<CalcOrderDbEntity>()
                   .Property(o => o.CustomerName)
                   .IsRequired();


            // CalcOrderItemDbEntity

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                    .ToTable("CalcOrderItem")
                    .HasKey(o => o.Id);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                .Property(o => o.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                .Property(o => o.CalcOrderId)
                .IsRequired();

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                .Property(o => o.CalcType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
               .Property(o => o.BasissetCode)
               .IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                .Property(o => o.MoleculeName)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                .Property(o => o.XYZ)
                .IsRequired();

            modelBuilder.Entity<CalcOrderItemDbEntity>()
                .Property(o => o.Charge)
                .IsRequired();




        }
    }
}
