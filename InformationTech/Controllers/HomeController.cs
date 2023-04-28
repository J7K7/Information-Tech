using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationTech.Models;
using System.Data;
using System.Data.SqlClient;

namespace InformationTech.Controllers
{
    public class HomeController : Controller
    {
        Class1 c1 = new Class1();
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
                        ViewBag.cookieAllow = "display:block";
                        ViewBag.cookieAllow1 = "display:none";
                        ViewBag.cookieNotAllow = "display:none";
                        ViewBag.name = ViewBag.name + dt.Rows[0]["first_name"].ToString().ToUpper() + " " + dt.Rows[0]["last_name"].ToString().ToUpper();

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


        public void chkcookie1()
        {
            Class1 cs = new Class1();
            HttpCookie id = Request.Cookies["UID"];

            if (id != null)
            {
                string uid = Request.Cookies["UID"].Value.ToString().Substring(4);
                DataTable dt = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                if (dt.Rows.Count > 0)
                {
                    ViewBag.cookieAllow = "display:block";
                    ViewBag.cookieNotAllow = "display:none";
                    ViewBag.name = ViewBag.name + dt.Rows[0]["first_name"].ToString().ToUpper() + " " + dt.Rows[0]["last_name"].ToString().ToUpper();

                }
            }
            else
            {
                ViewBag.name = null;
                ViewBag.cookieAllow = "display:none";
                ViewBag.cookieNotAllow = "display:block";
                Response.Redirect("../User/Index");
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

        // GET: Home
        public ActionResult Index()
        {

            ClientDb Courseview = new ClientDb();
            chkcookie();
            courses();
            //ViewBag.name = "Swaroop Daiya";
            DataTable dt = c1.Getdata("select * from tbl_price");
            if (dt.Rows.Count > 0)
            {

                ViewBag.price = dt.Rows[0]["price"].ToString();
                ViewBag.days = dt.Rows[0]["exp_days"].ToString();
            }
            return View(Courseview.display());


        }

        [HttpGet]
        public ActionResult Payment()
        {
            chkcookie1();
            courses();

            return View();
        }

        [HttpPost]
        public ActionResult Payment(string submit)
        {
            chkcookie1();
            courses();
            if (submit == "submit")
            {
                int row = 0;
                HttpCookie id = Request.Cookies["UID"];
                string uid = Request.Cookies["UID"].Value.ToString().Substring(4);
                DataTable dt = c1.Getdata("select * from tbl_price");
                if (dt.Rows.Count > 0)
                {

                    int price = Convert.ToInt32(dt.Rows[0]["price"]);
                    int exp_days = Convert.ToInt32(dt.Rows[0]["exp_days"]);
                    DateTime date = DateTime.Now;
                    date = date.AddDays(exp_days);
                    ITDBModel payment = new ITDBModel();
                    row = payment.insertrecrod("tbl_membership", "dateandtime,user_id,joining_date,exp_date,price", "'" + DateTime.Now.ToString() + "','" + uid + "','" + DateTime.Now.ToString() + "','" + date.ToString() + "','" + price + "'");
                    if (row > 0)
                    {
                        Response.Redirect("Index");

                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Account Detail');</script>");
                    }

                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult ContactUs()
        {
            chkcookie();
            courses();
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(contact ilist,string submit)
        {
            chkcookie();
            courses();
            if(submit == "message")
            {
                ClientDb enquiry = new ClientDb();
                int row = 0;
                row = enquiry.insertrecrod("tbl_contact", "dateandtime,name,email,subject,message", "'" + ilist.DateAndTime + "','" + ilist.name + "','" + ilist.email + "','" + ilist.subject + "','" + ilist.message + "'");
                if (row > 0)
                {
                    ViewBag.ab = "Save Successfully.";
                }
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            chkcookie();
            courses();

            return View();
        }
    }
}