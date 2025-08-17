using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Persistence.Entity;
using Restaurant.DTO;
using static restaurant.DTO.Payment.payment;

namespace Restaurant.Services.Interfaces
{
    public interface IUserPaymentServices
    {
        user_Payment_Responce create_User_Payment(payment_Request new_User_Payment_Request);
        List<Payment> get_Payments(long id);
        user_Payment_Responce update_User_Payment(Payment updated_User_Payment_Request);
        void delete_User_Payment(long id); 

    }
}