using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository userRepository) : BaseAPIController
    {
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var members = await userRepository.GetMembersAsync();
        if (members == null) { return NotFound(); }
        return Ok(members);
    }


    [HttpGet("{username}")]   //  /api/users/baker
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var member = await userRepository.GetMemberAsync(username);
        if (member == null) return NotFound(); 
        return member;
    }

}
    

