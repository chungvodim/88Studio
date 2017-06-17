using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tearc.Data.Entity
{
    //[Table("Authors")]
    public class Author : BaseEntity
    {
        public string UserName { get; set; } = "chungvodim";
        public string Name { get; set; } = "chungvodim";
        public DateTime? DOB { get; set; } = DateTime.UtcNow;
        public DateTime? JoinDate { get; set; } = DateTime.UtcNow;
        public virtual Source Source { get; set; } = new Source();
        public int Rating { get; set; } = 5;
    }
}