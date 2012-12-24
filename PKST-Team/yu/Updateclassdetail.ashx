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
       string id_classroom = context.Request.QueryString["id_classroom"];
       string startdate = context.Request.QueryString["startdate"];
       string enddate = context.Request.QueryString["enddate"];
       string lab1 = context.Request.QueryString["lab1"];
       string lab2 = context.Request.QueryString["lab2"];
       string lab3 = context.Request.QueryString["lab3"];
       string lab4 = context.Request.QueryString["lab4"];
       string lab5 = context.Request.QueryString["lab5"];
       string id_class = context.Request.QueryString["id_class"];
       //string strSQL2 = "select DISTINCT CourseName,TeacherPersonID,TeacherName,Length from Class_Course";
       string strSQL2 = "UPDATE [PKST].[dbo].[classdetail] SET [id_classroom] = @id_classroom ,[startdate] = @startdate,[enddate] = @enddate,[lab1] = @lab1,[lab2] = @lab2,[lab3] = @lab3,[lab4] =@lab4,[lab5] = @lab5 WHERE id_class=@id_class;";
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd2 = new SqlCommand(strSQL2, conn);
        cmd2.CommandType = CommandType.Text;
        cmd2.Parameters.AddWithValue("@id_classroom", id_classroom);
        cmd2.Parameters.AddWithValue("@startdate", startdate);
        cmd2.Parameters.AddWithValue("@enddate", enddate);
        cmd2.Parameters.AddWithValue("@lab1", lab1);
        cmd2.Parameters.AddWithValue("@lab2", lab2);
        cmd2.Parameters.AddWithValue("@lab3", lab3);
        cmd2.Parameters.AddWithValue("@lab4", lab4);
        cmd2.Parameters.AddWithValue("@lab5", lab5);
        cmd2.Parameters.AddWithValue("@id_class", id_class);
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
        
   