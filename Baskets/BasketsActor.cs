using Akka.Actor;

using BasketService.Core.Messaging;

namespace BasketService.Baskets
{
    public class BasketsActor : ReceiveActor
    {
        private IActorRef ProductActor { get; }

        public BasketsActor(IActorRef productActor)
        {
            this.ProductActor = productActor;

            ReceiveAny(m => {
                if (m is IEnvelopeWithCustomerId)
                {
                    var envelope = m as IEnvelopeWithCustomerId;
                    var basketActor = Context.Child(envelope.CustomerId.ToString()) is Nobody ?
                        Context.ActorOf(BasketActor.Props(this.ProductActor), envelope.CustomerId.ToString()) :
                        Context.Child(envelope.CustomerId.ToString());
                    basketActor.Forward(m);
                }
            });
        }
        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketsActor(productsActor));
        }
    }
}
