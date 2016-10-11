using Akka.Actor;

namespace BasketService.Products
{
    public class ProductsActorProvider
    {
        private IActorRef ProductsActor { get; set; }

        public ProductsActorProvider(ActorSystem actorSystem)
        {
            this.ProductsActor = actorSystem.ActorOf(Props.Create<ProductsActor>(), "products");
        }

        public IActorRef Get()
        {
            return ProductsActor;
        }
    }
}
