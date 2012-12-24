<%@ WebHandler Language="C#" Class="select1change" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
public class select1change : IHttpHandler {
    private static readonly JavaScriptSerializer jsonConvert = new JavaScriptSerializer();
    public void ProcessRequest (HttpContext context) {
        List<useclassselect1> useclass1 = new List<useclassselect1>();        
        //string name1 = context.Request.QueryString["name"];
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "select ClassIDNo from Class where EndDate >dateadd(month,-6,getdate())";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    useclassselect1 tea = new useclassselect1();
                   // tea.id = Convert.ToInt32(dr[0]);
                   // tea.title = dr[1].ToString();
                    tea.classidno = dr[0].ToString();
                    useclass1.Add(tea);
                }
                conn.Close();
            }
        }
        context.Response.ContentType = "application/json";
        context.Response.Write(jsonConvert.Serialize(useclass1));
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    public class useclassselect1
    {
       // public int id { get; set; }
        //public string title { get; set; }
        public string classidno { get; set; }
    }
}