using System;

namespace API.Helpers;

public class PaginationParams
{
    private int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1; // Default page number
    private int _pageSize = 10; // Default page size
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // Limit the maximum page size
    }
}
