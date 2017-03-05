using System.Security.Claims;
using System.Security.Principal;
using _88Studio.Entity;
using _88Studio.Utils.Common;

namespace _88Studio.Web
{
    public static class IdentityExtensions
    {
        public static string GetGivenName(this IIdentity identity)
        {
            if (identity == null)
                return null;

            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.GivenName);
        }

        public static string GetFirstName(this IIdentity identity)
        {
            return (identity as ClaimsIdentity)?.FirstOrNull(CustomClaimTypes.FirstName);
        }

        public static string GetLastName(this IIdentity identity)
        {
            return (identity as ClaimsIdentity)?.FirstOrNull(CustomClaimTypes.LastName);
        }

        public static int GetLocaleID(this IIdentity identity)
        {
            if (identity == null)
                return 0;

            return (identity as ClaimsIdentity).FirstOrDefault<int>(CustomClaimTypes.Locale);
        }

        public static int GetCompanyID(this IIdentity identity)
        {
            if (identity == null)
                return 0;

            return (identity as ClaimsIdentity).FirstOrDefault<int>(CustomClaimTypes.Company);
        }

        public static int GetBranchID(this IIdentity identity)
        {
            if (identity == null)
                return 0;

            return (identity as ClaimsIdentity).FirstOrDefault<int>(CustomClaimTypes.Branch);
        }

        public static bool? GetProtoolConsent(this IIdentity identity)
        {
            return (identity as ClaimsIdentity)?.FirstOrDefault<bool>(CustomClaimTypes.ProtoolConsent);
        }

        public static bool? GetMarketingConsent(this IIdentity identity)
        {
            return (identity as ClaimsIdentity)?.FirstOrDefault<bool>(CustomClaimTypes.MarketingConsent);
        }

        public static void UpdateProtoolAndMakertingConstent(this IIdentity identity)
        {
            var claimsIdentity = (identity as ClaimsIdentity);
            if (claimsIdentity != null)
            {
                var protoolClaim = claimsIdentity.FindFirst(CustomClaimTypes.ProtoolConsent);
                var marketingClaim = claimsIdentity.FindFirst(CustomClaimTypes.MarketingConsent);

                if (protoolClaim != null)
                    claimsIdentity.RemoveClaim(protoolClaim);

                if (marketingClaim != null)
                    claimsIdentity.RemoveClaim(marketingClaim);

                claimsIdentity.AddClaim(new Claim(CustomClaimTypes.ProtoolConsent, "true"));
                claimsIdentity.AddClaim(new Claim(CustomClaimTypes.MarketingConsent, "true"));
            }
        }

        //public static int GetAgencyID(this IIdentity identity)
        //{
        //    if (identity == null)
        //        return 0;

        //    return (identity as ClaimsIdentity).FirstOrDefault<int>(CustomClaimTypes.Agent);
        //}

        private static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }

        private static T FirstOrDefault<T>(this ClaimsIdentity identity, string claimType) where T : struct
        {
            var val = identity.FindFirst(claimType);

            return val == null ? default(T) : val.Value.To<T>();
        }

        private static T? FirstOrNull<T>(this ClaimsIdentity identity, string claimType) where T : struct
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value.ToNullable<T>();
        }
    }
}