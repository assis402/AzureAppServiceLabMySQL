using AzureSqlApp.Models;
using MySql.Data.MySqlClient;

namespace AzureSqlApp.Services
{
    public class ProductService : IProductService
    {
        readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        MySqlConnection GetConnection()
        {
            //Configuration
            //var key = "SQLAZURECONNSTR_DB_CONNECT";
            //var connectionString = Environment.GetEnvironmentVariable(key);
            //return new SqlConnection(_configuration.GetConnectionString("DB_CONNECT"));

            var connectionString = "Server=mysqlassislab.mysql.database.azure.com; Port=3306; Database=appdb; Uid=assis@mysqlassislab; Pwd=32833113Mt32833113; SslMode=Preferred;";

            return new MySqlConnection(connectionString);
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
