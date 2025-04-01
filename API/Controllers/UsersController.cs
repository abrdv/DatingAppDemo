using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Security.Claims;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseAPIController
    {
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var user = await userRepository.GetMembersAsync();
        if (user == null) { return NotFound(); }
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
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (username == null) return Unauthorized();
        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null) return BadRequest("Not Find User");
        mapper.Map(memberUpdateDto, user);
        userRepository.Update(user);
        if (await userRepository.SaveUsersAsync()) return NoContent();
        return BadRequest("Failed to update user");
    }
}
    

