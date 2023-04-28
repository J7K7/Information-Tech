using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationTech.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace InformationTech.Controllers
{
    public class techadminController : Controller
    {
        Class1 c1 = new Class1();

        ITDBModel cs = new ITDBModel();

        // GET: techadmin
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(itvariables ilist, string submit)
        {
            if (submit == "login")
            {

                SqlCommand cmdselect = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());

                cmdselect.Connection = con;
                cmdselect.CommandText = "select top 1 * from tbl_admin where (admin_email=@contactEmail OR admin_contact=@contactEmail) and admin_password = @password";
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
                        id = dt.Rows[0]["admin_id"].ToString();


                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    //string message = ex.ToString();
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    Response.Write("<script>alert('" + ex.ToString() + "')</script>");
                }
                finally
                {
                    con.Close();
                }

                if (id != "")
                {
                    //Create a Cookie with a suitable Key.
                    HttpCookie nameCookie = new HttpCookie("ID");

                    //Set the Cookie value.
                    nameCookie.Values["ID"] = id;

                    //Set the Expiry date.
                    nameCookie.Expires = DateTime.Now.AddDays(30);

                    //Add the Cookie to Browser.
                    Response.Cookies.Add(nameCookie);




                    //Session["ID"] = id;           
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    Response.Write("<script>alert('Invalid User Name And Password !!')</script>");
                    //string message = "alert('Invalid User Name And Password !!')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }


            }
            return View();


        }



        public ActionResult forget()
        {
            ViewBag.email = "display:block";
            ViewBag.otp = "display:none";
            ViewBag.password = "display:none";



            return View();
        }

        public static int num = 0;
        public static string id = "";

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = "simbasingh1999@gmail.com";
                string senderPassword = "chotu786786";

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



        [HttpPost]

        public ActionResult forget(itvariables ilist, string submit)
        {
            Random rno = new Random();
            if (submit == "email")
            {
                SqlCommand cmdselect = new SqlCommand();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());

                cmdselect.Connection = con;
                cmdselect.CommandText = "select top 1 * from tbl_admin where (admin_email=@contactEmail OR admin_contact=@contactEmail)";
                cmdselect.Parameters.AddWithValue("@contactEmail", SqlDbType.VarChar).Value = ilist.email;





                try
                {
                    con.Close();
                    con.Open();

                    DataTable dt = new DataTable();
                    dt.Load(cmdselect.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        id = dt.Rows[0]["admin_id"].ToString();


                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    //string message = ex.ToString();
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    Response.Write("<script>alert('" + ex.ToString() + "')</script>");
                }
                finally
                {
                    con.Close();
                }

                if (id != "")
                {
                    //Create a Cookie with a suitable Key.


                    //Set the Cookie value.


                    //Set the Expiry date.


                    //Add the Cookie to Browser.

                    num = rno.Next(1111, 9999);
                    String toEmail = ilist.email;
                    bool result = SendEmail(toEmail, "OTP", "<html><body><div style=border-top:3px solid #22BCE5 > &nbsp;</ div ><br /><h1 style=color:#22BCE5> Security code </h1><br />Please use the following security code for the Sms Portal account " + ilist.email + "<h5> Your Otp is " + num + " </h5><br /> <br /><br /> Thanks,<br />The information tech team </body></html> ");

                    if (result)
                    {
                        Response.Write("Successfuly");
                    }
                    else
                    {
                        Response.Write("Error");
                    }

                    Response.Write(num);
                    ViewBag.email = "display:none";
                    ViewBag.otp = "display:block";
                    ViewBag.password = "display:none";




                    //Session["ID"] = id;           

                }
                else
                {
                    ViewBag.email = "display:block";
                    ViewBag.otp = "display:none";
                    ViewBag.password = "display:none";
                    Response.Write("<script>alert('Invalid email !!')</script>");
                    //string message = "alert('Invalid User Name And Password !!')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }





            }
            else if (submit == "otp")
            {
                Response.Write(num);
                if (num == Convert.ToInt32(ilist.otp))
                {
                    ViewBag.email = "display:none";
                    ViewBag.otp = "display:none";
                    ViewBag.password = "display:block";

                }
                else
                {
                    ViewBag.email = "display:block";
                    ViewBag.otp = "display:none";
                    ViewBag.password = "display:none";
                    Response.Write("<script>alert('Invalid otp !!')</script>");


                }




            }
            else if (submit == "password")
            {

                if (ilist.password == ilist.repassword)
                {


                    int i = 0;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
                    try
                    {


                        SqlCommand cmd = new SqlCommand();


                        string str = "update tbl_admin  set  admin_password='" + ilist.password + "' where admin_id = '" + id + "'";
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


                    Response.Write("<script>alert('change password !!')</script>");
                    return RedirectToAction("student_detail");

                }





            }
            else
            {

                Response.Write("<script>alert(' password must be same !!')</script>");
                ViewBag.email = "display:none";
                ViewBag.otp = "display:none";
                ViewBag.password = "display:block";


            }





            return View();


        }


        //*******************************Check Admin Cookie***********************************
        public void chkcookie()
        {
            Class1 cs = new Class1();
            HttpCookie id = Request.Cookies["ID"];

            if (id == null)
            {
                Response.Redirect("Index");
            }

            string aid = Request.Cookies["ID"].Value.ToString().Substring(3);
            DataTable dt = cs.Getdata("select *from tbl_admin where admin_id = '" + aid + "'");
            ViewBag.profile = dt.Rows[0]["admin_image"].ToString();
            ViewBag.name = dt.Rows[0]["admin_name"].ToString();
            ViewBag.email = dt.Rows[0]["admin_email"].ToString();
            string subject = "";
            string classes = "";
            string strbody = "";
            string stream = "";
            dt = c1.Getdata("select * from tbl_stream");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stream = dt.Rows[i]["stream"].ToString();
                strbody += "<li> <a href='#' class='has-arrow waves-effect waves-dark'>" + stream + "</a> <ul aria-expanded='false' class='collapse'> ";
                DataTable dt2 = c1.Getdata("select * from tbl_class where stream_id = '" + dt.Rows[i]["stream_id"] + "'");
                if (dt2.Rows.Count > 0)
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {

                        classes = dt2.Rows[j]["class_name"].ToString();


                        strbody += "<li> <a href='#' class='has-arrow waves-effect waves-dark'>" + classes + "</a> <ul aria-expanded='false' class='collapse'> ";
                        DataTable dt3 = c1.Getdata("select * from tbl_subject where class_id = '" + dt2.Rows[j]["class_id"] + "'");
                        if (dt3.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt3.Rows.Count; k++)
                            {
                                subject = dt3.Rows[k]["subject_name"].ToString();
                                strbody += "<li> <a href='/techadmin/SubjectView/" + dt2.Rows[0]["class_id"].ToString() + "' class='waves-effect waves-dark'>" + subject + "</a></li>";


                            }
                        }
                        else
                        {
                            strbody += "<li> <a href='/techadmin/SubjectAdd/" + dt2.Rows[0]["class_id"].ToString() + "' class='waves-effect waves-dark'> Add Subject </a></li>";
                        }
                        strbody += "</ul> </li>";
                    }

                }
                else
                {
                    strbody += "<li> <a href='/techadmin/ClassAdd' class='waves-effect waves-dark'>Add Class</a></li>  ";
                    //strbody += "</ul> </li>";
                }
                strbody += "</ul> </li>";
            }

            ViewBag.subject = strbody;

        }


        //******************************Admin DashBoard*************************************
        public ActionResult DashBoard()
        {
            chkcookie();

            DataTable dt = c1.Getdata("select * from tbl_stream");
            ViewBag.totalstreamview = dt.Rows.Count;

            dt = c1.Getdata("select * from tbl_class");
            ViewBag.totalclassview = dt.Rows.Count;

            dt = c1.Getdata("select * from tbl_subject");
            ViewBag.totalsubjectview = dt.Rows.Count;

            dt = c1.Getdata("select * from tbl_paper");
            ViewBag.totalpaperview = dt.Rows.Count;

            dt = c1.Getdata("select * from tbl_contact where dateandtime = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
            ViewBag.totalcontactview = dt.Rows.Count;

            dt = c1.Getdata("select sum(price) as total from tbl_membership");
            ViewBag.totalpriceview = dt.Rows[0]["total"].ToString();

            return View();
            
        }


        public ActionResult AdminLogout()
        {
            //chkcookie();


            //Create a Cookie with a suitable Key.
            HttpCookie nameCookie = new HttpCookie("ID");

            //Set the Expiry date.
            nameCookie.Expires = DateTime.Now.AddDays(-1);

            //Add the Cookie to Browser.
            Response.Cookies.Add(nameCookie);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AdminChangePassword()
        {
            chkcookie();

            return View();
        }

        [HttpPost]
        public ActionResult AdminChangePassword(string submit, admin_change_password ilist)
        {
            chkcookie();
            if (submit == "update")
            {
                ITDBModel adminupdate = new ITDBModel();
                Class1 cs = new Class1();
                string aid = Request.Cookies["ID"].Value.ToString().Substring(3);
                DataTable dt = cs.Getdata("select *from tbl_admin where admin_id = '" + aid + "' AND admin_password = '" + ilist.admin_old_password + "'");
                if (dt.Rows.Count > 0)
                {
                    ilist.admin_id = aid;
                    if (adminupdate.PasswordUpdate(ilist))
                    {
                        ViewBag.ab = "Password Update Successfully.";
                    }
                }
                else
                {
                    ViewBag.cd = "Old Password Is Wrong.";
                }
                //ViewBag.profile = dt.Rows[0]["admin_image"].ToString();

            }
            ModelState.Clear();
            return View();
        }

        //**************************** Admin ****************************************************

        [HttpGet]
        public ActionResult AdminAdd()
        {
            chkcookie();
            return View();
        }

        [HttpPost]
        public ActionResult AdminAdd(itvariables ilist, HttpPostedFileBase photo, string submit)
        {
            chkcookie();
            if (submit == "submit")
            {
                Random rno = new Random();
                int num1 = rno.Next(1, 10000);
                int row = 0;

                string logopath1 = "/content/image/" + num1 + photo.FileName;
                string path = Server.MapPath(logopath1);
                photo.SaveAs(path);


                ilist.photo = logopath1;

                ITDBModel adminadd = new ITDBModel();
                row = adminadd.insertrecrod("tbl_admin", "dateandtime,admin_name,admin_contact,admin_email,admin_image,admin_password", "'" + ilist.DateAndTime + "','" + ilist.name + "','" + ilist.contact + "','" + ilist.email + "','" + "/content/image/" + num1 + photo.FileName + "','" + ilist.password + "'");
                if (row > 0)
                {
                    ViewBag.ab = "Save Successfully.";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult AdminView(itvariables ilist)
        {
            chkcookie();
            ITDBModel addview = new ITDBModel();

            return View(addview.display());
        }
        [HttpGet]
        public ActionResult AdminUpdate(int id)
        {
            chkcookie();
            ITDBModel AdminUpdate = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(AdminUpdate.display().Find(adminupdate => adminupdate.id == id));


        }

        [HttpPost]
        public ActionResult AdminUpdate(int id, itvariables iList, HttpPostedFileBase photo, string submit)
        {
            chkcookie();
            if (submit == "submit")
            {
                ITDBModel adminupdate = new ITDBModel();

                if (photo == null)
                {



                    if (adminupdate.AddUpdate(iList))
                    {
                        ViewBag.success = "display:block;";
                        ViewBag.successText = "Save Record.";
                        ViewBag.error = "display:none;";


                        ModelState.Clear();

                        return RedirectToAction("AdminView");
                    }
                    else
                    {
                        ViewBag.success = "display:none;";
                        ViewBag.error = "display:block;";
                        ViewBag.errorText = "Something Missing.";
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


                    string logopath1 = "/content/image/" + num1 + photo.FileName;
                    string path = Server.MapPath(logopath1);
                    photo.SaveAs(path);


                    iList.photo = logopath1;



                    if (adminupdate.AddUpdate(iList))
                    {
                        ViewBag.success = "display:block;";
                        ViewBag.successText = "Save Record.";
                        ViewBag.error = "display:none;";


                        ModelState.Clear();

                        return RedirectToAction("AdminView");
                    }
                    else
                    {
                        ViewBag.success = "display:none;";
                        ViewBag.error = "display:block;";
                        ViewBag.errorText = "Something Missing.";
                    }

                }

            }

            return View();
        }


        [HttpGet]
        public ActionResult AdminDelete(int id)
        {
            chkcookie();
            ITDBModel admindelete = new ITDBModel();
            DataTable dt = c1.Getdata("select * from tbl_admin where admin_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                string paths = Server.MapPath(@"" + dt.Rows[0]["admin_image"].ToString());
                FileInfo files = new FileInfo(paths);
                if (files.Exists)
                {
                    files.Delete();
                }
            }

            // string photoPath = "~" + iList.photo;




            admindelete.DeleteAdmin(id);
            return RedirectToAction("AdminView");

        }
        //**************************** Class ****************************************************

        [HttpGet]
        public ActionResult ClassAdd()
        {
            chkcookie();
            try
            {

                DataTable dt = c1.Getdata("select * from tbl_stream");
                ViewBag.dt = dt;


            }
            catch (Exception ex)
            { }
            finally
            {

            }


            return View();
        }
        [HttpPost]
        public ActionResult ClassAdd(Class_detail ilist, string submit, string taskOption)
        {
            chkcookie();
            ModelState.Clear();

            DataTable dt = c1.Getdata("select * from tbl_stream");
            ViewBag.dt = dt;


            if (submit == "class")
            {
                if (taskOption == "none")
                {
                    Response.Write("<script> alert('Please Select Stream.')</script>");
                }
                else
                {

                    DataTable dt1 = c1.Getdata("select * from tbl_class where class_name = '" + ilist.class_name + "' AND stream_id = '" + taskOption + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        ModelState.Clear();
                        Response.Write("<script> alert('This Name Already Exits.')</script>");
                    }
                    else
                    {
                        int row = 0;
                        ITDBModel addclass = new ITDBModel();
                        row = addclass.insertrecrod("tbl_class", "dateandtime,class_name,stream_id", "'" + ilist.DateAndTime + "','" + ilist.class_name + "','" + taskOption + "'");
                        if (row > 0)
                        {
                            Response.Write("<script> alert('Class Added')</script>");

                        }
                    }


                }



            }

            return View();
        }

        public ActionResult ClassView()
        {
            chkcookie();
            ModelState.Clear();

            DataTable dt = c1.Getdata("select * from tbl_stream order by stream");
            ViewBag.dt = dt;


            ITDBModel classview = new ITDBModel();

            return View(classview.viewclass());



        }

        [HttpGet]
        public ActionResult ClassUpdate(int id)
        {
            chkcookie();
            ITDBModel ClassUpdate = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(ClassUpdate.viewclass().Find(classupdate => classupdate.class_id == id));




        }
        [HttpPost]
        public ActionResult ClassUpdate(int id, Class_detail iList, string submit)
        {
            chkcookie();
            ITDBModel ClassUpdate = new ITDBModel();

            if (submit == "class")
            {
                DataTable dt = c1.Getdata("select * from tbl_class where class_id = '" + id + "'");
                string stream_id = dt.Rows[0]["stream_id"].ToString();
                DataTable dt1 = c1.Getdata("select * from tbl_class where stream_id = '" + stream_id + "' AND class_name='" + iList.class_name + "'");
                if (dt1.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Name Already Exits.')</script>");
                }
                else
                {
                    if (ClassUpdate.UpdateClass(iList))
                    {
                        ModelState.Clear();
                        return RedirectToAction("ClassView");
                    }
                }

            }



            return View();
        }




        [HttpGet]
        public ActionResult ClassDelete(int id)
        {
            chkcookie();
            ITDBModel ClassDelete = new ITDBModel();
            DataTable dt4 = c1.Getdata("select *from tbl_subject where class_id= '" + id + "'");
            foreach (DataRow dr3 in dt4.Rows)
            {
                DataTable questiondt = c1.Getdata("select *from tbl_question where subject_id= '" + Convert.ToInt32(dr3["subject_id"]) + "'");
                foreach (DataRow questiondr in questiondt.Rows)
                {
                    DataTable objdt = c1.Getdata("select  *from tbl_objective where question_id = '" + Convert.ToInt32(questiondr["question_id"]) + "'");
                    foreach (DataRow objdr in objdt.Rows)
                    {
                        ClassDelete.DeleteObjective(Convert.ToInt32(objdr["objective_id"]));
                    }
                    DataTable ansdt = c1.Getdata("select  *from tbl_ans where question_id = '" + Convert.ToInt32(questiondr["question_id"]) + "'");
                    foreach (DataRow ansdr in ansdt.Rows)
                    {
                        ClassDelete.DeleteAnswer(Convert.ToInt32(ansdr["ans_id"]));
                    }
                    ClassDelete.DeleteQuestion(Convert.ToInt32(questiondr["question_id"]));
                }
                DataTable dt3 = c1.Getdata("select *from tbl_chapter where subject_id= '" + Convert.ToInt32(dr3["subject_id"]) + "'");
                foreach (DataRow dr2 in dt3.Rows)
                {
                    DataTable dt1 = c1.Getdata("select *from tbl_topic where chapter_id= '" + Convert.ToInt32(dr2["chapter_id"]) + "'");
                    foreach (DataRow dr in dt1.Rows)
                    {

                        DataTable dt2 = c1.Getdata("select *from tbl_pdf where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                        foreach (DataRow dr1 in dt2.Rows)
                        {
                            string paths = Server.MapPath(@"" + dr1["pdf"].ToString());
                            FileInfo files = new FileInfo(paths);
                            if (files.Exists)
                            {
                                files.Delete();
                            }
                            ClassDelete.DeletePdf(Convert.ToInt32(dr1["pdf_id"]));
                        }
                        dt2 = c1.Getdata("select *from topic_tittle_detail where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                        foreach (DataRow dr1 in dt2.Rows)
                        {
                            ClassDelete.DeleteDetailTopic(Convert.ToInt32(dr1["topic_detail_id"]));
                        }
                        dt2 = c1.Getdata("select *from tbl_video where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                        foreach (DataRow dr1 in dt2.Rows)
                        {
                            ClassDelete.DeleteTopicVideo(Convert.ToInt32(dr1["video_id"]));
                        }
                        dt2 = c1.Getdata("select *from topic_source_code where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                        foreach (DataRow dr1 in dt2.Rows)
                        {
                            ClassDelete.DeletetopicSourcecode(Convert.ToInt32(dr1["topic_source_code_id"]));
                        }

                        ClassDelete.DeleteTopic(Convert.ToInt32(dr["topic_id"]));

                    }
                    ClassDelete.DeleteChapter(Convert.ToInt32(dr2["chapter_id"]));
                }
                DataTable paperdt = c1.Getdata("select *from tbl_paper where subject_id= '" + Convert.ToInt32(dr3["subject_id"]) + "'");
                foreach (DataRow paperdr in paperdt.Rows)
                {
                    string paths2 = Server.MapPath(@"" + paperdr["paper_pdf"].ToString());
                    FileInfo files2 = new FileInfo(paths2);
                    if (files2.Exists)
                    {
                        files2.Delete();
                    }
                    ClassDelete.DeletePaper(Convert.ToInt32(paperdr["paper_id"]));
                }
                string paths3 = Server.MapPath(@"" + Convert.ToString(dr3["subject_image"]));
                FileInfo files3 = new FileInfo(paths3);
                if (files3.Exists)
                {
                    files3.Delete();
                }

                ClassDelete.DeleteSubject(Convert.ToInt32(dr3["subject_id"]));
            }
            ClassDelete.DeleteClass(id);


            return RedirectToAction("ClassView");

        }



        //************************************** Subject*********************************************************
        [HttpGet]
        public ActionResult SubjectAdd(int id)
        {

            chkcookie();
            ViewBag.id = id;
            return View();

        }


        public ActionResult SubjectAdd(int id, subject ilist, string submit, HttpPostedFileBase subject_image)
        {
            ViewBag.id = id;
            chkcookie();
            if (submit == "subject")
            {
                Random rno = new Random();
                int num1 = rno.Next(1, 10000);
                int row = 0;

                string logopath1 = "/Content/subject_image/" + num1 + subject_image.FileName;
                string path = Server.MapPath(logopath1);
                subject_image.SaveAs(path);


                ilist.subject_image = logopath1;


                DataTable dt1 = c1.Getdata("select * from tbl_subject where subject_name = '" + ilist.subject_name + "' AND class_id = '" + id + "'");
                if (dt1.Rows.Count > 0)
                {
                    ModelState.Clear();
                    Response.Write("<script> alert('This Name Already Exits.')</script>");

                }
                else
                {

                    ITDBModel addsubject = new ITDBModel();
                    row = addsubject.insertrecrod("tbl_subject", "dateandtime,subject_name,class_id,subject_image", "'" + ilist.dateandtime + "','" + ilist.subject_name + "','" + id + "','" + "/Content/subject_image/" + num1 + subject_image.FileName + "'");
                    if (row > 0)
                    {
                        //Response.Write("<script> alert('subject added')</script>");
                        Response.Write("<script> alert('Subject Is Added.')</script>");
                    }
                }
            }


            return View();
        }


        [HttpGet]
        public ActionResult SubjectUpdate(int id)
        {

            chkcookie();
            DataTable dt = c1.Getdata("select  class_id from tbl_subject where subject_id = '" + id + "'");
            ViewBag.id = dt.Rows[0]["class_id"].ToString();
            ITDBModel SubjectUpdate = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(SubjectUpdate.viewUpdatesubject().Find(subjectupdate => subjectupdate.subject_id == id));


        }




        [HttpPost]
        public ActionResult SubjectUpdate(int id, subject iList, string submit, HttpPostedFileBase subject_image)
        {

            chkcookie();
            string id2 = "";
            DataTable dt = c1.Getdata("select  class_id from tbl_subject where subject_id = '" + id + "'");
            id2 = dt.Rows[0]["class_id"].ToString();
            ViewBag.id = id2;
            ITDBModel Amodel = new ITDBModel();

            if (submit == "subject")
            {


                if (subject_image == null)
                {
                    Amodel.UpdateSubject(iList);
                }
                else
                {

                    string photoPath = "~" + iList.subject_image;

                    string paths = Server.MapPath(@"" + photoPath);
                    FileInfo files = new FileInfo(paths);
                    if (files.Exists)
                    {
                        files.Delete();
                    }


                    Random rno = new Random();
                    int num1 = rno.Next(1, 10000);


                    string logopath1 = "/Content/subject_image/" + num1 + subject_image.FileName;
                    string path = Server.MapPath(logopath1);
                    subject_image.SaveAs(path);

                    iList.subject_image = logopath1;
                    Amodel.UpdateSubject(iList);


                }

                return Redirect("/techadmin/SubjectView/" + id2);

            }




            return View();
        }




        public ActionResult SubjectView(int id)
        {
            chkcookie();
            ViewBag.id = id;




            //DataTable dt = cs.Getdata("select * from tbl_subject where class_id = '"+ id +"'");
            //for(int )



            ITDBModel subjectview = new ITDBModel();
            ModelState.Clear();
            return View(subjectview.viewsubject(id));



        }

        [HttpGet]
        public ActionResult SubjectDelete(int id)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  * from tbl_subject where subject_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["class_id"].ToString();

            }
            ITDBModel SubjectDelete = new ITDBModel();

            DataTable questiondt = c1.Getdata("select *from tbl_question where subject_id= '" + id + "'");
            foreach (DataRow questiondr in questiondt.Rows)
            {
                DataTable objdt = c1.Getdata("select  *from tbl_objective where question_id = '" + Convert.ToInt32(questiondr["question_id"]) + "'");
                foreach (DataRow objdr in objdt.Rows)
                {
                    SubjectDelete.DeleteObjective(Convert.ToInt32(objdr["objective_id"]));
                }
                DataTable ansdt = c1.Getdata("select  *from tbl_ans where question_id = '" + Convert.ToInt32(questiondr["question_id"]) + "'");
                foreach (DataRow ansdr in ansdt.Rows)
                {
                    SubjectDelete.DeleteAnswer(Convert.ToInt32(ansdr["ans_id"]));
                }
                SubjectDelete.DeleteQuestion(Convert.ToInt32(questiondr["question_id"]));
            }
            DataTable dt3 = c1.Getdata("select *from tbl_chapter where subject_id= '" + id + "'");
            foreach (DataRow dr2 in dt3.Rows)
            {
                DataTable dt1 = c1.Getdata("select *from tbl_topic where chapter_id= '" + Convert.ToInt32(dr2["chapter_id"]) + "'");

                foreach (DataRow dr in dt1.Rows)
                {
                    string paths1 = Server.MapPath(@"" + dr["topic_image"].ToString());
                    FileInfo files1 = new FileInfo(paths1);
                    if (files1.Exists)
                    {
                        files1.Delete();
                    }
                    DataTable dt2 = c1.Getdata("select *from tbl_pdf where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                    foreach (DataRow dr1 in dt2.Rows)
                    {
                        string paths = Server.MapPath(@"" + dr1["pdf"].ToString());
                        FileInfo files = new FileInfo(paths);
                        if (files.Exists)
                        {
                            files.Delete();
                        }
                        SubjectDelete.DeletePdf(Convert.ToInt32(dr["pdf_id"]));
                    }
                    dt2 = c1.Getdata("select *from topic_tittle_detail where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                    foreach (DataRow dr1 in dt2.Rows)
                    {
                        SubjectDelete.DeleteDetailTopic(Convert.ToInt32(dr1["topic_detail_id"]));
                    }
                    dt2 = c1.Getdata("select *from tbl_video where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                    foreach (DataRow dr1 in dt2.Rows)
                    {
                        SubjectDelete.DeleteTopicVideo(Convert.ToInt32(dr1["video_id"]));
                    }
                    dt2 = c1.Getdata("select *from topic_source_code where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                    foreach (DataRow dr1 in dt2.Rows)
                    {
                        SubjectDelete.DeletetopicSourcecode(Convert.ToInt32(dr1["topic_source_code_id"]));
                    }
                    SubjectDelete.DeleteTopic(Convert.ToInt32(dr["topic_id"]));

                }
                SubjectDelete.DeleteChapter(Convert.ToInt32(dr2["chapter_id"]));
            }
            DataTable paperdt = c1.Getdata("select *from tbl_paper where subject_id= '" + id + "'");
            foreach (DataRow paperdr in paperdt.Rows)
            {
                string paths2 = Server.MapPath(@"" + paperdr["paper_pdf"].ToString());
                FileInfo files2 = new FileInfo(paths2);
                if (files2.Exists)
                {
                    files2.Delete();
                }
                SubjectDelete.DeletePaper(Convert.ToInt32(paperdr["paper_id"]));
            }

            string paths3 = Server.MapPath(@"" + dt.Rows[0]["subject_image"]);
            FileInfo files3 = new FileInfo(paths3);
            if (files3.Exists)
            {
                files3.Delete();
            }

            SubjectDelete.DeleteSubject(id);

            return Redirect("/techadmin/SubjectView/" + id2);

        }

        //*********************************** Stream ******************************************************************
        [HttpGet]
        public ActionResult StreamAdd()
        {

            chkcookie();
            return View();
        }


        [HttpPost]
        public ActionResult StreamAdd(stream_detail ilist, string submit)
        {
            chkcookie();
            if (submit == "stream")
            {

                int row = 0;
                DataTable dt = c1.Getdata("select * from tbl_stream where stream = '" + ilist.stream_name + "'");
                if (dt.Rows.Count > 0)
                {
                    ModelState.Clear();
                    Response.Write("<script> alert('This Name Already Exits.')</script>");
                }
                else
                {
                    ITDBModel addstream = new ITDBModel();
                    row = addstream.insertrecrod("tbl_stream", "dateandtime,stream", "'" + ilist.DateAndTime + "','" + ilist.stream_name + "'");
                    if (row > 0)
                    {
                        ModelState.Clear();
                        Response.Write("<script> alert('Stream Added')</script>");

                    }
                }


            }

            return View();
        }

        public ActionResult StreamView()
        {
            chkcookie();
            ITDBModel streamview = new ITDBModel();

            return View(streamview.viewstream());



        }

        [HttpGet]
        public ActionResult StreamUpdate(int id)
        {
            chkcookie();
            ITDBModel StreamUpdate = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(StreamUpdate.viewstream().Find(streamupdate => streamupdate.stream_id == id));


        }


        [HttpPost]
        public ActionResult StreamUpdate(int id, stream_detail iList, HttpPostedFileBase photo, string submit)
        {
            chkcookie();
            if (submit == "stream")
            {
                DataTable dt = c1.Getdata("select * from tbl_stream where stream = '" + iList.stream_name + "'");
                if (dt.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Name Already Exits.')</script>");
                }
                else
                {
                    ITDBModel StreamUpdate = new ITDBModel();

                    if (StreamUpdate.UpdateStream(iList))
                    {

                        ModelState.Clear();
                        return RedirectToAction("StreamView");
                    }
                }
            }

            return View();

        }


        [HttpGet]
        public ActionResult StreamDelete(int id)
        {
            chkcookie();
            ITDBModel StreamDelete = new ITDBModel();

            StreamDelete.DeleteStream(id);
            DataTable dt = c1.Getdata("select *from tbl_class where stream_id= '" + id + "'");
            foreach (DataRow dr4 in dt.Rows)
            {
                DataTable dt4 = c1.Getdata("select *from tbl_subject where class_id= '" + Convert.ToInt32(dr4["class_id"]) + "'");
                foreach (DataRow dr3 in dt4.Rows)
                {
                    DataTable questiondt = c1.Getdata("select *from tbl_question where subject_id= '" + Convert.ToInt32(dr3["subject_id"]) + "'");
                    foreach (DataRow questiondr in questiondt.Rows)
                    {
                        DataTable objdt = c1.Getdata("select  *from tbl_objective where question_id = '" + Convert.ToInt32(questiondr["question_id"]) + "'");
                        foreach (DataRow objdr in objdt.Rows)
                        {
                            StreamDelete.DeleteObjective(Convert.ToInt32(objdr["objective_id"]));
                        }
                        DataTable ansdt = c1.Getdata("select  *from tbl_ans where question_id = '" + Convert.ToInt32(questiondr["question_id"]) + "'");
                        foreach (DataRow ansdr in ansdt.Rows)
                        {
                            StreamDelete.DeleteAnswer(Convert.ToInt32(ansdr["ans_id"]));
                        }
                        StreamDelete.DeleteQuestion(Convert.ToInt32(questiondr["question_id"]));
                    }
                    DataTable dt3 = c1.Getdata("select *from tbl_chapter where subject_id= '" + Convert.ToInt32(dr3["subject_id"]) + "'");
                    foreach (DataRow dr2 in dt3.Rows)
                    {

                        DataTable dt1 = c1.Getdata("select *from tbl_topic where chapter_id= '" + Convert.ToInt32(dr2["chapter_id"]) + "'");
                        foreach (DataRow dr in dt1.Rows)
                        {
                            string paths1 = Server.MapPath(@"" + dr["topic_image"].ToString());
                            FileInfo files1 = new FileInfo(paths1);
                            if (files1.Exists)
                            {
                                files1.Delete();
                            }

                            DataTable dt2 = c1.Getdata("select *from tbl_pdf where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                            foreach (DataRow dr1 in dt2.Rows)
                            {
                                string paths = Server.MapPath(@"" + dr1["pdf"].ToString());
                                FileInfo files = new FileInfo(paths);
                                if (files.Exists)
                                {
                                    files.Delete();
                                }
                                StreamDelete.DeletePdf(Convert.ToInt32(dr1["pdf_id"]));
                            }
                            dt2 = c1.Getdata("select *from topic_tittle_detail where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                            foreach (DataRow dr1 in dt2.Rows)
                            {
                                StreamDelete.DeleteDetailTopic(Convert.ToInt32(dr1["topic_detail_id"]));
                            }
                            dt2 = c1.Getdata("select *from tbl_video where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                            foreach (DataRow dr1 in dt2.Rows)
                            {
                                StreamDelete.DeleteTopicVideo(Convert.ToInt32(dr1["video_id"]));
                            }
                            dt2 = c1.Getdata("select *from topic_source_code where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                            foreach (DataRow dr1 in dt2.Rows)
                            {
                                StreamDelete.DeletetopicSourcecode(Convert.ToInt32(dr1["topic_source_code_id"]));
                            }

                            StreamDelete.DeleteTopic(Convert.ToInt32(dr["topic_id"]));

                        }
                        StreamDelete.DeleteChapter(Convert.ToInt32(dr2["chapter_id"]));
                    }
                    DataTable paperdt = c1.Getdata("select *from tbl_paper where subject_id= '" + Convert.ToInt32(dr3["subject_id"]) + "'");
                    foreach (DataRow paperdr in paperdt.Rows)
                    {
                        string paths2 = Server.MapPath(@"" + paperdr["paper_pdf"].ToString());
                        FileInfo files2 = new FileInfo(paths2);
                        if (files2.Exists)
                        {
                            files2.Delete();
                        }
                        StreamDelete.DeletePaper(Convert.ToInt32(paperdr["paper_id"]));
                    }
                    string paths3 = Server.MapPath(@"" + Convert.ToString(dr3["subject_image"]));
                    FileInfo files3 = new FileInfo(paths3);
                    if (files3.Exists)
                    {
                        files3.Delete();
                    }

                    StreamDelete.DeleteSubject(Convert.ToInt32(dr3["subject_id"]));
                }
                StreamDelete.DeleteClass(Convert.ToInt32(dr4["class_id"]));
            }
            return RedirectToAction("StreamView");

        }

        //********************************* Chapter******************************************************
        [HttpGet]
        public ActionResult ChapterAdd(int id)
        {
            chkcookie();
            ViewBag.id = id;

            return View();

        }

        [HttpPost]

        public ActionResult ChapterAdd(Chapter ilist, string submit, int id)
        {
            chkcookie();
            //DataTable dt = c1.Getdata("select * from tbl_subject");
            //ViewBag.dt = dt;
            ViewBag.id = id;
            if (submit == "chapter")
            {
                DataTable dt = c1.Getdata("select * from tbl_chapter where chapter_name = '" + ilist.chapter_name + "' AND subject_id='" + id + "'");
                if (dt.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Name Already Exits.')</script>");
                }
                else
                {
                    int row = 0;
                    ITDBModel addchapter = new ITDBModel();
                    row = addchapter.insertrecrod("tbl_chapter", "dateandtime,chapter_name,subject_id", "'" + ilist.DateAndTime + "','" + ilist.chapter_name + "','" + id + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('Chapter added')</script>");

                    }
                    ModelState.Clear();

                }


            }



            return View();

        }

        [HttpGet]
        public ActionResult ChapterView(int id)
        {
            chkcookie();
            ViewBag.id = id;
            DataTable dt = c1.Getdata("select * from tbl_subject where subject_id = '" + id + "'");
            ViewBag.id2 = dt.Rows[0]["class_id"].ToString();

            ITDBModel ChapterView = new ITDBModel();

            return View(ChapterView.viewchapter(id));



        }

        [HttpGet]
        public ActionResult ChapterUpdate(int id)
        {
            chkcookie();
            ITDBModel SubjectUpdate = new ITDBModel();
            ModelState.Clear();
            string id2 = "";

            DataTable dt = c1.Getdata("select  subject_id from tbl_chapter where chapter_id = '" + id + "'");
            id2 = dt.Rows[0]["subject_id"].ToString();
            ViewBag.id2 = id2;
            //return View(studenView.GetStudentDetailsList());
            return View(SubjectUpdate.ViewUpdateChapter().Find(chapterupdate => chapterupdate.chapter_id == id));



        }
        [HttpPost]
        public ActionResult ChapterUpdate(int id, Chapter ilist, string submit)
        {
            chkcookie();
            ViewBag.id = id;
            string id2 = "";

            DataTable dt = c1.Getdata("select  subject_id from tbl_chapter where chapter_id = '" + id + "'");
            id2 = dt.Rows[0]["subject_id"].ToString();
            ViewBag.id2 = id2;
            if (submit == "chapter")
            {
                DataTable dt1 = c1.Getdata("select *from tbl_chapter where subject_id = '" + id2 + "' AND chapter_name = '" + ilist.chapter_name + "'");
                if (dt1.Rows.Count > 0)
                {
                    Response.Write("<script> alert('This Name Already Exits.')</script>");
                }
                else
                {
                    ITDBModel chapterupdate = new ITDBModel();
                    chapterupdate.UpdateChapter(ilist);

                    return Redirect("/techadmin/ChapterView/" + id2);
                }
            }
            return View();
        }

        public ActionResult ChapterDelete(int id)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  subject_id from tbl_chapter where chapter_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["subject_id"].ToString();

            }

            ITDBModel chapterdelete = new ITDBModel();
            DataTable dt1 = c1.Getdata("select *from tbl_topic where chapter_id= '" + id + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                string paths1 = Server.MapPath(@"" + dr["topic_image"].ToString());
                FileInfo files1 = new FileInfo(paths1);
                if (files1.Exists)
                {
                    files1.Delete();
                }

                DataTable dt2 = c1.Getdata("select *from tbl_pdf where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                foreach (DataRow dr1 in dt2.Rows)
                {
                    string paths = Server.MapPath(@"" + dr1["pdf"].ToString());
                    FileInfo files = new FileInfo(paths);
                    if (files.Exists)
                    {
                        files.Delete();
                    }
                    chapterdelete.DeletePdf(Convert.ToInt32(dr1["pdf_id"]));
                }
                dt2 = c1.Getdata("select *from topic_tittle_detail where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                foreach (DataRow dr1 in dt2.Rows)
                {
                    chapterdelete.DeleteDetailTopic(Convert.ToInt32(dr1["topic_detail_id"]));
                }
                dt2 = c1.Getdata("select *from tbl_video where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                foreach (DataRow dr1 in dt2.Rows)
                {
                    chapterdelete.DeleteTopicVideo(Convert.ToInt32(dr1["video_id"]));
                }
                dt2 = c1.Getdata("select *from topic_source_code where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                foreach (DataRow dr1 in dt2.Rows)
                {
                    chapterdelete.DeletetopicSourcecode(Convert.ToInt32(dr1["topic_source_code_id"]));
                }
                DataTable paperdt = c1.Getdata("select *from tbl_paper where topic_id= '" + Convert.ToInt32(dr["topic_id"]) + "'");
                foreach (DataRow paperdr in paperdt.Rows)
                {
                    string paths2 = Server.MapPath(@"" + paperdr["paper_pdf"].ToString());
                    FileInfo files2 = new FileInfo(paths2);
                    if (files2.Exists)
                    {
                        files2.Delete();
                    }
                    chapterdelete.DeletePaper(Convert.ToInt32(paperdr["paper_id"]));
                }
                chapterdelete.DeleteTopic(Convert.ToInt32(dr["topic_id"]));

            }
            chapterdelete.DeleteChapter(id);

            return Redirect("/techadmin/ChapterView/" + id2);


        }

        //******************************** TOPIC **************************************************************
        [HttpGet]
        public ActionResult TopicAdd(int id)
        {
            chkcookie();
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult TopicAdd(Topic ilist, int id, string submit, HttpPostedFileBase topic_image)
        {
            chkcookie();
            ViewBag.id = id;
            if (submit == "topic")
            {
                Random rno = new Random();
                int num1 = rno.Next(1, 10000);
                int row = 0;

                string logopath1 = "/Content/topic_images/" + num1 + topic_image.FileName;
                string path = Server.MapPath(logopath1);
                topic_image.SaveAs(path);


                ilist.topic_image = logopath1;





                DataTable dt = c1.Getdata("select * from tbl_topic where chapter_id = '" + id + "' AND topic_tittle ='" + ilist.topic_tittle + "'");
                if (dt.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Name Already Exits.')</script>");
                }
                else
                {

                    ITDBModel addtopic = new ITDBModel();
                    row = addtopic.insertrecrod("tbl_topic", "dateandtime,topic_tittle,chapter_id,topic_image", "'" + ilist.dateandtime + "','" + ilist.topic_tittle + "','" + id + "','" + "/Content/topic_images/" + num1 + topic_image.FileName + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('Topic Added')</script>");
                    }


                }

                ModelState.Clear();
            }

            return View();

        }

        public ActionResult TopicView(int id)
        {
            chkcookie();
            ViewBag.id = id;
            DataTable dt = c1.Getdata("select * from tbl_chapter where chapter_id = '" + id + "'");
            ViewBag.id2 = dt.Rows[0]["subject_id"].ToString();


            ITDBModel topicview = new ITDBModel();

            return View(topicview.ViewTopic(id));


        }

        [HttpGet]
        public ActionResult TopicUpdate(int id)
        {
            chkcookie();
            ITDBModel TopicUpdate = new ITDBModel();
            string id2 = "";

            DataTable dt = c1.Getdata("select  chapter_id from tbl_topic where topic_id = '" + id + "'");
            id2 = dt.Rows[0]["chapter_id"].ToString();
            ViewBag.id2 = id2;
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(TopicUpdate.ViewUpdateTopic().Find(topicupdate => topicupdate.topic_id == id));

        }

        [HttpPost]
        public ActionResult TopicUpdate(Topic ilist, int id, string submit, HttpPostedFileBase topic_image)
        {


            chkcookie();
            string id2 = "";
            ITDBModel TopicUpdate = new ITDBModel();
            DataTable dt = c1.Getdata("select  chapter_id from tbl_topic where topic_id = '" + id + "'");
            id2 = dt.Rows[0]["chapter_id"].ToString();
            ViewBag.id2 = id2;

            if (submit == "topic")
            {
                if (topic_image == null)
                {
                    TopicUpdate.UpdateTopic(ilist);
                }
                else
                {

                    string photoPath = "~" + ilist.topic_image;

                    string paths = Server.MapPath(@"" + photoPath);
                    FileInfo files = new FileInfo(paths);
                    if (files.Exists)
                    {
                        files.Delete();
                    }


                    Random rno = new Random();
                    int num1 = rno.Next(1, 10000);


                    string logopath1 = "/Content/topic_images/" + num1 + topic_image.FileName;
                    string path = Server.MapPath(logopath1);
                    topic_image.SaveAs(path);

                    ilist.topic_image = logopath1;
                    TopicUpdate.UpdateTopic(ilist);

                }

                return Redirect("/techadmin/TopicView/" + id2);
            }




            return View();

        }

        public ActionResult TopicDelete(int id)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  *   from tbl_topic where topic_id = '" + id + "'");
            id2 = dt.Rows[0]["chapter_id"].ToString();

            string paths1 = Server.MapPath(@"" + dt.Rows[0]["topic_image"].ToString());
            FileInfo files1 = new FileInfo(paths1);
            if (files1.Exists)
            {
                files1.Delete();
            }

            ITDBModel TopicDelete = new ITDBModel();
            DataTable dt1 = c1.Getdata("select *from tbl_pdf where topic_id= '" + id + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                string paths = Server.MapPath(@"" + dr["pdf"].ToString());
                FileInfo files = new FileInfo(paths);
                if (files.Exists)
                {
                    files.Delete();
                }
                TopicDelete.DeletePdf(Convert.ToInt32(dr["pdf_id"]));
            }
            dt1 = c1.Getdata("select *from topic_tittle_detail where topic_id= '" + id + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                TopicDelete.DeleteDetailTopic(Convert.ToInt32(dr["topic_detail_id"]));
            }
            dt1 = c1.Getdata("select *from tbl_video where topic_id= '" + id + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                TopicDelete.DeleteTopicVideo(Convert.ToInt32(dr["video_id"]));
            }
            dt1 = c1.Getdata("select *from topic_source_code where topic_id= '" + id + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                TopicDelete.DeletetopicSourcecode(Convert.ToInt32(dr["topic_source_code_id"]));
            }
            DataTable paperdt = c1.Getdata("select *from tbl_paper where topic_id= '" + id + "'");
            foreach (DataRow paperdr in paperdt.Rows)
            {
                string paths2 = Server.MapPath(@"" + paperdr["paper_pdf"].ToString());
                FileInfo files2 = new FileInfo(paths2);
                if (files2.Exists)
                {
                    files2.Delete();
                }
                TopicDelete.DeletePaper(Convert.ToInt32(paperdr["paper_id"]));
            }
            TopicDelete.DeleteTopic(id);



            return Redirect("/techadmin/TopicView/" + id2);


        }

        //********************************* Topic Details ************************************************

        [HttpGet]
        public ActionResult TopicDetailAdd(int id)
        {

            chkcookie();
            ViewBag.id = id;
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TopicDetailAdd(TopicDetail ilist, string submit, int id)
        {
            chkcookie();
            ModelState.Clear();
            ViewBag.id = id;

            if (submit == "topic_detail")
            {
                DataTable dt = c1.Getdata("select * from topic_tittle_detail where topic_id = '" + id + "' AND topic_detail_tittle ='" + ilist.topic_detail_tittle + "'");
                if (dt.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Topic Detail Already Exits.')</script>");
                }
                else
                {
                    int row = 0;
                    ITDBModel addtopicdt = new ITDBModel();
                    row = addtopicdt.insertrecrod("topic_tittle_detail", "dateandtime,topic_detail_tittle,topic_id", "'" + ilist.dateandtime + "','" + ilist.topic_detail_tittle + "','" + id + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('topic Detail added')</script>");
                        ModelState.Clear();

                    }
                }




            }


            return View();

        }

        public ActionResult TopicDetailView(int id)
        {
            chkcookie();
            ViewBag.id = id;
            DataTable dt = c1.Getdata("select * from tbl_topic where topic_id = '" + id + "'");
            ViewBag.id2 = dt.Rows[0]["chapter_id"].ToString();

            ITDBModel topicdtview = new ITDBModel();

            return View(topicdtview.ViewTopicDetail(id));

        }

        [HttpGet]
        public ActionResult TopicDetailUpdate(int id)
        {
            chkcookie();
            ITDBModel Topic_Detail_Update = new ITDBModel();

            string id2 = "";

            DataTable dt = c1.Getdata("select  topic_id from topic_tittle_detail where topic_detail_id = '" + id + "'");
            id2 = dt.Rows[0]["topic_id"].ToString();
            ViewBag.id2 = id2;
            ModelState.Clear();

            //return View(studenView.GetStudentDetailsList());
            return View(Topic_Detail_Update.ViewUpdateTopicDetail().Find(topic_detail_update => topic_detail_update.topic_detail_id == id));


        }

        [HttpPost]

        [ValidateInput(false)]
        public ActionResult TopicDetailUpdate(TopicDetail ilist, int id, string submit)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  topic_id   from topic_tittle_detail where topic_detail_id = '" + id + "'");
            id2 = dt.Rows[0]["topic_id"].ToString();
            ViewBag.id2 = id2;
            if (submit == "topic_detail_update")
            {
                DataTable dt1 = c1.Getdata("select *from topic_tittle_detail where topic_id = '" + id2 + "' AND topic_detail_tittle = '" + ilist.topic_detail_tittle + "'");
                if (dt1.Rows.Count > 0)
                {
                    Response.Write("<script> alert('This Topic Detail Already Exits.')</script>");
                }

                else
                {
                    ITDBModel TopicDetailUpdate = new ITDBModel();
                    TopicDetailUpdate.UpdateTopicDetail(ilist);

                    return Redirect("/techadmin/TopicDetailView/" + id2);

                }

            }
            return View();


        }

        [HttpGet]

        public ActionResult TopicDetailDelete(int id)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  topic_id   from topic_tittle_detail where topic_detail_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["topic_id"].ToString();

            }



            ITDBModel TopicDetailDelete = new ITDBModel();
            DataTable dt1 = c1.Getdata("select *from tbl_pdf where topic_id= '" + id2 + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                string paths = Server.MapPath(@"" + dr["pdf"].ToString());
                FileInfo files = new FileInfo(paths);
                if (files.Exists)
                {
                    files.Delete();
                }
                TopicDetailDelete.DeletePdf(Convert.ToInt32(dr["pdf_id"]));
            }
            dt1 = c1.Getdata("select *from tbl_video where topic_id= '" + id2 + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                TopicDetailDelete.DeleteTopicVideo(Convert.ToInt32(dr["video_id"]));
            }
            dt1 = c1.Getdata("select *from topic_source_code where topic_id= '" + id2 + "'");
            foreach (DataRow dr in dt1.Rows)
            {
                TopicDetailDelete.DeletetopicSourcecode(Convert.ToInt32(dr["topic_source_code_id"]));
            }
            TopicDetailDelete.DeleteDetailTopic(id);



            return Redirect("/techadmin/TopicDetailView/" + id2);



        }


        //***************************** Topic PDF *************************************************************************//
        [HttpGet]
        public ActionResult TopicPdf(int id)
        {

            chkcookie();
            ViewBag.id = id;
            return View();

        }
        public ActionResult TopicPdf(topicpdf ilist, HttpPostedFileBase pdf, int id, string submit)
        {
            chkcookie();
            ViewBag.id = id;
            if (submit == "topic_pdf")
            {

                Random rno = new Random();
                int num1 = rno.Next(1, 10000);
                int row = 0;

                string logopath1 = "/content/Pdf/" + num1 + pdf.FileName;
                string path = Server.MapPath(logopath1);
                pdf.SaveAs(path);


                ilist.pdf = pdf.FileName;

                ITDBModel pdfadd = new ITDBModel();
                row = pdfadd.insertrecrod("tbl_pdf", "dateandtime,pdf,topic_id", "'" + ilist.dateandtime + "','" + "/Content/Pdf/" + num1 + pdf.FileName + "', '" + id + "'");
                if (row > 0)
                {
                    Response.Write("<script>alert('pdf added') </script>");
                }





            }



            return View();
        }

        [HttpGet]
        public ActionResult TopicPdfView(int id)
        {
            chkcookie();
            ViewBag.id = id;

            ITDBModel topicpdfview = new ITDBModel();

            return View(topicpdfview.ViewTopicPdf(id));



        }
        [HttpGet]
        public ActionResult TopicPdfDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel pdfdelete = new ITDBModel();
            DataTable dt = c1.Getdata("select * from tbl_pdf where pdf_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["topic_id"].ToString();
                string paths = Server.MapPath(@"" + dt.Rows[0]["pdf"].ToString());
                FileInfo files = new FileInfo(paths);
                if (files.Exists)
                {
                    files.Delete();
                }
            }

            // string photoPath = "~" + iList.photo;




            pdfdelete.DeletePdf(id);
            return Redirect("/techadmin/TopicPdfView/" + id2);

        }

        //********************************* Topic Video**********************************************************************

        [HttpGet]
        public ActionResult TopicVideo(int id)
        {
            chkcookie();

            ViewBag.id = id;

            return View();

        }
        [HttpPost]
        public ActionResult TopicVideo(topicvideo ilist, string submit, int id)
        {
            chkcookie();
            ViewBag.id = id;

            if (submit == "topic_video")
            {
                DataTable dt = c1.Getdata("select * from tbl_video where topic_id = '" + id + "' AND video ='" + ilist.video + "'");
                if (dt.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Video Already Exits.')</script>");
                }
                else
                {
                    int row = 0;
                    ITDBModel addtopicvideo = new ITDBModel();
                    row = addtopicvideo.insertrecrod("tbl_video", "dateandtime,video,topic_id", "'" + ilist.dateandtime + "','" + ilist.video + "','" + id + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('topic video added')</script>");


                    }

                }


            }

            ModelState.Clear();
            return View();

        }

        public ActionResult TopicVideoView(int id)
        {
            chkcookie();
            ViewBag.id = id;


            ITDBModel topicvideoview = new ITDBModel();

            return View(topicvideoview.ViewTopicVideo(id));


        }


        public ActionResult TopicVideoDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel videodelete = new ITDBModel();
            DataTable dt = c1.Getdata("select * from tbl_video where video_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["topic_id"].ToString();

            }






            videodelete.DeleteTopicVideo(id);
            return Redirect("/techadmin/TopicVideoView/" + id2);

        }

        //************************************* Topic Source Code ***********************************************//
        [HttpGet]
        public ActionResult TopicSourceCodeAdd(int id)
        {
            chkcookie();

            ViewBag.id = id;

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult TopicSourceCodeAdd(topicsourcecode ilist, string submit, int id)
        {
            chkcookie();

            ViewBag.id = id;

            if (submit == "topic_source_code")
            {
                DataTable dt = c1.Getdata("select * from topic_source_code where topic_id = '" + id + "' AND topic_source_code ='" + ilist.topic_source_code + "'");
                if (dt.Rows.Count > 0)
                {

                    Response.Write("<script> alert('This Source Code Already Exits.')</script>");
                }
                else
                {
                    int row = 0;
                    ITDBModel addtopicvideo = new ITDBModel();
                    row = addtopicvideo.insertrecrod("topic_source_code", "dateandtime,topic_source_code,topic_id", "'" + ilist.DateAndTime + "','" + ilist.topic_source_code + "','" + id + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('topic source code added')</script>");


                    }

                }



            }
            ModelState.Clear();
            return View();

        }

        public ActionResult TopicSourceCodeView(int id)
        {
            chkcookie();
            ViewBag.id = id;


            ITDBModel topicsourcecodeview = new ITDBModel();

            return View(topicsourcecodeview.ViewTopicSourceCode(id));




        }

        [HttpGet]
        public ActionResult TopicSourceCodeUpdate(int id)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  topic_id   from topic_source_code where topic_source_code_id = '" + id + "'");
            id2 = dt.Rows[0]["topic_id"].ToString();
            ViewBag.id2 = id2;
            ITDBModel Topic_source_code_Update = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(Topic_source_code_Update.ViewUpdateTopicSourceCode().Find(topic_source_update => topic_source_update.topic_source_code_id == id));



        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TopicSourceCodeUpdate(topicsourcecode ilist, int id, string submit)
        {
            chkcookie();
            string id2 = "";

            DataTable dt = c1.Getdata("select  topic_id   from topic_source_code where topic_source_code_id = '" + id + "'");
            id2 = dt.Rows[0]["topic_id"].ToString();
            ViewBag.id2 = id2;
            if (submit == "topic_source_code")
            {
                ITDBModel Topic_source_code_Update = new ITDBModel();
                Topic_source_code_Update.UpdateTopicSourceCode(ilist);

                return Redirect("/techadmin/TopicSourceCodeView/" + id2);
            }


            return View();



        }


        public ActionResult TopicSourceCodeDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel codedelete = new ITDBModel();
            DataTable dt = c1.Getdata("select * from topic_source_code where topic_source_code_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["topic_id"].ToString();

            }






            codedelete.DeletetopicSourcecode(id);
            return Redirect("/techadmin/TopicSourceCodeView/" + id2);



        }

        //**************************************** Subject Questions ********************************************************************

        [HttpGet]
        public ActionResult QuestionAdd(int id)
        {
            chkcookie();
            ViewBag.id = id;


            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuestionAdd(Question ilist, int id, string submit)
        {
            chkcookie();
            ViewBag.id = id;
            if (submit == "question")
            {
                DataTable dt1 = c1.Getdata("select *from tbl_question where subject_id = '" + id + "' AND question = '" + ilist.question + "'");
                if (dt1.Rows.Count > 0)
                {
                    Response.Write("<script> alert('This Question Already Exits.')</script>");
                }

                else
                {
                    int row = 0;
                    ITDBModel addtopicvideo = new ITDBModel();
                    row = addtopicvideo.insertrecrod("tbl_question", "dateandtime,question,subject_id", "'" + ilist.DateAndTime + "','" + ilist.question + "','" + id + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('Question added')</script>");


                    }
                }
            }
            ModelState.Clear();


            return View();

        }


        [HttpGet]
        public ActionResult SubjectQuestionView()
        {
            chkcookie();
            List<SelectListItem> listitem = new List<SelectListItem>();
            stream_detail dropdn = new stream_detail();

            dropdn.stream_id = 0;
            dropdn.stream_name = "Select Stream";
            listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });

            DataTable dd = c1.Getdata("select * from tbl_stream order by stream");
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                dropdn.stream_id = Convert.ToInt32(dd.Rows[i]["stream_id"].ToString());
                dropdn.stream_name = dd.Rows[i]["stream"].ToString();
                listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });
            }
            ViewBag.DropDownValueStream = new SelectList(listitem, "Text", "Value");

            ViewBag.DropDownValueClass = new SelectList("", "Text", "Value");

            ViewBag.DropDownValueSubject = new SelectList("", "Text", "Value");


            return View();

        }

        [HttpPost]
        public ActionResult SubjectQuestionView(Question ilist)
        {
            chkcookie();
            List<SelectListItem> listitem = new List<SelectListItem>();
            stream_detail dropdn = new stream_detail();

            dropdn.stream_id = 0;
            dropdn.stream_name = "Select Stream";
            listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });

            DataTable dd = c1.Getdata("select * from tbl_stream order by stream");
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                dropdn.stream_id = Convert.ToInt32(dd.Rows[i]["stream_id"].ToString());
                dropdn.stream_name = dd.Rows[i]["stream"].ToString();
                listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });
            }
            ViewBag.DropDownValueStream = new SelectList(listitem, "Text", "Value");

            ViewBag.DropDownValueClass = new SelectList("", "Text", "Value");

            ViewBag.DropDownValueSubject = new SelectList("", "Text", "Value");


            int id2 = ilist.subject_id;


            return Redirect("/techadmin/QuestionView/" + id2);
        }

        public ActionResult QuestionView(int id)
        {
            chkcookie();
            ViewBag.id = id;


            ITDBModel questionview = new ITDBModel();

            return View(questionview.QuestionView(id));



        }

        [HttpGet]
        public ActionResult QuestionUpdate(int id)
        {
            chkcookie();
            string id2 = "";
            DataTable dt = c1.Getdata("select  subject_id   from tbl_question where question_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["subject_id"].ToString();

            }

            ViewBag.id = id2;
            ITDBModel questionupdate = new ITDBModel();

            //return View(studenView.GetStudentDetailsList());
            return View(questionupdate.ViewUpdateQuestion().Find(question_update => question_update.question_id == id));


        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult QuestionUpdate(Question ilist, int id, string submit)
        {
            chkcookie();
            string id2 = "";
            DataTable dt = c1.Getdata("select  subject_id   from tbl_question where question_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["subject_id"].ToString();

            }
            ViewBag.id = id2;

            if (submit == "questionupdate")
            {
                DataTable dt1 = c1.Getdata("select *from tbl_question where subject_id = '" + id2 + "' AND question = '" + ilist.question + "'");
                if (dt1.Rows.Count > 0)
                {
                    Response.Write("<script> alert('This Question Already Exits.')</script>");
                }

                else
                {
                    ITDBModel questionupdate = new ITDBModel();
                    questionupdate.UpdateQuestion(ilist);

                    return Redirect("/techadmin/QuestionView/" + id2);
                }
            }
            return View();


        }


        public ActionResult QuestionDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel questiondelete = new ITDBModel();
            DataTable dt = c1.Getdata("select  subject_id   from tbl_question where question_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["subject_id"].ToString();

            }
            DataTable objdt = c1.Getdata("select  *from tbl_objective where question_id = '" + id + "'");
            foreach (DataRow objdr in objdt.Rows)
            {
                questiondelete.DeleteObjective(Convert.ToInt32(objdr["objective_id"]));
            }
            DataTable ansdt = c1.Getdata("select  *from tbl_ans where question_id = '" + id + "'");
            foreach (DataRow ansdr in ansdt.Rows)
            {
                questiondelete.DeleteAnswer(Convert.ToInt32(ansdr["ans_id"]));
            }
            questiondelete.DeleteQuestion(id);
            return Redirect("/techadmin/QuestionView/" + id2);



        }

        //********************************** Objective **********************************************

        [HttpGet]
        public ActionResult ObjectiveAdd(int id)
        {
            chkcookie();
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ObjectiveAdd(Objective ilist, int id, string submit)
        {
            chkcookie();
            ViewBag.id = id;
            if (submit == "Objective")
            {
                DataTable dt1 = c1.Getdata("select *from tbl_objective where question_id = '" + id + "' AND objectives = '" + ilist.Objectives + "'");
                if (dt1.Rows.Count > 0)
                {
                    Response.Write("<script> alert('This Objective Already Exits.')</script>");
                }

                else
                {
                    int row = 0;
                    ITDBModel objectiveadd = new ITDBModel();
                    row = objectiveadd.insertrecrod("tbl_objective", "dateandtime,objectives,question_id", "'" + ilist.DateAndTime + "','" + ilist.Objectives + "','" + id + "'");
                    if (row > 0)
                    {
                        Response.Write("<script> alert('objective Added')</script>");


                    }
                }
            }
            ModelState.Clear();
            return View();
        }

        public ActionResult ObjectiveView(int id)
        {
            chkcookie();
            ViewBag.id = id;
            DataTable dt = c1.Getdata("select * from tbl_question where question_id = '" + id + "'");
            ViewBag.id2 = dt.Rows[0]["subject_id"].ToString();

            ITDBModel objectiveview = new ITDBModel();

            return View(objectiveview.ObjectiveView(id));



        }
        public ActionResult ObjectiveDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel objectivedelete = new ITDBModel();
            DataTable dt = c1.Getdata("select  question_id   from tbl_objective where objective_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["question_id"].ToString();

            }

            DataTable ansdelete = c1.Getdata("select *from tbl_ans where objective_id ='" + id + "'");
            if (ansdelete.Rows.Count > 0)
            {
                objectivedelete.DeleteAnswer(Convert.ToInt32(ansdelete.Rows[0]["ans_id"]));
            }
            objectivedelete.DeleteObjective(id);
            return Redirect("/techadmin/ObjectiveView/" + id2);

        }

        [HttpGet]
        public ActionResult ObjectiveUpdate(int id)
        {
            chkcookie();
            string id2 = "";
            DataTable dt = c1.Getdata("select  question_id   from tbl_objective where objective_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["question_id"].ToString();

            }
            ViewBag.id = id2;
            ITDBModel objectiveupdate = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(objectiveupdate.ViewUpdateObjective().Find(objective_update => objective_update.objective_id == id));


        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ObjectiveUpdate(Objective ilist, int id, string submit)
        {
            chkcookie();
            string id2 = "";
            DataTable dt = c1.Getdata("select  question_id   from tbl_objective where objective_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["question_id"].ToString();

            }
            ViewBag.id = id2;
            if (submit == "Objective_Update")
            {

                DataTable dt1 = c1.Getdata("select *from tbl_objective where question_id = '" + id2 + "' AND objectives = '" + ilist.Objectives + "'");
                if (dt1.Rows.Count > 0)
                {
                    Response.Write("<script> alert('This Objective Already Exits.')</script>");
                }

                else
                {
                    ITDBModel objectiveupdate = new ITDBModel();
                    objectiveupdate.UpdateObjective(ilist);

                    return Redirect("/techadmin/ObjectiveView/" + id2);
                }



            }

            return View();
        }






        //*********************************** ANSWER ****************************************************************

        [HttpGet]
        public ActionResult AnsAdd(int id)
        {
            chkcookie();
            DataTable dtjk = c1.Getdata("select * from tbl_question where question_id = '" + id + "'");
            ViewBag.id = dtjk.Rows[0]["subject_id"];
            string str_body = "";

            str_body += "<select name='taskOption' class='form-control'>";
            str_body += "<option value='0' selected>Select Objective</option> ";
            DataTable dtO = c1.Getdata("select  *  from tbl_objective where question_id = '" + id + "'");
            if (dtO.Rows.Count > 0)
            {
                for (int i = 0; i < dtO.Rows.Count; i++)
                {
                    str_body += "<option value='" + dtO.Rows[i]["objective_id"].ToString() + "'>" + dtO.Rows[i]["objectives"].ToString() + "</option> ";
                }
            }

            str_body += "</select>";

            ViewBag.DDlObjective = str_body;


            ViewBag.dt = c1.Getdata("select  *  from tbl_objective where question_id = '" + id + "'");
            DataTable dt1 = c1.Getdata("select * from tbl_ans where question_id = '" + id + "'");
            if (dt1.Rows.Count > 0)
            {


                return Redirect("/techadmin/AnsView/" + id);


            }


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AnsAdd(Answer ilist, int id, int taskoption, string submit)
        {
            chkcookie();
            DataTable dtjk = c1.Getdata("select * from tbl_question where question_id = '" + id + "'");
            ViewBag.id = dtjk.Rows[0]["subject_id"];
            ViewBag.dt = c1.Getdata("select  *  from tbl_objective where question_id = '" + id + "'");



            if (submit == "answer")
            {

                int row = 0;
                ITDBModel objectiveadd = new ITDBModel();
                row = objectiveadd.insertrecrod("tbl_ans", "dateandtime,objective_id,answer_detail,question_id", "'" + ilist.DateAndTime + "','" + taskoption + "', '" + ilist.answer_detail + "','" + id + "'");
                if (row > 0)
                {

                    ModelState.Clear();
                    return Redirect("/techadmin/AnsView/" + id);

                }


            }


            string str_body = "";

            str_body += "<select name='taskOption' class='form-control'>";
            str_body += "<option value='0' selected>Select Objective</option> ";
            DataTable dtO = c1.Getdata("select  *  from tbl_objective where question_id = '" + id + "'");
            if (dtO.Rows.Count > 0)
            {
                for (int i = 0; i < dtO.Rows.Count; i++)
                {
                    str_body += "<option value='" + dtO.Rows[i]["objective_id"].ToString() + "'>" + dtO.Rows[i]["objectives"].ToString() + "</option> ";
                }
            }

            str_body += "</select>";

            ViewBag.DDlObjective = str_body;


            return View();
        }

        public ActionResult AnsView(int id)
        {
            chkcookie();
            DataTable dtjk = c1.Getdata("select * from tbl_question where question_id = '" + id + "'");
            ViewBag.id = dtjk.Rows[0]["subject_id"];
            DataTable dt1 = c1.Getdata("select * from tbl_ans where question_id = '" + id + "'");
            if (dt1.Rows.Count > 0)
            {
                ITDBModel Ansview = new ITDBModel();

                return View(Ansview.AnswerView(id));

            }
            else
            {
                return Redirect("/techadmin/AnsAdd/" + id);


            }


        }

        [HttpGet]
        public ActionResult AnsUpdate(int id)
        {
            chkcookie();
            DataTable dt1 = c1.Getdata("select *from tbl_ans where ans_id = '" + id + "'");
            ViewBag.id = dt1.Rows[0]["question_id"];
            DataTable dtdrop = c1.Getdata("select * from tbl_objective where question_id=" + dt1.Rows[0]["question_id"]);

            string str_body = "";

            str_body += "<select name='taskOption' class='form-control' >";

            if (dtdrop.Rows.Count > 0)
            {
                for (int i = 0; i < dtdrop.Rows.Count; i++)
                {
                    if (dtdrop.Rows[i]["objective_id"].ToString() == dt1.Rows[0]["objective_id"].ToString())
                    {
                        str_body += "<option value='" + dtdrop.Rows[i]["objective_id"].ToString() + "' selected='selected'>" + dtdrop.Rows[i]["objectives"].ToString() + "</option> ";

                    }
                    else
                    {
                        str_body += "<option value='" + dtdrop.Rows[i]["objective_id"].ToString() + "'>" + dtdrop.Rows[i]["objectives"].ToString() + "</option> ";
                    }


                }
            }

            str_body += "</select>";

            ViewBag.DDlObjective = str_body;


            ITDBModel answerupdate = new ITDBModel();
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(answerupdate.ViewUpdateAnswer().Find(answer_update => answer_update.ans_id == id));


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AnsUpdate(Answer ilist, int id, string submit, string taskoption)
        {
            chkcookie();
            string id2 = "";
            ilist.objective_id = Convert.ToInt32(taskoption);
            DataTable dt = c1.Getdata("select  *   from tbl_ans where ans_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["question_id"].ToString();

            }
            ViewBag.id = id2;
            if (submit == "answer_update")
            {


                ITDBModel answerupdate = new ITDBModel();
                answerupdate.UpdateAnswer(ilist);

                return Redirect("/techadmin/AnsView/" + id2);

            }

            return View();

        }

        public ActionResult AnswerDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel objectivedelete = new ITDBModel();
            DataTable dt = c1.Getdata("select  question_id   from tbl_ans where ans_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["question_id"].ToString();

            }


            objectivedelete.DeleteAnswer(id);
            return Redirect("/techadmin/AnsView/" + id2);




        }

        //********************************* Paper Sample ****************************************************

        [HttpGet]
        public ActionResult SubjectPaperView()
        {
            chkcookie();
            List<SelectListItem> listitem = new List<SelectListItem>();
            stream_detail dropdn = new stream_detail();

            dropdn.stream_id = 0;
            dropdn.stream_name = "Select Stream";
            listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });

            DataTable dd = c1.Getdata("select * from tbl_stream order by stream");
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                dropdn.stream_id = Convert.ToInt32(dd.Rows[i]["stream_id"].ToString());
                dropdn.stream_name = dd.Rows[i]["stream"].ToString();
                listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });
            }
            ViewBag.DropDownValueStream = new SelectList(listitem, "Text", "Value");

            ViewBag.DropDownValueClass = new SelectList("", "Text", "Value");

            ViewBag.DropDownValueSubject = new SelectList("", "Text", "Value");


            return View();

        }
        [HttpPost]

        public ActionResult SubjectPaperView(paper ilist)
        {
            chkcookie();
            List<SelectListItem> listitem = new List<SelectListItem>();
            stream_detail dropdn = new stream_detail();

            dropdn.stream_id = 0;
            dropdn.stream_name = "Select Stream";
            listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });

            DataTable dd = c1.Getdata("select * from tbl_stream order by stream");
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                dropdn.stream_id = Convert.ToInt32(dd.Rows[i]["stream_id"].ToString());
                dropdn.stream_name = dd.Rows[i]["stream"].ToString();
                listitem.Add(new SelectListItem() { Value = dropdn.stream_name, Text = dropdn.stream_id.ToString() });
            }
            ViewBag.DropDownValueStream = new SelectList(listitem, "Text", "Value");

            ViewBag.DropDownValueClass = new SelectList("", "Text", "Value");

            ViewBag.DropDownValueSubject = new SelectList("", "Text", "Value");


            int id2 = ilist.subject_id;


            return Redirect("/techadmin/PaperView/" + id2);
        }

        [HttpGet]
        public ActionResult PaperAdd(int id)
        {

            chkcookie();

            string body = "";
            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                body += "<option value=" + i + ">" + i + "</option>";
            }
            ViewBag.option = body;
            return View();
        }

        public ActionResult PaperAdd(paper ilist, int id, string submit, string year, string month, HttpPostedFileBase pdf)
        {
            chkcookie();
            string body = "";
            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                body += "<option value=" + i + ">" + i + "</option>";
            }
            ViewBag.option = body;
            if (submit == "paper")
            {
                Random rno = new Random();
                int num1 = rno.Next(1, 10000);
                int row = 0;

                string logopath1 = "/content/exam_paper/" + num1 + pdf.FileName;
                string path = Server.MapPath(logopath1);
                pdf.SaveAs(path);


                ilist.pdf = pdf.FileName;


                ITDBModel paperadd = new ITDBModel();
                row = paperadd.insertrecrod("tbl_paper", "year,month,paper_pdf,subject_id", "'" + year + "','" + month + "', '" + "/Content/exam_paper/" + num1 + pdf.FileName + "','" + id + "'");
                if (row > 0)
                {
                    Response.Write("<script> alert('paper Added')</script>");
                    ModelState.Clear();
                    //return Redirect("/techadmin/AnsView/" + id);

                }


            }


            return View();
        }

        public ActionResult PaperView(int id)
        {
            chkcookie();
            ViewBag.id = id;


            ITDBModel paperview = new ITDBModel();

            return View(paperview.PaperView(id));


        }

        public ActionResult PaperDelete(int id)
        {
            chkcookie();
            string id2 = "";
            ITDBModel paperdelete = new ITDBModel();
            DataTable dt = c1.Getdata("select  subject_id   from tbl_paper where paper_id = '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                id2 = dt.Rows[0]["subject_id"].ToString();

                string paths2 = Server.MapPath(@"" + dt.Rows[0]["paper_pdf"].ToString());
                FileInfo files2 = new FileInfo(paths2);
                if (files2.Exists)
                {
                    files2.Delete();
                }

            }


            paperdelete.DeletePaper(id);
            return Redirect("/techadmin/PaperView/" + id2);




        }

        [HttpGet]
        public ActionResult Price()
        {
            chkcookie();
            ITDBModel PriceUpdate = new ITDBModel();


            int id = 1;
            ModelState.Clear();
            //return View(studenView.GetStudentDetailsList());
            return View(PriceUpdate.ViewPrice().Find(priceupdate => priceupdate.price_id == id));


        }

        [HttpPost]
        public ActionResult price(price ilist, string submit)
        {
            chkcookie();

            ITDBModel PriceUpdate = new ITDBModel();


            if (submit == "price")
            {

                PriceUpdate.UpdatePrice(ilist);

                Response.Write("<script> alert('Membership Price Updated Sucessfully.!!')</script>");
            }




            return View();
        }

        [HttpGet]
        public ActionResult Member()
        {
            chkcookie();
            ITDBModel member = new ITDBModel();

            return View(member.memberdisplay());
        }

        public ActionResult Contact()
        {
            chkcookie();
            ITDBModel member = new ITDBModel();

            return View(member.contactdisplay());
        }








        
        public ActionResult ExamResult()
        {
            chkcookie();
            ITDBModel member = new ITDBModel();

            return View(member.GetExamResult());
        }




        public ActionResult ExamResultView(string id)
        {
 
            string userid = HttpUtility.UrlEncode(Encrypt(id));           
            return Redirect("/Exam/result?examid=" + userid);
             
        }






        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }




    }
}