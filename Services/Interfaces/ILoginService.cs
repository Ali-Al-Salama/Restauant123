
using Restaurant.DTO;
namespace Restaurant.Services
{
    public interface ILoginService
    {
        LoginResponse UserLogin(LoginRequest loginRequest);
    }
}