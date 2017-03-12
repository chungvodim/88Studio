using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _88Studio.Enum
{
    [Serializable]
    public enum UserRole
    {
        [Description("SuperAdmin")]
        SuperAdmin = 1,
        [Description("User")]
        User = 2
    }
}
