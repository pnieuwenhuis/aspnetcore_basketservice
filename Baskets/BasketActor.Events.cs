using System;

namespace BasketService.Baskets
{
    public partial class BasketActor
    {
        public abstract class BasketEvent {}

        public class ItemAdded : BasketEvent
        {
            public readonly Guid BasketItemId;

            public ItemAdded(Guid basketItemId = new Guid()) 
            {
                this.BasketItemId = basketItemId;
            }
        }

        public class NotInStock : BasketEvent {}
        public class ProductNotFound : BasketEvent {}

        public class ItemRemoved : BasketEvent {}

        public class ItemNotFound : BasketEvent {}
    }
}
