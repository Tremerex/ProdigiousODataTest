using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.Core.Common.Contracts
{
    public interface IReadOnlyRepository<T, R> 
        where T : class
    {

        T Get(R Id);

        IQueryable<T> Get();

    }
}
