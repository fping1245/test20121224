<%@ WebHandler Language="C#" Class="saveclsroomdutylength" %>

using System;
using System.Web;
using System.Data.SqlClient;

public class saveclsroomdutylength : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string id_class = context.Request.QueryString["id_class"];
        string dateid = context.Request.QueryString["dateid"];
        string clsroom = context.Request.QueryString["clsroom"];
        string duty_length = context.Request.QueryString["duty_length"];
        string[] sp = dateid.Split('!');
        string sep_dateid_date = sp[0];
        string sep_dateid_duty = sp[1];

        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "UPDATE [PKST].[dbo].[timetable] set [clsroom]=@clsroom, [duty_length]=@duty_length where [date]=@sep_dateid_date and [duty]=@sep_dateid_duty and [id_class]=@id_class";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id_class", id_class);
                cmd.Parameters.AddWithValue("@sep_dateid_date", sep_dateid_date);
                cmd.Parameters.AddWithValue("@sep_dateid_duty", sep_dateid_duty);
                cmd.Parameters.AddWithValue("@clsroom", clsroom);
                cmd.Parameters.AddWithValue("@duty_length", duty_length);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        context.Response.ContentType = "text/plain";
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}