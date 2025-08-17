using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Persistence.Entity;
using Restaurant.Services.Implementation;
using Restaurant.DTO;
using static restaurant.DTO.Payment.payment;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class UserPaymentController:ControllerBase
    {
        private readonly user_Payment _userPaymentServices;
        public UserPaymentController(user_Payment userPaymentServices)
        {
            _userPaymentServices = userPaymentServices;
        }
        
       
        [HttpPost("create")]
        public ActionResult create_User_Payment(payment_Request user_Payment_Request)
        {
            _userPaymentServices.create_User_Payment(user_Payment_Request);
            return Created("Created", user_Payment_Request);
        }

        ///////
         [HttpGet("get/{id}")]
        public ActionResult get_User_Payment( long id)
        {
            List<Payment> payments_List = _userPaymentServices.get_Payments(id);
            if (payments_List == null)
                return NotFound();

            return Ok(payments_List);
        }
        ////////
        
        [HttpPut("update")]
        public ActionResult update_User_Payment(Payment user_Payment_Request)
        {
            _userPaymentServices.update_User_Payment(user_Payment_Request);
            return Ok(user_Payment_Request);
        }
        ////////
        
        [HttpDelete("delete")]
        public ActionResult delete_User_Payment(long id)
        {
            _userPaymentServices.delete_User_Payment(id);
            return Ok();
        }

    
    }
}