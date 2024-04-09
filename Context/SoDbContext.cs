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

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<SoDishTab> SoDishTabs { get; set; }

    public virtual DbSet<SoOrderDetailsTab> SoOrderDetailsTabs { get; set; }

    public virtual DbSet<SoOrderTab> SoOrderTabs { get; set; }

    public virtual DbSet<SoRestTab> SoRestTabs { get; set; }

    public virtual DbSet<SoUserTab> SoUserTabs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<SoDishTab>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PRIMARY");

            entity.ToTable("SoDishTab");

            entity.HasIndex(e => e.DishRestId, "IX_SoDishTab_DishRestId");

            entity.HasOne(d => d.DishRest).WithMany(p => p.SoDishTabs).HasForeignKey(d => d.DishRestId);
        });

        modelBuilder.Entity<SoOrderDetailsTab>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PRIMARY");

            entity.ToTable("SoOrderDetailsTab");

            entity.HasIndex(e => e.DishId, "IX_SoOrderDetailsTab_DishId");

            entity.HasIndex(e => e.OrderId, "IX_SoOrderDetailsTab_OrderId");

            entity.HasIndex(e => e.UserId, "IX_SoOrderDetailsTab_UserId");

            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Dish).WithMany(p => p.SoOrderDetailsTabs).HasForeignKey(d => d.DishId);

            entity.HasOne(d => d.Order).WithMany(p => p.SoOrderDetailsTabs).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.User).WithMany(p => p.SoOrderDetailsTabs).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<SoOrderTab>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("SoOrderTab");

            entity.HasIndex(e => e.RestaurantId, "IX_SoOrderTab_RestaurantId");

            entity.HasIndex(e => e.UserId, "IX_SoOrderTab_UserId");

            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.SoOrderTabs).HasForeignKey(d => d.RestaurantId);

            entity.HasOne(d => d.User).WithMany(p => p.SoOrderTabs).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<SoRestTab>(entity =>
        {
            entity.HasKey(e => e.RestId).HasName("PRIMARY");

            entity.ToTable("SoRestTab");

            entity.Property(e => e.RestAppTelephone).HasMaxLength(20);
            entity.Property(e => e.RestDelivery).HasMaxLength(20);
        });

        modelBuilder.Entity<SoUserTab>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("SoUserTab");

            entity.Property(e => e.UserId).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
