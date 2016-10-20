namespace BasketService.Products
{
    public partial class ProductsActor
    {
        public class GetAllProducts {}

        public class UpdateStock
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
