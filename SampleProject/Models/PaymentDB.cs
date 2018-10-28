using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SampleProject.Models
{
    public class PaymentDB
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        public decimal GetCostById(int id)
        {
            decimal myid=0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("spGetCostByCId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = com.ExecuteReader();
                while(rdr.Read())
                {
                    myid= Convert.ToDecimal(rdr["payment"]);
                }
                
            }

            return myid;

        }
    }
}