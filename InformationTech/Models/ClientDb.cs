using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InformationTech.Models
{
    public class ClientDb
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
        Class1 cs = new Class1();
        public int insertrecrod(string tblname, string columns, string values)
        {
            int row = 0;
            conn.Open();
            try
            {
                string query = "insert into " + tblname + " (" + columns + ") values (" + values + ")";
                SqlCommand cmd = new SqlCommand(query, conn);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();

            }


            return row;
        }

        public List<ClientVariables> display()
        {
            List<ClientVariables> ilist = new List<ClientVariables>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select S.*, LOWER( REPLACE( S.subject_name, ' ', '-')) as url , C.class_name, ST.stream from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id order by NEWID()", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new ClientVariables
                        {
                            
                            stream = Convert.ToString(dr["stream"]),
                            class_name = Convert.ToString(dr["class_name"]),
                            subject = Convert.ToString(dr["subject_name"]),
                            url = Convert.ToString(dr["url"]),
                            subject_image = Convert.ToString(dr["subject_image"])


                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public List<ClientVariables> subjectdisplay(string id)
        {
            List<ClientVariables> ilist = new List<ClientVariables>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("WITH a as(select S.*, LOWER(REPLACE(S.subject_name, ' ', '-')) as url, C.class_name, ST.stream,CH.chapter_name from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id LEFT JOIN tbl_chapter CH ON CH.subject_id = S.Subject_id  ) select* from a where a.url = '" + id+"'", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new ClientVariables
                        {

                            stream = Convert.ToString(dr["stream"]),
                            class_name = Convert.ToString(dr["class_name"]),
                            subject = Convert.ToString(dr["subject_name"]),
                            url = Convert.ToString(dr["url"]),
                            subject_id = Convert.ToInt32(dr["subject_id"]),
                            subject_image = Convert.ToString(dr["subject_image"]),
                            chapter_name = Convert.ToString(dr["chapter_name"]),
                            url1 = Convert.ToString(dr["chapter_name"]).Replace(" ","-")

                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public string subjectimage(string id)
        {
            string image="";
            
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("WITH a as(select S.*, LOWER(REPLACE(S.subject_name, ' ', '-')) as url, C.class_name, ST.stream,CH.chapter_name from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id LEFT JOIN tbl_chapter CH ON CH.subject_id = S.Subject_id  ) select* from a where a.url = '" + id + "'", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    image = dt.Rows[0]["subject_image"].ToString();

                   
                }
                
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return image;

        }

        public List<ChapterDetail> chapterdisplay(string id)
        {
            string[] id1 = id.Split('_');
            List<ChapterDetail> ilist = new List<ChapterDetail>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("WITH a as(select S.*, LOWER(REPLACE(CH.chapter_name, ' ', '-')) as url, C.class_name, ST.stream, CH.chapter_name, tc.topic_tittle, tc.topic_id,tc.topic_image, td.topic_detail_tittle from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id LEFT JOIN tbl_chapter CH ON CH.subject_id = S.Subject_id LEFT JOIN tbl_topic tc ON CH.chapter_id = tc.chapter_id LEFT JOIN topic_tittle_detail td ON tc.topic_id = td.topic_id) select * from a where a.url  = '" + id1[1] + "' AND subject_id = '" + id1[0] +"'", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new ChapterDetail
                        {
                            subject_id = Convert.ToInt32(dr["subject_id"]),
                            topic_id = Convert.ToInt32(dr["topic_id"]),
                            topic_tittle= Convert.ToString(dr["topic_tittle"]),
                            topic_detail_tittle = Convert.ToString(dr["topic_detail_tittle"]),
                            topic_image = Convert.ToString(dr["topic_image"]),
                            url = Convert.ToString(dr["url"]),
                            url1 = Convert.ToString(dr["topic_tittle"]).Replace(" ", "-")


                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public DataTable display1(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_chapter where subject_id = '" + id + "'", conn);

              
                adp.Fill(dt);
                
               

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public List<ClientTopicDetail> topicdisplay(string id)
        {
            string[] id1 = id.Split('_');
            List<ClientTopicDetail> ilist = new List<ClientTopicDetail>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("WITH a as(select S.*, LOWER(REPLACE(tc.topic_tittle, ' ', '-')) as url, C.class_name, tvideo.video, tp.pdf, ts.topic_source_code, ST.stream, CH.chapter_name, tc.topic_tittle, tc.topic_image,tc.topic_id, td.topic_detail_tittle from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id LEFT JOIN tbl_chapter CH ON CH.subject_id = S.Subject_id LEFT JOIN tbl_topic tc ON CH.chapter_id = tc.chapter_id LEFT JOIN topic_tittle_detail td ON tc.topic_id = td.topic_id LEFT JOIN tbl_video tvideo ON tc.topic_id = tvideo.topic_id LEFT JOIN tbl_pdf tp ON tc.topic_id = tp.topic_id LEFT JOIN topic_source_code ts ON tc.topic_id = ts.topic_id) select* from a where a.url = '"+id1[1]+"' AND topic_id = '"+id1[0]+"'", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new ClientTopicDetail
                        {
   
                            topic_tittle = Convert.ToString(dr["topic_tittle"]),
                            topic_detail_tittles = Convert.ToString(dr["topic_detail_tittle"]),
                            topic_image = Convert.ToString(dr["topic_image"]),
                            video  = Convert.ToString(dr["video"]),
                            pdf = Convert.ToString(dr["pdf"]),
                            source_code = Convert.ToString(dr["topic_source_code"]),
                            url1 = Convert.ToString(dr["topic_tittle"]).Replace(" ", "-")


                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public DataTable display2(string  id)
        {
            string[] id1 = id.Split('_');
            string ch_id = "";
            string subject_id = "";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                conn.Open();
                 dt = cs.Getdata("select chapter_id from tbl_topic where topic_id = '"+id1[0]+"'");
                if (dt.Rows.Count > 0)
                {
                    ch_id = dt.Rows[0]["chapter_id"].ToString();

                }


                dt1 = cs.Getdata("select * from tbl_chapter where chapter_id= '" + ch_id + "' ");
                if (dt1.Rows.Count > 0)
                {

                    subject_id = dt1.Rows[0]["subject_id"].ToString();
                }
                  
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return dt1;
        }

        public DataTable topicimage(string id)
        {
            string[] id1 = id.Split('_');
            string ch_id = "";
          
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                conn.Open();
                dt = cs.Getdata("select chapter_id from tbl_topic where topic_id = '" + id1[0] + "'");
                if (dt.Rows.Count > 0)
                {
                    ch_id = dt.Rows[0]["chapter_id"].ToString();

                }

                SqlDataAdapter adp = new SqlDataAdapter("WITH a as(select S.*, LOWER(REPLACE(tc.topic_tittle, ' ', '-')) as url, C.class_name, tvideo.video, tp.pdf, ts.topic_source_code, ST.stream, CH.chapter_name, CH.chapter_id,tc.topic_tittle, tc.topic_image,tc.topic_id, td.topic_detail_tittle from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id LEFT JOIN tbl_chapter CH ON CH.subject_id = S.Subject_id LEFT JOIN tbl_topic tc ON CH.chapter_id = tc.chapter_id LEFT JOIN topic_tittle_detail td ON tc.topic_id = td.topic_id LEFT JOIN tbl_video tvideo ON tc.topic_id = tvideo.topic_id LEFT JOIN tbl_pdf tp ON tc.topic_id = tp.topic_id LEFT JOIN topic_source_code ts ON tc.topic_id = ts.topic_id) select* from a where NOT a.url ='"+id1[1]+"' AND chapter_id='"+ch_id+"'", conn);


                adp.Fill(dt1);


            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return dt1;
        }

        public DataTable topicname(string id)
        {
            string[] id1 = id.Split('_');
            string ch_id = "";

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                conn.Open();
                dt = cs.Getdata("select chapter_id from tbl_topic where topic_id = '" + id1[0] + "'");
                if (dt.Rows.Count > 0)
                {
                    ch_id = dt.Rows[0]["chapter_id"].ToString();

                }

                SqlDataAdapter adp = new SqlDataAdapter("WITH a as(select S.*, LOWER(REPLACE(tc.topic_tittle, ' ', '-')) as url, C.class_name, tvideo.video, tp.pdf, ts.topic_source_code, ST.stream, CH.chapter_name, CH.chapter_id,tc.topic_tittle, tc.topic_image,tc.topic_id, td.topic_detail_tittle from tbl_subject S LEFT JOIN tbl_class c ON C.class_id = S.class_id LEFT JOIN tbl_stream ST ON ST.stream_id = C.stream_id LEFT JOIN tbl_chapter CH ON CH.subject_id = S.Subject_id LEFT JOIN tbl_topic tc ON CH.chapter_id = tc.chapter_id LEFT JOIN topic_tittle_detail td ON tc.topic_id = td.topic_id LEFT JOIN tbl_video tvideo ON tc.topic_id = tvideo.topic_id LEFT JOIN tbl_pdf tp ON tc.topic_id = tp.topic_id LEFT JOIN topic_source_code ts ON tc.topic_id = ts.topic_id) select* from a where NOT a.url ='" + id1[1] + "' AND chapter_id='" + ch_id + "'", conn);


                adp.Fill(dt1);


            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return dt1;
        }

        public List<ClientVariables> Examsubjectdisplay()
        {
            List<ClientVariables> ilist = new List<ClientVariables>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_subject", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new ClientVariables
                        {

                            subject = Convert.ToString(dr["subject_name"]),
                            subject_id = Convert.ToInt32(dr["subject_id"]),
                            subject_image = Convert.ToString(dr["subject_image"]),

                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public List<Exampaper> Exampaperdisplay()
        {
            List<Exampaper> ilist = new List<Exampaper>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select S.*,P.* from tbl_paper P LEFT JOIN tbl_subject S ON P.subject_id = s.subject_id order by NEWID()", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Exampaper
                        {

                            month = Convert.ToString(dr["month"]),
                            year = Convert.ToString(dr["year"]),
                            paper = Convert.ToString(dr["paper_pdf"]),
                            subject = Convert.ToString(dr["subject_name"]),

                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }


        public bool PasswordUpdate(clientchangepass iList)
        {

            int i = 0;

            try
            {


                string str = " Update user_registration set password= '" + iList.newpass +"' where user_id = '" + iList.user_id + "'";
                SqlCommand cmd = new SqlCommand(str, conn);
                conn.Open();
                i = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public List<tblExam_Result> Examresultdisplay(int user_id)
        {
            List<tblExam_Result> ilist = new List<tblExam_Result>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select S.*,P.*,S1.subject_name,S1.subject_id from  tbl_subject S1  INNER JOIN tblExam P ON P.subject_id = S1.subject_id INNER JOIN tblExam_Result S ON P.Examid = s.Examid   where P.user_id= '" + user_id+ "' order by P.dateandtime desc", conn);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new tblExam_Result
                        {
                            subject_name = Convert.ToString(dr["subject_name"]),
                            datetime = Convert.ToString(dr["dateandtime"]),
                            total_question = Convert.ToInt32(dr["total_question"]),
                            attempted_question = Convert.ToInt32(dr["attempted_question"]),
                            not_attempted_question = Convert.ToInt32(dr["not_attempted_question"]),
                            correct_answer = Convert.ToInt32(dr["correct_answer"]),
                            wrong_answer = Convert.ToInt32(dr["wrong_answer"]),

                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public List<ClientRegister> Updateprofile()
        {
            List<ClientRegister> ilist = new List<ClientRegister>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from user_registration", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new ClientRegister
                        {
                            user_id = Convert.ToInt32(dr["user_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            first_name = Convert.ToString(dr["first_name"]),
                            last_name = Convert.ToString(dr["last_name"]),
                            email = Convert.ToString(dr["email"]),
                            contact = Convert.ToString(dr["contact"]),
                            photo  = Convert.ToString(dr["photo"]),

                        });
                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return ilist;
        }

        public bool Updateprofile(ClientRegister iList)
        {

            int i = 0;

            try
            {


                string str = " Update user_registration set dateandtime = '" + iList.DateAndTime + "', first_name = '" + iList.first_name + "', last_name='" + iList.last_name+"', email='"+iList.email+"',contact='"+iList.contact+"',photo='"+iList.photo+"' where user_id = '" + iList.user_id + "'";
                SqlCommand cmd = new SqlCommand(str, conn);
                conn.Open();
                i = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


    }
}