using System;
using System.Collections.Generic;
using System.Data;
using Entity;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class DInvoice
    {
        public int Create(Invoice invoice)
        {
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_insert_invoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_id", invoice.CustomerID);
                cmd.Parameters.AddWithValue("@date", invoice.Date);
                cmd.Parameters.AddWithValue("@total", invoice.Total);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result); // Devuelve el ID de la factura creada
            }
        }

        public List<Invoice> Read()
        {
            var invoices = new List<Invoice>();
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_list_invoices", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoice = new Invoice
                        {
                            InvoiceID = reader.GetInt32(reader.GetOrdinal("invoice_id")),
                            CustomerID = reader.GetInt32(reader.GetOrdinal("customer_id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("date")),
                            Total = reader.GetDecimal(reader.GetOrdinal("total")),
                            Active = reader.GetBoolean(reader.GetOrdinal("active"))
                        };
                        invoices.Add(invoice);
                    }
                }
            }
            return invoices;
        }

        public void Update(Invoice invoice)
        {
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_update_invoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@invoice_id", invoice.InvoiceID);
                cmd.Parameters.AddWithValue("@customer_id", invoice.CustomerID);
                cmd.Parameters.AddWithValue("@date", invoice.Date);
                cmd.Parameters.AddWithValue("@total", invoice.Total);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int invoiceId)
        {
            using (SqlConnection conn = new SqlConnection(Constant._connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_delete_invoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@invoice_id", invoiceId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}