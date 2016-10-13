using Microsoft.Extensions.Logging;
using Akka.Actor;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasketService.Products.Routes
{
    public class GetAllProducts
    {
        private ILogger<GetProduct> Logger { get; set; }
        private IActorRef ProductsActor { get; set; }
        public GetAllProducts(ProductsActorProvider provider, ILogger<GetProduct> logger)
        {
            this.Logger = logger;
            this.ProductsActor = provider.Get();
        }
        public async Task<IEnumerable<Product>> Execute() {
            Logger.LogInformation("Requesting all products");
            return await this.ProductsActor.Ask<IEnumerable<Product>>(
                new ProductsActor.GetAllProducts()
            );
        }
    }
}
