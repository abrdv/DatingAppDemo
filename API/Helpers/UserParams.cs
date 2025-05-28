using System;
using CloudinaryDotNet.Actions;

namespace API.Helpers;

public class UserParams
{
    private int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1; // Default page number
    private int _pageSize = 10; // Default page size
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // Limit the maximum page size
    }

    public string? CurrentUsername { get; set; } // Username of the current user
    public string? Gender { get; set; } //
    public int minAge { get; set; } = 18; // Minimum age
    public int MaxAge { get; set; } = 100; // Maximum age
    public string OrderBy { get; set; } = "lastActive"; // Default order by last active
}
