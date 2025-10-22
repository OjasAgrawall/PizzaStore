using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Net;

namespace PizzaStore.Models.ModelBusinessLayer
{
    public class OrderBusinessLayer
    {
        public void AddDetail(int CustomerId)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";
            Debug.WriteLine(CustomerId.ToString() + "1");

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddDetailsToOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCustomerId = new SqlParameter();
                paramCustomerId.ParameterName = "@CustomerId";
                paramCustomerId.Value = CustomerId;
                cmd.Parameters.Add(paramCustomerId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
