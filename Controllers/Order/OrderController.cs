using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
namespace Restaurant.Controller
{
    [ApiController]
    [Route("api/order")]
    public class OrderController:ControllerBase
    {
        private readonly OrderServices orderServices;
        public OrderController(OrderServices orderServices1)
        {
            orderServices = orderServices1;
        }
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CreateOrder(OrderRequestForCreate orderRequest)
        {
            string? UserId = HttpContext.Request.Headers["UserId"];
            //long Id = convertString2Long.ConvertId2Long(UserId);
            OrderResponse response = orderServices.CreateOrder(orderRequest,UserId);
            if(response == null)
                return BadRequest("Item Id is wrong");
            return Created("Created",response);
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetOrders()
        {
            string? UserId = HttpContext.Request.Headers["UserId"];
            if(UserId is null)
            {
                return BadRequest();
            }
            List<OrderResponse> list = orderServices.GetOrders(UserId);
            if(list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult UpdateOrder(OrderRequest order)
        {
            string? UserId = HttpContext.Request.Headers["UserId"];
            OrderResponse orderResponse = orderServices.UpdateOrder(order,UserId);
            if(orderResponse == null)
                return BadRequest("order Id or Item Id is wrong");
            return Accepted(orderResponse);
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult DeleteOrder([FromRoute]long id)
        {
            bool ok = orderServices.DeleteOrder(id);
            if(!ok)
                return BadRequest("OrderId is wrong");
            return Accepted();
        }
    }
}
