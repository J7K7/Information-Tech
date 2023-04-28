using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using InformationTech.Models;

namespace InformationTech.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        Class1 cs = new Class1();

        public void chkcookie()
        {
            Class1 cs = new Class1();
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
                    DataTable dt = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[0]["dateandtime"]);
                        date = date.AddDays(15);

                        if (DateTime.Now > date)
                        {
                            Response.Redirect("../Home/Payment");
                        }
                        ViewBag.cookieAllow1 = "display:none";
                        ViewBag.cookieAllow = "display:block";
                        ViewBag.cookieNotAllow = "display:none";
                        ViewBag.name = ViewBag.name + dt.Rows[0]["first_name"].ToString().ToUpper() + " " + dt.Rows[0]["last_name"].ToString().ToUpper();

                    }
                }


            }
            else
            {
                ViewBag.name = null;
                ViewBag.cookieAllow1 = "display:none";
                ViewBag.cookieAllow = "display:none";
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
                DataTable dt1 = cs.Getdata("select *from tbl_membership where user_id = '" + uid + "'");
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
                    DataTable dt = cs.Getdata("select *from user_registration where user_id = '" + uid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[0]["dateandtime"]);
                        date = date.AddDays(15);

                        if (DateTime.Now > date)
                        {
                            Response.Redirect("../Home/Payment");
                        }
                        ViewBag.cookieAllow1 = "display:none";
                        ViewBag.cookieAllow = "display:block";
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
                Response.Redirect("../../User/Index");
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

        public ActionResult Index()
        {
            courses();
            ClientDb Subjectview = new ClientDb();
            chkcookie();
            return View(Subjectview.display());
           
        }
        public ActionResult SubjectDetail(string id)
        {
            courses();
            chkcookie();
            ViewBag.subject = id.ToUpper().Replace("-"," ");
            string image;
            ClientDb SubjecDetailtview = new ClientDb();
           
            image = SubjecDetailtview.subjectimage(id);
            ViewBag.image = image;
            return View(SubjecDetailtview.subjectdisplay(id));
        }

        public ActionResult ChapterDetail(string id)
        {
            courses();
            chkcookie1();
            string[] id1 = id.Split('_');
            ViewBag.chapter = id1[1];

            ClientDb ChapterDetailtview = new ClientDb();

           DataTable dt = ChapterDetailtview.display1(Convert.ToInt32(id1[0]));
            string body = "";
            int cnt = 1;
            for(int i = 0;i<dt.Rows.Count;i++)
            {
                body += "<li><a href='/Subject/ChapterDetail/"+ dt.Rows[i]["subject_id"].ToString()+"_"+ dt.Rows[i]["chapter_name"].ToString().Replace(" ","-") + "'>Chapter - " +cnt +" "+dt.Rows[i]["chapter_name"].ToString();
                body += "</a></li>";
                cnt += 1;
            }
            ViewBag.dt = body;


            return View(ChapterDetailtview.chapterdisplay(id));

            
        }

        public ActionResult TopicDetail(string id )
        {
            courses();
            chkcookie1();
            string[] id1 = id.Split('_');
            ClientDb TopicDetailtview = new ClientDb();

            DataTable dt1 = TopicDetailtview.display2(id);
            string sid = dt1.Rows[0]["subject_id"].ToString();

            DataTable dt = TopicDetailtview.display1(Convert.ToInt32(sid));
            string body = "";
            int cnt = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                body += "<li><a href='/Subject/ChapterDetail/" + dt.Rows[i]["subject_id"].ToString() + "_" + dt.Rows[i]["chapter_name"].ToString().Replace(" ", "-") + "'>Chapter - " + cnt + " " + dt.Rows[i]["chapter_name"].ToString();
                body += "</a></li>";
                cnt += 1;
            }
            ViewBag.dt = body;


            string body1 = "";
            DataTable dtTpID = cs.Getdata("select chapter_id from tbl_topic where topic_id = '" + id1[0] + "'");
            if (dtTpID.Rows.Count > 0)
            {

                DataTable dtTpDt = cs.Getdata("select *, CAST(topic_id as varchar(50)) + '_' + LOWER(REPLACE(topic_tittle, ' ', '-')) as url from tbl_topic where chapter_id = " + dtTpID.Rows[0]["chapter_id"].ToString() + " and topic_id != " + id1[0] + "");
                if (dtTpDt.Rows.Count > 0)
                {
                    for (int j = 0; j < dtTpDt.Rows.Count; j++)
                    {
                        body1 += "<div class='single__courses'>";
                        body1 += "<div class='recent__post__thumb'>";
                        body1 += "<a href='/Subject/TopicDetail/" + dtTpDt.Rows[j]["url"].ToString() +"'>";
                        body1 += "<img src='"+ dtTpDt.Rows[j]["topic_image"].ToString() +"' alt='"+ dtTpDt.Rows[j]["topic_tittle"].ToString() + "'>";
                        body1 += "</a></div>";
                        body1 += "<div class='recent__post__details'>";
                        body1 += "<h2><a href='/Subject/TopicDetail/" + dtTpDt.Rows[j]["url"].ToString() + "'>" + dtTpDt.Rows[j]["topic_tittle"].ToString() + "</a></h2>";
                        body1 += "</div>";
                        body1 += "</div>";
                    }
                }

            }

            //DataTable dt2 = TopicDetailtview.topicimage(id);
            //string body1 = "";
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    body1 += "<div class='recent__post__thumb'> <a href = '/Subject/TopicDetail/" + dt2.Rows[i]["topic_id"].ToString() + "_" + dt2.Rows[i]["topic_tittle"].ToString().Replace(" ", "-") + "'>< img src='" + dt2.Rows[i]["topic_image"] + "' alt='recent post images'>";

            //    body1 += "</a ></ div><div class='recent__post__details'><h2><a href = '/Subject/TopicDetail/" + dt2.Rows[i]["topic_id"].ToString() + "_" + dt2.Rows[i]["topic_tittle"].ToString().Replace(" ", "-") + "' >" + dt2.Rows[i]["topic_tittle"];

            //    body1 += "</a></h2></div>";                              
            // }

            ViewBag.dt2 = body1;






            //string body2 = "";
            //DataTable dt3 = TopicDetailtview.topicname(id);
            //for (int j = 0; j < dt3.Rows.Count; j++)
            //{

            //    body2 += "<h2><a href='/Subject/TopicDetail/" + dt3.Rows[j]["topic_id"].ToString() +"_"+ dt3.Rows[j]["topic_tittle"].ToString().Replace(" ", "-") + "'> " + dt3.Rows[j]["topic_tittle"].ToString();
            //    body2 += "</a></h2>";
            //}

            //ViewBag.dt3 = body2;

            return View(TopicDetailtview.topicdisplay(id));
        }
    }
}