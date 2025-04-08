using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using API.Data;
using API.DTO;
using API.Entitities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;
 
 [Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
   // [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()   ///created asynchronous task
    {
        var users=await userRepository.GetMembersAsync();
        //var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);
        return Ok(users); // can return BadRequest or can return NotFound
        
    }
    //[Authorize]
     [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user=await userRepository.GetMemberAsync(username);
        if(user==null) return NotFound();
        
        return user; // can return BadRequest or can return NotFound
        
    }

    // [HttpGet("{username}")]
    // public async Task<ActionResult<AppUser>> GetUsers(string username)
    // {
    //     var user=await context.Users.FirstOrDefaultAsync(x=> x.UserName == username);
    //     if(user==null) return NotFound();
        
    //     return user; // can return BadRequest or can return NotFound
        
    // }
    
}