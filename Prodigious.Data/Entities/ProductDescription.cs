using System;
using System.Collections.Generic;

namespace Prodigious.Data
{

    public partial class ProductDescription : IModificationHistory
    {

        public int ProductDescriptionID { get; set; }

        public string Description { get; set; }

        public Guid rowguid { get; set; }

        public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDirty { get; set; }

    }
}
