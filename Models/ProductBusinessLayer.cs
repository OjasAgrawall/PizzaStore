using Microsoft.Data.SqlClient;
using PizzaStore.Data;
using System.Data;
using System.Diagnostics;

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

        public void UpdateProduct(Products products)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";


            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spUpdateProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = products.Id;
                cmd.Parameters.Add(paramId);
                
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
