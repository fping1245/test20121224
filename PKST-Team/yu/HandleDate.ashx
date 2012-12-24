<%@ WebHandler Language="C#" Class="select1change" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
public class select1change : IHttpHandler {
    private static readonly JavaScriptSerializer jsonConvert = new JavaScriptSerializer();
    public void ProcessRequest (HttpContext context) {
        List<date1> classmanage1 = new List<date1>();        
        string name1 = context.Request.QueryString["name"];
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "select StartDate,EndDate,lab1,lab2,lab3,lab4,lab5 from classdetail where id_class=@id_class;";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id_class", name1);    
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    date1 tea = new date1();
                   // tea.id = Convert.ToInt32(dr[0]);
                   // tea.title = dr[1].ToString();
                    tea.star = (Convert.ToDateTime(dr[0])).ToString("yyyy/MM/dd");
                    tea.end = (Convert.ToDateTime(dr[1])).ToString("yyyy/MM/dd");
                    tea.Lab1 = dr[2].ToString();
                    tea.Lab2 = dr[3].ToString();
                    tea.Lab3 = dr[4].ToString();
                    tea.Lab4 = dr[5].ToString();
                    tea.Lab5 = dr[6].ToString();
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
    public class date1
    {
       // public int id { get; set; }
        //public string title { get; set; }
        //public string name { get; set; }
        public string star { get; set; }
        public string end { get; set; }
        public string Lab1 { get; set; }
        public string Lab2 { get; set; }
        public string Lab3 { get; set; }
        public string Lab4 { get; set; }
        public string Lab5 { get; set; }
    }
}