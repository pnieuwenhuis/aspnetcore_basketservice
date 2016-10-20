using System;
using System.Threading.Tasks;
using Akka.Actor;

using BasketService.Products;

namespace BasketService.Baskets
{
    public partial class BasketActor : ReceiveActor
    {
        public Basket BasketState { get; set; }
        private IActorRef ProductsActorRef { get; set; }
        public BasketActor(IActorRef productsActor)
        {
            this.BasketState = new Basket();
            this.ProductsActorRef = productsActor;

            Receive<GetBasket>(_ => Sender.Tell(this.BasketState));
            ReceiveAsync<AddItemToBasket>(m => AddItemToBasketAction(m).PipeTo(Sender), m => m.Amount > 0);
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
                return AddToBasket(product, message.Amount) as ItemAdded;
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

        private ItemAdded AddToBasket(Product productToAdd, int amount)
        {
            var existingBasketItemWithProduct = this.BasketState.Items.Find(item => item.ProductId == productToAdd.Id);
            if (existingBasketItemWithProduct is BasketItem)
            {
                // Add to existing basket item
                existingBasketItemWithProduct.Amount += amount;
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
                    ProductId = productToAdd.Id,
                    Title = productToAdd.Title,
                    Brand = productToAdd.Brand,
                    PricePerUnit = productToAdd.PricePerUnit,
                    Amount = amount
                });

                return new ItemAdded(basketItemId);
            }
        }
    }
}
