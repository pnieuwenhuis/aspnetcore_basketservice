namespace BasketService.Products
{
    public partial class ProductsActor
    {
        public class GetProduct
        {
            public int ProductId { get; set; }
        }

        public class GetAllProducts {}

        public class UpdateStock : ProductEvent
        {
            public int ProductId { get; set; }
            public int AmountChanged { get; set; }
        }
    }
}
