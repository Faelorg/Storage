using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace storage;

public partial class StorageEContext : DbContext
{
    private static StorageEContext _context;

    public static StorageEContext GetContext()
    {
        if (_context == null)
        {
            _context = new StorageEContext();
        }

        return _context;
    }

    public StorageEContext()
    {
    }

    public StorageEContext(DbContextOptions<StorageEContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSale> ProductSales { get; set; }

    public virtual DbSet<Punkt> Punkts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DANILA\\SQLEXPRESS;Initial Catalog=storage_E;Integrated Security=True; Trust Server Certificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.ToTable("Manufacturer");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdManufacturer).HasColumnName("id_Manufacturer");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Photo).HasMaxLength(50);

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdManufacturer)
                .HasConstraintName("FK_Product_Manufacturer");
        });

        modelBuilder.Entity<ProductSale>(entity =>
        {
            entity.ToTable("ProductSale");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProduct).HasColumnName("id_Product");
            entity.Property(e => e.IdSale).HasColumnName("id_Sale");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductSales)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_ProductSale_Product");

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.ProductSales)
                .HasForeignKey(d => d.IdSale)
                .HasConstraintName("FK_ProductSale_Sale");
        });

        modelBuilder.Entity<Punkt>(entity =>
        {
            entity.ToTable("Punkt");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sale");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdd).HasColumnType("date");
            entity.Property(e => e.IdPunkt).HasColumnName("id_Punkt");

            entity.HasOne(d => d.IdPunktNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdPunkt)
                .HasConstraintName("FK_Sale_Punkt");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdRole).HasColumnName("id_Role");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
