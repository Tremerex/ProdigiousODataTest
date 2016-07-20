using System;
using System.Collections.Generic;

namespace Prodigious.Data
{

    public partial class ProductCategory : IModificationHistory
    {

        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory1 { get; set; }

        public virtual ProductCategory ProductCategory2 { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDirty { get; set; }


    }
}
