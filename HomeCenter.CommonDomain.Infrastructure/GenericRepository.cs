using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using HomeCenter.Common;
//using HomeCenter.DeviceDomain.Model;

namespace HomeCenter.CommonDomain
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class, IAggregateRoot
    {
        public const string connectionString = "Data Source=localhost;Initial Catalog=HomeCenterDb;Uid=root;Port=3306;";

        protected readonly IDbContext _contextDb;
        protected readonly IDbSet<T> _set;
        public GenericRepository(IDbContext contextDb)
        {
            _contextDb = contextDb;
            _set = _contextDb.Set<T>();
            _contextDb.EnableLazyLoading();
        }

        //public void Add(T entity)
        //{
        //    _contextDb.Set<T>().Add(entity);
        //}

        //public void AddRange(IEnumerable<T> entities)
        //{
        //    _contextDb.Set<T>().AddRange(entities);
        //}

        //public void Delete(T entity)
        //{
        //    _contextDb.Set<T>().Remove(entity);
        //}



        //public IList<T> GetAsList()
        //{
        //    var data = _contextDb.Set<T>().ToList();
        //    return data;
        //}
        //public T Get(long id)
        //{
        //    var data = _contextDb.Set<T>().FirstOrDefault(item => item.Id.Equals(id));
        //    return data;
        //}


        //public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        //{
        //    var data = _contextDb.Set<T>().ToList();
        //    return data;
        //}
        public string getConnectionString()
        {
            return connectionString;
        }

        public void Save()
        {
            _contextDb.SaveChanges();
        }

        public void RollbackChanges()
        {
            _contextDb.Rollback();
        }
    }

}
