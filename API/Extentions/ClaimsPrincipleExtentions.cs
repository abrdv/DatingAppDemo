﻿using System.Security.Claims;

namespace API.Extentions
{
    public static class ClaimsPrincipleExtentions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var username = user.FindFirstValue(ClaimTypes.Name)
            ?? throw new Exception("Username not found from token");
            return username;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Username not found from token"));
            return userId;
        }
    }
}
