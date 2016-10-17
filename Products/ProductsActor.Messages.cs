namespace BasketService.Products
{
    public partial class ProductsActor
    {
        public class GetProduct
        {
            public readonly int ProductId;

            public GetProduct(int productId = 0)
            {
                this.ProductId = productId;
            }
        }

        public class GetAllProducts {}

        public class UpdateStock : ProductEvent
        {
            public readonly int ProductId;
            public readonly int AmountChanged;

            public UpdateStock(int productId = 0, int amountChanged = 0)
            {
                this.ProductId = productId;
                this.AmountChanged = amountChanged;
            }
        }
    }
}
