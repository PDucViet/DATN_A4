using DATN.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Infrastructures
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DATNDbContext Context;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(DATNDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        public async Task Create(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            //Context.Entry<TEntity>(entity).State = EntityState.Added;
        }

        public async Task CreateRange(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            // Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(params object[] ids)
        {
            var entity = await DbSet.FindAsync(ids);
            if (entity == null)
                throw new ArgumentException($"{string.Join(";", ids)} not exist in the {typeof(TEntity).Name} table");
            DbSet.Remove(entity);
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }
        public async Task<TEntity> GetById(params object[] primaryKey)
        {
            return await DbSet.FindAsync(primaryKey);
        }

        public IEnumerable<TEntity> GetPaging(IOrderedEnumerable<TEntity> orderBy, int currentPage = 1, int pageSize = 10, string filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
            // Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void UpdateRange(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }


    }
}
