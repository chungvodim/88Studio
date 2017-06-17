using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;

namespace Tearc.Repository
{
    public class MongoRepository : IMongoRepository
    {
        protected IMongoDatabase _dbContext;

        public MongoRepository(IMongoDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        #region MongoSpecific

        /// <summary>
        /// mongo collection
        /// </summary>
        public IMongoCollection<TEntity> Collection<TEntity>() where TEntity : class
        {
            return _dbContext.GetCollection<TEntity>(CollectionNameAttribute.GetCollectionName<TEntity>());
        }

        /// <summary>
        /// filter for collection
        /// </summary>
        public FilterDefinitionBuilder<TEntity> Filter<TEntity>() where TEntity : class
        {
            return Builders<TEntity>.Filter;
        }

        /// <summary>
        /// projector for collection
        /// </summary>
        public ProjectionDefinitionBuilder<TEntity> Project<TEntity>() where TEntity : class
        {
            return Builders<TEntity>.Projection;
        }

        /// <summary>
        /// updater for collection
        /// </summary>
        public UpdateDefinitionBuilder<TEntity> Updater<TEntity>() where TEntity : class
        {
            return Builders<TEntity>.Update;
        }

        /// <summary>
        /// sorter for collection
        /// </summary>
        public SortDefinitionBuilder<TEntity> Sorter<TEntity>() where TEntity : class
        {
            return Builders<TEntity>.Sort;
        }

        private FilterDefinition<TEntity> FilterById<TEntity>(object id) where TEntity : class
        {
            return Filter<TEntity>().Eq("_id", ObjectId.Parse(id.ToString()));
        }

        private FilterDefinition<TEntity> FilterByIds<TEntity>(IEnumerable<object> ids) where TEntity : class
        {
            return Filter<TEntity>().AnyIn("_id", ids.Select(x => ObjectId.Parse(x.ToString())));
        }

        #endregion MongoSpecific

        public virtual IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class
        {
            return Collection<TEntity>().AsQueryable();
        }

        public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return (int)Collection<TEntity>().Count(predicate);
        }

        public async virtual Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return (int)await Collection<TEntity>().CountAsync(predicate);
        }

        public virtual TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return Collection<TEntity>().Find(predicate).FirstOrDefault();
        }

        public virtual TEntity FindById<TEntity>(object id)
            where TEntity : class
        {
            return Collection<TEntity>().Find(FilterById<TEntity>(id)).FirstOrDefault();
        }

        public async virtual Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return await Collection<TEntity>().Find(predicate).FirstOrDefaultAsync();
        }

        public async virtual Task<TEntity> FindByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            return await Collection<TEntity>().Find<TEntity>(FilterById<TEntity>(id)).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return Collection<TEntity>().AsQueryable().Where(predicate);
        }

        public bool Contain<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return Collection<TEntity>().Find(predicate).Any();
        }

        public async Task<bool> ContainAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return await Collection<TEntity>().Find(predicate).AnyAsync();
        }

        public virtual void Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            Collection<TEntity>().InsertOne(entity);
        }

        public virtual void CreateMany<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            Collection<TEntity>().InsertMany(entities);
        }

        public async virtual Task CreateAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            await Collection<TEntity>().InsertOneAsync(entity);
        }

        public async virtual Task CreateManyAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            await Collection<TEntity>().InsertManyAsync(entities);
        }

        public virtual int DeleteById<TEntity>(object id)
            where TEntity : class
        {
            return (int)Collection<TEntity>().DeleteOne(FilterById<TEntity>(id)).DeletedCount;
        }

        public virtual int DeleteMany<TEntity>(IEnumerable<object> ids)
            where TEntity : class
        {
            return (int)Collection<TEntity>().DeleteMany(FilterByIds<TEntity>(ids)).DeletedCount;
        }

        public virtual int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return (int)Collection<TEntity>().DeleteMany(predicate).DeletedCount;
        }

        public async virtual Task<int> DeleteByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            return (int)(await Collection<TEntity>().DeleteOneAsync(FilterById<TEntity>(id))).DeletedCount;
        }

        public async virtual Task<int> DeleteManyAsync<TEntity>(IEnumerable<object> ids)
            where TEntity : class
        {
            return (int)(await Collection<TEntity>().DeleteManyAsync(FilterByIds<TEntity>(ids))).DeletedCount;
        }

        public async virtual Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return (int)(await Collection<TEntity>().DeleteManyAsync(predicate)).DeletedCount;
        }

        public virtual int Update<TEntity>(object id, TEntity entity)
            where TEntity : class
        {
            return (int)Collection<TEntity>().ReplaceOne(FilterById<TEntity>(id), entity).ModifiedCount;
        }

        public async virtual Task<int> UpdateAsync<TEntity>(object id, TEntity entity)
            where TEntity : class
        {
            return (int)(await Collection<TEntity>().ReplaceOneAsync(FilterById<TEntity>(id), entity)).ModifiedCount;
        }

        public void UpdatRelationship<TEntity, TRelationship>(object entityId, Expression<Func<TRelationship, bool>> relationShipFilter, Func<TEntity, ICollection<TRelationship>> relationshipSelector)
            where TEntity : class
            where TRelationship : class
        {
            throw new NotImplementedException();
        }

        public Task UpdatRelationshipAsync<TEntity, TRelationship>(object entityId, Expression<Func<TRelationship, bool>> relationShipFilter, Func<TEntity, ICollection<TRelationship>> relationshipSelector)
            where TEntity : class
            where TRelationship : class
        {
            throw new NotImplementedException();
        }

        public virtual int ExecuteNonQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual TResult ExecuteReader<TResult>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
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
                _dbContext = null;
            }
        }

        public void BeginAuditLog()
        {
            throw new NotImplementedException();
        }

        public string GetLastLog()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Attribute used to annotate Enities with to override mongo collection name. By default, when this attribute
    /// is not specified, the classname will be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the CollectionName class attribute with the desired name.
        /// </summary>
        /// <param name="value">Name of the collection.</param>
        public CollectionNameAttribute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Empty collection name is not allowed", "value");
            Name = value;
        }

        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        /// <value>The name of the collection.</value>
        public virtual string Name { get; private set; }


        /// <summary>
        /// Determines the collection name for TEntity and assures it is not empty
        /// </summary>
        /// <typeparam name="TEntity">The type to determine the collection name for.</typeparam>
        /// <returns>Returns the collection name for TEntity.</returns>
        public static string GetCollectionName<TEntity>() where TEntity : class
        {
            string collectionName;
            collectionName = typeof(TEntity).BaseType.Equals(typeof(object)) ?
                                      GetCollectionNameFromInterface<TEntity>() :
                                      GetCollectionNameFromType<TEntity>();

            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(TEntity).Name;
            }
            return collectionName.ToLowerInvariant();
        }

        /// <summary>
        /// Determines the collection name from the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type to get the collection name from.</typeparam>
        /// <returns>Returns the collection name from the specified type.</returns>
        public static string GetCollectionNameFromInterface<TEntity>() where TEntity : class
        {
            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(TEntity), typeof(CollectionNameAttribute));

            return att != null ? ((CollectionNameAttribute)att).Name : typeof(TEntity).Name;
        }

        /// <summary>
        /// Determines the collectionname from the specified type.
        /// </summary>
        /// <param name="entitytype">The type of the entity to get the collectionname from.</param>
        /// <returns>Returns the collectionname from the specified type.</returns>
        public static string GetCollectionNameFromType<TEntity>() where TEntity : class
        {
            Type entitytype = typeof(TEntity);
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionNameAttribute));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionNameAttribute)att).Name;
            }
            else
            {
                //if (typeof(Entity).IsAssignableFrom(entitytype))
                //{
                //    // No attribute found, get the basetype
                //    while (!entitytype.BaseType.Equals(typeof(Entity)))
                //    {
                //        entitytype = entitytype.BaseType;
                //    }
                //}
                collectionname = entitytype.Name;
            }

            return collectionname;
        }
    }

    public class MongoDatabaseFactory
    {
        public static IMongoDatabase Create(string connectionName = "Tearc")
        {
            var url = new MongoUrl(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);

            return db;
        }
    }
}
