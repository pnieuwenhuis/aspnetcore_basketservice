using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Baskets.Routes
{
   [Route("/api/basket")]
   public class BasketApiController
    {
        private GetBasket GetBasket { get; }
        private AddItemToBasket AddItemToBasket { get; }
        private RemoveItemFromBasket RemoveItemFromBasket { get; }

        public BasketApiController(GetBasket getBasket, AddItemToBasket addItemToBasket, RemoveItemFromBasket removeItemFromBasket)
        {
            this.GetBasket = getBasket;
            this.AddItemToBasket = addItemToBasket;
            this.RemoveItemFromBasket = removeItemFromBasket;
        }
        [HttpGet("{customerId}")] public async Task<Basket> Get(int customerId)
        {
            var result = await this.GetBasket.Execute(customerId);
            return result;
        }

        [HttpPut("{customerId}/items")] public async Task<IActionResult> PutItem(int customerId, [FromBody] ApiDsl.AddItem item)
        {
            var result = await this.AddItemToBasket.Execute(customerId, item.ProductId, item.Amount);
            return result;
        }

        [HttpDelete("{customerId}/items/{basketItemId}")] public async Task<IActionResult> DeleteItem(int customerId, Guid basketItemId)
        {
            var result = await this.RemoveItemFromBasket.Execute(customerId, basketItemId);
            return result;
        }
    }
}
