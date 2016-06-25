using Prodigious.Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.Core.Common.Data
{
    public abstract class RepositoryBase<T, R, C> : IRepository<T, R>
        where T: class
        where C: DbContext, new()
    {

        public RepositoryBase()
        {
        }

        public T Get(R id)
        {
            using (C context = new C())
            {
                return context.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> Get()
        {
            using (C context = new C())
            {
                return context.Set<T>().ToList();
            }
        }

        public T Insert(T Entity)
        {
            using (C context = new C())
            {
                context.Entry<T>(Entity).State = EntityState.Added;
                context.SaveChanges();
                return Entity;
            }
        }

        public T Update(T Entity)
        {
            using (C context = new C())
            {
                context.Entry<T>(Entity).State = EntityState.Modified;
                context.SaveChanges();
                return Entity;
            }
        }

        public void Delete(R Id)
        {
            using (C context = new C())
            {
                T Entity = context.Set<T>().Find(Id);
                context.Entry<T>(Entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}
