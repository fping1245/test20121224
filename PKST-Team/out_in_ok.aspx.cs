using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Text;
using System.IO;

public partial class out_in_ok : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
    }
    
    ArrayList ar = new ArrayList();
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strConn = "Data Source=.;Initial Catalog=TMS;User ID=sa";
        string strCmd = "select TeacherName,CourseID,CourseName,Length from Class_Course where TeacherName=@TeacherName";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {

                cmd.Parameters.AddWithValue("@TeacherName", this.DropDownList1.SelectedValue);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    mytms mm = new mytms();
                    mm.TeacherName = dr["TeacherName"].ToString();
                    mm.CourseID = dr["CourseID"].ToString();
                    mm.CourseName = dr["CourseName"].ToString();
                    mm.Length = dr["Length"].ToString();
                    ar.Add(mm);
                }
                conn.Close();
            }

        }

        int my_count = ar.Count;
        for (int i = 0; i < my_count; i++)
        {
            string strConn2 = "Data Source=.;Initial Catalog=TMS;User ID=sa";
            string strCmd2 = "INSERT INTO [PKST].[dbo].[out_teacher]([user_name],[CourseID],[CourseName],[Length],[out_in])VALUES(@user_name,@CourseID,@CourseName,@Length,@out_in)";
            using (SqlConnection conn = new SqlConnection(strConn2))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@user_name", ((mytms)(ar[i])).TeacherName);
                        cmd.Parameters.AddWithValue("@out_in", this.DropDownList4.SelectedValue);
                        cmd.Parameters.AddWithValue("@CourseID", ((mytms)(ar[i])).CourseID);
                        cmd.Parameters.AddWithValue("@CourseName", ((mytms)(ar[i])).CourseName);
                        cmd.Parameters.AddWithValue("@Length", ((mytms)(ar[i])).Length);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
    

    public class mytms
    {
        public string TeacherName { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string Length { get; set; }

    }



   
    
       
}