using System;
using Xunit;
using BasketService.Baskets;
using BasketService.Products;
using Akka.TestKit.Xunit2;
using Akka.Actor;
using System.Collections.Generic;

namespace BasketServiceTests.Baskets
{
    public class BasketActorTests : TestKit
    {
        private BasketActor.AddItemToBasket AddToBasketMessage()
        {
            return new BasketActor.AddItemToBasket(
                customerId: 1,
                productId: 1001,
                amount: 1
            );
        }

        public BasketActorTests() : base("akka.suppress-json-serializer-warning = on") { }
        
        [Fact]
        public void Return_Empty_Basket_After_Init() 
        {
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOf(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.Tell(
                new BasketActor.GetBasket()
            );

            var result = ExpectMsg<Basket>().Items;
            Assert.Empty(result);
        }

        [Fact]
        public void Return_Filled_Basket() 
        {
            var basketItem = new BasketItem { Id = Guid.NewGuid(), Title = "SomeProduct", ProductId = 1001 };
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOfAsTestActorRef<BasketActor>(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.UnderlyingActor.BasketState.Items.Add(basketItem);

            actorUnderTest.Tell(
                new BasketActor.GetBasket()
            );
            var result = ExpectMsg<Basket>().Items;
            Assert.Equal(new List<BasketItem> { basketItem }, result);
        }

        [Fact]
        public void Add_Item_To_Basket_Correctly()
        {
            var product = new Product { Id = 1001, Title = "SomeProduct", InStock = 1 };
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOfAsTestActorRef<BasketActor>(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.Tell(AddToBasketMessage());

            var updateMessage = productActor.ExpectMsg<ProductsActor.UpdateStock>();
            Assert.Equal(-1, updateMessage.AmountChanged);
            Assert.Equal(1001, updateMessage.ProductId);
            productActor.Reply(new ProductsActor.StockUpdated(product));

            var result = ExpectMsg<BasketActor.ItemAdded>();
            Assert.NotNull(result.BasketItemId);
            Assert.Equal(1, actorUnderTest.UnderlyingActor.BasketState.Items.Count);
        }

        [Fact]
        public void Addition_Fails_Because_Insuffient_Stock()
        {
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOf(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.Tell(AddToBasketMessage());

            var updateMessage = productActor.ExpectMsg<ProductsActor.UpdateStock>();
            productActor.Reply(new ProductsActor.InsuffientStock());

            var result = ExpectMsg<BasketActor.NotInStock>();
        }

        [Fact]
        public void Addition_Fails_Because_Product_Not_Found()
        {
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOf(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.Tell(AddToBasketMessage());

            var updateMessage = productActor.ExpectMsg<ProductsActor.UpdateStock>();
            productActor.Reply(new ProductsActor.ProductNotFound());

            var result = ExpectMsg<BasketActor.ProductNotFound>();
        }

        [Fact]
        public void Remove_Item_From_Basket_Correctly()
        {
            var basketItemId = new Guid("0fe5c840-ecc2-4c7c-b3fe-034f9a23bec2");
            var basketItem = new BasketItem { Id = basketItemId, Title = "SomeProduct", ProductId = 1001 };
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOfAsTestActorRef<BasketActor>(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.UnderlyingActor.BasketState.Items.Add(basketItem);

            actorUnderTest.Tell(new BasketActor.RemoveItemFromBasket(customerId: 0, basketItemId: basketItemId));
            var result = ExpectMsg<BasketActor.ItemRemoved>();
            Assert.Equal(0, actorUnderTest.UnderlyingActor.BasketState.Items.Count);
        }

        [Fact]
        public void Remove_Item_Fails_Because_Item_Not_In_Basket()
        {
            var basketItemId = new Guid("0fe5c840-ecc2-4c7c-b3fe-034f9a23bec2");
            var basketItem = new BasketItem { Id = basketItemId, Title = "SomeProduct", ProductId = 1001 };
            var productActor = CreateTestProbe("products");
            var actorUnderTest = ActorOfAsTestActorRef<BasketActor>(Props.Create<BasketActor>(productActor.Ref));
            actorUnderTest.UnderlyingActor.BasketState.Items.Add(basketItem);

            var otherBasketItemId = new Guid("51b9ad1c-d1f2-4d61-a8e1-4dbf961b590f");
            actorUnderTest.Tell(new BasketActor.RemoveItemFromBasket(customerId: 0, basketItemId: otherBasketItemId));
            var result = ExpectMsg<BasketActor.ItemNotFound>();
        }
    }
}
