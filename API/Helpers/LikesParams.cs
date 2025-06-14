using System;

namespace API.Helpers;

public class LikesParams: PaginationParams
{
    public int UserId { get; set; }
    public required string Predicate { get; set; } = "liked";// Predicate to filter likes, e.g., "liked", "likedBy"
}
