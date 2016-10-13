using System;

namespace BasketService.Baskets
{
    public partial class BasketActor
    {
        public abstract class BasketEvent {}

        public class ItemAdded : BasketEvent
        {
            public Guid BasketItemId { get; set; }
        }

        public class NotInStock : BasketEvent {}
        public class ProductNotFound : BasketEvent {}
    }
}
