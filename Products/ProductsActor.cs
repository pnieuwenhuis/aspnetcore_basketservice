using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace BasketService.Products
{
    public class ProductsActor : ReceiveActor
    {
        #region Messages

        public class GetProduct
        {
            public int ProductId { get; set; }
        }

        #endregion

        #region Events

        public abstract class ProductEvent {}

        public class ProductFound : ProductEvent
        {
            public Product Product { get; set; }
        }

        public class ProductNotFound : ProductEvent {}

        #endregion

        private IEnumerable<Product> Products { get; set; }
        public ProductsActor()
        {
            // Set sample products
            this.Products = SampleData.Get();

            Receive<GetProduct>(m => Sender.Tell(GetProductAction(m)));
        }

        public ProductEvent GetProductAction(GetProduct message)
        {
            var product = this.Products
                .FirstOrDefault(p => p.Id == message.ProductId);

            return product is Product ?
                new ProductFound { Product = product } as ProductEvent :
                new ProductNotFound() as ProductEvent;
        }
    }
}
