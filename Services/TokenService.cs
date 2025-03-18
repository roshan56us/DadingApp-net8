using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entitities;
using Microsoft.IdentityModel.Tokens;

namespace API;
public class TokenService (IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenkey=config["TokenKey"]?? throw new Exception ("Cannot access tokenyey from appsetting");  //It could be null 
        if (tokenkey.Length < 64) throw new Exception("Your TokenKey needs to be longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));
        var claims=new List<Claim>
        {
            new (ClaimTypes.NameIdentifier,user.UserName)
        };
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor=new SecurityTokenDescriptor
        {
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.UtcNow.AddDays(7),
            SigningCredentials=creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token=tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}