namespace BasketService.Core.Messaging
{
    public abstract class MessageWithCustomerId
    {
        public readonly int CustomerId;

        public MessageWithCustomerId(int customerId = 0)
        {
            this.CustomerId = customerId;
        }
    }
}
