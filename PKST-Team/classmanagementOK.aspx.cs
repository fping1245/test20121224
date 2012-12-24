using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using AjaxControlToolkit;

public partial class classmanagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            aaaaa();
            try
            {
                DropDownList1_SelectedIndexChanged(sender, e);
                dorp1();
            }
            catch { };
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void dorp1()
    {
        foreach (GridViewRow theRow in GridView1.Rows)
        {

            ((DropDownList)theRow.FindControl("DropDownList3")).Items.Clear();
            ((DropDownList)theRow.FindControl("DropDownList3")).Items.Add("請選擇");

            if(((Label)theRow.FindControl("Label5")).Text!="")
            {
            ((DropDownList)theRow.FindControl("DropDownList4")).SelectedValue =((Label)theRow.FindControl("Label5")).Text;
            }
            else
            {
               ((DropDownList)theRow.FindControl("DropDownList4")).SelectedValue ="無";
            }
  
    
            string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
            string strCmd = "SELECT DISTINCT [ClassMentor] FROM [TMS].[dbo].[Class] ORDER BY [ClassMentor]";
            string strCmd2 = "SELECT DISTINCT [TeacherName] FROM [TMS].[dbo].[Class_Course] ORDER BY [TeacherName]";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                //using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                //{
                //    conn.Open();
                //    SqlDataReader dr = cmd.ExecuteReader();
                //    while (dr.Read())
                //    {
                //        ((DropDownList)theRow.FindControl("DropDownList3")).Items.Add(dr[0].ToString());
                //    }
                //    conn.Close();
                //}
                using (SqlCommand cmd2 = new SqlCommand(strCmd2, conn))
                {

                    conn.Open();

                    SqlDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read())
                    {
                        string aa = dr[0].ToString();
                        char cr = ' ';
                        string[] s = aa.Split(cr);
                        ((DropDownList)theRow.FindControl("DropDownList3")).Items.Add(s[0]);
                        try
                        {
                            ((DropDownList)theRow.FindControl("DropDownList3")).SelectedValue = ((Label)theRow.FindControl("Label4")).Text;
                        }
                        catch
                        {

                            ((DropDownList)theRow.FindControl("DropDownList3")).SelectedValue = "請選擇";
                        }
                    }
                    conn.Close();
                }
            }


        }
    }
    string startdate;
    public string dropdowndstart(string ClassID)
    {
        string strSQL = "select StartDate from Class where ClassIDNo=@ClassID";
        string strConn = "Data Source=192.168.32.37;Initial Catalog=TMS;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@ClassID", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = ClassID;
        cmd.Parameters.Add(p1);


        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            startdate = Convert.ToDateTime((dr["StartDate"])).ToString("yyyy/MM/dd");
        }

        conn.Close();
        conn.Dispose();
        return startdate;
    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string l1, l2, l3, l4, l5, l6;
        string aa = this.DropDownList1.SelectedValue.ToString();
        List<date1> aaa = caclass(aa);
        this.TextBox1.Text = aaa[0].star;
        this.TextBox2.Text = aaa[0].end;
        l1 = aaa[0].Lab1.ToString();
        l2 = aaa[0].Lab2.ToString();
        l3 = aaa[0].Lab3.ToString();
        l4 = aaa[0].Lab4.ToString();
        l5 = aaa[0].Lab5.ToString();
        l6 = aaa[0].idclassroom.ToString();
        if (l1 == "Checked")
        {
            this.CheckBox1.Checked = true;
        }
        else { this.CheckBox1.Checked = false; }
        if (l2 == "Checked")
        {
            this.CheckBox2.Checked = true;
        }
        else { this.CheckBox2.Checked = false; }
        if (l3 == "Checked")
        {
            this.CheckBox3.Checked = true;
        }
        else { this.CheckBox3.Checked = false; }
        if (l4 == "Checked")
        {
            this.CheckBox4.Checked = true;
        }
        else { this.CheckBox4.Checked = false; }
        if (l5 == "Checked")
        {
            this.CheckBox5.Checked = true;
        }
        else { this.CheckBox5.Checked = false; }
        if (l6 != "")
        {
            this.DropDownList2.SelectedValue = l6;
        }
        else { this.DropDownList2.SelectedValue = "無"; }
        this.Button2.Visible = true;
        this.Button4.Visible = true;
        this.GridView1.DataBind();
        dorp1();

    }
    protected List<date1> caclass(string idclass)
    {
        List<date1> classmanage1 = new List<date1>();
        string name1 = idclass;
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "select StartDate,EndDate,lab1,lab2,lab3,lab4,lab5,id_classroom from classdetail where id_class=@id_class;";
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
                    tea.idclassroom = dr[7].ToString();
                    classmanage1.Add(tea);
                }
                conn.Close();
            }
        }
        return classmanage1;

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }
    protected void yusps_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        //ScriptManager.RegisterStartupScript(UpdatePanel3, this.GetType(), "bind", "init();", true);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string id_classroom = this.DropDownList2.SelectedValue.ToString();
        string startdate = this.TextBox1.Text;
        string enddate = this.TextBox2.Text;
        string lab1;
        if (CheckBox1.Checked)
        {
            lab1 = "Checked";
        }
        else { lab1 = "Unchecked"; }

        string lab2;
        if (CheckBox2.Checked)
        {
            lab2 = "Checked";
        }
        else { lab2 = "Unchecked"; }
        string lab3;
        if (CheckBox3.Checked)
        {
            lab3 = "Checked";
        }
        else { lab3 = "Unchecked"; }
        string lab4;
        if (CheckBox4.Checked)
        {
            lab4 = "Checked";
        }
        else { lab4 = "Unchecked"; }
        string lab5;
        if (CheckBox5.Checked)
        {
            lab5 = "Checked";
        }
        else { lab5 = "Unchecked"; }
        string id_class = this.DropDownList1.SelectedValue.ToString();
        updateclassdetail(id_classroom, startdate, enddate, lab1, lab2, lab3, lab4, lab5, id_class);
        //string js = "$(function(){alert('修改成功')});";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", js, true);
    }
    public void updateclassdetail(string id_classroom, string startdate, string enddate, string lab1, string lab2, string lab3, string lab4, string lab5, string id_class)
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "UPDATE [PKST].[dbo].[classdetail] SET [id_classroom] = @id_classroom ,[startdate] = @startdate,[enddate] = @enddate,[lab1] = @lab1,[lab2] = @lab2,[lab3] = @lab3,[lab4] =@lab4,[lab5] = @lab5 WHERE id_class=@id_class;";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id_classroom", id_classroom);
                cmd.Parameters.AddWithValue("@startdate", startdate);
                cmd.Parameters.AddWithValue("@enddate", enddate);
                cmd.Parameters.AddWithValue("@lab1", lab1);
                cmd.Parameters.AddWithValue("@lab2", lab2);
                cmd.Parameters.AddWithValue("@lab3", lab3);
                cmd.Parameters.AddWithValue("@lab4", lab4);
                cmd.Parameters.AddWithValue("@lab5", lab5);
                cmd.Parameters.AddWithValue("@id_class", id_class);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }



    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            insert1();
        }
        catch { }
        this.GridView1.DataBind();
        aaaaa();
    }
    public List<date12> insert1()
    {
        List<date12> aa = new List<date12>();
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "SELECT [ClassIDNo],[ClassID],[StartDate],[EndDate],[Date],[TimesType],[ClassRoom],[ClassMentor],[UserID],[ClassName],[Length],[Tuition],[TuitionDiscount],[GroupDeptID],[Maintainer] FROM [TMS].[dbo].[Class] ;";//where StartDate between getdate() and dateadd(month,8,getdate())
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                //cmd.Parameters.AddWithValue("@id_class", name1);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    date12 tea = new date12();
                    // tea.id = Convert.ToInt32(dr[0]);
                    // tea.title = dr[1].ToString();
                    tea.id_class = dr["ClassIDNo"].ToString();
                    tea.id_classNO = dr["ClassID"].ToString();
                    tea.classmentor = dr["ClassMentor"].ToString();
                    tea.employeeID = dr["UserID"].ToString();
                    tea.startdate = (Convert.ToDateTime(dr["StartDate"].ToString())).ToString("yyyy/MM/dd");
                    tea.enddate = (Convert.ToDateTime(dr["EndDate"].ToString())).ToString("yyyy/MM/dd");
                    tea.id_classroom = dr["ClassRoom"].ToString();
                    tea.tms_timesType = dr["TimesType"].ToString();
                    tea.date = dr["Date"].ToString();
                    //[GroupDeptID],[Maintainer]
                    tea.classname = dr["ClassName"].ToString();
                    tea.lenght = dr["Length"].ToString();
                    tea.tuition = dr["Tuition"].ToString();
                    tea.tuition_discount = dr["TuitionDiscount"].ToString();
                    tea.groupdeptID = dr["GroupDeptID"].ToString();
                    tea.maintainer = dr["Maintainer"].ToString();
                    insertsp(dr["ClassID"].ToString(), dr["ClassRoom"].ToString(), dr["ClassIDNo"].ToString(), dr["ClassMentor"].ToString());
                    insert2(tea);
                    aa.Add(tea);
                }
                conn.Close();
            }
        }
        return aa;
    }
    public void insertsp(string aa, string aa2, string aa3, string aa4)
    {
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "SELECT [CourseID],[CourseName],[Length],[ClassID] FROM [TMS].[dbo].[ClassCourse] where classid=@classid";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@classid", aa);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    insertsp2(aa3, dr["ClassID"].ToString(), dr["CourseID"].ToString(), dr["CourseName"].ToString(), dr["Length"].ToString(), aa2, aa4);
                }
                conn.Close();
            }
        }
    }

    public void insertsp2(string a1, string a2, string a3, string a4, string a5, string a6, string a7)
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "INSERT INTO [PKST].[dbo].[special_model]([id_class],[id_classNo],[CourseID],[CourseName],[Length],[clsroom],[teachername]) VALUES (@id_class,@id_classNo,@CourseID,@CourseName,@Length,@clsroom,@teachername)";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                //cmd.Parameters.AddWithValue("@id_class", name1);
                cmd.Parameters.AddWithValue("@id_class", a1);
                cmd.Parameters.AddWithValue("@id_classNo", a2);
                cmd.Parameters.AddWithValue("@CourseID", a3);
                cmd.Parameters.AddWithValue("@CourseName", a4);
                cmd.Parameters.AddWithValue("@Length", a5);
                cmd.Parameters.AddWithValue("@clsroom", a6);
                cmd.Parameters.AddWithValue("@teachername", a7);
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


    public void insert2(date12 a1)
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "INSERT INTO [PKST].[dbo].[classdetail]([id_class],[id_classNo],[id_classroom],[startdate],[enddate],[date],[tms_timesType],[classmentor],[employeeID],[classname],[lenght],[tuition],[tuition_discount],[groupdeptID],[maintainer]) VALUES (@id_class,@id_classNo,@id_classroom,@startdate,@enddate,@date,@tms_timesType,@classmentor,@employeeID,@classname,@lenght,@tuition,@tuition_discount,@groupdeptID,@maintainer)";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                //cmd.Parameters.AddWithValue("@id_class", name1);
                cmd.Parameters.AddWithValue("@id_class", a1.id_class);
                cmd.Parameters.AddWithValue("@id_classNo", a1.id_classNO);
                cmd.Parameters.AddWithValue("@id_classroom", a1.id_classroom);
                cmd.Parameters.AddWithValue("@startdate", a1.startdate);
                cmd.Parameters.AddWithValue("@enddate", a1.enddate);
                cmd.Parameters.AddWithValue("@date", a1.date);
                cmd.Parameters.AddWithValue("@tms_timesType", a1.tms_timesType);
                cmd.Parameters.AddWithValue("@classmentor", a1.classmentor);
                cmd.Parameters.AddWithValue("@employeeID", a1.employeeID);
                //[[classname],],[lenght],[tuition],[tuition_discount],[groupdeptID],[maintainer]
                cmd.Parameters.AddWithValue("@classname", a1.classname);
                cmd.Parameters.AddWithValue("@lenght", a1.lenght);
                cmd.Parameters.AddWithValue("@tuition", a1.tuition);
                cmd.Parameters.AddWithValue("@tuition_discount", a1.tuition_discount);
                cmd.Parameters.AddWithValue("@groupdeptID", a1.groupdeptID);
                cmd.Parameters.AddWithValue("@maintainer", a1.maintainer);
                
                
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
    public void aaaaa()
    {
        this.DropDownList1.Items.Clear();
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "SELECT [id_class] FROM [PKST].[dbo].[classdetail] where  endDate between dateadd(month,-6,getdate()) and dateadd(month,6,getdate()) and classmentor='王孝弘'";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.DropDownList1.Items.Add(dr["id_class"].ToString());
                }
                conn.Close();
            }
        }

    }

    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        int a1 = 0;
        int a2 = 0;
        foreach (GridViewRow theRow in GridView1.Rows)
        {
            string clasid = this.DropDownList1.SelectedValue;
            string cousname = ((Label)theRow.FindControl("Label1")).Text;
            String strteacher = ((DropDownList)theRow.FindControl("DropDownList3")).SelectedValue;
            string strleng = ((TextBox)theRow.FindControl("TextBox5")).Text;
            string strcsrm = ((DropDownList)theRow.FindControl("DropDownList4")).SelectedValue;
            //string strteacher=
            bool boldel = ((CheckBox)theRow.FindControl("CheckBox6")).Checked;
            if (boldel)
            {
                a1 = a1 + delect1(cousname, clasid);
            }
            else
            {
                a2 = a2 + updatesp(strteacher, strleng, strcsrm, clasid, cousname);
            }
            //to do 將資料存到另一個function 去做更新動作     
        }
        // DropDownList1_SelectedIndexChanged(sender, e);
        //this.DataBind();
        //string js = "$(function(){alert('" + "共有" + a1.ToString() + "資料被刪除" + "　和　" + a2.ToString() + "資料被修改" + "')});";
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", js, true);
        this.GridView1.DataBind();
        dorp1();

    }

    public int delect1(string aa, string aa2)
    {
        int i = 0;
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "DELETE FROM [PKST].[dbo].[special_model] WHERE CourseName=@CourseName AND id_class=@id_class";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                //cmd.Parameters.AddWithValue("@id_class", name1);
                cmd.Parameters.AddWithValue("@CourseName", aa);
                cmd.Parameters.AddWithValue("@id_class", aa2);
                conn.Open();
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch { }
                conn.Close();
            }
        }
        return i;
    }
    public int updatesp(string a1, string a2, string a3, string a4, string a5)
    {
        int i = 0;

        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "UPDATE [PKST].[dbo].[special_model] SET [TeacherName] = @TeacherName,[Length] = @Length,[clsroom] = @clsroom WHERE id_class=@id_class and CourseName=@CourseName";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@TeacherName", a1);
                cmd.Parameters.AddWithValue("@Length", a2);
                cmd.Parameters.AddWithValue("@clsroom", a3);
                cmd.Parameters.AddWithValue("@id_class", a4);
                cmd.Parameters.AddWithValue("@CourseName", a5);
                i = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        return i;

    }


}
public class date1
{
    public string IDclass { get; set; }
    public string star { get; set; }
    public string end { get; set; }
    public string Lab1 { get; set; }
    public string Lab2 { get; set; }
    public string Lab3 { get; set; }
    public string Lab4 { get; set; }
    public string Lab5 { get; set; }
    public string idclassroom { get; set; }
}
public class date12
{
    public string id_class { get; set; }
    public string id_classNO { get; set; }
    public string classmentor { get; set; }
    public string employeeID { get; set; }
    public string startdate { get; set; }
    public string enddate { get; set; }
    public string id_classroom { get; set; }
    public string tms_timesType { get; set; }
    public string date { get; set; }

    public string classname { get; set; }
    public string lenght { get; set; }
    public string tuition { get; set; }
    public string tuition_discount { get; set; }
    public string groupdeptID { get; set; }
    public string maintainer { get; set; }
}