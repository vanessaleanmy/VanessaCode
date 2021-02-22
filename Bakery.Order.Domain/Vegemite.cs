using System.Collections.Generic;

namespace Bakery.Order.Domain
{
    public class Vegemite : IProduct
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public List<Pack> packs { get; set; }

        
        public Vegemite()
        {
            ProductName = "Vegemite Scroll";
            ProductCode = "VS5";

            List<Pack> configuredPack = new List<Pack>();
            configuredPack.Add(new Pack(3, 6.99m));
            configuredPack.Add(new Pack(5, 8.99m));
            packs = configuredPack;            
        }
    }
}
