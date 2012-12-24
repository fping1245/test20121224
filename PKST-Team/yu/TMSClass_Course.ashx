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
       //string strSQL2 = "select DISTINCT CourseName,TeacherPersonID,TeacherName,Length from Class_Course";
        string strSQL2 = "select DISTINCT CourseName from Class_Course";
        string strConn = "Data Source=.;Initial Catalog=TMS;User ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd2 = new SqlCommand(strSQL2, conn);
        cmd2.CommandType = CommandType.Text;
        conn.Open();
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            date1 m2 = new date1();
            m2.CourseName = (dr2["CourseName"]).ToString();          
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
        public string CourseName { get; set; }      
    }
} 
