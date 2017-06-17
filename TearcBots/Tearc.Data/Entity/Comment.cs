using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tearc.Data.Entity
{
    //[Table("Comments")]
    public class Comment : MongoEntity
    {
        public Int64 AdvertID { get; set; }
        public string Content { get; set; } = "Unknown Title";
        public virtual Author Author { get; set; } = new Author();
    }
}
