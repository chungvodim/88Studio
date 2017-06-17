using System.ComponentModel.DataAnnotations.Schema;

namespace Tearc.Data.Entity
{
    //[Table("Sources")]
    public class Source : BaseEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}