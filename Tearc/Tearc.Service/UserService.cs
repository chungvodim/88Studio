using Tearc.Data;
using Tearc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tearc.Service
{
    public class UserService: IUserService
    {
        private IRepository<ApplicationUser > userRepository;
        private IRepository<UserProfile> userProfileRepository;

        public UserService(IRepository<ApplicationUser > userRepository, IRepository<UserProfile> userProfileRepository)
        {
            this.userRepository = userRepository;
            this.userProfileRepository = userProfileRepository;
        }

        public IEnumerable<ApplicationUser > GetUsers()
        {
            return userRepository.GetAll();
        }

        public ApplicationUser  GetUser(long id)
        {
            return userRepository.Get(id);
        }

        public void InsertUser(ApplicationUser  user)
        {
            userRepository.Insert(user);
        }
        public void UpdateUser(ApplicationUser  user)
        {
            userRepository.Update(user);
        }

        public void DeleteUser(long id)
        {            
            UserProfile userProfile = userProfileRepository.Get(id);
            userProfileRepository.Remove(userProfile);
            ApplicationUser  user = GetUser(id);
            userRepository.Remove(user);
            userRepository.SaveChanges();
        }
    }
}
