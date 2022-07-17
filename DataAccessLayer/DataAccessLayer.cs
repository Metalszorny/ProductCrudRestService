using Common.Interfaces;
using Common.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        public static readonly string MissingApplicationConfiguration = "The application's configuration is not found.";
        public static readonly string MissingConnectionString = "The connection string to the database is not found in the application's configuration.";
        private readonly IConfiguration configuration;
        private string connectionString;
        private SqlConnection connection;

        public DataAccessLayer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Product AddProduct(Product product)
        {
            var command = "INSERT INTO [dbo].[Products] (ean, price, is_deleted) VALUES (@param1, @param2, @param3)";
            this.OpenConnection();

            using (var sqlCommand = new SqlCommand(command, this.connection))
            {
                sqlCommand.Parameters.AddWithValue("@param1", product.Ean);
                sqlCommand.Parameters.AddWithValue("@param2", product.Price);
                sqlCommand.Parameters.AddWithValue("@param3", false);
                sqlCommand.ExecuteNonQuery();
            }

            return this.GetProduct(product.Ean);
        }

        public Product EditProduct(Product product)
        {
            var command = "UPDATE [dbo].[Products] SET price = @param2 WHERE ean = @param1";
            this.OpenConnection();

            using (var sqlCommand = new SqlCommand(command, this.connection))
            {
                sqlCommand.Parameters.AddWithValue("@param1", product.Ean);
                sqlCommand.Parameters.AddWithValue("@param2", product.Price);
                sqlCommand.ExecuteNonQuery();
            }

            return this.GetProduct(product.Ean);
        }

        public Product GetProduct(string ean)
        {
            var product = new Product();
            var command = "SELECT TOP 1 ean, price FROM [dbo].[Products] WHERE ean = @param1 AND is_deleted = 0";
            this.OpenConnection();

            using (var sqlCommand = new SqlCommand(command, this.connection))
            {
                sqlCommand.Parameters.AddWithValue("@param1", ean);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        product = new Product()
                        {
                            Ean = sqlDataReader.GetString(0),
                            Price = sqlDataReader.GetString(1),
                        };
                    }

                    sqlDataReader.Close();
                }
            }

            return product;
        }

        public ICollection<Product> GetProducts()
        {
            var products = new List<Product>();
            var command = "SELECT ean, price FROM [dbo].[Products] WHERE is_deleted = 0";
            this.OpenConnection();

            using (var sqlCommand = new SqlCommand(command, this.connection))
            {
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        products.Add(new Product()
                        {
                            Ean = sqlDataReader.GetString(0),
                            Price = sqlDataReader.GetString(1),
                        });
                    }

                    sqlDataReader.Close();
                }
            }

            return products;
        }

        public void RemoveProduct(string ean)
        {
            var command = "UPDATE [dbo].[Products] SET is_deleted = 1 WHERE ean = @param1";
            this.OpenConnection();

            using (var sqlCommand = new SqlCommand(command, this.connection))
            {
                sqlCommand.Parameters.AddWithValue("@param1", ean);
                sqlCommand.ExecuteNonQuery();
            }
        }

        private void OpenConnection()
        {
            if (this.configuration == null)
            {
                throw new ApplicationException(MissingApplicationConfiguration);
            }

            if (string.IsNullOrWhiteSpace(this.connectionString))
            {
                this.connectionString = this.configuration.GetConnectionString("ProductCrudRestServiceDatabase");

                if (string.IsNullOrWhiteSpace(this.connectionString))
                {
                    throw new ApplicationException(MissingConnectionString);
                }
            }

            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.connectionString);
            }

            if (this.connection.State != ConnectionState.Open)
            {
                this.connection.Open();
            }
        }
    }
}
