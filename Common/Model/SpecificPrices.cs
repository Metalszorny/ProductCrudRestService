using System.Collections.Generic;

namespace Common.Model
{
    public class SpecificPrices
    {
        public string Customer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
