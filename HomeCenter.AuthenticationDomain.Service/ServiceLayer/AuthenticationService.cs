using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.AuthenticationDomain.Model.DomainLayer.Enums;
using HomeCenter.HistoryDomain;
using Microsoft.Win32;

namespace HomeCenter.AuthenticationDomain
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserRepository _userRepository;
        private readonly IDomainLoggerService _domainLoggerService;

        public AuthenticationService(IUserRepository userRepository,
            IDomainLoggerService domainLoggerService)
        {
            _userRepository = userRepository;
            _domainLoggerService = domainLoggerService;
        }

        public bool IsUserValid(UserDtoInput user)
        {
            var userEntity = _userRepository.GetAllUsers().FirstOrDefault(u => u.Username == user.Username);
            if (userEntity != null)
                return userEntity.IsPasswordMatch(user.Password);

            return false;
        }

        public string GetRolesForUser(string username)
        {
            var roles = string.Empty;
            try
            {
                var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    roles = user.Roles;
                }
            }
            catch (Exception)
            {
            }
            return roles;
        }

        public bool CreateUser(UserDtoInput model, string operatorUsername)
        {

            try
            {
                if (UserExists(model.Username))
                    return false;

                var user = new User(model.Password, model.Username);
                _userRepository.AddUser(user);
                _userRepository.Save();
                _domainLoggerService.Publish(operatorUsername, string.Format("User with username \"{0}\" has been created", model.Username));
                return true;
            }
            catch (Exception e)
            {
           
            }

            return false;

        }

        private bool UserExists(string username)
        {
            var user = _userRepository.GetUser(username);
            return user != null;
        }

        public bool CheckIfAlreadyExist(string username)
        {
            return _userRepository.GetAllUsers().Any(user => user.Username == username);
        }

        public string[] GetRoles()
        {
            return Enum.GetNames(typeof(AuthenticationRoles));
        }

        public IEnumerable<string> GetAllUsernames()
        {
            var userList = new List<string>();
            try
            {
                userList = _userRepository.GetAllUsers().Select(user => user.Username).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
            }
            return userList;
        }

        public void ChangeRoles(string username, string rolesString, string operatorUsername, string roomAccess)
        {
            try
            {
                var user = _userRepository.GetUser(username);
                user.Roles = rolesString;
                user.RoomAccess = rolesString.Contains("User") && !rolesString.Contains("Administrator") ? roomAccess : "";
                _userRepository.UpdateUser(user);
                _userRepository.Save();
                _domainLoggerService.Publish(operatorUsername, string.Format("Roles and access for user {0} has been changed", username));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public string GetRoomAccessForUser(string username)
        {
            return _userRepository.GetUser(username).RoomAccess;
        }
    }

    public interface IAuthenticationService
    {
        bool IsUserValid(UserDtoInput user);
        string GetRolesForUser(string username);
        bool CreateUser(UserDtoInput model, string operatorUsername);
        bool CheckIfAlreadyExist(string username);
        string[] GetRoles();
        IEnumerable<string> GetAllUsernames();
        void ChangeRoles(string username, string rolesString, string operatorUsername, string roomAccessString);
        string GetRoomAccessForUser(string username);
    }
}
