using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tearc.Data.Entity
{
    //[Table("Comments")]
    public class Post : MongoEntity
    {
        public string Content { get; set; } = "Unknown Content";
        public virtual Author Author { get; set; } = new Author();
        public Post() { }
        public Post(string content)
        {
            this.Content = content;
        }
    }
}
