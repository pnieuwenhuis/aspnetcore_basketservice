namespace BasketService
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;
    using Akka.Actor;

    using Baskets;
    using Products;

    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Setup logging
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole()
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvcCore()
                    .AddJsonFormatters();

            services.AddSingleton<ActorSystem>(_ => ActorSystem.Create("basketservice"));

            services.AddBasketServices();
            services.AddProductServices();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                        ILoggerFactory loggerFactory)
        {
            app.UseMvc();
            loggerFactory.AddSerilog();
        }
    }
}
