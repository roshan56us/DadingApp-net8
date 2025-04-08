using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entitities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController (DataContext context, ITokenService tokenService): BaseApiController
{
 [HttpPost("Register")]
public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
{
    if( await UserExist(registerDTO.Username)) return BadRequest("Username is taken");
    return Ok();
    // using var hmac= new HMACSHA512();
    // var user=new AppUser
    // {
    //     UserName=registerDTO.Username.ToLower(),
    //     PassswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
    //     PassswordSalt=hmac.Key
    // };
    // context.Users.Add(user);
    // await context.SaveChangesAsync();
    // return new UserDTO
    // {
    //     Username=user.UserName,
    //     Token =  tokenService.CreateToken(user)
    // };

}
[HttpPost("Login")]

public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
{
    var user=await context.Users.FirstOrDefaultAsync(x => 
    x.UserName == loginDto.Username.ToLower());

    if (user == null) return Unauthorized("Invalid Username");
    using var hmac= new HMACSHA512(user.PassswordSalt);
    var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
    for (int i=0; i<ComputeHash.Length;i++)
    {
        if (ComputeHash[i]!=user.PassswordHash[i]) return Unauthorized("Invalid Password");
    }
    return new UserDTO
    {
        Username=user.UserName,
        Token =  tokenService.CreateToken(user)
    };
}
private async Task<bool> UserExist(string username)
{
    return await context.Users.AnyAsync(x => x.UserName.ToLower()==username.ToLower());
}
}