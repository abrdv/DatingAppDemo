﻿
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService): BaseAPIController
{
    [HttpPost("register")] //account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
    { 
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        return Ok();
        //using var hmac = new HMACSHA512();
        //var user = new AppUser
        //{
        //    UserName = registerDto.Username.ToLower(),
        //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //    PasswordSalt = hmac.Key
        //};
        //await context.AddAsync(user);
        //await context.SaveChangesAsync();
        //return new UserDto 
        //{ 
        //    Username = user.UserName,
        //    Token = tokenService.CreateToken(user)
        //};
    }
    [HttpPost("login")] //account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if (user == null) return Unauthorized("Invalid username");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        //logger.Info("UserName: " + user.UserName + ", PasswordSalt:" + user.PasswordSalt + ", computedHash:" + computedHash + ", Password: "+ loginDto.Password);

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }
        private async Task<bool> UserExists(string username) 
    { 
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
