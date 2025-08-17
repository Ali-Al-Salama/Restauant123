using Restaurant.DTO;
using Restaurant.Persistence.Entity;
using static restaurant.DTO.User.user;

namespace Restaurant.Services.Interfaces
{
    public interface IUserService
    {
         List<User> GetUsers(long id);

         user_Response CreateUser(ManagerUserRequest user);

         user_Response UpdateUser(ManagerUserUpdateRequest user);

         user_Response DeleteUser(long id,string Password);
    }
}