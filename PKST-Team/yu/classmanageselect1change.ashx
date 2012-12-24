<%@ WebHandler Language="C#" Class="select1change" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
public class select1change : IHttpHandler {
    private static readonly JavaScriptSerializer jsonConvert = new JavaScriptSerializer();
    public void ProcessRequest (HttpContext context) {
        List<classmanage> classmanage1 = new List<classmanage>();        
        string name1 = context.Request.QueryString["name"];
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "select ClassIDNo from Class where ClassMentor=@name and EndDate >dateadd(month,-6,getdate())";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();            
                cmd.Parameters.AddWithValue("@name",name1);    
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    classmanage tea = new classmanage();
                   // tea.id = Convert.ToInt32(dr[0]);
                   // tea.title = dr[1].ToString();
                    tea.name = dr[0].ToString();
                    classmanage1.Add(tea);
                }
                conn.Close();
            }
        }
        context.Response.ContentType = "application/json";
        context.Response.Write(jsonConvert.Serialize(classmanage1));
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    public class classmanage
    {
       // public int id { get; set; }
        //public string title { get; set; }
        public string name { get; set; }
    }
}