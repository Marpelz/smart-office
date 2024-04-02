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
