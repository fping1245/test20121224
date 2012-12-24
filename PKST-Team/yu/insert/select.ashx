<%@ WebHandler Language="C#" Class="select" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
public class select : IHttpHandler {
    private static readonly JavaScriptSerializer jsonConvert = new JavaScriptSerializer();
    public void ProcessRequest (HttpContext context) {
        List<classmanage> classmanage1 = new List<classmanage>();
        string classidno = context.Request.QueryString["classidno"];
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "select ClassIDNo,startdate,enddate,date,timestype,classname,classroom from [TMS].[dbo].[Class] where classidno=@classidno";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@classidno", classidno);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    classmanage tea = new classmanage();
                    // tea.id = Convert.ToInt32(dr[0]);
                    // tea.title = dr[1].ToString();
                    tea.start = Convert.ToDateTime(dr["startdate"]).ToString("d");
                    tea.end = Convert.ToDateTime(dr["enddate"]).ToString("d");
                    tea.date = dr["date"].ToString();
                    tea.timestype = dr["timestype"].ToString();
                    tea.classroom = dr["classroom"].ToString();
                    classmanage1.Add(tea);
                }
                conn.Close();
            }
        }
           context.Response.ContentType = "text/plain";
           context.Response.Write(jsonConvert.Serialize(classmanage1));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
  public class classmanage
    {        
        public string start { get; set; }
        public string end { get; set; }
        public string date { get; set; }
        public string timestype { get; set; }
        public string classroom { get; set; }
    }        
}