using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using InformationTech.Models;
using System.IO;

namespace InformationTech.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        Class1 cs = new Class1();

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
            courses();
        }



        [HttpGet]

        public ActionResult Index()
        {
            chkcookie();
            HttpCookie id = Request.Cookies["UID"];
            string uid = "";
            if (id != null)
            {
                uid = Request.Cookies["UID"].Value.ToString().Substring(4);
                DataTable dt1 = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                if (dt1.Rows.Count > 0)
                {
                    ViewBag.user_id = dt1.Rows[0]["user_id"].ToString();
                    ViewBag.image = dt1.Rows[0]["photo"].ToString();
                    ViewBag.name1 = dt1.Rows[0]["first_name"].ToString().ToUpper();
                    ViewBag.name2 = dt1.Rows[0]["last_name"].ToString().ToUpper();
                    ViewBag.contact = dt1.Rows[0]["contact"].ToString();
                    ViewBag.email = dt1.Rows[0]["email"].ToString();
                    ViewBag.dt = dt1.Rows[0]["dateandtime"].ToString();
                }
                dt1 = cs.Getdata("select *from tbl_membership where user_id = '" + uid + "'");
                if (dt1.Rows.Count > 0)
                {
                    ViewBag.memberdt = dt1.Rows[0]["joining_date"].ToString();
                    ViewBag.expdt = dt1.Rows[0]["exp_date"].ToString();
                }

            }

            return View();

        }


        [HttpGet]
        public ActionResult Changepassword()
        {
            chkcookie();
            HttpCookie id = Request.Cookies["UID"];



            return View();
        }


        [HttpPost]

        public ActionResult Changepassword(clientchangepass ilist, string submit)
        {
            chkcookie();
            HttpCookie id = Request.Cookies["UID"];
            string uid = "";

            ClientDb clientchange = new ClientDb();
            if (id != null)
            {
                uid = Request.Cookies["UID"].Value.ToString().Substring(4);

                if (submit == "client_pass")
                {

                    DataTable dt = cs.Getdata("select * from user_registration where user_id = '" + uid + "' AND password = '" + ilist.oldpass + "'");
                    if (dt.Rows.Count > 0)
                    {
                        ilist.user_id = Convert.ToInt32(uid);
                        if (clientchange.PasswordUpdate(ilist))
                        {
                            ViewBag.ab = "Password Update Successfully.";
                        }
                        
                    }
                    else
                    {
                        Response.Write("<script>alert('old password is wrong')</script>");

                    }

                }

            }
            ModelState.Clear();
            return View();
        }

        public ActionResult result()
        {
            chkcookie();
            HttpCookie id = Request.Cookies["UID"];
            string uid = "";
            uid = Request.Cookies["UID"].Value.ToString().Substring(4);

            if (id != null)
            {
               
                DataTable dt1 = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                if (dt1.Rows.Count > 0)
                {
                    ViewBag.user_id = dt1.Rows[0]["user_id"].ToString();
                    ViewBag.image = dt1.Rows[0]["photo"].ToString();
                    ViewBag.name1 = dt1.Rows[0]["first_name"].ToString();
                    ViewBag.name2 = dt1.Rows[0]["last_name"].ToString();
                    ViewBag.contact = dt1.Rows[0]["contact"].ToString();
                    ViewBag.email = dt1.Rows[0]["email"].ToString();
                    ViewBag.dt = dt1.Rows[0]["dateandtime"].ToString();
                }

            }

            ClientDb clientresult = new ClientDb();
     
                
            return View(clientresult.Examresultdisplay(Convert.ToInt32(uid)));
        }
        [HttpGet]
        public ActionResult editprofile()
        {
            chkcookie();
            HttpCookie id = Request.Cookies["UID"];
            string uid = "";

            uid = Request.Cookies["UID"].Value.ToString().Substring(4);

            ClientDb clientprofile = new ClientDb();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(clientprofile.Updateprofile().Find(clientupdate => clientupdate.user_id == Convert.ToInt32(uid)));

           
        }
        [HttpPost]
        public ActionResult editprofile(ClientRegister iList, string submit, HttpPostedFileBase photo)
        {
            chkcookie();
            if (submit == "submit")
            {
                ClientDb clientprofile = new ClientDb();


                if (photo == null)
                {



                    if (clientprofile.Updateprofile(iList))
                    {
                        //ViewBag.success = "display:block;";
                        //ViewBag.successText = "Save Record.";
                        //ViewBag.error = "display:none;";


                        ModelState.Clear();

                        return RedirectToAction("index");
                    }
                    else
                    {
                        //ViewBag.success = "display:none;";
                        //ViewBag.error = "display:block;";
                        //ViewBag.errorText = "Something Missing.";
                    }
                }
                else
                {

                    string photoPath = "~" + iList.photo;

                    string paths = Server.MapPath(@"" + photoPath);
                    FileInfo files = new FileInfo(paths);
                    if (files.Exists)
                    {
                        files.Delete();
                    }


                    Random rno = new Random();
                    int num1 = rno.Next(1, 10000);


                    string logopath1 = "/Content/Userprofile/" + num1 + photo.FileName;
                    string path = Server.MapPath(logopath1);
                    photo.SaveAs(path);


                    iList.photo = logopath1;



                    if (clientprofile.Updateprofile(iList))
                    {
                        //ViewBag.success = "display:block;";
                        //ViewBag.successText = "Save Record.";
                        //ViewBag.error = "display:none;";


                        ModelState.Clear();

                        return RedirectToAction("index");
                    }
                    else
                    {
                        //    ViewBag.success = "display:none;";
                        //    ViewBag.error = "display:block;";
                        //    ViewBag.errorText = "Something Missing.";
                        //}

                    }

                }



               
            }

            return View();
        }
       
    }
}