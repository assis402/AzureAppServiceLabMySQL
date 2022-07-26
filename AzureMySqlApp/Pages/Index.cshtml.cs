using Microsoft.AspNetCore.Mvc.RazorPages;
using AzureSqlApp.Models;
using AzureSqlApp.Services;

namespace AzureSqlApp.Pages
{
    public class IndexModel : PageModel
    {
        readonly IProductService _productService;
        public List<Product> Products;
        public bool IsBeta;

        public IndexModel(IProductService productService) => _productService = productService;

        public async Task OnGetAsync() 
        {
            IsBeta = await _productService.IsBeta();
            Products = _productService.GetProducts();
        }
    }
}