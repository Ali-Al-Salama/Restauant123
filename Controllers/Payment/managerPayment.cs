using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Persistence.Entity;

//using Restaurant.Persistence.Entity;
using Restaurant.Services.Implementation;
using Restaurant.DTO;
using static restaurant.DTO.Payment.payment;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/payment/manager")]
    public class ManagerPaymentController: ControllerBase
    {
         private readonly manager_Payment _managerPaymentServices;
         public ManagerPaymentController(manager_Payment managerPaymentServices1){
            _managerPaymentServices=managerPaymentServices1;
         }


         [HttpPost("create")]
        public ActionResult create_Manager_Payment(payment_Request manager_Payment_Request)
        {
            _managerPaymentServices.create_Manager_Payment(manager_Payment_Request);
            return Created("Created", manager_Payment_Request);
        }

        ///////
         [HttpGet("get/{id}")]
        public ActionResult get_Manager_Payment( long id)
        {
            List<Payment> paymentsList = _managerPaymentServices.get_Payments(id);
            if (paymentsList == null)
                return NotFound();

            return Ok(paymentsList);
        }

        ////////
        
        [HttpPut("update")]
        public ActionResult update_Manager_Payment(Payment manager_Payment_Request)
        {
            _managerPaymentServices.update_Manager_Payment(manager_Payment_Request);
            return Ok(manager_Payment_Request);
        }
        ////////
        
        [HttpDelete("delete")]
        public ActionResult delete_User_Payment(long id)
        {
            _managerPaymentServices.delete_Manager_Payment(id);
            return Ok();
        }


    }
}