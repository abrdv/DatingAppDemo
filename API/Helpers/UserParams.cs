using System;
using CloudinaryDotNet.Actions;

namespace API.Helpers;

public class UserParams: PaginationParams
{


    public string? CurrentUsername { get; set; } // Username of the current user
    public string? Gender { get; set; } //
    public int minAge { get; set; } = 18; // Minimum age
    public int MaxAge { get; set; } = 100; // Maximum age
    public string OrderBy { get; set; } = "lastActive"; // Default order by last active
}
