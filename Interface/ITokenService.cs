using API.Entitities;

namespace API;

public interface ITokenService
{
    string CreateToken(AppUser user);
}