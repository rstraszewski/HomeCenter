using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.CommonDomain
{
    public interface IGenericRepository<T> where T : class
    {
        string getConnectionString();
        void Save();
        void RollbackChanges();
    }
}
