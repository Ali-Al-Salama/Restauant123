using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.DTO.Payment
{
    public class payment
    {
            public record payment_Request(
        long orderId,
    //    string userName,
        string userAddress
       
    );
    public record user_Payment_Responce(
        long Id,
        long orderId,
        long userId,
    //    string userName,
        string userAddress,
        string orderSttus,
        float paymentCost

    );
    }
}