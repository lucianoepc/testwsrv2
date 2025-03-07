using Microsoft.EntityFrameworkCore;
using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWSrv2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Set the schema for all entities
        modelBuilder.HasDefaultSchema("sales");
        
        // Configure the IdentityDocumentType entity
        modelBuilder.Entity<IdentityDocumentType>(entity =>
        {
            entity.ToTable("identity_document_types");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(10);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(100);
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // Configure the Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(20);
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.BaseCost).HasColumnName("base_cost").HasPrecision(10, 2);
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // Configure the Person entity
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("people");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(20);
            entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(100);
            entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.PlaceOfBirth).HasColumnName("place_of_birth").HasMaxLength(100);
            entity.Property(e => e.IdentityDocumentTypeId).HasColumnName("identity_document_type_id");
            
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasOne(e => e.IdentityDocumentType)
                  .WithMany()
                  .HasForeignKey(e => e.IdentityDocumentTypeId);
        });

        // Configure the SoldProducts entity
        modelBuilder.Entity<SoldProducts>(entity =>
        {
            entity.ToTable("sold_products");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PersonCode).HasColumnName("person_code").HasMaxLength(20);
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductCode).HasColumnName("product_code").HasMaxLength(20);
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(e => e.Person)
                  .WithMany()
                  .HasForeignKey(e => e.PersonId);
            entity.HasOne(e => e.Product)
                  .WithMany()
                  .HasForeignKey(e => e.ProductId);
        });
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<IdentityDocumentType> IdentityDocumentTypes { get; set; }
    public DbSet<SoldProducts> SoldProducts { get; set; }
}