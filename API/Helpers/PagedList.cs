using System;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PagedList<T> : List<T>
{
    // This class is used to create a paginated list of items.
    // It inherits from List<T> to provide a list of items and adds properties for pagination.
    // The constructor takes the items, total count, page size, and current page as parameters.
    // It calculates the total number of pages based on the total count and page size.
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        PageSize = pageSize;

        // Add the items to the list
        AddRange(items);
    }
    public int TotalCount { get; set; } // Total number of items
    public int PageSize { get; set; } // Number of items per page
    public int CurrentPage { get; set; } // Current page number
    public int TotalPages { get; set; } // Total number of pages

    public bool HasNextPage => CurrentPage < TotalPages; // Check if there is a next page
    public bool HasPreviousPage => CurrentPage > 1; // Check if there is a previous page
    public bool IsFirstPage => CurrentPage == 1; // Check if it is the first page
    public bool IsLastPage => CurrentPage == TotalPages; // Check if it is the last page    
    public int NextPage => CurrentPage + 1; // Get the next page number
    public int PreviousPage => CurrentPage - 1; // Get the previous page number
    public int FirstPage => 1; // Get the first page number
    public int LastPage => TotalPages; // Get the last page number
    public int Skip => (CurrentPage - 1) * PageSize; // Calculate the number of items to skip for pagination
    public int Take => PageSize; // Get the number of items to take for pagination
    public int From => Skip + 1; // Get the starting index of the items on the current page
    public int To => Math.Min(Skip + PageSize, TotalCount); // Get the ending index of the items on the current page
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        // This method creates a PagedList asynchronously from a source IQueryable.
        var count = await source.CountAsync(); // Get the total count of items
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(); // Get the items for the current page
        return new PagedList<T>(items, count, pageNumber, pageSize); // Return the PagedList
    }

}
