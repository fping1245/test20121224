using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;

public partial class hour_keyin_new : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime datetoday = System.DateTime.Today;
        int my_year = Convert.ToInt32(datetoday.Year);
        List<string> list_my_year = new List<string>();
        for (int i = my_year + 1; i >= my_year - 5; i--)
        {
            this.DropDownList3.Items.Add(i.ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.DropDownList1.SelectedValue

        string strConn2 = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd2 = "INSERT INTO [PKST].[dbo].[hour_project_teacherlist]([TeacherName])VALUES(@TeacherName)";
        using (SqlConnection conn = new SqlConnection(strConn2))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
            {
                cmd.Parameters.AddWithValue("@TeacherName", this.DropDownList1.SelectedValue);


                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
                conn.Close();

            }
        }
        this.ListBox1.DataBind();
        this.DropDownList2.DataBind();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strConn2 = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd2 = "DELETE FROM [PKST].[dbo].[hour_project_teacherlist] WHERE TeacherName=@TeacherName";
        using (SqlConnection conn = new SqlConnection(strConn2))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
            {
                cmd.Parameters.AddWithValue("@TeacherName", this.DropDownList2.SelectedValue);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        this.ListBox1.DataBind();
        this.DropDownList2.DataBind();

    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void Button3_Click(object sender, EventArgs e)
    {
        # region INSERT
        for (int i = 0; i < this.ListBox1.Items.Count; i++)
        {
            string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
            string strCmd = "INSERT INTO hour_project([TeacherName],[year],[month]) VALUES (@TeacherName,@year,@month)";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherName", this.ListBox1.Items[i].ToString());
                    cmd.Parameters.AddWithValue("@year", this.DropDownList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@month", this.DropDownList4.SelectedValue);
                    
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch { }
                    conn.Close();
                }
            }

        }
        # endregion

        for (int i = 0; i < this.ListBox1.Items.Count; i++)
        {
            string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
            string strCmd = "INSERT INTO hour_project([TeacherName],[year],[month]) VALUES (@TeacherName,@year,@month)";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherName", this.ListBox1.Items[i].ToString());
                    cmd.Parameters.AddWithValue("@year", this.DropDownList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@month", this.DropDownList4.SelectedValue);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch { }
                    conn.Close();
                }
            }

        }
        this.GridView1.DataBind();
    }
   
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (this.DropDownList4.SelectedValue == this.ListBox1.SelectedValue) { }
        //else
        //{
        //    string strConn = "Data Source=.;Initial Catalog=TMS;User ID=sa";
        //    string strCmd = "INSERT INTO [PKST].[dbo].[out_teacher]([TeacherName],[year],[month],[hours],[id_class])VALUES(@TeacherName,@year,@month,@hours,@id_class)";
        //    using (SqlConnection conn = new SqlConnection(strConn))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(strCmd, conn))
        //        {
        //            try
        //            {
        //                cmd.Parameters.AddWithValue("@user_name", ((mytms)(ar[i])).TeacherName);
        //                cmd.Parameters.AddWithValue("@out_in", this.DropDownList4.SelectedValue);
        //                cmd.Parameters.AddWithValue("@CourseID", ((mytms)(ar[i])).CourseID);
        //                cmd.Parameters.AddWithValue("@CourseName", ((mytms)(ar[i])).CourseName);
        //                cmd.Parameters.AddWithValue("@Length", ((mytms)(ar[i])).Length);
        //                conn.Open();
        //                cmd.ExecuteNonQuery();
        //                conn.Close();
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //}

    }
}

