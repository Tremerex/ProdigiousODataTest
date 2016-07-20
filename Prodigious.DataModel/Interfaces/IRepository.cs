using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.DataModel
{
    public interface IRepository<T, R> : IReadOnlyRepository<T, R>, IDisposable
        where T: class
    {

        T Insert(T Entity);

        T Update(T Entity);

        void Delete(R Id);

    }
}
