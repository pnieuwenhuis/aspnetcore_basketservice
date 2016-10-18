using Akka.Actor;

namespace BasketService.Products
{
    public class ProductsActorProvider
    {
        private IActorRef ProductsActor { get; set; }

        public ProductsActorProvider(ActorSystem actorSystem)
        {
            var products = SampleData.Get(); // set sample products
            this.ProductsActor = actorSystem.ActorOf(Props.Create<ProductsActor>(products), "products");
        }

        public IActorRef Get()
        {
            return ProductsActor;
        }
    }
}
