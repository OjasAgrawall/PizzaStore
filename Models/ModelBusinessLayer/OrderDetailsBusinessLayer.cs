using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using PizzaStore.Data;
using System.Data;

namespace PizzaStore.Models.ModelBusinessLayer
{
    public class OrderDetailsBusinessLayer
    {
        //Add a new item order to a cart to be later added to an order to be added to a customer
        public void AddItem(Product product, int quantity)
        {   
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";

            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddItem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramQuantity = new SqlParameter();
                paramQuantity.ParameterName = "@Quantity";
                paramQuantity.Value = quantity;
                cmd.Parameters.Add(paramQuantity);

                SqlParameter paramProductId = new SqlParameter();
                paramProductId.ParameterName = "@ProductId";
                paramProductId.Value = product.Id;
                cmd.Parameters.Add(paramProductId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteItem(int id)
        {
            string connectionString = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteItem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateItem(int id, int quantity)
        {
            string connectionString = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateItem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramQuantity = new SqlParameter();
                paramQuantity.ParameterName = "@Quantity";
                paramQuantity.Value = quantity;
                cmd.Parameters.Add(paramQuantity);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
