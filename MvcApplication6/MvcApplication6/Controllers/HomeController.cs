using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication6.Models;
using System.Data.SqlClient;

namespace MvcApplication6.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /User/
        SqlConnection con = new SqlConnection(@"Data Source=ADENZAHRA;Initial Catalog=Assignment4;Integrated Security=True");

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login()
        {
            string name = Request["name"];
            string login =Request["login"];
            string password = Request["pswd"];
            string email=Request["email"];
            string gender = Request["gender"];
            string address = Request["address"];
            string a = Request["age"];
            string cnic = Request["cnic"];
            string dob = Request["dob"];
            string cricket = Request["cricket"];
            string hockey = Request["hockey"];
            string chess = Request["chess"];
            string image = Request["image"];

            int atposition = email.IndexOf("@");
            int dotposition = email.LastIndexOf(".");

            if (atposition < 1 || dotposition < atposition + 2 || dotposition + 2 >= email.Length)
            {
                //TempData["registermsg"] = "Please enter a valid e-mail address i.e abc@gmail.com";
                //return RedirectToAction("Register", "Home");
                ViewBag.errorE = "Invalid email(abc@gmail.com)";
                ViewBag.name = name;
                ViewBag.login = login;
                ViewBag.gender = gender;
                ViewBag.address = address;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");
            }
            

            if (login.Length <= 3)
            {

                ViewBag.error = "Login should be atleast 3 character long.";
                ViewBag.name = name;
                ViewBag.gender = gender;
                ViewBag.address = address;
                ViewBag.email = email;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");

            }
            con.Open();
            String query = "Select Login,Email from Assignment4.dbo.[Users] where Login='" + login + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader read = cmd.ExecuteReader();
            
            if (read.HasRows)
            {
                ViewBag.errorLogin = "Login name already exits.";
                ViewBag.name = name;
                ViewBag.gender = gender;
                ViewBag.address = address;
                ViewBag.email = email;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");
            } 
            con.Close();
            con.Open();
            String q2 = "Select Email from Assignment4.dbo.[Users] where Email='" + email + "';";
            SqlCommand cmd2 = new SqlCommand(q2, con);
            read = cmd2.ExecuteReader();
            if (read.HasRows)
            {
                ViewBag.errorEmail = "Email already exits.";
                ViewBag.name = name;
                ViewBag.login = login;
                ViewBag.gender = gender;
                ViewBag.address = address;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");
            }
            con.Close();
            if (password.Length < 5)
            {
                ViewBag.erP = "Password should be atleast 5 character long.";
                ViewBag.name = name;
                ViewBag.gender = gender;
                ViewBag.login = login;
                ViewBag.address = address;
                ViewBag.email = email;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");
            }
            if (!IsDigitsOnly(a))
            {
                ViewBag.errorAge = "Age should be in numbers.";
                ViewBag.name = name;
                ViewBag.gender = gender;
                ViewBag.login = login;
                ViewBag.address = address;
                ViewBag.email = email;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");
            }
            if (!IsDigitsOnly(cnic)||cnic.Length<13)
            {
                ViewBag.errorCNIC = "String entered or Incomplete CNIC number.";
                ViewBag.name = name;
                ViewBag.gender = gender;
                ViewBag.login = login;
                ViewBag.address = address;
                ViewBag.email = email;
                ViewBag.age = a;
                ViewBag.cnic = cnic;
                ViewBag.dob = dob;
                return View("user");
            }

            int cr = Convert.ToInt32(cricket);
            if (cr != 1)
            {
                cr = 0;
            }
            int ho = Convert.ToInt32(hockey);
            if (ho != 1)
            {
                ho = 0;
            }
            int ch = Convert.ToInt32(chess);
            if (ch != 1)
            {
                ch = 0;
            }

            char gen = Convert.ToChar(gender);
            
            DateTime d = Convert.ToDateTime(dob);
            var date=d.Date;

            var uniqueName = "";
            if (Request.Files["image"] != null)
            {
                var file = Request.Files["image"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootpath = Server.MapPath("~/UploadedFiles");
                    var filesSavePath = System.IO.Path.Combine(rootpath, uniqueName);
                    file.SaveAs(filesSavePath);
                    Session["image"] = uniqueName;
                }
            }

            int age = Convert.ToInt32(a);

            con.Open();
            String q = "insert into Assignment4.dbo.[Users](Name,Login,Password,Gender,Address,Age,NIC,DOB,IsCricket,Hockey,Chess,ImageName,CreatedOn,Email) values('" + name + "','" + login + "','" + password + "','" + gen + "','" + address + "','" + age + "','" + cnic + "','"+date+"','" + cr + "','" + ho + "','" + ch + "','"+uniqueName+"',SYSDATETIME(),'"+email+"');";
            SqlCommand cmd1 = new SqlCommand(q, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            Session["name"] = name;
            
            
            return RedirectToAction("log", "Home");
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public ActionResult Existinguser()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        //[ActionName]
        [HttpPost]
        public ActionResult Adminlogin()
        {
            string login = Request["login"];
            string pass = Request["pswd"];
            con.Open();
            string query = "select Login,Password from Assignment4.dbo.[Admin] where Login='" + login + "' and Password='" + pass + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader data = cmd.ExecuteReader();
            if (data.HasRows)
            {
                ViewBag.name = login;
                return RedirectToAction("userdata","Home");
            }
            else
            {
                ViewBag.error = "No Admin Exits";
                return View("Admin");
            }

        }
        public ActionResult Userdata()
        {
            user u = new user();
            List<user> li = new List<user>();
            li = u.GetUser();

            return View(li);
        }
        public ActionResult Updateuser(string id)
        {
            int ob = int.Parse(id);
            //try
            //{
            con.Open();
            String selectQuery = "select * from Assignment4.dbo.[Users] where UserID=" + ob + ";";

            SqlCommand cmd = new SqlCommand(selectQuery, con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                @ViewBag.UFormID = Convert.ToInt32(rd.GetInt32(0));
                @ViewBag.Uname = Convert.ToString(rd.GetSqlValue(1));
                @ViewBag.Ulogin = Convert.ToString(rd.GetSqlValue(2));
                @ViewBag.Ugender = Convert.ToString(rd.GetSqlValue(4));
                @ViewBag.Uaddress = Convert.ToString(rd.GetSqlValue(5));
                @ViewBag.Uage = Convert.ToInt32(rd.GetInt32(6));
                @ViewBag.Ucnic = Convert.ToString(rd.GetSqlValue(7));
                @ViewBag.Udob = (Convert.ToDateTime(rd.GetDateTime(8))).Date;
                @ViewBag.Ucricket = Convert.ToBoolean(rd.GetBoolean(9));
                @ViewBag.Uhockey = Convert.ToBoolean(rd.GetBoolean(10));
                @ViewBag.Uchess = Convert.ToBoolean(rd.GetBoolean(11));
                @ViewBag.Uimage= Convert.ToString(rd.GetSqlValue(12));
                @ViewBag.Uemail = Convert.ToString(rd.GetSqlValue(14));
            }

            return View("user");
        }
        [HttpPost]
        public ActionResult Existuser()
        {
            string name = Request["login"];
            string password = Request["pswd"];
            con.Open();
            String query = "select Login,Password,ImageName from Assignment4.dbo.[Users] where Login='" + name + "' AND Password='" + password + "';";

            SqlCommand insertCmd = new SqlCommand(query, con);

            SqlDataReader reader = insertCmd.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    Session["name"] = name;

                    Session["image"] = Convert.ToString(reader[2]);
                   
                    con.Close();
                    return RedirectToAction("log", "Home");
                }
            }
            else
            {
                 TempData["registermsg"] = "Invalid User Please register first!!!!";
                 con.Close();
                 return RedirectToAction("user", "Home");
            }
            return View("esistnguser");
         }
        

        public ActionResult Log()
        {

            return View();
        }
    }
}
