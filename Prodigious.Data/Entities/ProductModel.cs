using Prodigious.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Prodigious.Data
{

    [Table("SalesLT.ProductModel")]
    public partial class ProductModel : IModificationHistory
    {

        public ProductModel()
        {
            Products = new HashSet<Product>();
            ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
        }

        public int ProductModelID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "xml")]
        public string CatalogDescription { get; set; }

        public Guid rowguid { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public bool IsDirty { get; set; }

    }
}
