<%@ WebHandler Language="C#" Class="TMSClass_Course" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
public class TMSClass_Course : IHttpHandler {
    private static readonly JavaScriptSerializer jsonConvert = new JavaScriptSerializer();
    public void ProcessRequest (HttpContext context) {
        List<date1> aa = new List<date1>();
       int i=0;
       string id_class1 = context.Request.QueryString["id_class1"];
       string CourseName1 = context.Request.QueryString["CourseName2"];
       string TeacherName1 = context.Request.QueryString["TeacherName3"];
       string Length1 = context.Request.QueryString["Length4"];
       //string strSQL2 = "select DISTINCT CourseName,TeacherPersonID,TeacherName,Length from Class_Course";
        string strSQL2 = "INSERT INTO [PKST].[dbo].[special_model]([id_class],[CourseName],[TeacherName],[Length]) VALUES (@id_class1,@CourseName2,@TeacherName3,@Length4);";
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd2 = new SqlCommand(strSQL2, conn);
        cmd2.CommandType = CommandType.Text;
        cmd2.Parameters.AddWithValue("@id_class", id_class1);
        cmd2.Parameters.AddWithValue("@CourseName", CourseName1);
        cmd2.Parameters.AddWithValue("@TeacherName", TeacherName1);
        cmd2.Parameters.AddWithValue("@Length", Length1);
        conn.Open();
         i= cmd2.ExecuteNonQuery();
         date1 m2 = new date1();
         m2.Length = i;  
         aa.Add(m2);
        conn.Close();
        context.Response.ContentType = "application/json";
        context.Response.Write(jsonConvert.Serialize(aa));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }     
    public class date1
    {        
        public int Length { get; set; }      
    }
}  
        
   