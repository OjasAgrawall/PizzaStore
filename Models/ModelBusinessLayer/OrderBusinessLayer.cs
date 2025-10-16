using Microsoft.Data.SqlClient;
using System.Data;

namespace PizzaStore.Models.ModelBusinessLayer
{
    public class OrderBusinessLayer
    {
        //The user should select all the orderDetails they want to add and then add then to this
        public void AddDetail(OrderDetail detail)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddDetailsToOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter param
            }
        }
    }
}
