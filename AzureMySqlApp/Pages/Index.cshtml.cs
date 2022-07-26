using Microsoft.AspNetCore.Mvc.RazorPages;
using AzureSqlApp.Models;
using AzureSqlApp.Services;

namespace AzureSqlApp.Pages
{
    public class IndexModel : PageModel
    {
        readonly IProductService _productService;
        public List<Product> Products;

        public IndexModel(IProductService productService) => _productService = productService;

        public void OnGetAsync() 
        {
            Products = _productService.GetProducts();
        }
    }
}