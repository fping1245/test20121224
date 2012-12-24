<%@ WebHandler Language="C#" Class="saveschedule" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class saveschedule : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string id_class = context.Request.QueryString["id_class"];
        string dateid = context.Request.QueryString["dateid"];
        string[] sp = dateid.Split('!');
        string sep_dateid_date = sp[0];
        string sep_dateid_duty = sp[1];

        string sn = context.Request.QueryString["sn"];

        string CourseName = "";
        string id_user = "";
        string TeacherName = "";
        string clsroom = "";

        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd2 = "select CourseName,id_user,TeacherName,clsroom from special_model where sn=@sn";

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd2 = new SqlCommand(strCmd2, conn))
            {
                cmd2.Parameters.AddWithValue("@sn", sn);
                conn.Open();
                try
                {
                    SqlDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read())
                    {
                        CourseName = Convert.ToString(dr[0]);
                        id_user = Convert.ToString(dr[1]);
                        TeacherName = Convert.ToString(dr[2]);
                        clsroom = Convert.ToString(dr[3]);
                    }
                }
                catch
                {
                    if (sn == "H")
                    {
                        CourseName = "H";
                        id_user = "";
                        TeacherName = "";
                        clsroom = "";
                    }
                    else if (sn == "L")
                    {
                        CourseName = "L";
                        id_user = "";
                        TeacherName = "";
                        clsroom = "";
                    }
                    //誤打數字到課表會自動清空資料
                    else
                    {
                        CourseName = "";
                        id_user = "";
                        TeacherName = "";
                        clsroom = "";
                    }
                }
                conn.Close();
            }
        }



        using (SqlConnection conn = new SqlConnection(strConn))
        {
            string strCmd = "UPDATE [PKST].[dbo].[timetable] set [CourseName]=@CourseName,[id_user]=@id_user,[TeacherName]=@TeacherName,[clsroom]=@clsroom where [date]=@sep_dateid_date and [duty]=@sep_dateid_duty and [id_class]=@id_class";
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@CourseName", CourseName);
                cmd.Parameters.AddWithValue("@id_user", id_user);
                cmd.Parameters.AddWithValue("@TeacherName", TeacherName);
                cmd.Parameters.AddWithValue("@clsroom", clsroom);

                cmd.Parameters.AddWithValue("@sep_dateid_date", sep_dateid_date);
                cmd.Parameters.AddWithValue("@sep_dateid_duty", sep_dateid_duty);
                cmd.Parameters.AddWithValue("@id_class", id_class);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        context.Response.ContentType = "text/plain";
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}