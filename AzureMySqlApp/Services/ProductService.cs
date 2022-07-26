using AzureSqlApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.FeatureManagement;
using MySql.Data.MySqlClient;

namespace AzureSqlApp.Services
{
    public class ProductService : IProductService
    {
        readonly IConfiguration _configuration;
        readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }

        public async Task<bool> IsBeta() => await _featureManager.IsEnabledAsync("beta");

        MySqlConnection GetConnection()
        {
            //Configuration
            //var key = "SQLAZURECONNSTR_DB_CONNECT";
            //var connectionString = Environment.GetEnvironmentVariable(key);
            //return new SqlConnection(_configuration.GetConnectionString("DB_CONNECT"));

            return new MySqlConnection(_configuration["DB_CONNECT"]);
        }

        public List<Product> GetProducts()
        {
            var connection = GetConnection();

            var productList = new List<Product>();
            var statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            connection.Open();

            var command = new MySqlCommand(statement, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    productList.Add(product);
                }
            }

            connection.Close();

            return productList;
        }
    }
}
