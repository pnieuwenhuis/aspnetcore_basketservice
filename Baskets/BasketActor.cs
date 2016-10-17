using System;
using System.Threading.Tasks;
using Akka.Actor;

using BasketService.Products;

namespace BasketService.Baskets
{
    public partial class BasketActor : ReceiveActor
    {
        private Basket BasketState { get; set; }
        private IActorRef ProductsActorRef { get; set; }
        public BasketActor(IActorRef productsActor)
        {
            this.BasketState = new Basket();
            this.ProductsActorRef = productsActor;

            Receive<GetBasket>(_ => Sender.Tell(this.BasketState));
            ReceiveAsync<AddItemToBasket>(m => AddItemToBasketAction(m).PipeTo(Sender));
            Receive<RemoveItemFromBasket>(m => Sender.Tell(RemoveItemToBasketAction(m)));
        }

        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketActor(productsActor));
        }

        public async Task<BasketEvent> AddItemToBasketAction(AddItemToBasket message)
        {
            var productActorResult = await this.ProductsActorRef.Ask<ProductsActor.ProductEvent>(
                new ProductsActor.UpdateStock(
                    productId: message.ProductId,
                    amountChanged: -message.Amount
                )
            );

            if (productActorResult is ProductsActor.StockUpdated)
            {
                var product = ((ProductsActor.StockUpdated)productActorResult).Product;

                var existingBasketItemWithProduct = this.BasketState.Items.Find(item => item.ProductId == product.Id);
                if (existingBasketItemWithProduct is BasketItem)
                {
                    // Add to existing basket item
                    existingBasketItemWithProduct.Amount += message.Amount;
                    return new ItemAdded(
                        basketItemId: existingBasketItemWithProduct.Id
                    );
                }
                else
                {
                    // Create a new basket item
                    var basketItemId = Guid.NewGuid();
                    this.BasketState.Items.Add(new BasketItem {
                        Id = basketItemId,
                        ProductId = product.Id,
                        Title = product.Title,
                        Brand = product.Brand,
                        PricePerUnit = product.PricePerUnit,
                        Amount = message.Amount
                    });

                    return new ItemAdded(basketItemId);
                }
            }
            else if (productActorResult is ProductsActor.ProductNotFound)
            {
                return new ProductNotFound();
            }
            else if (productActorResult is ProductsActor.InsuffientStock)
            {
                return new NotInStock();
            }
            else
            {
                throw new NotImplementedException($"Unknown response: {productActorResult.GetType().ToString()}");
            }
        }

        public BasketEvent RemoveItemToBasketAction(RemoveItemFromBasket message)
        {
            var basketItem = this.BasketState.Items.Find(item => item.Id == message.BasketItemId);
            if (basketItem is BasketItem)
            {
                this.BasketState.Items.Remove(basketItem);
                return new ItemRemoved();
            }
            else
            {
                return new ItemNotFound();
            }
        }
    }
}
