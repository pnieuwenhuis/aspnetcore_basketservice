using System;
using BasketService.Core.Messaging;

namespace BasketService.Baskets
{
    public partial class BasketActor
    {
        public class GetBasket : MessageWithCustomerId {
            public GetBasket(int customerId = 0) : base(customerId) { }
        }

        public class AddItemToBasket : MessageWithCustomerId {
            public readonly int ProductId;
            public readonly int Amount;

            public AddItemToBasket(int customerId = 0, int productId = 0, int amount = 0) : base(customerId)
            {
                this.ProductId = productId;
                this.Amount = amount;
            }
        }

        public class RemoveItemFromBasket : MessageWithCustomerId {
            public readonly Guid BasketItemId;

            public RemoveItemFromBasket(int customerId = 0, Guid basketItemId = new Guid()) : base(customerId)
            {
                this.BasketItemId = basketItemId;
            }
        }
    }
}
