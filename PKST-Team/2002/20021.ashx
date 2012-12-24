<%@ WebHandler Language="C#" Class="_20021" %>
//---------------------------------------------------------------------------- 
//程式功能	檔案上傳下載 (以資料庫存放檔案) > 檔案下載
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;

public class _20021 : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string mErr = "";
        int fc_sid = -1;

        if (context.Request["sid"] != null)
        {
            if (int.TryParse(context.Request["sid"].Trim(), out fc_sid))
            {
                // 處理下載檔案
                using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
                {
                    Sql_conn.Open();

                    using (SqlCommand Sql_Command = new SqlCommand())
                    {
                        string SqlString = "";

                        SqlString = "Select Top 1 fc_name, fc_type, fc_content From Fi_Content Where fc_sid = @fc_sid";

                        Sql_Command.Connection = Sql_conn;
                        Sql_Command.CommandText = SqlString;
                        Sql_Command.Parameters.AddWithValue("fc_sid", fc_sid);

                        SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

                        if (Sql_Reader.Read())
                        {
                            byte[] fc_content = (byte[])Sql_Reader["fc_content"];

                            context.Response.Clear();
                            context.Response.Charset = "utf-8";

                            // 檔名要先編碼，中文檔名才不會有問題
                            context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(Sql_Reader["fc_name"].ToString().Trim()));
                            context.Response.ContentType = Sql_Reader["fc_type"].ToString();
                            context.Response.BinaryWrite(fc_content);
                            context.Response.End();
                        }
                        else
                            mErr = "找不到這個檔案的資料!\\n";
                    }
                }
            }
            else
                mErr = "參數傳送有問題!\\n";
        }
        else
            mErr = "參數傳送有問題!\\n";

        if (mErr != "")
        {
            context.Response.ContentType = "text/html";
            context.Response.Write(mErr);
            context.Response.End();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}