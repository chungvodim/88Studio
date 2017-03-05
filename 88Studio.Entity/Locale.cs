using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _88Studio.Entity
{
    [Table("Locales")]
    public class Locale
    {
        public const string DEFAULT_LOCALE = _88Studio.Resource.LanguageCode.Nl2dehandsCulture;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocaleID { get; set; }
        [Required, StringLength(100)]
        public string LocaleName { get; set; }
        [Required, StringLength(10)]
        public string LanguageCode { get; set; }
    }
}
