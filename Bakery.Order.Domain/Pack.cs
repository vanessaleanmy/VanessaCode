namespace Bakery.Order.Domain
{
    public class Pack
    {
        public int Number { get; set; }
        public decimal Price { get; set; }

        public Pack(int n, decimal p)
        {
            Number = n;
            Price = p;
        }
    }
}
