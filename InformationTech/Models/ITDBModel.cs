using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace InformationTech.Models
{
    public class ITDBModel
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
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

        public List<itvariables> display()
        {
            List<itvariables> ilist = new List<itvariables>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select *from tbl_admin", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new itvariables
                        {
                            id = Convert.ToInt32(dr["admin_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            name = Convert.ToString(dr["admin_name"]),
                            contact = Convert.ToString(dr["admin_contact"]),
                            email = Convert.ToString(dr["admin_email"]),
                            photo = Convert.ToString(dr["admin_image"])


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



        public bool AddUpdate(itvariables iList)
        {

            int i = 0;

            try
            {


                string str = " Update tbl_admin set dateandtime = '" + iList.DateAndTime + "', admin_name = '" + iList.name + "', admin_email = '" + iList.email + "', admin_contact = '" + iList.contact + "', admin_image = '" + iList.photo + "' where admin_id = '" + iList.id + "'";
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
        public bool DeleteAdmin(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_admin where admin_id = '" + id + "'";
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


        public bool PasswordUpdate(admin_change_password iList)
        {

            int i = 0;

            try
            {


                string str = " Update tbl_admin set admin_password=" + iList.admin_new_password + " where admin_id = '" + iList.admin_id + "'";
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
        //******************************* stream ****************************************************
        public List<stream_detail> viewstream()
        {
            List<stream_detail> ilist = new List<stream_detail>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select *from tbl_stream order by stream", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new stream_detail
                        {
                            stream_id = Convert.ToInt32(dr["stream_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            stream_name = Convert.ToString(dr["stream"]),


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


        public bool UpdateStream(stream_detail iList)
        {

            int i = 0;

            try
            {


                string str = " Update tbl_stream set dateandtime = '" + iList.DateAndTime + "', stream = '" + iList.stream_name + "' where stream_id = '" + iList.stream_id + "'";
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


        public bool DeleteStream(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_stream where stream_id = '" + id + "'";
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

        //******************************* class ***************************************************
        public List<Class_detail> viewclass()
        {
            List<Class_detail> ilist = new List<Class_detail>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select *from tbl_class order by class_name", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Class_detail
                        {
                            class_id = Convert.ToInt32(dr["class_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            class_name = Convert.ToString(dr["class_name"]),
                            stream_id = Convert.ToInt32(dr["stream_id"]),

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


        public bool UpdateClass(Class_detail iList)
        {

            int i = 0;

            try
            {


                string str = " Update tbl_class set dateandtime = '" + iList.DateAndTime + "', class_name = '" + iList.class_name + "' where class_id = '" + iList.class_id + "'";
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


        public bool DeleteClass(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_class where class_id = '" + id + "'";
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


        //**************************************   Subject ***********************

        public List<subject> viewsubject(int id)
        {
            List<subject> ilist = new List<subject>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_subject where class_id = '" + id + "' order by subject_name", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new subject
                        {
                            subject_id = Convert.ToInt32(dr["subject_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            subject_name = Convert.ToString(dr["subject_name"]),
                            class_id = Convert.ToInt32(dr["class_id"]),
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

        public List<subject> viewUpdatesubject()
        {
            List<subject> ilist = new List<subject>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_subject order by subject_name", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new subject
                        {
                            subject_id = Convert.ToInt32(dr["subject_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            subject_name = Convert.ToString(dr["subject_name"]),
                            class_id = Convert.ToInt32(dr["class_id"]),
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





        public bool UpdateSubject(subject iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_subject set dateandtime = '" + iList.dateandtime + "', subject_name = '" + iList.subject_name + "',subject_image= '" + iList.subject_image + "'  where subject_id = '" + iList.subject_id + "'";
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

        public bool DeleteSubject(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_subject where subject_id = '" + id + "'";
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

        //****************************** Chapter*************************************************

        public List<Chapter> viewchapter(int id)
        {
            List<Chapter> ilist = new List<Chapter>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_chapter where subject_id='" + id + "' order by chapter_name ", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Chapter
                        {
                            chapter_id = Convert.ToInt32(dr["chapter_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            chapter_name = Convert.ToString(dr["chapter_name"]),
                            subject_id = Convert.ToInt32(dr["subject_id"]),

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

        public List<Chapter> ViewUpdateChapter()
        {
            List<Chapter> ilist = new List<Chapter>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_chapter order by chapter_name", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Chapter
                        {
                            chapter_id = Convert.ToInt32(dr["chapter_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            chapter_name = Convert.ToString(dr["chapter_name"]),
                            subject_id = Convert.ToInt32(dr["subject_id"]),

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

        public bool UpdateChapter(Chapter iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_chapter set dateandtime = '" + iList.DateAndTime + "', chapter_name = '" + iList.chapter_name + "'  where chapter_id = '" + iList.chapter_id + "'";
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

        public bool DeleteChapter(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_chapter where chapter_id = '" + id + "'";
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

        //********************************* Topic **************************************************

        public List<Topic> ViewUpdateTopic()
        {
            List<Topic> ilist = new List<Topic>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_topic order by topic_tittle", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Topic
                        {
                            topic_id = Convert.ToInt32(dr["topic_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            topic_tittle = Convert.ToString(dr["topic_tittle"]),
                            topic_image = Convert.ToString(dr["topic_image"]),

                            chapter_id = Convert.ToInt32(dr["chapter_id"]),

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


        public bool UpdateTopic(Topic iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_topic set dateandtime = '" + iList.dateandtime + "', topic_tittle = '" + iList.topic_tittle + "',topic_image= '" + iList.topic_image + "'  where topic_id = '" + iList.topic_id + "'";
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

        public List<Topic> ViewTopic(int id)
        {
            List<Topic> ilist = new List<Topic>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_topic where chapter_id = '" + id + "' order by topic_tittle", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Topic
                        {
                            topic_id = Convert.ToInt32(dr["topic_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            topic_tittle = Convert.ToString(dr["topic_tittle"]),
                            topic_image = Convert.ToString(dr["topic_image"]),
                            chapter_id = Convert.ToInt32(dr["chapter_id"])

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



        public bool DeleteTopic(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_topic where topic_id = '" + id + "'";
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

        //**************************** Topic Detail***************************************************************

        public List<TopicDetail> ViewTopicDetail(int id)
        {
            List<TopicDetail> ilist = new List<TopicDetail>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from topic_tittle_detail where topic_id = '" + id + "'", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new TopicDetail
                        {
                            topic_detail_id = Convert.ToInt32(dr["topic_detail_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            topic_detail_tittle = Convert.ToString(dr["topic_detail_tittle"]),
                            topic_id = Convert.ToInt32(dr["topic_id"]),

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

        public List<TopicDetail> ViewUpdateTopicDetail()
        {
            List<TopicDetail> ilist = new List<TopicDetail>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from topic_tittle_detail", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new TopicDetail
                        {
                            topic_detail_id = Convert.ToInt32(dr["topic_detail_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            topic_detail_tittle = Convert.ToString(dr["topic_detail_tittle"]),
                            topic_id = Convert.ToInt32(dr["topic_id"]),

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

        public bool UpdateTopicDetail(TopicDetail iList)
        {

            int i = 0;

            try
            {
                string str = " Update topic_tittle_detail set dateandtime = '" + iList.dateandtime + "', topic_detail_tittle = '" + iList.topic_detail_tittle + "'  where topic_detail_id = '" + iList.topic_detail_id + "'";
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

        public bool DeleteDetailTopic(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from topic_tittle_detail where topic_detail_id = '" + id + "'";
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


        //***************************************** Topic PDF ************************************************************

        public List<topicpdf> ViewTopicPdf(int id)
        {
            List<topicpdf> ilist = new List<topicpdf>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select T.*,P.* from tbl_pdf P Left join tbl_topic T on T.topic_id = P.topic_id where P.topic_id = '" + id + "' order by T.topic_tittle", conn);
                //
                //SqlDataAdapter adp = new SqlDataAdapter("select T.*,P.* from tbl_pdf P Left join tbl_topic T on T.topic_id = P.topic_id where P.topic_id='" + id +"' order by T.topic_title", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new topicpdf
                        {
                            pdf_id = Convert.ToInt32(dr["pdf_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            topic_name = Convert.ToString(dr["topic_tittle"]),
                            pdf = Convert.ToString(dr["pdf"]),
                            topic_id = Convert.ToInt32(dr["topic_id"]),

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

        public bool DeletePdf(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_pdf where pdf_id = '" + id + "'";
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

        public List<topicvideo> ViewTopicVideo(int id)
        {
            List<topicvideo> ilist = new List<topicvideo>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select T.topic_tittle,T.topic_id,v.* from tbl_video v Left join tbl_topic T on T.topic_id = v.topic_id where v.topic_id='" + id + "' order by T.topic_tittle", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new topicvideo
                        {
                            video_id = Convert.ToInt32(dr["video_id"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),
                            topic_tittle = Convert.ToString(dr["topic_tittle"]),
                            video = Convert.ToString(dr["video"]),
                            topic_id = Convert.ToInt32(dr["topic_id"]),

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

        public bool DeleteTopicVideo(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_video where video_id = '" + id + "'";
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
        //*********************************** Topic Source Code ************************************************

        public List<topicsourcecode> ViewTopicSourceCode(int id)
        {
            List<topicsourcecode> ilist = new List<topicsourcecode>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select T.topic_tittle,T.topic_id,s.* from topic_source_code s Left join tbl_topic T on T.topic_id = s.topic_id where s.topic_id='" + id + "'", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new topicsourcecode
                        {
                            topic_source_code_id = Convert.ToInt32(dr["topic_source_code_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            topic_source_code = Convert.ToString(dr["topic_source_code"]),
                            topic_tittle = Convert.ToString(dr["topic_tittle"]),
                            topic_id = Convert.ToInt32(dr["topic_id"])

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

        public List<topicsourcecode> ViewUpdateTopicSourceCode()
        {
            List<topicsourcecode> ilist = new List<topicsourcecode>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from topic_source_code", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new topicsourcecode
                        {
                            topic_source_code_id = Convert.ToInt32(dr["topic_source_code_id"]),
                            DateAndTime = Convert.ToString(dr["dateandtime"]),
                            topic_source_code = Convert.ToString(dr["topic_source_code"]),

                            topic_id = Convert.ToInt32(dr["topic_id"])

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

        public bool UpdateTopicSourceCode(topicsourcecode iList)
        {

            int i = 0;

            try
            {
                string str = " Update topic_source_code set dateandtime = '" + iList.DateAndTime + "', topic_source_code = '" + iList.topic_source_code + "'  where topic_source_code_id = '" + iList.topic_source_code_id + "'";
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

        public bool DeletetopicSourcecode(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from topic_source_code where topic_source_code_id = '" + id + "'";
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

        //********************************* Question view***************************************************
        public List<Question> QuestionView(int id)
        {
            List<Question> ilist = new List<Question>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_question where subject_id = '" + id + "' ", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Question
                        {
                            question_id = Convert.ToInt32(dr["question_id"]),
                            DateAndTime = Convert.ToDateTime(dr["dateandtime"]),
                            question = Convert.ToString(dr["question"]),
                            subject_id = Convert.ToInt32(dr["subject_id"])


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

        public List<Question> ViewUpdateQuestion()
        {
            List<Question> ilist = new List<Question>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_question", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Question
                        {
                            question_id = Convert.ToInt32(dr["question_id"]),
                            DateAndTime = Convert.ToDateTime(dr["dateandtime"]),
                            question = Convert.ToString(dr["question"]),

                            subject_id = Convert.ToInt32(dr["subject_id"])

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

        public bool UpdateQuestion(Question iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_question set dateandtime = '" + iList.DateAndTime + "', question = '" + iList.question + "'  where question_id = '" + iList.question_id + "'";
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

        public bool DeleteQuestion(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_question where question_id = '" + id + "'";
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

        //***************************************** Objective *********************************************************

        public List<Objective> ObjectiveView(int id)
        {
            List<Objective> ilist = new List<Objective>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_objective where question_id = '" + id + "' ", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Objective
                        {
                            objective_id = Convert.ToInt32(dr["objective_id"]),
                            DateAndTime = Convert.ToDateTime(dr["dateandtime"]),
                            Objectives = Convert.ToString(dr["objectives"]),
                            question_id = Convert.ToInt32(dr["question_id"])


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

        public bool DeleteObjective(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_objective where objective_id = '" + id + "'";
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

        public List<Objective> ViewUpdateObjective()
        {
            List<Objective> ilist = new List<Objective>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_objective", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Objective
                        {
                            objective_id = Convert.ToInt32(dr["objective_id"]),
                            DateAndTime = Convert.ToDateTime(dr["dateandtime"]),
                            Objectives = Convert.ToString(dr["objectives"]),
                            question_id = Convert.ToInt32(dr["question_id"])


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

        public bool UpdateObjective(Objective iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_objective set dateandtime = '" + iList.DateAndTime + "', objectives = '" + iList.Objectives + "'  where objective_id = '" + iList.objective_id + "'";
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

        //******************************* ANSWER ***************************************************************

        public List<Answer> AnswerView(int id)
        {
            List<Answer> ilist = new List<Answer>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_ans where question_id = '" + id + "' ", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Answer
                        {
                            ans_id = Convert.ToInt32(dr["ans_id"]),
                            DateAndTime = Convert.ToDateTime(dr["dateandtime"]),
                            objective_id = Convert.ToInt32(dr["objective_id"]),
                            answer_detail = Convert.ToString(dr["answer_detail"]),
                            question_id = Convert.ToInt32(dr["question_id"])


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

        public List<Answer> ViewUpdateAnswer()
        {
            List<Answer> ilist = new List<Answer>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_ans ", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new Answer
                        {
                            ans_id = Convert.ToInt32(dr["ans_id"]),
                            DateAndTime = Convert.ToDateTime(dr["dateandtime"]),
                            objective_id = Convert.ToInt32(dr["objective_id"]),
                            answer_detail = Convert.ToString(dr["answer_detail"]),
                            question_id = Convert.ToInt32(dr["question_id"])


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

        public bool UpdateAnswer(Answer iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_ans set dateandtime = '" + iList.DateAndTime + "', objective_id = '" + iList.objective_id + "', answer_detail = '" + iList.answer_detail + "'  where ans_id = '" + iList.ans_id + "'";
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

        public bool DeleteAnswer(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_ans where ans_id = '" + id + "'";
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

        //************************************ Subject Paper ****************************************************

        public List<paper> PaperView(int id)
        {
            List<paper> ilist = new List<paper>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select S.*,P.* from tbl_paper P Left join tbl_subject S on S.subject_id = P.subject_id where P.subject_id='" + id + "' ", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new paper
                        {
                            exam_paper_id = Convert.ToInt32(dr["paper_id"]),
                            year = Convert.ToString(dr["year"]),
                            month = Convert.ToString(dr["month"]),
                            subject = Convert.ToString(dr["subject_name"]),
                            subject_id = Convert.ToInt32(dr["subject_id"]),
                            pdf = Convert.ToString(dr["paper_pdf"])



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

        public bool DeletePaper(int id)
        {

            int i = 0;

            try
            {

                string str = " Delete from tbl_paper where paper_id = '" + id + "'";
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

        public List<price> ViewPrice()
        {
            List<price> ilist = new List<price>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from tbl_price", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new price
                        {
                            price_id = Convert.ToInt32(dr["price_id"]),
                            money = Convert.ToInt32(dr["price"]),
                            days = Convert.ToString(dr["exp_days"]),
                            dateandtime = Convert.ToString(dr["dateandtime"]),

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


        public bool UpdatePrice(price iList)
        {

            int i = 0;

            try
            {
                string str = " Update tbl_price set dateandtime = '" + iList.dateandtime + "', price = '" + iList.money + "',exp_days= '" + iList.days + "'  where price_id = '" + iList.price_id + "'";
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

        public List<member> memberdisplay()
        {
            List<member> ilist = new List<member>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select *from user_registration", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select *from tbl_membership where user_id = '" + Convert.ToString(dr["user_id"]) + "' ", conn);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                ilist.Add(new member
                                {

                                   name = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]),
                                    email = Convert.ToString(dr["email"]),
                                    price = Convert.ToInt32(dr1["price"]),
                                    dateandtime = Convert.ToString(dr1["joining_date"]),
                                    exday = Convert.ToString(dr1["exp_date"])
                                   


                                });
                            }
                        }
                        
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








        public List<tblExam_Result> GetExamResult()
        {
            List<tblExam_Result> ilist = new List<tblExam_Result>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select ER.*, S.subject_name, UR.first_name + ' ' + UR.last_name as Name  from tblExam_Result ER LEFT JOIN tblExam E On E.Examid = ER.Examid LEFT JOIN user_registration UR ON UR.user_id = E.user_id LEFT JOIN tbl_subject S ON S.subject_id = E.subject_id order by ER.Exam_Result_id Desc", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ilist.Add(new tblExam_Result
                        {
                            Exam_Result_id = Convert.ToInt32(dr["Exam_Result_id"]),
                            total_question = Convert.ToInt32(dr["total_question"]),
                            attempted_question = Convert.ToInt32(dr["attempted_question"]),
                            not_attempted_question = Convert.ToInt32(dr["not_attempted_question"]),
                            correct_answer = Convert.ToInt32(dr["correct_answer"]),
                            wrong_answer = Convert.ToInt32(dr["wrong_answer"]),
                            Examid = Convert.ToInt32(dr["Examid"]),
                            subject_name = Convert.ToString(dr["subject_name"]),
                            Name = Convert.ToString(dr["Name"])


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

        public List<enquiry> contactdisplay()
        {
            List<enquiry> ilist = new List<enquiry>();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select *from tbl_contact order by dateandtime desc", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                ilist.Add(new enquiry
                                {
                                    name = Convert.ToString(dr["name"]),
                                    email = Convert.ToString(dr["email"]),
                                    subject = Convert.ToString(dr["subject"]),
                                    message = Convert.ToString(dr["message"])
                                   



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


    }
}