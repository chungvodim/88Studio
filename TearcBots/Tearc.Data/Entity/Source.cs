using System.ComponentModel.DataAnnotations.Schema;

namespace Tearc.Data.Entity
{
    //[Table("Sources")]
    public class Source : BaseEntity
    {
        public string Name { get; set; } = "Vozforums";
        public string URL { get; set; } = "https://vozforums.com/forumdisplay.php?f=68";
    }
}