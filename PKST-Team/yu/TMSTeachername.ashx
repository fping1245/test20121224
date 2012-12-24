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
        string coursename=context.Request.QueryString["cosname"];
       //string strSQL2 = "select DISTINCT CourseName,TeacherPersonID,TeacherName,Length from Class_Course";
        string strSQL2 = "select TeacherName,Length from Class_Course where CourseName=@CourseName";
        string strConn = "Data Source=.;Initial Catalog=TMS;User ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd2 = new SqlCommand(strSQL2, conn);
        cmd2.CommandType = CommandType.Text;
        cmd2.Parameters.AddWithValue("@CourseName", coursename);
        conn.Open();
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            date1 m2 = new date1();
            m2.TeacherName = (dr2["TeacherName"]).ToString();
            m2.Length = (dr2["Length"]).ToString();                   
            aa.Add(m2);
        }
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
        public string TeacherName { get; set; }
        public string Length { get; set; }      
    }
} 
