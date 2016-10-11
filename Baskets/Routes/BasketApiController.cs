using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BasketService.Baskets.Routes
{
   [Route("/api/basket")]
   public class BasketApiController
    {
        private GetBasket GetBasket { get; set; }
        public BasketApiController(GetBasket getBasket)
        {
            this.GetBasket = getBasket;
        }
        [HttpGet("{customerId}")] public async Task<Basket> Get(int customerId)
        {
            var result = await this.GetBasket.Execute(customerId);
            return result;
        }
    }
}
