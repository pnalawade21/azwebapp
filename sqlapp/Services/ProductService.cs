using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string? db_source = "appserver2121.database.windows.net";
        private static string? db_user = "sqladmin";
        private static string? db_password = "$Prashant";
        private static string? db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.Password = db_password;
            builder.UserID = db_user;
            builder.InitialCatalog = db_database;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            SqlConnection sqlConnection = GetConnection();

            string statement = "SELECT ProductID, ProductName, Quantity FROM PRODUCTS";
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);

            using(SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                    products.Add(product);
                }
            }
            sqlConnection.Close();

            return products;
        }
    }
}
