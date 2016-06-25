using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Prodigious.Data.Interfaces;

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
                .Property(e => e.StandardCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory1)
                .WithOptional(e => e.ProductCategory2)
                .HasForeignKey(e => e.ParentProductCategoryID);

            modelBuilder.Entity<ProductDescription>()
                .HasMany(e => e.ProductModelProductDescriptions)
                .WithRequired(e => e.ProductDescription)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductModel>()
                .HasMany(e => e.ProductModelProductDescriptions)
                .WithRequired(e => e.ProductModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductModelProductDescription>()
                .Property(e => e.Culture)
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
