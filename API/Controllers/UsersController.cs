using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers;
[Authorize]
    public class UsersController(DataContext dataContext) : BaseAPIController
    {
    
    [AllowAnonymous]    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        { 
            var users = await dataContext.Users.ToListAsync();
            //if (users == null || users.Count == 0 ) {return NotFound();}
            return users;
        }
    
    [Authorize]
    [HttpGet("{id}")]   //  /api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await dataContext.Users.FindAsync(id);
            if (user == null) { return NotFound(); }
            return user;
        }

    [HttpPost]   //  /api/users
    public async Task<ActionResult<AppUser>> PostUser(AppUser user)
        {
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();
            //return CreatedAtAction(nameof(AppUser), new { id = user.Id }, user); //generate error
            return NoContent();
        }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<AppUser>> PutUser(int id, AppUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            dataContext.Entry(user).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await dataContext.Users.FindAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
        {
            var userItem = await dataContext.Users.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            dataContext.Users.Remove(userItem);
            await dataContext.SaveChangesAsync();

            return NoContent();
        }
    }
    

