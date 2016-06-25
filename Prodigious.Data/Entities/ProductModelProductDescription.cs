using Prodigious.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Prodigious.Data
{

    [Table("SalesLT.ProductModelProductDescription")]
    public partial class ProductModelProductDescription : IModificationHistory
    {

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductModelID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductDescriptionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string Culture { get; set; }

        public Guid rowguid { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }

        public virtual ProductModel ProductModel { get; set; }

        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public bool IsDirty { get; set; }

        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;
            ProductModelProductDescription t = obj as ProductModelProductDescription;
            if(t == null)
                return false;
            return this.ProductModelID == t.ProductModelID 
                && this.ProductDescriptionID == t.ProductDescriptionID 
                && this.Culture.CompareTo(t.Culture) == 1;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash += 379 * this.ProductModelID.GetHashCode();
            hash += 379 * this.ProductDescriptionID.GetHashCode();
            hash += 379 * this.Culture.GetHashCode();
            return hash;
        }

    }
}
