using API.Entities;
//https://jwt.io/ - check token
namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
