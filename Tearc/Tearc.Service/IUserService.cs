using Tearc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tearc.Service
{
  public  interface IUserService
    {
        IEnumerable<ApplicationUser > GetUsers();
        ApplicationUser  GetUser(long id);
        void InsertUser(ApplicationUser  user);
        void UpdateUser(ApplicationUser  user);
        void DeleteUser(long id);
    }
}
