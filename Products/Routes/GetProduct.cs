using Microsoft.Extensions.Logging;
using Akka.Actor;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Products.Routes
{
    public class GetProduct
    {
        private ILogger<GetProduct> Logger { get; set; }
        private IActorRef ProductsActor { get; set; }
        public GetProduct(ProductsActorProvider provider, ILogger<GetProduct> logger)
        {
            this.Logger = logger;
            this.ProductsActor = provider.Get();
        }
        public async Task<IActionResult> Execute(int productId) {
            Logger.LogInformation($"Requesting product '{productId}'");
            var result = await this.ProductsActor.Ask<ProductsActor.ProductEvent>(
                new ProductsActor.GetProduct { ProductId = productId }
            );

            if (result is ProductsActor.ProductFound)
            {
                return new OkObjectResult(((ProductsActor.ProductFound)result).Product);
            }
            else
            {
                return new NotFoundResult();
            }


        }
    }
}
