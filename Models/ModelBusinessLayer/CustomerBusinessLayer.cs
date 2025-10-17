using Microsoft.Data.SqlClient;
using System.Data;

namespace PizzaStore.Models.ModelBusinessLayer
{
    public class CustomerBusinessLayer
    {
        public void AddCustomer(Customer customer)
        {
            string connectionstring = "Server=.;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("spAddCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFName = new SqlParameter();
                paramFName.ParameterName = "@FName";
                paramFName.Value = customer.FirstName;
                cmd.Parameters.Add(paramFName);

                SqlParameter paramLName = new SqlParameter();
                paramLName.ParameterName = "@LName";
                paramLName.Value = customer.LastName;
                cmd.Parameters.Add(paramLName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = customer.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = customer.Password;
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramAddress = new SqlParameter();
                paramAddress.ParameterName = "@Address";
                paramAddress.Value = customer.Address;
                cmd.Parameters.Add(paramAddress);

                SqlParameter paramPhone = new SqlParameter();
                paramPhone.ParameterName = "@Phone";
                paramPhone.Value = customer.Phone;
                cmd.Parameters.Add(paramPhone);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
