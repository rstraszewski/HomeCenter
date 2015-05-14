using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.CommonDomain;

namespace HomeCenter.AuthenticationDomain
{
    public interface IUserRepository: IGenericRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        User GetUser(string username);
        void UpdateUser(User user);
    }
}
