using System;
using System.Collections;
using System.Data.Entity;

namespace DataBusinessService.Core
{
    public class UnitOfWork<TDbContext> : IDisposable
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly Hashtable _repositories;

        public UnitOfWork(TDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public TEntity Repository<TEntity>()
            where TEntity : class
        {
            var type = typeof (TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                //var repositoryType = typeof(EntityRepository<CContext, TEntity>);
                var repositoryType = typeof (TEntity);

                //var repositoryInstance = Activator.CreateInstance(
                //    repositoryType.MakeGenericType(typeof(TEntity))
                // );
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (TEntity) _repositories[type];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual TEntity SaveChanges<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual TEntity Insert<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Dispose

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free managed objects here.
            }

            _context.Dispose();
            _disposed = true;
        }

        #endregion
    }
}