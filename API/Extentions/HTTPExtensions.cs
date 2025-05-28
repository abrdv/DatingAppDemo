using System;
using System.Text.Json;
using API.Helpers;

namespace API.Extentions;

public static class HTTPExtensions
{
    public static void AddPaginationHeader<T>(this HttpResponse response, PagedList<T> pagedList)
    {
        // This method adds pagination headers to the HTTP response.
        // It takes a PagedList<T> object as a parameter.
        var paginationHeader = new PaginationHeader(pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}