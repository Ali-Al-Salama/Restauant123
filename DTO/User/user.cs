using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.DTO.User
{
    public class user
    {
        public record pending_User_Request(
        string Email
    );

    public record pending_User_Responce(
        string Message,
        long Id,
        string Email,
        long VerificationCode
    );

    public record user_Request(
       long verification_Code,
       string Email,
       string Name,
       string Phone,
       string Role,
       string Address,
       string Password
    );
    public record ManagerUserRequest(
       string Email,
       string Name,
       string Phone,
       string Role,
       string Address,
       string Password
    );
    public record user_Rigester(
       long verification_Code,
       string Email,
       string Name,
       string Phone,
       string Address,
       string Password
    );

    public record user_Response(
       string Message,
       long Id,
       string Email,
       string Name,
       string Phone,
       string Address,
       string AccessToken,
       string RefreshToken
    );
    public record ManagerUserUpdateRequest(
       long Id,
       string Name,
       string Phone,
       string Role,
       string Address
    );
    public record UserUpdateRequest(
       long Id,
       string Name,
       string Phone,
       string Address
    );
    }
}