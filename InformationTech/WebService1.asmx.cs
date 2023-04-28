using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using InformationTech.Models;

namespace InformationTech
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        Class1 cs = new Class1();
        

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }




        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetClassDetails(string sid)
        {
            DataTable dt = new DataTable();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
            SqlDataAdapter dad;

            //dad = new SqlDataAdapter("select top 1 * from tbl_student_details where college_name =  '" + sid.ToString() + "' order by student_id desc ", con);
            dad = new SqlDataAdapter("select * from tbl_class where stream_id = " + sid.ToString() + " ", con);
            dad.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            con.Close();
            return jsSerializer.Serialize(parentRow);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSubjectDetails(string cid)
        {
            DataTable dt = new DataTable();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
            SqlDataAdapter dad;

            //dad = new SqlDataAdapter("select top 1 * from tbl_student_details where college_name =  '" + sid.ToString() + "' order by student_id desc ", con);
            dad = new SqlDataAdapter("select * from tbl_subject where class_id = " + cid.ToString() + " ", con);
            dad.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            con.Close();
            return jsSerializer.Serialize(parentRow);
        }







        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string insertExamData(string select_obj_id, string qesid, string QExamId)
        {


            string msg = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());

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

                //int insert = cs.insertRecord("tblexam_desc", "question_id,objid,Examid", " " + qId + ", " + objId + ", " + QExamId + " ");
                //DoSomeThingWith(i);

                SqlCommand cmd = new SqlCommand("insert into tblexam_desc (question_id,objective_id,Examid) values (@question_id, @objid, @Examid)", con);
                cmd.Parameters.AddWithValue("@dateandtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@question_id", qId);
                cmd.Parameters.AddWithValue("@objid", objId);
                cmd.Parameters.AddWithValue("@Examid", QExamId);

                //cmd.Parameters.AddWithValue("@message", message);

                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();


            }

            int total_question = 0;
            int attempted_question = 0;
            int not_attempted_question = 0;
            int correct_answer = 0;
            int wrong_answer = 0;

            DataTable dt = cs.Getdata("select * from tblexam_desc where Examid = " + QExamId + "");
            for (int l = 0; l < dt.Rows.Count; l++)
            {
                total_question = total_question + 1;

                DataTable dts = cs.Getdata("select * from tbl_ans where question_id = " + dt.Rows[l]["question_id"].ToString() + "");
                if (dts.Rows.Count > 0)
                {
                    if (dt.Rows[l]["objective_id"].ToString() == "0")
                    {
                        not_attempted_question = not_attempted_question + 1;
                    }
                    else
                    {
                        attempted_question = attempted_question + 1;

                        if (dt.Rows[l]["objective_id"].ToString() == dts.Rows[0]["objective_id"].ToString())
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



            SqlCommand cmd1 = new SqlCommand("insert into tblExam_Result (total_question, attempted_question, not_attempted_question, correct_answer, wrong_answer,Examid) values (@total_question, @attempted_question, @not_attempted_question, @correct_answer, @wrong_answer,@Examid)", con);
            cmd1.Parameters.AddWithValue("@total_question", total_question);
            cmd1.Parameters.AddWithValue("@attempted_question", attempted_question);
            cmd1.Parameters.AddWithValue("@not_attempted_question", not_attempted_question);
            cmd1.Parameters.AddWithValue("@correct_answer", correct_answer);
            cmd1.Parameters.AddWithValue("@wrong_answer", wrong_answer);
            cmd1.Parameters.AddWithValue("@Examid", QExamId);
            //cmd.Parameters.AddWithValue("@message", message);

            con.Open();
            int m = cmd1.ExecuteNonQuery();

            if (m == 1)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }
            con.Close();

            con.Close();



            return msg;


        }






    }
}
