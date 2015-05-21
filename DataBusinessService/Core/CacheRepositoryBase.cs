using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataBusinessService.Model;

namespace DataBusinessService.Core
{

    public class CacheBlog : CacheRepositoryBase<Blog, int>
    {

        //Sauvegarde spécifique
        public void SaveData()
        {
        }

        public IQueryable<Blog> GetWithPosts()
        {
            return GetAll().Include(p => p.Posts).AsQueryable();
        }
    }


    public abstract class CacheRepositoryBase<TEntity, TPrimaryKey>

        where TEntity : class
    {
        protected readonly TDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected CacheRepositoryBase(TDbContext context)
        {
            DbContext = context;
            DbContext.Configuration.ProxyCreationEnabled = false;
            DbSet = context.Set<TEntity>();
        }

        protected virtual TDbContext Context
        {
            get { return DbContext; }
        }

        protected virtual DbSet<TEntity> Table
        {
            get { return DbSet; }
        }

        private object GetPropValueById(object src)
        {
            var type = src.GetType();
            return type.GetProperty(type.Name + "Id").GetValue(src, null);
        }

        private int GetId(object src)
        {
            return (int) GetPropValueById(src);
        }

        #region EF

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualExpressionForId(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Table.Add(entity));
        }


        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = Table.Local.FirstOrDefault(ent => GetId(ent) == id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }
        #endregion

        #region base

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            return entity;
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            return entity;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual TEntity FirstOrDefault()
        {
            return GetAll().FirstOrDefault();
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualExpressionForId(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }


        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return GetId(entity) == default(int)
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return GetId(entity) == default(int)
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }


        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }


        public virtual async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
        }


        public virtual async Task DeleteAsync(int id)
        {
            Delete(id);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
        }

        public virtual int Count()
        {
            return GetAll().Count();
        }


        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof (TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof (TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        #endregion
    }
}