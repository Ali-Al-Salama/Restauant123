using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Persistence.Entity;

//using Restaurant.Persistence.Entity;
using Restaurant.DTO;
using static restaurant.DTO.Payment.payment;

namespace Restaurant.Services.Interfaces
{
    public interface  IManagerPaymentServices
    {
       user_Payment_Responce create_Manager_Payment(payment_Request newUserPaymentRequest);
        List<Payment> get_Payments(long id);
         user_Payment_Responce update_Manager_Payment(Payment updatedUserPaymentRequest);
        void delete_Manager_Payment(long id); 
    }
    
}