using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasketService.Products.Routes
{
   [Route("/api/products")]
   public class ProductApiController
    {
        private GetAllProducts GetAllProducts { get; }
        public ProductApiController(GetAllProducts getAllProducts)
        {
            this.GetAllProducts = getAllProducts;
        }

        [HttpGet()] public async Task<IEnumerable<Product>> Get()
        {
            var result = await this.GetAllProducts.Execute();
            return result;
        }
    }
}
