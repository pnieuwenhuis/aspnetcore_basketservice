using Akka.Actor;

using BasketService.Core.Messaging;

namespace BasketService.Baskets
{
    public class BasketActor : ReceiveActor
    {
        #region Messages

        public class GetBasket : IEnvelopeWithCustomerId {
            public int CustomerId { get; set; }
        }

        #endregion


        private Basket BasketState { get; set; }
        public BasketActor()
        {
            this.BasketState = new Basket();

            // TODO: logging DI
            Receive<GetBasket>(_ => Sender.Tell(this.BasketState));
        }
    }
}
