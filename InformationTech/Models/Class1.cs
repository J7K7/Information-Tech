using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InformationTech.Models
{
    public class Class1
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString.ToString());
        SqlCommand cmd = null;
        int RowAffect = 0, rowAffect = 0;
        SqlDataAdapter adp = new SqlDataAdapter();


        public DataTable Getdata(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);

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



        public int insertRecord(string tableName, string columns, string values)
        {
            if (conn != null)
                conn.Close();

            try
            {
                conn.Open();
                try
                {
                    cmd = new SqlCommand("Insert into " + tableName + " (" + columns + ") values (" + values + ")", conn);
                    rowAffect = cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            conn.Close();
            return rowAffect;
        }




    }
}