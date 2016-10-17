namespace BasketService.Products
{
    public partial class ProductsActor
    {
        public abstract class ProductEvent {}

        public class ProductFound : ProductEvent
        {
            public readonly Product Product;

            public ProductFound(Product product) 
            {
                this.Product = product;
            }
        }

        public class ProductNotFound : ProductEvent {}

        public class StockUpdated : ProductEvent
        {
            public readonly Product Product;

            public StockUpdated(Product product)
            {
                this.Product = product;
            }
        }

        public class InsuffientStock : ProductEvent {}
    }
}
