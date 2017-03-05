using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace _88Studio.Entity
{
    public class UserRole: IdentityUserRole<int>
    {
        [ForeignKey("Role")]
        public override int RoleId { get; set; }
        [ForeignKey("User")]
        public override int UserId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
