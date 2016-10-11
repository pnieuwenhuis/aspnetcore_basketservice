using BasketService.Products;

namespace BasketService.Baskets {
    public class BasketItem {
        public Product Product { get; set; }

        public string Title { get; set; }

        public string Brand { get; set; }

        public int PricePerUnit { get; set; }

        public int Amount { get; set; }
    }
}
