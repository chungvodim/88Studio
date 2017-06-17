using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tearc.Repository
{
    public class EntityFrameWorkRepository : IRepository
    {
        protected ApplicationDbContext _dbContext;

        public EntityFrameWorkRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().Count(predicate);
        }

        public async virtual Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public virtual TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual TEntity FindById<TEntity>(object id)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public async virtual Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async virtual Task<TEntity> FindByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public bool Contain<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().Any(predicate);
        }

        public async Task<bool> ContainAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public virtual void Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);

            _dbContext.SaveChanges();
        }

        public virtual void CreateMany<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().AddRange(entities);

            _dbContext.SaveChanges();
        }

        public async virtual Task CreateAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task CreateManyAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual int DeleteById<TEntity>(object id)
            where TEntity : class
        {
            var entity = FindById<TEntity>(id);

            _dbContext.Set<TEntity>().Remove(entity);

            return _dbContext.SaveChanges();
        }

        public virtual int DeleteMany<TEntity>(IEnumerable<object> ids)
            where TEntity : class
        {
            foreach (var id in ids)
            {
                var entity = FindById<TEntity>(id);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                _dbContext.Set<TEntity>().Remove(entity);
            }
            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var entities = Filter<TEntity>(predicate).ToArray();

            foreach (var entity in entities)
            {
                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return _dbContext.SaveChanges();
        }

        public async virtual Task<int> DeleteByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            var entity = await FindByIdAsync<TEntity>(id);

            _dbContext.Set<TEntity>().Remove(entity);

            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteManyAsync<TEntity>(IEnumerable<object> ids)
            where TEntity : class
        {
            foreach (var id in ids)
            {
                var entity = await FindByIdAsync<TEntity>(id);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                _dbContext.Set<TEntity>().Remove(entity);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var entities = await Filter<TEntity>(predicate).ToArrayAsync();

            foreach (var entity in entities)
            {
                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public virtual int Update<TEntity>(object id, TEntity entity)
            where TEntity : class
        {
            var entry = _dbContext.Entry(entity);

            return _dbContext.SaveChanges();
        }

        public void UpdatRelationship<TEntity, TRelationship>(object entityId, Expression<Func<TRelationship, bool>> relationShipFilter, Func<TEntity, ICollection<TRelationship>> relationshipSelector)
            where TEntity : class
            where TRelationship : class
        {
            var entity = FindById<TEntity>(entityId);

            var filteredRelationship = Filter(relationShipFilter).ToList();

            var existingRelationship = relationshipSelector.Invoke(entity);

            var removingRelationship = new List<TRelationship>();
            var newRelationship = new List<TRelationship>();

            foreach (var relationship in existingRelationship)
            {
                if (!filteredRelationship.Contains(relationship))
                {
                    removingRelationship.Add(relationship);
                }
            }
            foreach (var relationship in filteredRelationship)
            {
                if (!existingRelationship.Contains(relationship))
                {
                    newRelationship.Add(relationship);
                }
            }

            removingRelationship.ForEach(x => existingRelationship.Remove(x));
            newRelationship.ForEach(x => existingRelationship.Add(x));

            _dbContext.SaveChanges();
        }

        public async Task UpdatRelationshipAsync<TEntity, TRelationship>(object entityId, Expression<Func<TRelationship, bool>> relationShipFilter, Func<TEntity, ICollection<TRelationship>> relationshipSelector)
            where TEntity : class
            where TRelationship : class
        {
            var entity = await FindByIdAsync<TEntity>(entityId);

            var filteredRelationship = await Filter(relationShipFilter).ToListAsync();

            var existingRelationship = relationshipSelector.Invoke(entity);

            var removingRelationship = new List<TRelationship>();
            var newRelationship = new List<TRelationship>();

            foreach (var relationship in existingRelationship)
            {
                if (!filteredRelationship.Contains(relationship))
                {
                    removingRelationship.Add(relationship);
                }
            }
            foreach (var relationship in filteredRelationship)
            {
                if (!existingRelationship.Contains(relationship))
                {
                    newRelationship.Add(relationship);
                }
            }

            removingRelationship.ForEach(x => existingRelationship.Remove(x));
            newRelationship.ForEach(x => existingRelationship.Add(x));

            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> UpdateAsync<TEntity>(object id, TEntity entity)
            where TEntity : class
        {
            var entry = _dbContext.Entry(entity);

            return await _dbContext.SaveChangesAsync();
        }

        public virtual int ExecuteNonQuery(string query, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlCommand(query, parameters);
        }

        public virtual TResult ExecuteReader<TResult>(string query, params object[] parameters)
        {
            return _dbContext.Database.SqlQuery<TResult>(query, parameters).FirstOrDefault();
        }

        public TDbContext GetDbContext<TDbContext>()
            where TDbContext : class
        {
            return this._dbContext as TDbContext;
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }

    public interface ICascadeDelete
    {
        void OnDelete();
    }
}
