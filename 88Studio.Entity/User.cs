using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _88Studio.Entity
{
    [Table("Users")]
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string Gender { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public bool ProtoolConsent { get; set; }
        public bool MarketingConsent { get; set; }

        public Enum.UserStatus UserStatus { get; set; }

        public int? CreatedUserID { get; set; }
        public int? UpdatedUserId { get; set; }

        public virtual Locale Locale { get; set; }

        public User()
        {
        }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, FullName));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.FirstName, FirstName));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.LastName, LastName));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.Locale, LocaleID.ToString()));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.Company, CompanyID.ToString()));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.Branch, BranchID.ToString()));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.ProtoolConsent, ProtoolConsent.ToString()));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.MarketingConsent, MarketingConsent.ToString()));

            return userIdentity;
        }
    }

    public class CustomClaimTypes
    {
        public const string Locale = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Locale";
        public const string Company = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Company";
        public const string Branch = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Branch";
        public const string FirstName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/FirstName";
        public const string LastName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/LastName";
        public const string ProtoolConsent = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/ProtoolConsent";
        public const string MarketingConsent = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/MarketingConsent";
    }
}
