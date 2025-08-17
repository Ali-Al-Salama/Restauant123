using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Persistence.Entity;
using Restaurant.DTO;
using static restaurant.DTO.User.user;

namespace Restaurant.Services.Interfaces
{
    public interface IUserRegisterServices
    {
       user_Response create_User(user_Rigester user);
       List <User> get_User(long id);
       user_Response update_User(UserUpdateRequest user);
       user_Response delete_User(long id,string Password);
    }
}