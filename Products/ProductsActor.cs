using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace BasketService.Products
{
    public partial class ProductsActor : ReceiveActor
    {
        private IEnumerable<Product> Products { get; set; }
        public ProductsActor()
        {
            // Set sample products
            this.Products = SampleData.Get();

            Receive<GetProduct>(m => Sender.Tell(GetProductAction(m)));
            Receive<GetAllProducts>(_ => Sender.Tell(this.Products));
            Receive<UpdateStock>(m => Sender.Tell(UpdateStockAction(m)));
        }

        public ProductEvent GetProductAction(GetProduct message)
        {
            var product = this.Products
                .FirstOrDefault(p => p.Id == message.ProductId);

            return product is Product ?
                new ProductFound { Product = product } as ProductEvent :
                new ProductNotFound() as ProductEvent;
        }

        public ProductEvent UpdateStockAction(UpdateStock message)
        {
            var product = this.Products
                .FirstOrDefault(p => p.Id == message.ProductId);

            if (product is Product)
            {
                if (product.InStock + message.AmountChanged >= 0)
                {
                    product.InStock += message.AmountChanged;
                    return new StockUpdated { InStock = product.InStock };
                }
                else
                {
                    return new InsuffientStock();
                }
            }

            return new ProductNotFound();
        }
    }
}
