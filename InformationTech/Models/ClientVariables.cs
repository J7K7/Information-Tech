using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationTech.Models
{
    public class ClientVariables
    {

        public string stream { get; set; }

        public string class_name { get; set; }
         
        public string subject { get; set; }
        public string subject_image { get; set; }
        public string url { get; set; }

        public string url1 { get; set; }

        
        public int subject_id { get; set; }

        public string chapter_name { get; set; }

    }

    public class ChapterDetail
    {
        public int topic_id { get; set; }
        public int subject_id { get; set; }
        public string topic_tittle { get; set; }

        public string topic_image { get; set; }
         
        public string topic_detail_tittle { get; set; }
        public string url { get; set; }
        public string url1 { get; set; }
        public string chapter_name { get; set; }
    }

    public class ClientTopicDetail
    {
        public int subject_id { get; set; }
        public string topic_tittle { get; set; }

        public string topic_image { get; set; }

        public string topic_detail_tittles { get; set; }
        public string url1 { get; set; }
        public string chapter_name { get; set; }

        public string video { get; set; }
        public string pdf { get; set; }
        public string source_code { get; set; }

    }

    public class ClientRegister
    {

        public int user_id { get; set; }
        public string DateAndTime { get; set; }
        public string first_name { get; set; }
         public string last_name { get; set; }
        
        public string email { get; set; }

        public string uemail { get; set; }
        public string otp { get; set; }

        public string password { get; set; }

        public string rpassword { get; set; }

        public string contact { get; set; }

        public string photo { get; set; }

    }


    public class tblExam
    {
        public int Examid { get; set; }
        public string dateandtime { get; set; }
        public int subject_id { get; set; }
        public int user_id { get; set; }
    }


    public class tblexam_desc
    {
        public int ExamDes_id { get; set; }
        public int question_id { get; set; }
        public int objective_id { get; set; }
        public int Examid { get; set; }


        public string select_obj_id { get; set; }
        public string qesid { get; set; }
        public int QExamId { get; set; }
    }


    public class tblExam_Result
    {
        public int Exam_Result_id { get; set; }
        public int total_question { get; set; }
        public int attempted_question { get; set; }
        public int not_attempted_question { get; set; }
        public int correct_answer { get; set; }
        public int wrong_answer { get; set; }
        public int Examid { get; set; }

        public string datetime { get; set; }

        public string subject_name { get; set; }

        public string Name { get; set; }
        
    }

    
    public class Exampaper
    {

        public int paper_id { get; set; }
        public string year { get; set; }

        public string subject { get; set; }
        public string month { get; set; }
        public string paper { get; set; }
    }

    public class clientchangepass
    {

        public int user_id { get; set; }
        public string oldpass { get; set; }

        public string newpass { get; set; }

        public string repass { get; set; }

    }

    public class contact
    {
        public string name { get; set; }

        public string email { get; set; }

        public string subject { get; set; }

        public string message { get; set;}

        public string DateAndTime { get; set;}
    }
}