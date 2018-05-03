using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MvcApplication6.Models
{
    public class admin
    {
        SqlConnection con = new SqlConnection();
        //List<admin> admin1 = new List<admin>();
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}