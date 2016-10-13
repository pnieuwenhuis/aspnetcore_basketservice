using System;
using Akka.Actor;

namespace BasketService.Baskets
{
    public partial class BasketActor : ReceiveActor
    {
        private Basket BasketState { get; set; }
        public BasketActor()
        {
            this.BasketState = new Basket();
            Receive<GetBasket>(_ => Sender.Tell(this.BasketState));
            Receive<AddItemToBasket>(m => Sender.Tell(AddItemToBasketAction(m)));
        }

        public BasketEvent AddItemToBasketAction(AddItemToBasket message)
        {
            var basketItemId = new Guid();

            // Get Product

            // Check Amount

            // Create ItemAdded
            return new ItemAdded { BasketItemId = basketItemId };

        }
    }
}
