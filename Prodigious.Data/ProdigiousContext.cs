using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Prodigious.Data
{

    public partial class ProdigiousContext : DbContext
    {
        public ProdigiousContext()
            : base("name=prodigious")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }
        public virtual DbSet<ProductModel> ProductModels { get; set; }
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .ToTable("Product", "SalesLT")
                .HasKey<int>(e => e.ProductID)
                .Ignore(e => e.IsDirty);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductNumber)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Product>()
                .Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Product>()
                .Property(e => e.StandardCost)
                .HasColumnType("money")
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ListPrice)
                .HasColumnType("money")
                .HasPrecision(19, 4);
                
            modelBuilder.Entity<Product>()
                .Property(e => e.Size)
                .HasMaxLength(5);

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            modelBuilder.Entity<ProductCategory>()
                .ToTable("ProductCategory", "SalesLT")
                .HasKey<int>(e => e.ProductCategoryID)
                .Ignore(e => e.IsDirty);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory1)
                .WithOptional(e => e.ProductCategory2)
                .HasForeignKey(e => e.ParentProductCategoryID);

            modelBuilder.Entity<ProductDescription>()
                .ToTable("ProductDescription", "SalesLT")
                .HasKey<int>(e => e.ProductDescriptionID)
                .Ignore(e => e.IsDirty);

            modelBuilder.Entity<ProductDescription>()
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(400);

            modelBuilder.Entity<ProductDescription>()
                .HasMany(e => e.ProductModelProductDescriptions)
                .WithRequired(e => e.ProductDescription)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductModel>()
                .ToTable("ProductModel", "SalesLT")
                .HasKey<int>(e => e.ProductModelID)
                .Ignore(e => e.IsDirty);

            modelBuilder.Entity<ProductModel>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ProductModel>()
                .Property(e => e.CatalogDescription)
                .HasColumnType("xml");

            modelBuilder.Entity<ProductModel>()
                .HasMany(e => e.ProductModelProductDescriptions)
                .WithRequired(e => e.ProductModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductModelProductDescription>()
                .ToTable("ProductModelProductDescription", "SalesLT")
                .HasKey(e => new { e.ProductModelID, e.ProductDescriptionID, e.Culture })
                .Ignore(e => e.IsDirty);

            modelBuilder.Entity<ProductModelProductDescription>()
                .Property(e => e.ProductModelID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<ProductModelProductDescription>()
                .Property(e => e.ProductDescriptionID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<ProductModelProductDescription>()
                .Property(e => e.Culture)
                .HasColumnOrder(2)
                .HasMaxLength(6)
                .IsFixedLength();

        }

        public override int SaveChanges()
        {
            this.ChangeTracker.Entries()
                .Where(p => p.Entity is IModificationHistory && p.State == EntityState.Modified)
                .Select(p => p.Entity as IModificationHistory)
                .ToList().ForEach(p => p.ModifiedDate = DateTime.Now);
            int result = base.SaveChanges();
            this.ChangeTracker.Entries()
                .Where(p => p.Entity is IModificationHistory)
                .Select(p => p.Entity as IModificationHistory)
                .ToList().ForEach(p => p.IsDirty = false);
            return result;
        }

    }
}
