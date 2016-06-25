using Prodigious.DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.DataModel
{
    public abstract class RepositoryBase<T, R, C> : IRepository<T, R>, IDisposable
        where T: class
        where C: DbContext, new()
    {

        C _context = null;

        public RepositoryBase()
        {
            _context = new C();
            _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public T Get(R id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T Insert(T Entity)
        {
            _context.Entry<T>(Entity).State = EntityState.Added;
            _context.SaveChanges();
            return Entity;
        }

        public T Update(T Entity)
        {
            _context.Entry<T>(Entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Entity;
        }

        public void Delete(R Id)
        {
            T Entity = _context.Set<T>().Find(Id);
            _context.Entry<T>(Entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if(_context != null)
            _context.Dispose();
        }

    }
}
