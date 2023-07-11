﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using molecules.infrastructure.data;

#nullable disable

namespace molecules.infrastructure.data.Migrations
{
    [DbContext(typeof(MoleculesDbContext))]
    partial class MoleculesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("moleculesapp")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("molecule.infrastructure.data.interfaces.DbEntities.CalcOrderDbEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.ToTable("CalcOrder", "moleculesapp");
                });

            modelBuilder.Entity("molecule.infrastructure.data.interfaces.DbEntities.CalcOrderItemDbEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CalcOrderId")
                        .HasColumnType("integer");

                    b.Property<string>("CalcType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Charge")
                        .HasColumnType("integer");

                    b.Property<string>("MoleculeName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("XYZ")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CalcOrderId");

                    b.ToTable("CalcOrderItem", "moleculesapp");
                });

            modelBuilder.Entity("molecule.infrastructure.data.interfaces.DbEntities.CalcOrderItemDbEntity", b =>
                {
                    b.HasOne("molecule.infrastructure.data.interfaces.DbEntities.CalcOrderDbEntity", "CalcOrder")
                        .WithMany("CalcOrderItems")
                        .HasForeignKey("CalcOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalcOrder");
                });

            modelBuilder.Entity("molecule.infrastructure.data.interfaces.DbEntities.CalcOrderDbEntity", b =>
                {
                    b.Navigation("CalcOrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
