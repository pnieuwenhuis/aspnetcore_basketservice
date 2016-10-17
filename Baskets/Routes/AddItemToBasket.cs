using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace BasketService.Baskets.Routes
{
    public class AddItemToBasket
    {
        private ILogger<GetBasket> Logger { get; set; }
        private IActorRef BasketsActor { get; set; }

        public AddItemToBasket(BasketsActorProvider provider, ILogger<GetBasket> logger)
        {
            this.Logger = logger;
            this.BasketsActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int customerId, int productId, int amount)
        {
            Logger.LogInformation($"Adding {amount} of product '{productId}' to basket for customer '{customerId}'");
            var result = await this.BasketsActor.Ask<BasketActor.BasketEvent>(new BasketActor.AddItemToBasket(
                customerId,
                productId,
                amount
            ));

            if (result is BasketActor.ItemAdded)
            {
                var e = result as BasketActor.ItemAdded;
                return new CreatedResult($"/api/baskets/{customerId}/", e.BasketItemId);
            }
            else if (result is BasketActor.ProductNotFound || result is BasketActor.NotInStock)
            {
                return new BadRequestResult();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
