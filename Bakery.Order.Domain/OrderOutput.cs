using System.Collections.Generic;

namespace Bakery.Order.Domain
{
    public class OrderOutput
    {
        public OrderInput OrderInputShow { get; set; }
        public string TotalPrice { get; set; }
        public string PackCombination { get; set; }
    }
}
