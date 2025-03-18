using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using API.Data;
using API.Entitities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()   ///created asynchronous task
    {
        var users=await context.Users.ToListAsync();
        return users; // can return BadRequest or can return NotFound
        
    }
    [Authorize]
     [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
        var user=await context.Users.FindAsync(id);
        if(user==null) return NotFound();
        
        return user; // can return BadRequest or can return NotFound
        
    }
    
}