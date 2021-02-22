using Bakery.Order.Domain;

namespace Bakery.Order.Pricing
{
    public interface IOrderPricer
    {
        OrderOutput CalculatePack(OrderInput oi, IProduct p);
    }
}
