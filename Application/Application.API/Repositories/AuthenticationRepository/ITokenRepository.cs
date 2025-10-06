using Microsoft.AspNetCore.Identity;

namespace Application.API.Repositories.AuthenticationRepository
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
