using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Microsoft.Data.SqlClient;
namespace Data
{
    public class DProduct
    {

 
        public void Create(Product product)
        {
           
        }
        public List<Product> Read()
        {
            var products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_list_products", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("product_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            Stock = reader.GetInt32(reader.GetOrdinal("stock")),
                            Active = reader.GetBoolean(reader.GetOrdinal("active"))
                        };

                        products.Add(product);
                    }
                }
            }

            return products;
        }
        public void Update(Product product)
        {

        }
        public void Delete(Product product)
        {

        }

    }
}
