using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            var username= user.FindFirstValue(ClaimTypes.NameIdentifier) 
                ?? throw new Exception("Cannot get username from the token");
            return username;

        }
    }
}