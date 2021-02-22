using System.Collections.Generic;

namespace Bakery.Order.Domain
{
    public interface IProduct 
    {
        string ProductName { get; set; }
        string ProductCode { get; set; }
        List<Pack> packs { get; set; }
    }
}
