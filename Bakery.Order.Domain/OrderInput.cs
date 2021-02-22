namespace Bakery.Order.Domain
{
    public class OrderInput
    {
        public int OrderQuantity { get; set; }
        public string ProductCode { get; set; }

        public OrderInput (int orderQuantity, string productCode)
        {
            OrderQuantity = orderQuantity;
            ProductCode = productCode;
        }
    }
}
