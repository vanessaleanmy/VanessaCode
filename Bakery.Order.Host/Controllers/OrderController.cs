using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bakery.Order.Domain;
using Bakery.Order.Pricing;

namespace Bakery.Order.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IEnumerable<IProduct> _products;
        private readonly IOrderPricer _orderPricer;

        public OrderController(IEnumerable<IProduct> products,
             IOrderPricer orderPricer)
        {
            _products = products;
            _orderPricer = orderPricer;
        }

        
        // POST api/order
        [HttpPost]
        public ActionResult<List<OrderOutput>> Post([FromBody] OrderInput[] value)
        {            
            //--ensure value must have value
            if (value == null) return new NotFoundResult();

            List<OrderInput> orderInputs = value.ToList();
            //--ensure product code Exist
            var productCodeNotExist = orderInputs.Where(p => !_products.Any(p2 => p2.ProductCode.ToLower() == p.ProductCode.ToLower())).ToList();
            if (productCodeNotExist.Any()) return new NotFoundResult();
                       
            List<OrderOutput> orderOutputs = new List<OrderOutput>();
            foreach (var order in orderInputs)
            {
                var product = _products.Where(p => p.ProductCode.ToLower() == order.ProductCode.ToLower()).FirstOrDefault();
                if (product == null)
                {
                    var oo = new OrderOutput();
                    oo.OrderInputShow = order;
                    oo.TotalPrice = "0.00";
                    oo.PackCombination = "Product code not exist";
                    orderOutputs.Add(oo);
                }
                else
                {
                    var oo = new OrderOutput();
                    oo = _orderPricer.CalculatePack(order, product);
                    orderOutputs.Add(oo);
                }
            }
            return orderOutputs;

        }

       
    }
}
