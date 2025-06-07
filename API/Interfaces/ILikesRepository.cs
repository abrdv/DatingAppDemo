using API.DTOs;
using API.Entities;
using API.Extentions;
using API.Helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike?> GetUserLikeAsync(int sourceUserId, int targetUserId);
        Task<PagedList<MemberDto>> GetUserLikesAsync(LikesParams likesParams);
        Task<IEnumerable<int>> GetCurrentUserLikesIdsAsync(int currentUserId);
        void AddUserLike(UserLike userLike);
        void DeleteUserLike(UserLike userLike);
        Task<bool> SaveAllAsync();
    }
    
}