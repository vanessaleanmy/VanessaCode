using System.Collections.Generic;

namespace Bakery.Order.Domain
{
    public class Croissant : IProduct
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public List<Pack> packs { get; set; }


        public Croissant()
        {
            ProductName = "Croissant";
            ProductCode = "CF";

            List<Pack> configuredPack = new List<Pack>();
            configuredPack.Add(new Pack(3, 5.95m));
            configuredPack.Add(new Pack(5, 9.95m));
            configuredPack.Add(new Pack(9, 16.99m));
            packs = configuredPack;
        }
    }
}
