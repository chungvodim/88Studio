using System;

namespace Tearc.Data.Entity
{
    public class Author : BaseEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? JoinDate { get; set; }
        public Source Source { get; set; }
        public int Rating { get; set; }
    }
}