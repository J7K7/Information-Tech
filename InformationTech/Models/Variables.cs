using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationTech.Models
{
    public class Variables
    {
       
    }

    //*************************************Admin Register Variables********************************************
    public class itvariables
    {
        public int id { get; set; }
        public string DateAndTime { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string repassword { get; set; }
        public int otp { get; set; }
        public string photo { get; set; }
        
        
       
      

    }
   

    //*****************************************Admin Change Password Variables*************************

    public class admin_change_password
    {
        public string admin_id { get; set; }
        public string admin_old_password { get; set; }
        public string admin_new_password { get; set; }

        public string admin_new_repassword { get; set; }

    }

    //******************************************Stream Variables******************************
    public class stream_detail
    {
        public int stream_id { get; set; }
        public string stream_name { get; set; }
        public string DateAndTime { get; set; }

    }
    //************************************Class Variables***********************************
    public class Class_detail
    {
        public int class_id { get; set; }
        public string DateAndTime { get; set; }
        public string class_name { get; set; }
        public int stream_id { get; set; }


    }

    //***********************************Subject Variables******************************
    public class subject
    {
        public int subject_id { get; set; }
        public String dateandtime { get; set; }
        public string subject_name { get; set; }
        public string subject_image { get; set; }
        public int class_id { get; set; }

    }

    //***************************Chapter Variables**********************************
    public class Chapter
    {

        public int chapter_id { get; set; }
        public String DateAndTime { get; set; }
        public string chapter_name { get; set; }
         public int subject_id { get; set; }

    }

    //**************************Topic Variables***************************************
    public class Topic
    {
        public int topic_id { get; set; }
        public String dateandtime { get; set; }
        public string topic_tittle { get; set; }
        public int chapter_id { get; set; }
        public string topic_image { get; set; }

    }

    //*****************************************Topic Detail Variables******************************************
    public class TopicDetail
    {
        public int topic_detail_id { get; set; }

        public string dateandtime { get; set; }
        public string topic_detail_tittle { get; set; }
        public int topic_id { get; set; }



    }

    //********************************Topic Pdf Variables*****************************************
    public class topicpdf
    {
        public int pdf_id { get; set; }

        public string dateandtime { get; set; }
         public string pdf { get; set; }

        public string topic_name { get; set; }

        public int topic_id { get; set; }

    }

    //**********************************Topic Video Variables*****************************************
    public class topicvideo
    {

        public int video_id { get; set; }

        public string dateandtime { get; set; }

        public string video { get; set; }
        public string topic_tittle { get; set; }


        public int topic_id { get; set; }
    }
	
	//*******************************Topic  Source Code **********************************
	
	public class topicsourcecode
    {

        public int topic_source_code_id { get; set; }
         public String DateAndTime { get; set; }
           
        public string topic_source_code { get; set; }

        public string topic_tittle { get; set; }
        
        public int topic_id { get; set; } 

    }
	
	//*************************************Question**************************************
	
	
	public class Question
    {

        public int question_id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string question { get; set; }
         public int stream_id { get; set; }
        public int class_id { get; set; }
        public int subject_id { get; set; }


    }

    public class Objective
    {
        public int objective_id { get; set; }
        public DateTime DateAndTime { get; set; }

        public string  Objectives { get; set; }
         public int question_id { get; set; }


    }

    public class Answer
    {

        public int ans_id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string answer_detail { get; set; }
        public int objective_id { get; set; }

        public int question_id { get; set; }
       

    }

    public class paper
    {
         public int exam_paper_id { get; set; }
        public string pdf { get; set; }

        public int subject_id { get; set; }
        public int stream_id { get; set; }
        public int class_id { get; set; }
        
        public string subject { get; set; }

        public string month { get; set; }

        public string year { get; set; }

    }

    public class price
    {
        public int price_id { get; set; }

        public string dateandtime { get; set; }
        public int money { get; set; }

        public  string days { get; set; }
    }
    public class member
    {
        public int price { get; set; }

        public string dateandtime { get; set; }

        public string name { get; set; }

        public string exday { get; set; }

        public string email { get; set; }
    }

    public class enquiry
    {
        public string name { get; set; }

        public string dateandtime { get; set; }
        public string email { get; set; }

        public string subject { get; set; }

        public string message { get; set; }
    }
}