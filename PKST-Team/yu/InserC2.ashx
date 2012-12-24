<%@ WebHandler Language="C#" Class="InserC2" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
public class InserC2 : IHttpHandler
{

    private static readonly JavaScriptSerializer jsonConvert = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        List<date1> aa = new List<date1>();
        
        string id_class1 = context.Request.QueryString["id_class1"];
        string CourseName1 = context.Request.QueryString["CourseName2"];
        string TeacherName1 = context.Request.QueryString["TeacherName3"];        
        char cr = ' ';
        string[] s = TeacherName1.Split(cr);
        string Length1 = context.Request.QueryString["Length4"];
        string clsroom = context.Request.QueryString["clsroom"];
        string id_user = context.Request.QueryString["id_user"];
        //string strSQL2 = "select DISTINCT CourseName,TeacherPersonID,TeacherName,Length from Class_Course";
        string strSQL2 = "INSERT INTO [PKST].[dbo].[special_model]([id_class],[CourseName],[TeacherName],[Length],[clsroom],[id_user]) VALUES (@id_class1,@CourseName2,@TeacherName3,@Length4,@clsroom,@id_user);";
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd2 = new SqlCommand(strSQL2, conn);
        cmd2.CommandType = CommandType.Text;      
        conn.Open();
        cmd2.Parameters.AddWithValue("@id_class1", id_class1);
        cmd2.Parameters.AddWithValue("@CourseName2", CourseName1);
        cmd2.Parameters.AddWithValue("@TeacherName3", s[0]);
        cmd2.Parameters.AddWithValue("@Length4", Length1);
        cmd2.Parameters.AddWithValue("@clsroom", clsroom);
        cmd2.Parameters.AddWithValue("@id_user", id_user);
        cmd2.ExecuteNonQuery();
        date1 m2 = new date1();
        //m2.Length = i;
        aa.Add(m2);
        conn.Close();
        context.Response.ContentType = "application/json";
        context.Response.Write(jsonConvert.Serialize(aa));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public class date1
    {
        public int Length { get; set; }
    }
}  