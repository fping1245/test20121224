<%@ WebHandler Language="C#" Class="getclslist" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;


public class getclslist : IHttpHandler {

    private static readonly System.Web.Script.Serialization.JavaScriptSerializer jsonConvert = new System.Web.Script.Serialization.JavaScriptSerializer();
    
    public void ProcessRequest (HttpContext context) {
        List<gcls> gc = new List<gcls>();
        string id_class=context.Request.Params["id_class"];
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        //string strCmd = "select CourseName from special_model where id_class='" + id_class+"'";
        //string strCmd = "select CourseName,rtrim(TeacherName),Length from special_model where id_class='WB1289'";
        string strCmd = "select CourseName,rtrim(TeacherName),Length,clsroom,sn from special_model where id_class='" + id_class + "'";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                int i = 1;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    gcls gg = new gcls();
                    gg.listno = Convert.ToString(i);
                    gg.coursename = Convert.ToString(dr[0]);
                    gg.teachername = Convert.ToString(dr[1]);
                    gg.courselength = Convert.ToString(dr[2]);
                    gg.clsroom = Convert.ToString(dr[3]);
                    gg.sn = Convert.ToString(dr[4]);
                    gc.Add(gg);
                    i++;
                }
                conn.Close();
            }
        }
        context.Response.ContentType = "application/json";
        context.Response.Write(jsonConvert.Serialize(gc));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public class gcls
    {
        private string m_listno;
        public string listno
        {
            get { return m_listno; }
            set { m_listno = value; }
        }
        
        private string m_coursename;
        public string coursename
        {
            get { return m_coursename; }
            set { m_coursename = value; }
        }
        
        private string m_teachername;
        public string teachername
        {
            get { return m_teachername; }
            set { m_teachername = value; }
        }

        private string m_courselength;
        public string courselength
        {
            get { return m_courselength; }
            set { m_courselength = value; }
        }
        
        private string m_clsroom;
        public string clsroom
        {
            get { return m_clsroom; }
            set { m_clsroom = value; }
        }

        private string m_sn;
        public string sn
        {
            get { return m_sn; }
            set { m_sn = value; }
        }
    }
    

}