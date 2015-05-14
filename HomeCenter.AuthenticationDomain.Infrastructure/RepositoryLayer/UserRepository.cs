using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.AuthenticationDomain
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext contextDb) : base(contextDb)
        {
        }


        public IEnumerable<User> GetAllUsers()
        {
            return _set;
        }

        public void AddUser(User user)
        {
            _set.Add(user);
        }

        public User GetUser(string username)
        {
            return _set.FirstOrDefault(user => user.Username == username);
        }

        public void UpdateUser(User user)
        {
            _set.Add(user);
            _contextDb.Entry(user).State = EntityState.Modified;
        }
    }

    
}
