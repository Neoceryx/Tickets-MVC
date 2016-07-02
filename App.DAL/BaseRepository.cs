using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DivisaxDBLayer.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        #region Private Members
        protected DbContext Context { get; set; }
        #endregion

        #region Protected Properties
        protected DbSet<TEntity> EntityDbSet { get; set; }
        #endregion

        #region Constructor
        public BaseRepository(DivisaxDbContext context)
        {
            Context = context;
            Context.Configuration.LazyLoadingEnabled = false;
            Context.Configuration.ProxyCreationEnabled = false;
            EntityDbSet = context.Set<TEntity>();
        }
        #endregion

        #region Public Instance Methods
        public virtual IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = EntityDbSet;
            return query.AsNoTracking().ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            EntityDbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = EntityDbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                EntityDbSet.Attach(entityToDelete);
            }
            EntityDbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State != EntityState.Detached)
            {
                Context.Entry(entityToUpdate).State = EntityState.Detached;
            }

            EntityDbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            var t=Context.Database.SqlQuery<TEntity>(query, parameters).ToList();
            return t;
        }
        #endregion
    }
}
