using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BasketService.Products.Routes
{
   [Route("/api/products")]
   public class ProductApiController
    {
        private GetProduct GetProduct { get; set; }
        private GetAllProducts GetAllProducts { get; set; }
        public ProductApiController(GetProduct getProduct, GetAllProducts getAllProducts)
        {
            this.GetProduct = getProduct;
            this.GetAllProducts = getAllProducts;
        }
        [HttpGet("{productId}")] public async Task<IActionResult> Get(int productId)
        {
            var result = await this.GetProduct.Execute(productId);
            return result;
        }

        [HttpGet()] public async Task<IEnumerable<Product>> Get()
        {
            var result = await this.GetAllProducts.Execute();
            return result;
        }
    }
}
