using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class Repository<T, TKey> : IRepository<T, TKey>
         where T : IEntity<TKey>
    {
        protected internal MongoCollection<T> collection;
        public Repository()
            : this(Util<TKey>.GetDefaultConnectionString())
        {
        }
        public Repository(string connectionString)
        {
            this.collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }
        public Repository(string connectionString, string collectionName)
        {
            this.collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }
        public Repository(MongoUrl url)
        {
            this.collection = Util<TKey>.GetCollectionFromUrl<T>(url);
        }
        public Repository(MongoUrl url, string collectionName)
        {
            this.collection = Util<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }
        public MongoCollection<T> Collection
        {
            get { return this.collection; }
        }
        public string CollectionName
        {
            get { return this.collection.Name; }
        }
        public virtual T GetById(TKey id)
        {
            if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                return this.GetById(new ObjectId(id as string));
            }

            return this.collection.FindOneByIdAs<T>(BsonValue.Create(id));
        }
        public virtual T GetById(ObjectId id)
        {
            return this.collection.FindOneByIdAs<T>(id);
        }
        public virtual T Insert(T entity)
        {
            this.collection.Insert<T>(entity);

            return entity;
        }
        public virtual void Insert(IEnumerable<T> entities)
        {
            this.collection.InsertBatch<T>(entities);
        }
        public virtual T Update(T entity)
        {
            this.collection.Save<T>(entity);

            return entity;
        }
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.collection.Save<T>(entity);
            }
        }
        public virtual void Delete(TKey id)
        {
            if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                this.collection.Remove(Query.EQ("_id", new ObjectId(id as string)));
            }
            else
            {
                this.collection.Remove(Query.EQ("_id", BsonValue.Create(id)));
            }
        }
        public virtual void Delete(ObjectId id)
        {
            this.collection.Remove(Query.EQ("_id", id));
        }
        public virtual void Delete(T entity)
        {
            this.Delete(entity.Id);
        }
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (T entity in this.collection.AsQueryable<T>().Where(predicate))
            {
                this.Delete(entity.Id);
            }
        }
        public virtual void DeleteAll()
        {
            this.collection.RemoveAll();
        }

        public virtual long Count()
        {
            return this.collection.Count();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.collection.AsQueryable<T>().Any(predicate);
        }

        /// <summary>
        /// Lets the server know that this thread is about to begin a series of related operations that must all occur
        /// on the same connection. The return value of this method implements IDisposable and can be placed in a using
        /// statement (in which case RequestDone will be called automatically when leaving the using statement). 
        /// </summary>
        /// <returns>A helper object that implements IDisposable and calls RequestDone() from the Dispose method.</returns>
        /// <remarks>
        ///     <para>
        ///         Sometimes a series of operations needs to be performed on the same connection in order to guarantee correct
        ///         results. This is rarely the case, and most of the time there is no need to call RequestStart/RequestDone.
        ///         An example of when this might be necessary is when a series of Inserts are called in rapid succession with
        ///         SafeMode off, and you want to query that data in a consistent manner immediately thereafter (with SafeMode
        ///         off the writes can queue up at the server and might not be immediately visible to other connections). Using
        ///         RequestStart you can force a query to be on the same connection as the writes, so the query won't execute
        ///         until the server has caught up with the writes.
        ///     </para>
        ///     <para>
        ///         A thread can temporarily reserve a connection from the connection pool by using RequestStart and
        ///         RequestDone. You are free to use any other databases as well during the request. RequestStart increments a
        ///         counter (for this thread) and RequestDone decrements the counter. The connection that was reserved is not
        ///         actually returned to the connection pool until the count reaches zero again. This means that calls to
        ///         RequestStart/RequestDone can be nested and the right thing will happen.
        ///     </para>
        ///     <para>
        ///         Use the connectionstring to specify the readpreference; add "readPreference=X" where X is one of the following
        ///         values: primary, primaryPreferred, secondary, secondaryPreferred, nearest.
        ///         See http://docs.mongodb.org/manual/applications/replication/#read-preference
        ///     </para>
        /// </remarks>
        ////public virtual IDisposable RequestStart()
        ////{
        ////    return this.collection.Database.RequestStart();
        ////}

        /// <summary>
        /// Lets the server know that this thread is done with a series of related operations.
        /// </summary>
        /// <remarks>
        /// Instead of calling this method it is better to put the return value of RequestStart in a using statement.
        /// </remarks>
        ////public virtual void RequestDone()
        ////{
        ////    this.collection.Database.RequestDone();
        ////}

        #region IQueryable<T>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }



        public virtual Type ElementType
        {
            get { return this.collection.AsQueryable<T>().ElementType; }
        }
        public virtual Expression Expression
        {
            get { return this.collection.AsQueryable<T>().Expression; }
        }

        public virtual IQueryProvider Provider
        {
            get { return this.collection.AsQueryable<T>().Provider; }
        }
        #endregion
    }

    public class Repository<T> : Repository<T, string>, IRepository<T>
        where T : IEntity<string>
    {
        public Repository()
            : base() { }

        public Repository(MongoUrl url)
            : base(url) { }

        public Repository(MongoUrl url, string collectionName)
            : base(url, collectionName) { }

        public Repository(string connectionString)
            : base(connectionString) { }
        public Repository(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }

    }
}
