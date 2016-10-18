using System;

namespace BasketService.Baskets {
    public class BasketItem {
        public Guid Id { get; set; }

        public int ProductId { get ; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public int PricePerUnit { get; set; }

        public int Amount { get; set; }
    }
}
