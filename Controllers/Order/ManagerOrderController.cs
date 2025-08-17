using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;

namespace Restaurant.Controller
{
    [ApiController]
    [Route("api/manager/order")]
    public class ManagerOrderController:ControllerBase
    {
        private readonly ManagerOrderServices managerOrderServices;
        public ManagerOrderController(ManagerOrderServices managerOrderServices1)
        {
            managerOrderServices = managerOrderServices1;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get()
        {
            List<ManagerOrderResponse> list = managerOrderServices.MangerGet();
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get(long id)
        {
            List<ManagerOrderResponse> list = managerOrderServices.MangerGet(id);
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Create([FromBody]ManagerOrderRequestForCreate orderRequest)
        { 
            OrderResponse orderResponse = managerOrderServices.MangerCreate(orderRequest);
            if(orderResponse == null)
                return BadRequest("Item Id is wrong");
            return Created("Created",orderResponse);
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Update([FromBody]OrderRequest orderRequest,[FromQuery]long id)
        {
            OrderResponse orderResponse = managerOrderServices.ManagerUpdate(orderRequest,id);
            if(orderResponse == null)
                return BadRequest("Order Id or Item Id is wrong");
            return Accepted(orderResponse);
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete([FromRoute]long id){
            bool ok = managerOrderServices.ManagerDelete(id);
            if(!ok)
                return BadRequest("Order Id is wrong");
            return Accepted();
        }
    }
}