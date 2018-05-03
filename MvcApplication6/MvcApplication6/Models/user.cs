using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MvcApplication6.Models
{
    public class user
    {
        SqlConnection con = new SqlConnection();
        List<user> user1 = new List<user>();

        public int UserID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string NIC { get; set; }
        public DateTime DOB{ get; set; }
        public bool IsCricket { get; set; }
        public bool Hockey { get; set; }
        public bool Chess { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn{ get; set; }

        user u = null;
        public List<user> GetUser()
        {
            con.ConnectionString = "Data Source=ADENZAHRA;Initial Catalog=Assignment4;Integrated Security=True";
            con.Open();
                using(con)
                {
                    SqlCommand cmd = new SqlCommand("Select * from Users", con);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        u = new user();
                        u.UserID = Convert.ToInt32(r.GetInt32(0));
                        u.Name = Convert.ToString(r.GetString(1));
                        u.Login = Convert.ToString(r.GetString(2)); 
                        u.Password = Convert.ToString(r.GetString(3));
                        //u.Gender = Convert.ToChar(r.GetChars(4));
                        u.Address = Convert.ToString(r.GetString(5));
                        u.Age = Convert.ToInt32(r.GetInt32(6));
                        u.NIC = Convert.ToString(r.GetString(7));
                        u.DOB = Convert.ToDateTime(r.GetDateTime(8));
                        u.IsCricket = Convert.ToBoolean(r.GetBoolean(9));
                        u.Hockey = Convert.ToBoolean(r.GetBoolean(10));
                        u.Chess = Convert.ToBoolean(r.GetBoolean(11));
                        u.ImageName = Convert.ToString(r.GetString(12));
                        u.CreatedOn = Convert.ToDateTime(r.GetDateTime(13));
                        user1.Add(u);
                    }
                }
                return user1;
        }
       
    }
}