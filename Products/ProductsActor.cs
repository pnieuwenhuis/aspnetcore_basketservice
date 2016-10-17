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

            Receive<GetAllProducts>(_ => Sender.Tell(this.Products));
            Receive<UpdateStock>(m => Sender.Tell(UpdateStockAction(m)));
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
                    return new StockUpdated(product);
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
