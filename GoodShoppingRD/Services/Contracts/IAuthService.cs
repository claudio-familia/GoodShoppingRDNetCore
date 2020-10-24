using GoodShoppingRD.Models.Auth;

namespace GoodShoppingRD.Services.Contracts
{
    public interface IAuthService
    {
        public string GenerateJwt(User user);
    }
}
