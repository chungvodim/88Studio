using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _88Studio.Resource;
using BGP.Utils.Common;

namespace _88Studio.Enum
{
    [Serializable]
    public enum UserStatus : byte
    {
        [EnumInformation(Value = "All", ResourceName = "AllStatus", ResourceType = typeof(LabelResource))]
        [IgnoredEnum]
        All = 0,
        [EnumInformation(Value = "Active", ResourceName = "Active", ResourceType = typeof(LabelResource))]
        Active = 1,
        [EnumInformation(Value = "Inactive", ResourceName = "Inactive", ResourceType = typeof(LabelResource))]
        Inactive = 2
    }
}
