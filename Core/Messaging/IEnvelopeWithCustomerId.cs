namespace BasketService.Core.Messaging
{
    public interface IEnvelopeWithCustomerId
    {
        int CustomerId { get; }
    }
}
