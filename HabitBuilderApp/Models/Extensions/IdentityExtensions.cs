using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace HabitBuilderApp.Models.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserProfileName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserProfileId");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}