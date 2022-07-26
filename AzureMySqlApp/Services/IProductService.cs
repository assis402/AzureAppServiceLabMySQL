using AzureSqlApp.Models;

namespace AzureSqlApp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}