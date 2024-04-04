using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartOffice.Entities;

namespace SmartOffice.Context;

public partial class SoDbContext : DbContext
{
    public SoDbContext(DbContextOptions<SoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Sodishtab> Sodishtabs { get; set; }

    public virtual DbSet<Soorderdetailstab> Soorderdetailstabs { get; set; }

    public virtual DbSet<Soordertab> Soordertabs { get; set; }

    public virtual DbSet<Soresttab> Soresttabs { get; set; }

    public virtual DbSet<Sousertab> Sousertabs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Sodishtab>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PRIMARY");

            entity.ToTable("sodishtab");

            entity.HasIndex(e => e.DishRestId, "IX_SoDishTab_DishRestId");

            entity.HasOne(d => d.DishRest).WithMany(p => p.Sodishtabs)
                .HasForeignKey(d => d.DishRestId)
                .HasConstraintName("FK_SoDishTab_SoRestTab_DishRestId");
        });

        modelBuilder.Entity<Soorderdetailstab>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PRIMARY");

            entity.ToTable("soorderdetailstab");

            entity.HasIndex(e => e.DishId, "IX_SoOrderDetailsTab_DishId");

            entity.HasIndex(e => e.OrderId, "IX_SoOrderDetailsTab_OrderId");

            entity.HasIndex(e => e.UserId, "IX_SoOrderDetailsTab_UserId");

            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Dish).WithMany(p => p.Soorderdetailstabs)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK_SoOrderDetailsTab_SoDishTab_DishId");

            entity.HasOne(d => d.Order).WithMany(p => p.Soorderdetailstabs)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_SoOrderDetailsTab_SoOrderTab_OrderId");

            entity.HasOne(d => d.User).WithMany(p => p.Soorderdetailstabs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SoOrderDetailsTab_SoUserTab_UserId");
        });

        modelBuilder.Entity<Soordertab>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("soordertab");

            entity.HasIndex(e => e.RestaurantId, "IX_SoOrderTab_RestaurantId");

            entity.HasIndex(e => e.UserId, "IX_SoOrderTab_UserId");

            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Soordertabs)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK_SoOrderTab_SoRestTab_RestaurantId");

            entity.HasOne(d => d.User).WithMany(p => p.Soordertabs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SoOrderTab_SoUserTab_UserId");
        });

        modelBuilder.Entity<Soresttab>(entity =>
        {
            entity.HasKey(e => e.RestId).HasName("PRIMARY");

            entity.ToTable("soresttab");

            entity.Property(e => e.RestAppTelephone).HasMaxLength(20);
            entity.Property(e => e.RestDelivery).HasMaxLength(20);
        });

        modelBuilder.Entity<Sousertab>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("sousertab");

            entity.Property(e => e.UserId).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
