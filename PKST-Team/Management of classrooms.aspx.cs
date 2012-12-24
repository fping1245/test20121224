using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;

public partial class Management_of_classrooms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //  INSERT INTO [PKST].[dbo].[Classroom planning]([date],[id_classroom]) VALUES (@date,@id_classroom)
        if (!IsPostBack)
        {
            if (select1() <= 0)
            {
                insert3();
            }
            string ac = DateTime.Now.ToString("yyyy");
            string ac2 = DateTime.Now.AddYears(1).ToString("yyyy");
            this.DropDownList4.Items.Add(ac);
            this.DropDownList4.Items.Add(ac2);
           string ac1 =DateTime.Now.ToString("MM");
           this.DropDownList3.SelectedValue=ac1;
            this.DropDownList4.SelectedValue=ac;

        }
        if (this.DropDownList1.Items.Count <= 0)
        {
            this.DropDownList1.Items.Clear();
            string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
            string strCmd = "SELECT [id_class] FROM [PKST].[dbo].[Six months of classes];";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    conn.Open();
                    //cmd.Parameters.AddWithValue("@id_class", name1);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        this.DropDownList1.Items.Add(dr[0].ToString());
                    }
                    conn.Close();
                }
            }
        }
    }
    protected void Page_PreLoad(object sender, EventArgs e)
    {
        //this.DropDownList1.Items.Add("123");
        Session["aaa"] = this.DropDownList1.Text;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        insert1();
        Page_PreLoad(sender, e);
    }

    public List<date1> insert1()
    {
        List<date1> aa = new List<date1>();
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "SELECT [ClassIDNo],[StartDate],[EndDate],[Date],[TimesType],[ClassRoom],[ClassMentor],[UserID] FROM [TMS].[dbo].[Class] where StartDate between getdate() and dateadd(month,8,getdate());";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                //cmd.Parameters.AddWithValue("@id_class", name1);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    date1 tea = new date1();
                    // tea.id = Convert.ToInt32(dr[0]);
                    // tea.title = dr[1].ToString();
                    tea.id_class = dr["ClassIDNo"].ToString();
                    tea.classmentor = dr["ClassMentor"].ToString();
                    tea.id_user = dr["UserID"].ToString();
                    tea.startdate = (Convert.ToDateTime(dr["StartDate"].ToString())).ToString("yyyy/MM/dd");
                    tea.enddate = (Convert.ToDateTime(dr["EndDate"].ToString())).ToString("yyyy/MM/dd");
                    tea.id_classroom = dr["ClassRoom"].ToString();
                    tea.tms_timesType = dr["TimesType"].ToString();
                    tea.date = dr["Date"].ToString();
                    insert2(tea);
                    aa.Add(tea);
                }
                conn.Close();
            }
        }
        return aa;
    }
    public void insert2(date1 a1)
    {
        string strConn = "Data Source=.;Initial Catalog=TMS;user ID=sa";
        string strCmd = "INSERT INTO [PKST].[dbo].[Six months of classes]([id_class],[classmentor],[id_user] ,[startdate] ,[enddate] ,[id_classroom]  ,[tms_timesType],[date]) VALUES (@id_class,@classmentor,@id_user,@startdate,@enddate,@id_classroom,@tms_timesType,@date);";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {

                //cmd.Parameters.AddWithValue("@id_class", name1);
                cmd.Parameters.AddWithValue("@id_class", a1.id_class);
                cmd.Parameters.AddWithValue("@classmentor", a1.classmentor);
                cmd.Parameters.AddWithValue("@id_user", a1.id_user);
                cmd.Parameters.AddWithValue("@startdate", a1.startdate);
                cmd.Parameters.AddWithValue("@enddate", a1.enddate);
                cmd.Parameters.AddWithValue("@id_classroom", a1.id_classroom);
                cmd.Parameters.AddWithValue("@tms_timesType", a1.tms_timesType);
                cmd.Parameters.AddWithValue("@date", a1.date);

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
    }
    public class date1
    {
        public string id_class { get; set; }
        public string classmentor { get; set; }
        public string id_user { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string id_classroom { get; set; }
        public string tms_timesType { get; set; }
        public string date { get; set; }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.DropDownList1.SelectedValue = Session["aaa"].ToString();
        }
        catch { }

        List<date1> aa = select1(this.DropDownList1.Text);
        this.TextBox1.Text = (Convert.ToDateTime(aa[0].startdate.ToString())).ToString("yyyy/MM/dd");
        this.TextBox2.Text = (Convert.ToDateTime(aa[0].enddate.ToString())).ToString("yyyy/MM/dd");
        string aaa = aa[0].date.ToString();
        char cr = '、';
        string[] s = aaa.Split(cr);
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        CheckBox3.Checked = false;
        CheckBox4.Checked = false;
        CheckBox5.Checked = false;
        CheckBox6.Checked = false;
        CheckBox7.Checked = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i].ToString() == "一")
            {
                this.CheckBox1.Checked = !this.CheckBox1.Checked;
            }
            if (s[i].ToString() == "二")
            {
                this.CheckBox2.Checked = !this.CheckBox2.Checked;
            }
            if (s[i].ToString() == "三")
            {
                this.CheckBox3.Checked = !this.CheckBox3.Checked;
            }
            if (s[i].ToString() == "四")
            {
                this.CheckBox4.Checked = !this.CheckBox4.Checked;
            }
            if (s[i].ToString() == "五")
            {
                this.CheckBox5.Checked = !this.CheckBox5.Checked;
            }
            if (s[i].ToString() == "六")
            {
                this.CheckBox6.Checked = !this.CheckBox6.Checked;
            }
            if (s[i].ToString() == "日")
            {
                this.CheckBox7.Checked = !this.CheckBox7.Checked;
            }
        }
        string timestype1 = aa[0].tms_timesType.ToString();
        if (timestype1 == "白天")
        {
            this.RadioButtonList1.SelectedIndex = 0;
        }
        if (timestype1 == "夜間")
        {
            this.RadioButtonList1.SelectedIndex = 1;
        }
        string clasrom = aa[0].id_classroom.ToString();
        if (clasrom != "")
        {
            this.DropDownList2.SelectedValue = clasrom;
        }
        else
        {
            this.DropDownList2.SelectedValue = "無";
        }
    }


    public List<date1> select1(string idclassno)
    {
        List<date1> aa = new List<date1>();
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "SELECT [id_class],[classmentor],[id_user],[startdate] ,[enddate],[id_classroom],[tms_timesType],[date] FROM [PKST].[dbo].[Six months of classes] where id_class=@id_class ;";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id_class", idclassno);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    date1 tea = new date1();
                    // tea.id = Convert.ToInt32(dr[0]);
                    // tea.title = dr[1].ToString();
                    tea.id_class = dr[0].ToString();
                    tea.classmentor = dr[1].ToString();
                    tea.id_user = dr[2].ToString();
                    tea.startdate = dr[3].ToString();
                    tea.enddate = dr[4].ToString();
                    tea.id_classroom = dr[5].ToString();
                    tea.tms_timesType = dr[6].ToString();
                    tea.date = dr[7].ToString();
                    aa.Add(tea);
                }
                conn.Close();
            }
        }
        return aa;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
      
        try
        {
            this.DropDownList1.SelectedValue = Session["aaa"].ToString();
        }
        catch { }
        date2 d2 = new date2();
        d2.id_classroom = this.DropDownList2.Text;
        d2.id_class = Session["aaa"].ToString();
        d2.startdate = this.TextBox1.Text;
        string aa = d2.startdate.ToString();
        char cr = '/';
        string[] s = aa.Split(cr);
        DateTime date1 = new DateTime(Convert.ToInt32(s[0].ToString()), Convert.ToInt32(s[1].ToString()), Convert.ToInt32(s[2].ToString()));
        d2.enddate = this.TextBox2.Text;
        string aa2 = d2.enddate.ToString();
        string[] s1 = aa2.Split(cr);
        DateTime date2 = new DateTime(Convert.ToInt32(s1[0].ToString()), Convert.ToInt32(s1[1].ToString()), Convert.ToInt32(s1[2].ToString()));
        int sss = new TimeSpan(date2.Ticks - date1.Ticks).Days;
        d2.tms_timesType = this.RadioButtonList1.SelectedValue;

        if (this.CheckBox1.Checked)
        {
            d2.m1 = "Monday";
        }
        if (this.CheckBox2.Checked)
        {
            d2.m2 = "Tuesday";
        }
        if (this.CheckBox3.Checked)
        {
            d2.m3 = "Wednesday";
        }
        if (this.CheckBox4.Checked)
        {
            d2.m4 = "Thursday";
        }
        if (this.CheckBox5.Checked)
        {
            d2.m5 = "Friday";
        }
        if (this.CheckBox6.Checked)
        {
            d2.m6 = "Saturday";
        }
        if (this.CheckBox7.Checked)
        {
            d2.m7 = "Sunday";
        }
        for (int i = 0; i <= sss; i++)
        {
            string str = System.DateTime.Parse(d2.startdate).AddDays(i).ToString("yyyy-MM-dd");
   
            DateTime datetime1 = Convert.ToDateTime(d2.startdate).AddDays(i);
            string datestring = datetime1.ToString("yyyy/MM/dd");
            string chdateweek;
            chdateweek = datetime1.DayOfWeek.ToString();
            if (chdateweek == d2.m1 || chdateweek == d2.m2 || chdateweek == d2.m3 || chdateweek == d2.m4 || chdateweek == d2.m5 || chdateweek == d2.m6 || chdateweek == d2.m7)
            {
                if (d2.tms_timesType == "白天")
                {
                    Update1(d2.id_class, d2.id_class, "", datestring, d2.id_classroom);
                }
                else
                {
                    Update1(datestring, d2.id_class, d2.id_classroom);
                }
            }
        }
    }
    public class date2
    {
        public string id_class { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string id_classroom { get; set; }
        public string tms_timesType { get; set; }
        public string m1 { get; set; }
        public string m2 { get; set; }
        public string m3 { get; set; }
        public string m4 { get; set; }
        public string m5 { get; set; }
        public string m6 { get; set; }
        public string m7 { get; set; }
    }
    public void Update1(string aa1, string aa2, string aa3, string aa4, string aa5)
    {
        string strCmd2 = "UPDATE [PKST].[dbo].[Classroom planning] SET [mon] = @mon,[aft] = @aft,[nig] = @nig WHERE date=@date and id_classroom=@id_classroom";
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "INSERT INTO [PKST].[dbo].[classrom201]([date],[mon],[aft],[nig],[id_class])VALUES (@date,@mon,@aft,@nig,@id_class);";
        //string strCmd2 = "select datediff(day,@sterdate,@enddate)";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@date", aa4);
                cmd.Parameters.AddWithValue("@mon", aa1);
                cmd.Parameters.AddWithValue("@aft", aa2);
                cmd.Parameters.AddWithValue("@nig", aa3);
                cmd.Parameters.AddWithValue("@id_classroom", aa5);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    public void Update1(string aa1, string aa2, string aa3)
    {
        string strCmd2 = "UPDATE [PKST].[dbo].[Classroom planning] SET [nig] = @nig WHERE date=@date and id_classroom=@id_classroom";
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "INSERT INTO [PKST].[dbo].[classrom201]([date],[nig],[id_class])VALUES (@date,@nig,@id_class);";
        //string strCmd2 = "select datediff(day,@sterdate,@enddate)";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@date", aa1);
                cmd.Parameters.AddWithValue("@nig", aa2);
                cmd.Parameters.AddWithValue("@id_classroom", aa3);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    public int select1()
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "SELECT [date] FROM [PKST].[dbo].[Classroom planning] where getdate()>dateadd(month,6,getdate());";
        int ii = 0;
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                conn.Open();
                //cmd.Parameters.AddWithValue("@id_class", name1);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ii++;
                }
                conn.Close();
            }
        }
        return ii;
    }

    public void insert3()
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd2 = "SELECT [id_classroom] FROM [PKST].[dbo].[classroom];";
        ArrayList aa = new ArrayList();
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd2 = new SqlCommand(strCmd2, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    aa.Add(dr[0].ToString());
                }
                conn.Close();
                for (int i2 = 0; i2 < aa.Count - 1; i2++)
                {
                    string iii = aa[i2].ToString();
                    for (int i = 0; i <= 365; i++)
                    {
                        string date1 = DateTime.Now.AddDays(i).ToString("yyyy/MM/dd");
                        string date2 = DateTime.Now.AddDays(i).ToString("MM");
                        string date3 = DateTime.Now.AddDays(i).ToString("yyyy");
                        insert4(date1, iii,date2,date3);
                    }
                }
            }
        }
    }
    public void insert4(string a1, string a2,string a3,string a4)
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        string strCmd = "INSERT INTO [PKST].[dbo].[Classroom planning]([date],[id_classroom],[month],[years]) VALUES (@date,@id_classroom,@month,@years);";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@date", a1);
                cmd.Parameters.AddWithValue("@id_classroom", a2);
                cmd.Parameters.AddWithValue("@month", a3);
                cmd.Parameters.AddWithValue("@years", a4);
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


    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlDataSource3.SelectCommand = "select * from classroom";
        //GridView13.DataSource = SqlDataSource3;
        //   GridView13.DataBind();
        
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
       // SqlDataSource1.SelectCommand = "";
    }
}