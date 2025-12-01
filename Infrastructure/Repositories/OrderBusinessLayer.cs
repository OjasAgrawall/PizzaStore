using Microsoft.Data.SqlClient;
using System.Data;

namespace PizzaStore.Infrastructure.ModelBusinessLayer
{
    public class OrderBusinessLayer
    {
        public void AddCustomerId(int CustomerId)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";
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
        public void AddOrderPlaced(int Id, DateTime orderPlaced)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddOrderPlaced", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramOrderPlaced = new SqlParameter();
                paramOrderPlaced.ParameterName = "@OrderPlaced";
                paramOrderPlaced.Value = orderPlaced;
                cmd.Parameters.Add(paramOrderPlaced);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
