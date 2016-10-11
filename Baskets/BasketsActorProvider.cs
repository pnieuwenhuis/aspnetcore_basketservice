using Akka.Actor;
using Akka.Routing;
using BasketService.Core.Messaging;

namespace BasketService.Baskets
{
    public class BasketsActorProvider
    {
        private IActorRef BasketsActor { get; set; }

        public BasketsActorProvider(ActorSystem actorSystem)
        {
            var pool = new ConsistentHashingPool(10).WithHashMapping(o =>
            {
                if (o is IEnvelopeWithCustomerId)
                    return ((IEnvelopeWithCustomerId)o).CustomerId;

                return null;
            });

            this.BasketsActor = actorSystem.ActorOf(Props.Create<BasketActor>()
                .WithRouter(pool), "baskets");
        }

        public IActorRef Get()
        {
            return this.BasketsActor;
        }
    }
}
