using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _88Studio.Entity
{
    [Table("Roles")]
    public class Role : IdentityRole<int, UserRole>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get { return base.Id; } set { base.Id = value; } }
        [StringLength(2000)]
        public string Description { get; set; }
    }
}
