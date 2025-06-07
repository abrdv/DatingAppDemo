using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Extentions;
using API.DTOs;
using API.Helpers;
namespace API.Controllers;

public class LikesController(ILikesRepository likesRepository) : BaseAPIController
{
    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> ToggleLike(int targetUserId)
    {
        var sourceUserId = User.GetUserId();
        if (sourceUserId == targetUserId)
        {
            return BadRequest("You cannot like yourself");
        }
        var existinglike = await likesRepository.GetUserLikeAsync(sourceUserId, targetUserId);
        if (existinglike == null)
        {
            var userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };
            likesRepository.AddUserLike(userLike);
        }
        else
        {
            likesRepository.DeleteUserLike(existinglike);
        }
        if (await likesRepository.SaveAllAsync())
        {
            return Ok();
        }
        return BadRequest("Failed to toggle like");
    }
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikes()
    {
        var userId = User.GetUserId();
        var likes = await likesRepository.GetCurrentUserLikesIdsAsync(userId);
        return Ok(likes);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserId();
        if (likesParams.UserId == 0)
        {
            return BadRequest("User ID is required");
        }
        var userId = await likesRepository.GetUserLikesAsync(likesParams);
        Response.AddPaginationHeader(userId);
        if (userId == null)
        {
            return NotFound("No likes found");
        }
        return Ok(userId);
    }
    }
