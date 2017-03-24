using Tearc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tearc.Service
{
    public interface IUserProfileService
    {
        UserProfile GetUserProfile(long id);
    }
}
