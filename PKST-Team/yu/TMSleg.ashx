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
        string cousname = context.Request.QueryString["cousname"];
        string TeacherName = context.Request.QueryString["TeacherName"];
       //string strSQL2 = "select DISTINCT CourseName,TeacherPersonID,TeacherName,Length from Class_Course";
        string strSQL2 = "select TeacherName,Length,UserID from Class_Course where CourseName=@CourseName and TeacherName=@TeacherName";
        string strSQL = "SELECT [EmployeeID] FROM [TMS].[dbo].[Tteacher] where [name]=@name";
        string strConn = "Data Source=.;Initial Catalog=TMS;User ID=sa";
        date1 m2 = new date1();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd2 = new SqlCommand(strSQL2, conn);
        cmd2.CommandType = CommandType.Text;
        cmd2.Parameters.AddWithValue("@CourseName", cousname);
        //cmd2.Parameters.AddWithValue("@CourseName", "C# 程式設計");
        cmd2.Parameters.AddWithValue("@TeacherName", TeacherName);
        //cmd2.Parameters.AddWithValue("@TeacherName", "王孝弘");
        conn.Open();
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            m2.Length = (dr2["Length"]).ToString();
         
        }
        conn.Close();

        SqlCommand cmd3 = new SqlCommand(strSQL, conn);
        cmd3.CommandType = CommandType.Text;
        cmd3.Parameters.AddWithValue("@name", TeacherName);
        //cmd3.Parameters.AddWithValue("@name", "王孝弘");
        conn.Open();
        SqlDataReader dr3 = cmd3.ExecuteReader();
        while (dr3.Read())
        {
            m2.iduser = (dr3[0]).ToString();
            aa.Add(m2);
        }
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
        public string Length { get; set; }
        public string iduser { get; set; }
    }
} 
