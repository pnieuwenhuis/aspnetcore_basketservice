using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketService.Products.Routes
{
   [Route("/api/products")]
   public class ProductApiController
    {
        private GetProduct GetProduct { get; set; }
        public ProductApiController(GetProduct getProduct)
        {
            this.GetProduct = getProduct;
        }
        [HttpGet("{productId}")] public async Task<IActionResult> Get(int productId)
        {
            var result = await GetProduct.Execute(productId);
            return result;
        }
    }
}
