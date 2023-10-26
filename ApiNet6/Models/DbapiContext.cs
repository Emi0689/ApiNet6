using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Models;

public partial class DbapiContext : DbContext
{
    public DbapiContext()
    {
    }

    public DbapiContext(DbContextOptions<DbapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__CATEGORY__CBD747061F3FB6C3");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__PRODUCT__2E8946D4595202CB");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Barcode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.oCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_IDCATEGORY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
