using _88Studio.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _88Studio.Entity
{
    [Table("LocalizationResources")]
    public class LocalizationResource
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocalizationResourceID { get; set; }
        [Required, StringLength(5)]
        [Index("IX_UniqueResource", 1, IsUnique = true)]
        public string LanguageCode { get; set; }
        [Index("IX_UniqueResource", 2, IsUnique = true)]
        public LocalizationResourceGroup? Group { get; set; }
        [Required, StringLength(100)]
        [Index("IX_UniqueResource", 3, IsUnique = true)]
        public string Key { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "IsRequired")]
        public string Value { get; set; }
    }
}
