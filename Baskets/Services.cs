using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Baskets
{
    public static class Services
    {
        public static void AddBasketServices(this IServiceCollection services)
        {
            services.AddSingleton<BasketsActorProvider>();

            services.AddSingleton<Routes.GetBasket>();
            services.AddSingleton<Routes.AddItemToBasket>();
            services.AddSingleton<Routes.RemoveItemFromBasket>();
        }
    }
}
