using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Persistence.Entity;
using Restaurant.DTO;
using static restaurant.DTO.User.user;

namespace Restaurant.Services.Interfaces
{
    public interface IPendingUserServices
    {
       /* List <PendingUser> getPendingUser(long id);*/
        pending_User_Responce create_User(pending_User_Request user);
        
    }
}