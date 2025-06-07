using System;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class LikesRepository(DataContext context, IMapper mapper) : ILikesRepository
{
    public void AddUserLike(UserLike userLike)
    {
         context.Likes.Add(userLike);
    }

    public void DeleteUserLike(UserLike userLike)
    {
        context.Likes.Remove(userLike);
    }

    public async Task<IEnumerable<int>> GetCurrentUserLikesIdsAsync(int currentUserId)
    {
        return await context.Likes
            .Where(l => l.SourceUserId == currentUserId)
            .Select(l => l.TargetUserId)
            .ToListAsync();
    }

    public async Task<UserLike?> GetUserLikeAsync(int sourceUserId, int targetUserId)
    {
        return await context.Likes
            .FirstOrDefaultAsync(l => l.SourceUserId == sourceUserId && l.TargetUserId == targetUserId);
    }

    public async Task<PagedList<MemberDto>> GetUserLikesAsync(LikesParams likesParams)
    {
        var likes = context.Likes.AsQueryable();
        IQueryable<MemberDto> query;

        if (likesParams.Predicate == "liked")
        {
            query = likes.Where(l => l.SourceUserId == likesParams.UserId)
            .Select(l => l.TargetUser)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
        }
        else if (likesParams.Predicate == "likedBy")
        {
            query = likes.Where(l => l.TargetUserId == likesParams.UserId)
            .Select(l => l.SourceUser)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
        }
        else
        {
            var likedIds = await GetCurrentUserLikesIdsAsync(likesParams.UserId);
            query = likes
                .Where(u => u.TargetUserId == likesParams.UserId && likedIds.Contains(u.SourceUserId))
                .Select(l => l.SourceUser)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
        }
        return await PagedList<MemberDto>.CreateAsync(query, likesParams.PageNumber, likesParams.PageSize);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
