using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tearc.Data.Entity
{
    //[Table("Comments")]
    public class Comment : BaseEntity
    {
        public Int64 AdvertID { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
