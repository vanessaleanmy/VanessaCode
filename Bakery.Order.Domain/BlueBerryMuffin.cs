using System.Collections.Generic;

namespace Bakery.Order.Domain
{
    public class BlueBerryMuffin : IProduct
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public List<Pack> packs { get; set; }

        
        public BlueBerryMuffin()
        {
            ProductName = "BlueBerry Muffin";
            ProductCode = "MB11";

            List<Pack> configuredPack = new List<Pack>();
            configuredPack.Add(new Pack(2, 9.95m));
            configuredPack.Add(new Pack(5, 16.95m));
            configuredPack.Add(new Pack(8, 24.95m));
            packs = configuredPack;
        }
    }
}
