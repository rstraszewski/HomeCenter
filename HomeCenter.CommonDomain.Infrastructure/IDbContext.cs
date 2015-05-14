using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HomeCenter.CommonDomain
{


    public interface IDbContext : IObjectContextAdapter
    {
        DbChangeTracker ChangeTracker { get; } // Entity change tracker from EF
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T o) where T : class;
        //ObjectStateManager ObjectStateManager { get; }
        void Dispose();
        int SaveChanges();
        void Rollback();
        void CreateDatabaseIfNotExisting();
        void EnableLazyLoading();
    }
}