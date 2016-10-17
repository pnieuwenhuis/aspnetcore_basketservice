using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace BasketService.Baskets.Routes
{
    public class RemoveItemFromBasket
    {
        private ILogger<GetBasket> Logger { get; set; }
        private IActorRef BasketsActor { get; set; }

        public RemoveItemFromBasket(BasketsActorProvider provider, ILogger<GetBasket> logger)
        {
            this.Logger = logger;
            this.BasketsActor = provider.Get();
        }

        public async Task<IActionResult> Execute(int customerId, Guid basketItemId)
        {
            Logger.LogInformation($"Removing item {basketItemId} from basket of customer {customerId}");
            var result = await this.BasketsActor.Ask<BasketActor.BasketEvent>(new BasketActor.RemoveItemFromBasket(
                customerId,
                basketItemId
            ));

            if (result is BasketActor.ItemRemoved)
            {
                return new OkResult();
            }
            else if (result is BasketActor.ItemNotFound)
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
