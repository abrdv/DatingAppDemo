using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Security.Claims;
using API.Extentions;
using API.Entities;
using API.Helpers;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
    {
        var user = await userRepository.GetMembersAsync(userParams);
        if (user == null) return NotFound("No user found");
        var currentUser = await userRepository.GetMembersAsync(userParams);
        Response.AddPaginationHeader(user);
        return Ok(user);
    }


    [HttpGet("{username}")]   //  /api/users/baker
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("No user found. Cannot update user");
        mapper.Map(memberUpdateDto, user);
        //userRepository.Update(user);
        if (await userRepository.SaveUsersAsync()) return NoContent();
        return BadRequest("Failed to update user");
    }
    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user != null)
        {
            var result = await photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = user.Photos.Count == 0
            };
            user.Photos.Add(photo);
            if (await userRepository.SaveUsersAsync()) return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, mapper.Map<PhotoDto>(photo));
            return BadRequest("Problem adding photo");
        }

        return BadRequest("Cannot find user");
    }
    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return NotFound("No user found. Cannot set main photo");
        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
        if (photo == null) return NotFound("No photo found. Cannot set main photo");
        if (photo.IsMain) return BadRequest("This is already your main photo");
        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null) currentMain.IsMain = false;
        photo.IsMain = true;
        if (await userRepository.SaveUsersAsync()) return NoContent();
        return BadRequest("Failed to set main photo");
    }
    [HttpDelete("delete-photo/{photoId}")]  
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return NotFound("No user found. Cannot delete photo");
        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
        if (photo == null) return NotFound("No photo found. Cannot delete photo");
        if (photo.IsMain) return BadRequest("You cannot delete your main photo");
        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }
        user.Photos.Remove(photo);
        if (await userRepository.SaveUsersAsync()) return Ok();
        return BadRequest("Failed to delete the photo");
    }
}

