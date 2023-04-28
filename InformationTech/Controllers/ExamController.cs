using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using InformationTech.Models;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace InformationTech.Controllers
{
    public class ExamController : Controller
    {
        Class1 cs = new Class1();
        // GET: Exam
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
                    Response.Redirect("../Home/Payment");
                }
            }
            else
            {
                ViewBag.name = null;
                ViewBag.cookieAllow1 = "display:none";
                ViewBag.cookieAllow = "display:none";
                ViewBag.cookieNotAllow = "display:block";
                Response.Redirect("../User/Index");
            }
            courses();
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




        [HttpGet]
        public ActionResult Index()
        {
            chkcookie();


            ClientDb examsubject = new ClientDb();
            //int cnt = 0;

            //string body = "";
            //DataTable dt = cs.Getdata("select *, REPLACE( REPLACE(question, '<p>', ''), '</p>', '') as ques from tbl_question where subject_id = '15' order by NEWID()");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    body += "<div class='single__list__view clearfix'>";
            //    body += "<div class='row'>";
            //    body += "<div class='mb-12' style='margin-left:60px;'>";
            //    body += "<h6 style='font-weight:500; font-size:1.125rem; color:#202428; line-height:1.3;display:block'><b>";
            //    body += ++cnt + "). ";
            //    body += "&nbsp; " + dt.Rows[i]["ques"].ToString();
            //    body += "</b></h6>";
            //    body += "<ul style='margin-top:10px; '>";
            //    DataTable dt1 = cs.Getdata("select * from tbl_objective where question_id = '" + dt.Rows[i]["question_id"].ToString() + "' order by NEWID()");
            //    for (int j = 0; j < dt1.Rows.Count; j++)
            //    {
            //        body += "<li style='list-style-type:upper-latin; margin-left:24px; color: black; font-size: initial;'>";
            //        body += "<label class='containerrdo'>";
            //        body += dt1.Rows[j]["objectives"].ToString();
            //        body += "<input type='radio' name='radio" + dt1.Rows[j]["question_id"].ToString() + "' value='" + dt1.Rows[j]["question_id"].ToString() + "," + dt1.Rows[j]["objective_id"].ToString() + "'>";
            //        body += " <span class='checkmarkrdo'></span>";
            //        body += "</label></li>";
            //    }
            //    body += "</ul></div></div></div>";
            //}

            return View(examsubject.Examsubjectdisplay());
        }


        [HttpPost]
        public ActionResult Index(tblExam iList, string submit )
        {
            chkcookie();


                DataTable inst = cs.Getdata("Insert into tblExam(dateandtime, subject_id, user_id) values('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"+ submit +"', '" + Request.Cookies["UID"].Value.ToString().Substring(4) + "') SELECT SCOPE_IDENTITY() as id; ");
                if (inst.Rows.Count > 0)
                {
                    string ids = inst.Rows[0]["id"].ToString();
                    string userid = HttpUtility.UrlEncode(Encrypt(ids));
                    //string userid = CryptoEngine.Encrypt(ids, "sblw-3hn8-sqoy19");
                    //string userid = HttpUtility.UrlEncode(Encrypt(ids, "sblw-3hn8-sqoy19"));
                    return Redirect("/Exam/onlineExam?examid=" + userid);
                }

            
            
           

                 return View();
        }




        [HttpGet]
        public ActionResult onlineExam()
        {
            chkcookie();

            string examid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["examid"]));

            //string value = HttpUtility.UrlEncode(Decrypt(id, "sblw-3hn8-sqoy19")); 
            string a = Request.QueryString["examid"];
            ViewBag.value = examid;
            ViewBag.a = a;

            string question_id = "";

            DataTable dtE = cs.Getdata("select E.*, UR.first_name + ' ' + UR.last_name as Name, S.subject_name, DATEADD(MINUTE, 21, e.dateandtime) as newtime from tblExam E LEFT JOIN user_registration UR ON UR.user_id = E.user_id LEFT JOIN tbl_subject S ON S.subject_id = E.subject_id Where E.Examid = " + examid + "");
            if (dtE.Rows.Count > 0)
            {
                ViewBag.todate = dtE.Rows[0]["newtime"].ToString();
                ViewBag.subtitl = dtE.Rows[0]["subject_name"].ToString();
                ViewBag.name1 = dtE.Rows[0]["Name"].ToString();

                int cnt = 0;

                string body = "";
                DataTable dt = cs.Getdata("select Top 10 *, REPLACE( REPLACE(question, '<p>', ''), '</p>', '') as ques from tbl_question where subject_id = '" + dtE.Rows[0]["subject_id"].ToString() + "' order by NEWID()");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    body += "<div class='single__list__view clearfix'>";
                    body += "<div class='row'>";
                    body += "<div class='mb-12' style='margin-left:60px;'>";
                    body += "<h6 style='font-weight:500; font-size:1.125rem; color:#202428; line-height:1.3;display:block'><b>";
                    body += ++cnt + "). ";
                    body += "&nbsp; " + dt.Rows[i]["ques"].ToString();
                    body += "</b></h6>";
                    body += "<ul style='margin-top:10px; '>";
                    DataTable dt1 = cs.Getdata("select * from tbl_objective where question_id = '" + dt.Rows[i]["question_id"].ToString() + "' order by NEWID()");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        body += "<li style='list-style-type:upper-latin; margin-left:24px; color: black; font-size: initial;'>";
                        body += "<label class='containerrdo'>";
                        body += dt1.Rows[j]["objectives"].ToString();
                        body += "<input type='radio' name='radio" + dt1.Rows[j]["question_id"].ToString() + "' value='" + dt1.Rows[j]["question_id"].ToString() + "," + dt1.Rows[j]["objective_id"].ToString() + "' onclick='getValueUsingOffer(this.value)'>";
                        body += " <span class='checkmarkrdo'></span>";
                        body += "</label></li>";
                    }
                    body += "</ul></div></div></div>";


                    question_id += dt.Rows[i]["question_id"].ToString() + "|";
                }
                ViewBag.dt = body;
                ViewBag.que_id = string.Concat(question_id.Reverse().Skip(1).Reverse());
            }


            return View();
        }

        [HttpPost]
        public ActionResult onlineExam(tblexam_desc iList)
        {
            chkcookie();
            try
            {

                int QExamId = iList.QExamId;
                string select_obj_id = iList.select_obj_id;
                string qesid = iList.qesid;


                string[] strArr = qesid.Split('|');
                string[] strobjArr = select_obj_id.Split('|');
                int qId = 0;
                int strQID;
                int strobjID;
                int objId = 0;

                for (int i = 0; i < strArr.Length; i++)
                {
                    qId = Convert.ToInt32(strArr[i]);

                    for (int j = 0; j < strobjArr.Length; j++)
                    {
                        string[] strobjArr2 = strobjArr[j].Split(',');

                        if (strobjArr2.Length > 1)
                        {
                            strQID = Convert.ToInt32(strobjArr2[0]);
                            strobjID = Convert.ToInt32(strobjArr2[1]);


                            if (qId == strQID)
                            {
                                objId = strobjID;
                                break;
                            }
                            else
                            {
                                objId = 0;
                            }
                        }
                    }
                    int insert = cs.insertRecord("tblexam_desc", "question_id,objective_id,Examid", " " + qId + ", " + objId + ", " + QExamId + " ");
                    //DoSomeThingWith(i);
                }


                int total_question = 0;
                int attempted_question = 0;
                int not_attempted_question = 0;
                int correct_answer = 0;
                int wrong_answer = 0;


                DataTable dt = cs.Getdata("select * from tblexam_desc where Examid = " + QExamId + "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total_question = total_question + 1;

                    DataTable dts = cs.Getdata("select * from tbl_ans where question_id = " + dt.Rows[i]["question_id"].ToString() + "");
                    if (dts.Rows.Count > 0)
                    {
                        if (dt.Rows[i]["objective_id"].ToString() == "0")
                        {
                            not_attempted_question = not_attempted_question + 1;
                        }
                        else
                        {
                            attempted_question = attempted_question + 1;

                            if (dt.Rows[i]["objective_id"].ToString() == dts.Rows[0]["objective_id"].ToString())
                            {
                                correct_answer = correct_answer + 1;
                            }
                            else
                            {
                                wrong_answer = wrong_answer + 1;
                            }
                        }
                    }
                }

                ViewBag.total_question = total_question;
                ViewBag.attempted_question = attempted_question;
                ViewBag.correct_answer = correct_answer;
                ViewBag.wrong_answer = wrong_answer;
                ViewBag.not_attempted_question = not_attempted_question;

                int insertresult = cs.insertRecord("tblExam_Result", "total_question, attempted_question, not_attempted_question, correct_answer, wrong_answer,Examid", " " + total_question + ", " + attempted_question + ", " + not_attempted_question + "," + correct_answer + "," + wrong_answer + ", " + QExamId + " ");



                return Redirect("/Exam/result?examid=" + HttpUtility.UrlEncode(Encrypt(Convert.ToString(QExamId))));
            }
            catch (Exception ex)
            {
                return Redirect("/Exam/");
            }


            
        }



        public ActionResult result()
        {
            chkcookie();
            int cnt = 0;

            string examid = Decrypt(HttpUtility.UrlDecode(Request.QueryString["examid"]));

            DataTable dtE = cs.Getdata("select E.*, UR.first_name + ' ' + UR.last_name as Name, S.subject_name from tblExam E LEFT JOIN user_registration UR ON UR.user_id = E.user_id LEFT JOIN tbl_subject S ON S.subject_id = E.subject_id Where E.Examid = " + examid + "");
            if (dtE.Rows.Count > 0)
            {             
                ViewBag.subtitle = dtE.Rows[0]["subject_name"].ToString();
                ViewBag.name1 = dtE.Rows[0]["Name"].ToString();
            }


                string body = "";

            DataTable exam_desc = cs.Getdata("select * from tblexam_desc where Examid = "+ examid + "");
            for (int a = 0; a < exam_desc.Rows.Count; a++)
            {
                string objid = exam_desc.Rows[a]["objective_id"].ToString();
                string question_id = exam_desc.Rows[a]["question_id"].ToString();
                int no = a + 1;

                DataTable dt = cs.Getdata("select Q.question_id ,REPLACE( REPLACE(Q.question, '<p>', ''), '</p>', '') as ques, A.objective_id, A.ans_id  from tbl_question Q Left join tbl_ans A ON A.question_id = Q.question_id where Q.question_id = " + question_id + "");
                if (dt.Rows.Count > 0)
                {
                    body += "<div class='single__list__view clearfix'>";
                    body += "<div class='row'>";
                    body += "<div class='mb-12' style='margin-left:60px;'>";
                    body += "<h6 style='font-weight:500; font-size:1.125rem; color:#202428; line-height:1.3;display:block'><b>";
                    body += ++cnt + "). ";
                    body += "&nbsp; " + dt.Rows[0]["ques"].ToString();
                    body += "</b></h6>";
                    body += "<ul style='margin-top:10px; '>";


                    DataTable dt1 = cs.Getdata("select *,REPLACE( REPLACE(objectives, '<p>', ''), '</p>', '') as obj from tbl_objective where question_id = '" + dt.Rows[0]["question_id"].ToString() + "'");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {

                        if (objid == dt1.Rows[j]["objective_id"].ToString())
                        {
                            if (dt1.Rows[j]["objective_id"].ToString() == dt.Rows[0]["objective_id"].ToString())
                            {
                                //body += "<li style='list-style-type:upper-latin; margin-left:24px; color:#66bd00; font-weight: bold;'>  " + dt1.Rows[j]["optn"].ToString() + " </li>";
                                body += "<li style='list-style-type:upper-latin; margin-left:24px; color:#66bd00; font-size: initial; font-weight: bold;'>";
                                body += "<label class='containerrdo' style='font-weight: bold;'>";
                                body += dt1.Rows[j]["obj"].ToString();
                                body += "</label></li>";
                            }
                            else
                            {
                                //body += "<li style='list-style-type:upper-latin; margin-left:24px; color:#ff0000; font-weight: bold;'>  " + dt1.Rows[j]["optn"].ToString() + " </li>";
                                body += "<li style='list-style-type:upper-latin; margin-left:24px; color:#ff0000; font-size: initial; font-weight: bold;'>";
                                body += "<label class='containerrdo'  style='font-weight: bold;'>";
                                body += dt1.Rows[j]["obj"].ToString();
                                body += "</label></li>";
                            }
                        }
                        else
                        {

                            body += "<li style='list-style-type:upper-latin; margin-left:24px; color: black; font-size: initial;'>";
                            body += "<label class='containerrdo'>";
                            body += dt1.Rows[j]["obj"].ToString();
                            body += "</label></li>";
                        }



                    }


                    body += "</ul>";

                    body += "<div class='px-4' style='margin-top:10px;'>";
                    body += "<p style='font-size:14px; margin-left:30px; color: black'>";


                    DataTable ans = cs.Getdata("select *, REPLACE( REPLACE(answer_detail, '<p>', '' ), '</p>', '') as ans_desc  from tbl_ans where question_id = " + question_id + "");
                    for (int k = 0; k < ans.Rows.Count; k++)
                    {
                        DataTable dts = cs.Getdata("WITH a as( select ROW_NUMBER() OVER(ORDER BY objective_id) as rn, *,  REPLACE(REPLACE(objectives, '<p>', ''), '</p>', '') as optn from tbl_objective where question_id = " + question_id + " ) select *, Char(a.rn + 64) as ans from a where a.objective_id = " + ans.Rows[k]["objective_id"].ToString() + "");
                        if (dts.Rows.Count > 0)
                        {
                            body += "<b>ANS: " + dts.Rows[0]["ans"].ToString() + " - " + dts.Rows[0]["optn"].ToString() + "</b><br />";
                        }
                        body += " " + ans.Rows[k]["ans_desc"].ToString() + " ";
                    }


                    body += "";
                    body += "";


                    body += "</p>";
                    body += "</div>";


                    body += "</div></div></div>";
                }

            }

            ViewBag.dt = body;



            DataTable dtERe = cs.Getdata("select * from tblExam_Result where Examid = "+ examid +"");
            if (dtERe.Rows.Count > 0)
            {
                ViewBag.total_question = dtERe.Rows[0]["total_question"].ToString();                
                ViewBag.not_attempted_question = dtERe.Rows[0]["not_attempted_question"].ToString();
                ViewBag.correct_answer = dtERe.Rows[0]["correct_answer"].ToString();
                ViewBag.wrong_answer = dtERe.Rows[0]["wrong_answer"].ToString();

            }


            return View();
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