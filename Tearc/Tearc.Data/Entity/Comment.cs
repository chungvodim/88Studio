using System;
using System.Collections.Generic;
using System.Text;

namespace Tearc.Data.Entity
{
    public class Comment : BaseEntity
    {
        public Int64 AdvertID { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
