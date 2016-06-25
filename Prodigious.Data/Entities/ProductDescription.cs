using Prodigious.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Prodigious.Data
{

    [Table("SalesLT.ProductDescription")]
    public partial class ProductDescription : IModificationHistory
    {

        public ProductDescription()
        {
            ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
        }

        public int ProductDescriptionID { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; }

        public Guid rowguid { get; set; }

        public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public bool IsDirty { get; set; }

    }
}
