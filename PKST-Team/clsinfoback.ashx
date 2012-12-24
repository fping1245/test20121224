<%@ WebHandler Language="C#" Class="clsinfoback" %>
using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

public class clsinfoback : IHttpHandler {

    private static readonly System.Web.Script.Serialization.JavaScriptSerializer jsonConvert = new System.Web.Script.Serialization.JavaScriptSerializer();
    
    public void ProcessRequest (HttpContext context) {
        List<clasbak> clabaklist = new List<clasbak>();
        string id_class = context.Request.QueryString["id_class"];
        string dateid = context.Request.QueryString["dateid"];
        string[] sp = dateid.Split('!');
        string sep_dateid_date = sp[0];
        string sep_dateid_duty = sp[1];
        
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select clsroom,duty_length from timetable where [date]=@sep_dateid_date and [duty]=@sep_dateid_duty and [id_class]=@id_class";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id_class", id_class);
                cmd.Parameters.AddWithValue("@sep_dateid_date", sep_dateid_date);
                cmd.Parameters.AddWithValue("@sep_dateid_duty", sep_dateid_duty);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clasbak haha = new clasbak();
                    haha.clsroom = dr[0].ToString();
                    haha.duty_length = dr[1].ToString();
                    clabaklist.Add(haha);
                }
                conn.Close();
            }
        }

        context.Response.ContentType = "application/json";
        context.Response.Write(jsonConvert.Serialize(clabaklist));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public class clasbak
    {
        private string m_clsroom;
        public string clsroom
        {
            get { return m_clsroom; }
            set { m_clsroom = value; }
        }

        private string m_duty_length;
        public string duty_length
        {
            get { return m_duty_length; }
            set { m_duty_length = value; }
        }
        
    }
}