using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NTIGPayloadAPI.Models;
using NTIGPayloadAPI.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTIGPayloadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IService _orderService;
        private string _fileName = "PayLoads.json";

        public OrderController(IService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        //public IEnumerable<Order> Get()
        //{
        //    return _orderService.ReadUserInfoFromFile();
        //}
        public async Task<ActionResult<List<Order>>> Get()
        {
            List<Order>? orders = await _orderService.ReadUserInfoFromFile(_fileName);

            if (orders.Count > 0)
            {
                return Ok(orders);
            } else
            {
                return NotFound();
            }
        }

        // GET api/<OrderController>/5O
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            List<Order>? orders = await _orderService.ReadUserInfoFromFile(_fileName);
            Order order = null;

            if (orders.Count > 0)
            {
                order = orders.Find(x => x.Id == id);
            }

            if (order == null)
            {
                return NotFound();
            } else
            {
                return Ok(order);
            }
        }

        // POST api/<OrderController>
        [HttpPost]
        public async void Post([FromBody] Order order)
        {
            try
            {
                await _orderService.SaveFile(order, _fileName);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
