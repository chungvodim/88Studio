using System;
using System.Collections.Generic;
using System.Text;
using Tearc.Core.Interfaces;

namespace Tearc.Core.Implementations
{
    public class MachineClockDateTime : IDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
