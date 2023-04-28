using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationTech.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace InformationTech.Controllers
{
    public class UserController : Controller
    {

        Class1 cs = new Class1();

        public void chkcookie()
        {
            Class1 cs = new Class1();
            HttpCookie id = Request.Cookies["UID"];

            if (id != null)
            {
                string uid = Request.Cookies["UID"].Value.ToString().Substring(4);
                DataTable dt1 = cs.Getdata("select *from tbl_membership where user_id = '" + uid + "' order by user_id desc");
                if (dt1.Rows.Count > 0)
                {
                    ViewBag.cookieAllow1 = "display:block";
                    DateTime date = Convert.ToDateTime(dt1.Rows[0]["exp_date"]);
                    if (DateTime.Now > date)
                    {
                        Response.Redirect("Payment");
                    }
                    DataTable dt = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.cookieAllow = "display:block";
                        ViewBag.cookieNotAllow = "display:none";
                        ViewBag.name = ViewBag.name + dt.Rows[0]["first_name"].ToString().ToUpper() + " " + dt.Rows[0]["last_name"].ToString().ToUpper();

                        Response.Redirect("/Home/index");
                    }
                }
                else
                {
                    DataTable dt = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[0]["dateandtime"]);
                        date = date.AddDays(15);

                        if (DateTime.Now > date)
                        {
                            Response.Redirect("Payment");
                        }
                        ViewBag.cookieAllow1 = "display:none";
                        ViewBag.cookieAllow = "display:block";
                        ViewBag.cookieNotAllow = "display:none";
                        ViewBag.name = ViewBag.name + dt.Rows[0]["first_name"].ToString().ToUpper() + " " + dt.Rows[0]["last_name"].ToString().ToUpper();
                        Response.Redirect("/Home/index");
                    }
                    
                }


            }
            else
            {
                ViewBag.name = null;
                ViewBag.cookieAllow = "display:none";
                ViewBag.cookieAllow1 = "display:none";
                ViewBag.cookieNotAllow = "display:block";
            }
        }

        public void courses()
        {
            Class1 c1 = new Class1();
            string subject = "";
            string classes = "";
            string strbody = "";
            string stream = "";
            DataTable dt = c1.Getdata("select * from tbl_stream");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stream = dt.Rows[i]["stream"].ToString();
                strbody += "<li class='dropdown-submenu'><a href='#' class='test'>" + stream + " <i class='icon ion-ios-arrow-right' style='margin-left:5px; font-size:15px;'></i></a> <ul class='dropdown-menu'> ";
                DataTable dt2 = c1.Getdata("select * from tbl_class where stream_id = '" + dt.Rows[i]["stream_id"] + "'");
                if (dt2.Rows.Count > 0)
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {

                        classes = dt2.Rows[j]["class_name"].ToString();


                        strbody += "<li class='dropdown-submenu'> <a href='#' class='test'>" + classes + " <i class='icon ion-ios-arrow-right' style='margin-left:5px; font-size:15px;'></i></a> <ul class='dropdown-menu'> ";
                        DataTable dt3 = c1.Getdata("select * from tbl_subject where class_id = '" + dt2.Rows[j]["class_id"] + "'");
                        if (dt3.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt3.Rows.Count; k++)
                            {
                                subject = dt3.Rows[k]["subject_name"].ToString();
                                strbody += "<li> <a href='../../Subject/SubjectDetail/" + dt3.Rows[k]["subject_name"].ToString().Replace(" ", "-") + "'>" + subject + "</a></li>";


                            }

                        }
                        else
                        {
                            strbody += "<li> <a href='#'>Comming Soon</a></li>";
                        }
                        strbody += "</ul> </li>";
                    }

                }
                else
                {
                    strbody += "<li class='dropdown-submenu'> <a href='#' class='test'>Comming Soon</a></li>";
                }
                strbody += "</ul> </li>";
            }

            ViewBag.courses = strbody;

        }


        // GET: User
        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = "7vyas7jainish@gmail.com";
                string senderPassword = "jk2290800";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);





                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            chkcookie();
            courses();
            return View();
        }
        [HttpPost]
        public ActionResult Index(ClientRegister ilist,string submit)
        {
            chkcookie();
            courses();
            int row = 0;
            ClientDb clienadd = new ClientDb();
            if(submit  == "client_regi")
            {
                DataTable dt = cs.Getdata("select *from user_registration where email = '" + ilist.email + "'");
                if(dt.Rows.Count > 0)
                {
                   ViewBag.showdetail1 =  "This Email Address Is Already Exists.";
                }
                else
                {
                    String toEmail = ilist.email;
                    bool result = SendEmail(toEmail, "Registration", "<html><body><div style=border-top:3px solid #22BCE5 > &nbsp;</ div ><br /><h1 style=color:#22BCE5> Congratulations! You’ve successfully registered in Information Tech </h1><br /><h3> Welcome , "+ ilist.first_name.ToUpper() + " "+ ilist.last_name.ToUpper()  +" To Our Team. </h5><br /> <br /><br /> Thanks,<br />The information tech team </body></html> ");
                    if(result)
                    {
                        row = clienadd.insertrecrod("user_registration", "dateandtime,first_name,last_name,email,password,contact", "'" + ilist.DateAndTime + "','" + ilist.first_name + "','" + ilist.last_name + "','" + ilist.email + "','" + ilist.password + "','" + ilist.contact + "'");
                        if (row > 0)
                        {
                            ViewBag.showdetail = "You Are Registered Successfully. Please You First Login!";
                        }
                    }
                    else
                    {
                        ViewBag.showdetail1 = "Please Enter Valid Email Address.'";
                    }
                    
                }
                


            }
            ModelState.Clear();
            return View();

        }

        [HttpGet]
        public ActionResult Login()
        {
            chkcookie();
            courses();
            ViewBag.dt = "block";
            ViewBag.dt1 = "none";
            ViewBag.dt2 = "none";
            ViewBag.dt3 = "none";

            return View();
        }
        public static int num = 0;
        public static string id = "";

        [HttpPost]
        public ActionResult Login(ClientRegister ilist, string submit)
        {
            chkcookie();
            courses();
            if (submit == "client_regi")
            {
                ViewBag.dt = "block";
                ViewBag.dt1 = "none";
                ViewBag.dt2 = "none";
                ViewBag.dt3 = "none";
                SqlCommand cmdselect = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());

                cmdselect.Connection = con;
                cmdselect.CommandText = "select top 1 * from user_registration where (email=@contactEmail OR contact=@contactEmail) and password = @password";
                cmdselect.Parameters.AddWithValue("@contactEmail", SqlDbType.VarChar).Value = ilist.email;
                cmdselect.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = ilist.password;
                string id = "";


                try
                {
                    con.Close();
                    con.Open();

                    DataTable dt = new DataTable();
                    dt.Load(cmdselect.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        id = dt.Rows[0]["user_id"].ToString();


                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    //string message = ex.ToString();
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    //Response.Write("<script>alert('" + ex.ToString() + "')</script>");
                }
                finally
                {
                    con.Close();
                }

                if (id != "")
                {
                    //Create a Cookie with a suitable Key.
                    HttpCookie nameCookie = new HttpCookie("UID");

                    //Set the Cookie value.
                    nameCookie.Values["UID"] = id;

                    //Set the Expiry date.
                    nameCookie.Expires = DateTime.Now.AddDays(30);

                    //Add the Cookie to Browser.
                    Response.Cookies.Add(nameCookie);




                    //Session["ID"] = id;           
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewBag.showdetail = "Invalid User Name Or Password !!";
                   
                    //string message = "alert('Invalid User Name And Password !!')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            Random rno = new Random();
           
           
            if (submit == "submit")
            {
                SqlCommand cmdselect = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
                con.Open();
                cmdselect.Connection = con;
                try
                {
                    ViewBag.dt = "none";
                    ViewBag.dt1 = "block";
                    ViewBag.dt2 = "none";
                    ViewBag.dt3 = "none";
                   
                    cmdselect.CommandText = "select top 1 * from user_registration where email=@contactEmail ";
                    cmdselect.Parameters.AddWithValue("@contactEmail", SqlDbType.VarChar).Value = ilist.uemail;

                    DataTable dt = new DataTable();
                    dt.Load(cmdselect.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            id = dt.Rows[0]["user_id"].ToString();


                        }
                        num = rno.Next(1111, 9999);
                        String toEmail = ilist.uemail;
                        bool result = SendEmail(toEmail, "OTP", "<html><body><div style=border-top:3px solid #22BCE5 > &nbsp;</ div ><br /><h1 style=color:#22BCE5> Security code </h1><br />Please use the following security code for the Sms Portal account " + ilist.email + "<h5> Your Otp is " + num + " </h5><br /> <br /><br /> Thanks,<br />The information tech team </body></html> ");

                        if (result)
                        {
                            Response.Write("<script>alert('" + num + "')</script>");
                            ViewBag.dt1 = "none";
                            ViewBag.dt2 = "block";
                            ViewBag.dt3 = "none";
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Internet Connection !!')</script>");
                        }



                    }
                    else
                    {
                        Response.Write("<script>alert('This Email Not Exists !!')</script>");
                    }
                }
                catch(Exception  ex)
                { }
                finally
                {
                    con.Close();
                }
               
            }
            if(submit == "otp")
            {

                ViewBag.dt = "none";
                ViewBag.dt1 = "none";
                ViewBag.dt2 = "block";
                if (num.ToString() == ilist.otp)
                {
                   
                    ViewBag.dt2 = "none";
                    ViewBag.dt3 = "block";
                }
                else
                {
                    ViewBag.dt1 = "block";
                    ViewBag.dt2 = "none";
                    Response.Write("<script>alert('Invalid OTP !!')</script>");
                }
            }
            if(submit == "password")
            {
                if(ilist.password == ilist.rpassword)
                {
                    int i = 0;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
                    try
                    {


                        SqlCommand cmd = new SqlCommand();


                        string str = "update user_registration  set  password='" + ilist.password + "' where user_id = '" + id + "'";
                        cmd = new SqlCommand(str, con);
                        con.Open();
                        i = cmd.ExecuteNonQuery();
                        con.Close();


                    }
                    catch (Exception ex) { }
                    finally
                    {
                        con.Close();

                    }

                    ViewBag.dt = "block";
                    ViewBag.dt1 = "none";
                    ViewBag.dt2 = "none";
                    ViewBag.dt3 = "none";
                    Response.Write("<script>alert('Your Password Is Changed !!')</script>");
                }
                else
                {
                    ViewBag.dt = "none";
                    ViewBag.dt1 = "none";
                    ViewBag.dt2 = "none";
                    ViewBag.dt3 = "block";
                    Response.Write("<script>alert('Password Must Be Missmatched !!')</script>");
                }
            }
            ModelState.Clear();
           
            return View();
        }


        public ActionResult Logout()
        {
            HttpCookie nameCookie = new HttpCookie("UID");

            //Set the Expiry date.
            nameCookie.Expires = DateTime.Now.AddDays(-1);

            //Add the Cookie to Browser.
            Response.Cookies.Add(nameCookie);
            return RedirectToAction("../Home/Index");
        }


       
    }
}