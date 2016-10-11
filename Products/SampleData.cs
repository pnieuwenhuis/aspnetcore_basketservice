using System.Collections.Generic;

namespace BasketService.Products
{
    public static class SampleData {
        public static IEnumerable<Product> Get()
        {
            return new List<Product> {
                new Product {
                    Id = 1000,
                    Title = "Playstation 4 500GB",
                    Brand = "Sony",
                    Price = 29900,
                    InStock = 5
                },
                new Product {
                    Id = 1001,
                    Title = "Playstation 4 Pro 1TB",
                    Brand = "Sony",
                    Price = 39900,
                    InStock = 2
                },
                new Product {
                    Id = 1002,
                    Title = "XBOX One",
                    Brand = "Microsoft",
                    Price = 26700,
                    InStock = 10
                },
                new Product {
                    Id = 1003,
                    Title = "XBOX One Scorpio",
                    Brand = "Microsoft",
                    Price = 499000,
                    InStock = 1
                },
                new Product {
                    Id = 1004,
                    Title = "Wii U",
                    Brand = "Nintendo",
                    Price = 19900,
                    InStock = 8
                },
            };
        }
    }
}
