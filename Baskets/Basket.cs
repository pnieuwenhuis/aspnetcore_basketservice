using System.Collections.Generic;

namespace BasketService.Baskets {
    public class Basket {

        public IEnumerable<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
