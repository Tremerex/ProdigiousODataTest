using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.DataModel
{
    public abstract class RepositoryBase<T, R, C> : IRepository<T, R>
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

        public T Insert(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public void Delete(R id)
        {
            T entity = _context.Set<T>().Find(id);
            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if(_context != null)
            _context.Dispose();
        }

    }
}
