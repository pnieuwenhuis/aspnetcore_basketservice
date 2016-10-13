using BasketService.Core.Messaging;

namespace BasketService.Baskets
{
    public partial class BasketActor
    {
        public class GetBasket : IEnvelopeWithCustomerId {
            public int CustomerId { get; set; }
        }

        public class AddItemToBasket : IEnvelopeWithCustomerId {
            public int CustomerId { get; set; }
            public int ProductId { get; set; }
            public int Amount { get; set; }
        }
    }
}
