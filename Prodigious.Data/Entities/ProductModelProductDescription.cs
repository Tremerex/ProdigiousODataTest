using System;
using System.Collections.Generic;

namespace Prodigious.Data
{

    public partial class ProductModelProductDescription : IModificationHistory
    {

        public int ProductModelID { get; set; }

        public int ProductDescriptionID { get; set; }

        public string Culture { get; set; }

        public Guid rowguid { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }

        public virtual ProductModel ProductModel { get; set; }

        public DateTime ModifiedDate { get; set; }

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
