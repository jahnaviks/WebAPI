using RegisterAPI.Models;
using RepositoryLayer.Repository_Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RegisterAPI.RegisterRepository
{
    public class RegisterMapper : IRegisterRepository
    {
        // Insert a row -- POST
        public void Register(Registration regs)
        {
            int tempId = 0;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
                SqlCommand cmd1 = new SqlCommand("sp_getId", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                tempId = Convert.ToInt32(cmd1.ExecuteScalar());
                tempId++;
                con.Close();

                var command = new SqlCommand("sp_RegisterQueries", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", tempId);
                command.Parameters.AddWithValue("@FirstName", regs.FirstName);
                command.Parameters.AddWithValue("@LastName", regs.LastName);
                command.Parameters.AddWithValue("@Address", regs.Address);
                command.Parameters.AddWithValue("@email", regs.Email);
                command.Parameters.AddWithValue("@MobileNumber", regs.PhoneNumber);
                command.Parameters.AddWithValue("@Password", regs.Password);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
        }


        // Validate Login password for entered emailID
        public string ValidateLogin(string email)
        {
            string pswd = null;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_getpassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pswd = dr.GetValue(0).ToString();
            }
            dr.Close();
            con.Close();
            return pswd;

        }


        // Get details of specified user -- GET single user details
        public Registration GetUserDetails(string email)
        {
            Registration regs = new Registration();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from Register where EmailId ='" + email + "'", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                regs.FirstName = dr1.GetValue(1).ToString();
                regs.LastName = dr1.GetValue(2).ToString();
                regs.PhoneNumber = dr1.GetValue(4).ToString();
                regs.Address = dr1.GetValue(5).ToString();
                regs.Password = dr1.GetValue(6).ToString();
            };
            con.Close();
            return regs;
        }


        // Update details of a single user -- PUT
        public void UpdateClick(Registration regs)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", regs.FirstName);
            cmd.Parameters.AddWithValue("@LastName", regs.LastName);
            cmd.Parameters.AddWithValue("@Address", regs.Address);
            cmd.Parameters.AddWithValue("@email", regs.Email);
            cmd.Parameters.AddWithValue("@MobileNumber", regs.PhoneNumber);
            cmd.Parameters.AddWithValue("@Password", regs.Password);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        // Delete a user -- DELETE
        public void DeleteClick(string email)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_deleteRow", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        // Get entire data from table -- GET
        public List<Registration> GetList()
        {
            List<Registration> lmd = new List<Registration>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lmd.Add(new Registration
                {
                    id = dr["id"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Email = dr["EmailId"].ToString(),
                    Address = dr["Address"].ToString(),
                    PhoneNumber = dr["MobileNumber"].ToString(),
                    Password = dr["Password"].ToString(),
                });
            }
            return lmd;
        }
    }
}