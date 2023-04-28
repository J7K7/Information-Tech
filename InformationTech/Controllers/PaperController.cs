using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using InformationTech.Models;

namespace InformationTech.Controllers
{
    public class PaperController : Controller
    {
        Class1 cs = new Class1();
        // GET: Paper

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

        public void chkcookie()
        {
            HttpCookie id = Request.Cookies["UID"];

            if (id != null)
            {
                string uid = Request.Cookies["UID"].Value.ToString().Substring(4);
                DataTable dt1 = cs.Getdata("select *from tbl_membership where user_id = '" + uid + "' ORDER BY user_id desc");
                if (dt1.Rows.Count > 0)
                {
                    ViewBag.cookieAllow1 = "display:block";
                    DateTime date = Convert.ToDateTime(dt1.Rows[0]["exp_date"]);
                    if (DateTime.Now > date)
                    {
                        Response.Redirect("../Home/Payment");
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
                    Response.Redirect("../Home/Payment");
                }
            }
            else
            {
                ViewBag.name = null;
                ViewBag.cookieAllow = "display:none";
                ViewBag.cookieAllow1 = "display:none";
                ViewBag.cookieNotAllow = "display:block";
                Response.Redirect("../User/Index");
            }

        }


        public ActionResult Index()
        {
            chkcookie();
            courses();
            ClientDb paper = new ClientDb();


            return View(paper.Exampaperdisplay());
        }


   

    }


}