﻿using Prodigious.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.DataModel
{
    public interface IProductRepository : IRepository<Product, int> 
    {
    }
}
