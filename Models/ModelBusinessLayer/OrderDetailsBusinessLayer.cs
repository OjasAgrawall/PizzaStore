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

    }
}
