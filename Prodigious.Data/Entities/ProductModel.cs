using System;
using System.Collections.Generic;

namespace Prodigious.Data
{

    public partial class ProductModel : IModificationHistory
    {

        public int ProductModelID { get; set; }

        public string Name { get; set; }

        public string CatalogDescription { get; set; }

        public Guid rowguid { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDirty { get; set; }

    }
}
