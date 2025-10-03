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
    public class DCustomer
    {
        public void Create(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_insert_customer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@address", customer.Address ?? "");
                cmd.Parameters.AddWithValue("@phone", customer.Phone ?? "");
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Customer> Read()
        {
            var customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_list_customers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new Customer
                        {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("customer_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Address = reader.IsDBNull(reader.GetOrdinal("address")) ? "" : reader.GetString(reader.GetOrdinal("address")),
                            Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? "" : reader.GetString(reader.GetOrdinal("phone")),
                            Active = reader.GetBoolean(reader.GetOrdinal("active"))
                        };
                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }

        public void Update(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_update_customer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_id", customer.CustomerID);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@address", customer.Address ?? "");
                cmd.Parameters.AddWithValue("@phone", customer.Phone ?? "");
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_delete_customer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_id", customerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}