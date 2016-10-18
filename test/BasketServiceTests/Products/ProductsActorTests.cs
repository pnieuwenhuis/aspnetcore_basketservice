using Xunit;
using BasketService.Products;
using Akka.TestKit.Xunit2;
using Akka.Actor;
using System.Collections.Generic;
using System.Linq;

namespace BasketServiceTests.Products
{
    public class ProductsActorTests : TestKit
    {
        private IEnumerable<Product> Products { get; } = GetProductData();

        public ProductsActorTests() : base("akka.suppress-json-serializer-warning = on") { }
        
        [Fact]
        public void Return_All_Products() 
        {
            var actorUnderTest = ActorOf(Props.Create<ProductsActor>(this.Products));
            actorUnderTest.Tell(
                new ProductsActor.GetAllProducts()
            );

            var result = ExpectMsg<IEnumerable<Product>>();
            Assert.Equal(this.Products, result);
        }

        [Fact]
        public void Update_Stock_When_Product_With_Enough_Stock_Is_Found()
        {
            var actorUnderTest = ActorOf(Props.Create<ProductsActor>(this.Products));
            actorUnderTest.Tell(new ProductsActor.UpdateStock(
                productId: 1000,
                amountChanged: 1
            ));

            var result = ExpectMsg<ProductsActor.StockUpdated>();
            Assert.Equal(this.Products.Single(p => p.Id == 1000), result.Product);
        }

        [Fact]
        public void Cannot_Update_Stock_Because_Insufficient_Stock()
        {
            var actorUnderTest = ActorOf(Props.Create<ProductsActor>(this.Products));
            actorUnderTest.Tell(new ProductsActor.UpdateStock(
                productId: 1000,
                amountChanged: -2
            ));

            var result = ExpectMsg<ProductsActor.InsuffientStock>();
        }

        [Fact]
        public void Cannot_Update_Stock_Because_Not_Found()
        {
            var actorUnderTest = ActorOf(Props.Create<ProductsActor>(this.Products));
            actorUnderTest.Tell(new ProductsActor.UpdateStock(
                productId: 9999,
                amountChanged: -1
            ));

            var result = ExpectMsg<ProductsActor.ProductNotFound>();
        }        

        private static IEnumerable<Product> GetProductData()
        {
            return new List<Product> {
                new Product {
                    Id = 1000,
                    Title = "Product 1",
                    InStock = 1
                },
                new Product {
                    Id = 1001,
                    Title = "Product 2",
                    InStock = 2
                }
            };
        }
    }
}
