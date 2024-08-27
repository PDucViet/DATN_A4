namespace DATN.Core.Infrastructures
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Change state of entity to added
        /// </summary>
        /// <param name="entity"></param>
        Task Create(TEntity entity);

        /// <summary>
        ///  Change state of entities to added
        /// </summary>
        /// <param name="entities"></param>
        Task CreateRange(List<TEntity> entities);

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Change state of entity to deleted
        /// </summary>
        /// <param name="entity"></param>
        Task Delete(params object[] ids);


        /// <summary>
        /// Change state of entity to modified
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Change state of entities to modified
        /// </summary>
        /// <param name="entity"></param>
        void UpdateRange(List<TEntity> entities);

        /// <summary>
        /// Get all <paramref name="TEntity"></paramref> from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetById(params object[] primaryKey);

        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
    }
}
