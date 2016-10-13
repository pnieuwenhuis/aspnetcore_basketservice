using Microsoft.Extensions.Logging;
using Akka.Actor;
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

        public async Task<Basket> Execute(int customerId, int productId, int amount) {
            Logger.LogInformation($"Adding {amount} of product '{productId}' to basket for customer '{customerId}'");
            return await this.BasketsActor.Ask<Basket>(new BasketActor.GetBasket { CustomerId = customerId });
        }
    }
}
