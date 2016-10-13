namespace BasketService.Products
{
    public partial class ProductsActor
    {
        public abstract class ProductEvent {}

        public class ProductFound : ProductEvent
        {
            public Product Product { get; set; }
        }

        public class ProductNotFound : ProductEvent {}

        public class StockUpdated : ProductEvent
        {
            public int InStock { get; set; }
        }

        public class InsuffientStock : ProductEvent {}
    }
}
