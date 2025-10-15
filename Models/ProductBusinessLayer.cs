using Microsoft.Data.SqlClient;
using PizzaStore.Data;
using System.Data;

namespace PizzaStore.Models
{
    public class ProductBusinessLayer
    {
        public void AddProduct(Products products)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = products.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramPrice = new SqlParameter();
                paramPrice.ParameterName = "@Price";
                paramPrice.Value = products.Price;
                cmd.Parameters.Add(paramPrice);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
