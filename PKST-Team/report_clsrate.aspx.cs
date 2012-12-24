using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;

public partial class report_clsrate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime datetoday = System.DateTime.Today;
            int my_year = Convert.ToInt32(datetoday.Year);
            List<string> list_my_year = new List<string>();
            this.DropDownList1.Items.Clear();
            for (int i = my_year + 1; i >= my_year - 5; i--)
            {
                this.DropDownList1.Items.Add(i.ToString());
            }
            this.DropDownList1.SelectedValue = datetoday.Year.ToString();

            # region Dropdownlist2,Dropdownlist3
            switch (datetoday.Month.ToString())
            {
                case "1":
                    this.DropDownList2.SelectedValue = "Q1";
                    this.DropDownList3.SelectedValue = "1";
                    break;
                case "2":
                    this.DropDownList2.SelectedValue = "Q1";
                    this.DropDownList3.SelectedValue = "2";
                    break;
                case "3":
                    this.DropDownList2.SelectedValue = "Q1";
                    this.DropDownList3.SelectedValue = "3";
                    break;
                case "4":
                    this.DropDownList2.SelectedValue = "Q2";
                    this.DropDownList3.SelectedValue = "4";
                    break;
                case "5":
                    this.DropDownList2.SelectedValue = "Q2";
                    this.DropDownList3.SelectedValue = "5";
                    break;
                case "6":
                    this.DropDownList2.SelectedValue = "Q2";
                    this.DropDownList3.SelectedValue = "6";
                    break;
                case "7":
                    this.DropDownList2.SelectedValue = "Q3";
                    this.DropDownList3.SelectedValue = "7";
                    break;
                case "8":
                    this.DropDownList2.SelectedValue = "Q3";
                    this.DropDownList3.SelectedValue = "8";
                    break;
                case "9":
                    this.DropDownList2.SelectedValue = "Q3";
                    this.DropDownList3.SelectedValue = "9";
                    break;
                case "10":
                    this.DropDownList2.SelectedValue = "Q4";
                    this.DropDownList3.SelectedValue = "10";
                    break;
                case "11":
                    this.DropDownList2.SelectedValue = "Q4";
                    this.DropDownList3.SelectedValue = "11";
                    break;
                case "12":
                    this.DropDownList2.SelectedValue = "Q4";
                    this.DropDownList3.SelectedValue = "12";
                    break;
                default:
                    break;
            }
            #endregion
            Button3_Click(sender, e);
            this.Label1.Text = this.DropDownList1.Text;
            this.Label2.Text = this.DropDownList2.Text;
            this.Label3.Text = this.DropDownList3.Text;
            this.Label4.Text = "月報表";
        }
        this.Label1.Text = this.DropDownList1.Text;
        this.Label2.Text = this.DropDownList2.Text;
        this.Label3.Text = this.DropDownList3.Text; 
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList3.Items.Clear();
        switch (this.DropDownList2.SelectedValue)
        {
            case "Q1":
                this.DropDownList3.Items.Add("1");
                this.DropDownList3.Items.Add("2");
                this.DropDownList3.Items.Add("3");
                break;
            case "Q2":
                this.DropDownList3.Items.Add("4");
                this.DropDownList3.Items.Add("5");
                this.DropDownList3.Items.Add("6");
                break;
            case "Q3":
                this.DropDownList3.Items.Add("7");
                this.DropDownList3.Items.Add("8");
                this.DropDownList3.Items.Add("9");
                break;
            case "Q4":
                this.DropDownList3.Items.Add("10");
                this.DropDownList3.Items.Add("11");
                this.DropDownList3.Items.Add("12");
                break;

        }
    }

    public class clsroomrate
    {
        public string clsroom { get; set; }
        public string weak_duty { get; set; }      //1~5日(0);1~5夜(1);休日(2)
        public string used_times { get; set; }
        public string can_times { get; set; }
        public string clsuserate { get; set; }
    }
    public class forgridused
    {
        public string 教室 { get; set; }
        public string 時段 { get; set; }
        public string 使用時段次數 { get; set; }
        public string 可使用時段次數 { get; set; }
        public string 使用率 { get; set; }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        # region  塞入教室到Dropdownlist
        List<string> clsroomlist = new List<string>();
        clsroomlist.Add("201");
        clsroomlist.Add("202");
        clsroomlist.Add("203");
        clsroomlist.Add("204");
        clsroomlist.Add("205");
        clsroomlist.Add("206");
        clsroomlist.Add("301");
        clsroomlist.Add("302");
        clsroomlist.Add("303");
        clsroomlist.Add("304");
        clsroomlist.Add("305");
        clsroomlist.Add("306");
        clsroomlist.Add("307");
        clsroomlist.Add("308");
        clsroomlist.Add("309");
        clsroomlist.Add("310");
        clsroomlist.Add("311");
        # endregion

        # region get六日天數,一到五天數
        int Sun_and_saturday_count = 0;  //六日天數
        int mon_to_friday_count = 0;     //一到五天數
        DateTime dt_start = new DateTime();
        dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-01-01");
        DateTime dt = dt_start;
        while (dt <= dt_start.AddYears(1).AddDays(-1))
        {
            string day_of_week = dt.DayOfWeek.ToString();
            switch (day_of_week)
            {
                case "Saturday":
                    Sun_and_saturday_count++;
                    break;
                case "Sunday":
                    Sun_and_saturday_count++;
                    break;
                default:
                    mon_to_friday_count++;
                    break;
            }
            dt = dt.AddDays(1);
        }
        //Response.Write(Sun_and_saturday_count);
        //Response.Write(mon_to_friday_count);
        # endregion

        # region get 年曆資料
        int holiday_count = 0;
        int no_workday_count = 0;
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select data,workday from yearcalendar where data between @dt_start and @dt_end";

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@dt_start", dt_start);
                cmd.Parameters.AddWithValue("@dt_end", dt_start.AddYears(1).AddDays(-1));
                conn.Open();
                clsroomrate clsr = new clsroomrate();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    switch (Convert.ToDateTime(dr["data"]).DayOfWeek.ToString())
                    {
                        case "Saturday":
                            if (dr["workday"].ToString() == "True")
                            {
                                holiday_count++;
                            }
                            else
                            { }
                            break;
                        case "Sunday":
                            if (dr["workday"].ToString() == "True")
                            {
                                holiday_count++;
                            }
                            else
                            { }
                            break;
                        default:
                            if (dr["workday"].ToString() == "False")
                            {
                                no_workday_count++;
                            }
                            else
                            { }
                            break;
                    }
                }
                conn.Close();
            }
        }
        # endregion

        int total_year_holiday = Sun_and_saturday_count + no_workday_count - holiday_count;//年假日天
        int total_year_workday = mon_to_friday_count - no_workday_count + holiday_count;    //年工作天
        int total_year_67 = Sun_and_saturday_count - holiday_count;//六日非國定假天數

        # region 1~5日
        string my_year = this.DropDownList1.SelectedValue + "/%";
        string strCmd2 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=1 or [duty]=2) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";

        ArrayList al = new ArrayList();
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "0";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_workday * 2).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        # region 1~5夜
        ArrayList al2 = new ArrayList();
        string strCmd3 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=3) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd3, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "1";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_workday * 1).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al2.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        # region 休日
        ArrayList al3 = new ArrayList();
        string strCmd4 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=1 or [duty]=2) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd4, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "1";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_67 * 2).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al3.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        ArrayList total_sum = new ArrayList();
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            forgridused aa1 = new forgridused();
            aa1.教室 = ((clsroomrate)(al[i])).clsroom;
            aa1.時段 = "1~5日";
            aa1.使用時段次數 = ((clsroomrate)(al[i])).used_times;
            aa1.可使用時段次數 = ((clsroomrate)(al[i])).can_times;
            aa1.使用率 = ((clsroomrate)(al[i])).clsuserate;
            total_sum.Add(aa1);

            forgridused aa2 = new forgridused();
            aa2.教室 = "";
            aa2.時段 = "1~5夜";
            aa2.使用時段次數 = ((clsroomrate)(al2[i])).used_times;
            aa2.可使用時段次數 = ((clsroomrate)(al2[i])).can_times;
            aa2.使用率 = ((clsroomrate)(al2[i])).clsuserate;
            total_sum.Add(aa2);

            forgridused aa3 = new forgridused();
            aa3.教室 = "";
            aa3.時段 = "休日";
            aa3.使用時段次數 = ((clsroomrate)(al3[i])).used_times;
            aa3.可使用時段次數 = ((clsroomrate)(al3[i])).can_times;
            aa3.使用率 = ((clsroomrate)(al3[i])).clsuserate;
            total_sum.Add(aa3);
        }
        this.GridView1.DataSource = total_sum;
        this.GridView1.DataBind();

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial1", " var gg1 = new Array(" + ((forgridused)total_sum[0]).使用率 + ", " + ((forgridused)total_sum[3]).使用率 + ", " + ((forgridused)total_sum[6]).使用率 + ", " + ((forgridused)total_sum[9]).使用率 + ", " + ((forgridused)total_sum[12]).使用率 + ", " + ((forgridused)total_sum[15]).使用率 + ", " + ((forgridused)total_sum[18]).使用率 + ", " + ((forgridused)total_sum[21]).使用率 + ", " + ((forgridused)total_sum[24]).使用率 + ", " + ((forgridused)total_sum[27]).使用率 + ", " + ((forgridused)total_sum[30]).使用率 + ", " + ((forgridused)total_sum[33]).使用率 + ", " + ((forgridused)total_sum[35]).使用率 + ", " + ((forgridused)total_sum[38]).使用率 + ", " + ((forgridused)total_sum[41]).使用率 + ", " + ((forgridused)total_sum[44]).使用率 + ", " + ((forgridused)total_sum[47]).使用率 + ");", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial2", " var gg2 = new Array(" + ((forgridused)total_sum[1]).使用率 + ", " + ((forgridused)total_sum[4]).使用率 + ", " + ((forgridused)total_sum[7]).使用率 + ", " + ((forgridused)total_sum[10]).使用率 + ", " + ((forgridused)total_sum[13]).使用率 + ", " + ((forgridused)total_sum[16]).使用率 + ", " + ((forgridused)total_sum[19]).使用率 + ", " + ((forgridused)total_sum[22]).使用率 + ", " + ((forgridused)total_sum[25]).使用率 + ", " + ((forgridused)total_sum[28]).使用率 + ", " + ((forgridused)total_sum[31]).使用率 + ", " + ((forgridused)total_sum[34]).使用率 + ", " + ((forgridused)total_sum[36]).使用率 + ", " + ((forgridused)total_sum[39]).使用率 + ", " + ((forgridused)total_sum[42]).使用率 + ", " + ((forgridused)total_sum[45]).使用率 + ", " + ((forgridused)total_sum[48]).使用率 + ");", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial3", " var gg3 = new Array(" + ((forgridused)total_sum[2]).使用率 + ", " + ((forgridused)total_sum[5]).使用率 + ", " + ((forgridused)total_sum[8]).使用率 + ", " + ((forgridused)total_sum[11]).使用率 + ", " + ((forgridused)total_sum[14]).使用率 + ", " + ((forgridused)total_sum[17]).使用率 + ", " + ((forgridused)total_sum[20]).使用率 + ", " + ((forgridused)total_sum[23]).使用率 + ", " + ((forgridused)total_sum[26]).使用率 + ", " + ((forgridused)total_sum[29]).使用率 + ", " + ((forgridused)total_sum[32]).使用率 + ", " + ((forgridused)total_sum[35]).使用率 + ", " + ((forgridused)total_sum[37]).使用率 + ", " + ((forgridused)total_sum[40]).使用率 + ", " + ((forgridused)total_sum[43]).使用率 + ", " + ((forgridused)total_sum[46]).使用率 + ", " + ((forgridused)total_sum[49]).使用率 + ");", true);
        this.Label4.Text = "年報表";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        # region  塞入教室到Dropdownlist
        List<string> clsroomlist = new List<string>();
        clsroomlist.Add("201");
        clsroomlist.Add("202");
        clsroomlist.Add("203");
        clsroomlist.Add("204");
        clsroomlist.Add("205");
        clsroomlist.Add("206");
        clsroomlist.Add("301");
        clsroomlist.Add("302");
        clsroomlist.Add("303");
        clsroomlist.Add("304");
        clsroomlist.Add("305");
        clsroomlist.Add("306");
        clsroomlist.Add("307");
        clsroomlist.Add("308");
        clsroomlist.Add("309");
        clsroomlist.Add("310");
        clsroomlist.Add("311");
        # endregion

        # region get六日天數,一到五天數
        int Sun_and_saturday_count = 0;  //六日天數
        int mon_to_friday_count = 0;     //一到五天數
        DateTime dt_start = new DateTime();
        switch (this.DropDownList2.SelectedValue)
        {
            case "Q1":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-01-01");
                break;
            case "Q2":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-04-01");
                break;
            case "Q3":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-07-01");
                break;
            case "Q4":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-10-01");
                break;
            default:
                break;
        }
        DateTime dt = dt_start;
        while (dt <= dt_start.AddMonths(3).AddDays(-1))
        {
            string day_of_week = dt.DayOfWeek.ToString();
            switch (day_of_week)
            {
                case "Saturday":
                    Sun_and_saturday_count++;
                    break;
                case "Sunday":
                    Sun_and_saturday_count++;
                    break;
                default:
                    mon_to_friday_count++;
                    break;
            }
            dt = dt.AddDays(1);
        }
        # endregion

        # region get 年曆資料
        int holiday_count = 0;
        int no_workday_count = 0;
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select data,workday from yearcalendar where data between @dt_start and @dt_end";

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@dt_start", dt_start);
                cmd.Parameters.AddWithValue("@dt_end", dt_start.AddYears(3).AddDays(-1));
                conn.Open();
                clsroomrate clsr = new clsroomrate();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    switch (Convert.ToDateTime(dr["data"]).DayOfWeek.ToString())
                    {
                        case "Saturday":
                            if (dr["workday"].ToString() == "True")
                            {
                                holiday_count++;
                            }
                            else
                            { }
                            break;
                        case "Sunday":
                            if (dr["workday"].ToString() == "True")
                            {
                                holiday_count++;
                            }
                            else
                            { }
                            break;
                        default:
                            if (dr["workday"].ToString() == "False")
                            {
                                no_workday_count++;
                            }
                            else
                            { }
                            break;
                    }
                }
                conn.Close();
            }
        }
        # endregion

        int total_year_holiday = Sun_and_saturday_count + no_workday_count - holiday_count; //季假日天
        int total_year_workday = mon_to_friday_count - no_workday_count + holiday_count;    //季工作天
        int total_year_67 = Sun_and_saturday_count - holiday_count;//(季)六日非國定假天數

        # region 1~5日
        string my_year = this.DropDownList1.SelectedValue + "/%";
        string strCmd2 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=1 or [duty]=2) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";

        ArrayList al = new ArrayList();
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "0";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_workday * 2).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        # region 1~5夜
        ArrayList al2 = new ArrayList();
        string strCmd3 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=3) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd3, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "1";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_workday * 1).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al2.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        # region 休日
        ArrayList al3 = new ArrayList();
        string strCmd4 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=1 or [duty]=2) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd4, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "1";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_67 * 2).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al3.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        ArrayList total_sum = new ArrayList();
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            forgridused aa1 = new forgridused();
            aa1.教室 = ((clsroomrate)(al[i])).clsroom;
            aa1.時段 = "1~5日";
            aa1.使用時段次數 = ((clsroomrate)(al[i])).used_times;
            aa1.可使用時段次數 = ((clsroomrate)(al[i])).can_times;
            aa1.使用率 = ((clsroomrate)(al[i])).clsuserate;
            total_sum.Add(aa1);

            forgridused aa2 = new forgridused();
            aa2.教室 = "";
            aa2.時段 = "1~5夜";
            aa2.使用時段次數 = ((clsroomrate)(al2[i])).used_times;
            aa2.可使用時段次數 = ((clsroomrate)(al2[i])).can_times;
            aa2.使用率 = ((clsroomrate)(al2[i])).clsuserate;
            total_sum.Add(aa2);

            forgridused aa3 = new forgridused();
            aa3.教室 = "";
            aa3.時段 = "休日";
            aa3.使用時段次數 = ((clsroomrate)(al3[i])).used_times;
            aa3.可使用時段次數 = ((clsroomrate)(al3[i])).can_times;
            aa3.使用率 = ((clsroomrate)(al3[i])).clsuserate;
            total_sum.Add(aa3);
        }
        this.GridView1.DataSource = total_sum;
        this.GridView1.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial1", " var gg1 = new Array(" + ((forgridused)total_sum[0]).使用率 + ", " + ((forgridused)total_sum[3]).使用率 + ", " + ((forgridused)total_sum[6]).使用率 + ", " + ((forgridused)total_sum[9]).使用率 + ", " + ((forgridused)total_sum[12]).使用率 + ", " + ((forgridused)total_sum[15]).使用率 + ", " + ((forgridused)total_sum[18]).使用率 + ", " + ((forgridused)total_sum[21]).使用率 + ", " + ((forgridused)total_sum[24]).使用率 + ", " + ((forgridused)total_sum[27]).使用率 + ", " + ((forgridused)total_sum[30]).使用率 + ", " + ((forgridused)total_sum[33]).使用率 + ", " + ((forgridused)total_sum[35]).使用率 + ", " + ((forgridused)total_sum[38]).使用率 + ", " + ((forgridused)total_sum[41]).使用率 + ", " + ((forgridused)total_sum[44]).使用率 + ", " + ((forgridused)total_sum[47]).使用率 + ");", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial2", " var gg2 = new Array(" + ((forgridused)total_sum[1]).使用率 + ", " + ((forgridused)total_sum[4]).使用率 + ", " + ((forgridused)total_sum[7]).使用率 + ", " + ((forgridused)total_sum[10]).使用率 + ", " + ((forgridused)total_sum[13]).使用率 + ", " + ((forgridused)total_sum[16]).使用率 + ", " + ((forgridused)total_sum[19]).使用率 + ", " + ((forgridused)total_sum[22]).使用率 + ", " + ((forgridused)total_sum[25]).使用率 + ", " + ((forgridused)total_sum[28]).使用率 + ", " + ((forgridused)total_sum[31]).使用率 + ", " + ((forgridused)total_sum[34]).使用率 + ", " + ((forgridused)total_sum[36]).使用率 + ", " + ((forgridused)total_sum[39]).使用率 + ", " + ((forgridused)total_sum[42]).使用率 + ", " + ((forgridused)total_sum[45]).使用率 + ", " + ((forgridused)total_sum[48]).使用率 + ");", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial3", " var gg3 = new Array(" + ((forgridused)total_sum[2]).使用率 + ", " + ((forgridused)total_sum[5]).使用率 + ", " + ((forgridused)total_sum[8]).使用率 + ", " + ((forgridused)total_sum[11]).使用率 + ", " + ((forgridused)total_sum[14]).使用率 + ", " + ((forgridused)total_sum[17]).使用率 + ", " + ((forgridused)total_sum[20]).使用率 + ", " + ((forgridused)total_sum[23]).使用率 + ", " + ((forgridused)total_sum[26]).使用率 + ", " + ((forgridused)total_sum[29]).使用率 + ", " + ((forgridused)total_sum[32]).使用率 + ", " + ((forgridused)total_sum[35]).使用率 + ", " + ((forgridused)total_sum[37]).使用率 + ", " + ((forgridused)total_sum[40]).使用率 + ", " + ((forgridused)total_sum[43]).使用率 + ", " + ((forgridused)total_sum[46]).使用率 + ", " + ((forgridused)total_sum[49]).使用率 + ");", true);
        this.Label4.Text = "季報表";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        # region  塞入教室到Dropdownlist
        List<string> clsroomlist = new List<string>();
        clsroomlist.Add("201");
        clsroomlist.Add("202");
        clsroomlist.Add("203");
        clsroomlist.Add("204");
        clsroomlist.Add("205");
        clsroomlist.Add("206");
        clsroomlist.Add("301");
        clsroomlist.Add("302");
        clsroomlist.Add("303");
        clsroomlist.Add("304");
        clsroomlist.Add("305");
        clsroomlist.Add("306");
        clsroomlist.Add("307");
        clsroomlist.Add("308");
        clsroomlist.Add("309");
        clsroomlist.Add("310");
        clsroomlist.Add("311");
        # endregion

        # region get六日天數,一到五天數
        int Sun_and_saturday_count = 0;  //六日天數
        int mon_to_friday_count = 0;     //一到五天數
        DateTime dt_start = new DateTime();
        switch (this.DropDownList3.SelectedValue)
        {
            case "1":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-01-01");
                break;
            case "2":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-02-01");
                break;
            case "3":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-03-01");
                break;
            case "4":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-04-01");
                break;
            case "5":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-05-01");
                break;
            case "6":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-06-01");
                break;
            case "7":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-07-01");
                break;
            case "8":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-08-01");
                break;
            case "9":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-09-01");
                break;
            case "10":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-10-01");
                break;
            case "11":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-11-01");
                break;
            case "12":
                dt_start = Convert.ToDateTime(this.DropDownList1.SelectedValue + "-12-01");
                break;

            default:
                break;
        }
        DateTime dt = dt_start;
        while (dt <= dt_start.AddMonths(1).AddDays(-1))
        {
            string day_of_week = dt.DayOfWeek.ToString();
            switch (day_of_week)
            {
                case "Saturday":
                    Sun_and_saturday_count++;
                    break;
                case "Sunday":
                    Sun_and_saturday_count++;
                    break;
                default:
                    mon_to_friday_count++;
                    break;
            }
            dt = dt.AddDays(1);
        }
        # endregion

        # region get 年曆資料
        int holiday_count = 0;
        int no_workday_count = 0;
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select data,workday from yearcalendar where data between @dt_start and @dt_end";

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@dt_start", dt_start);
                cmd.Parameters.AddWithValue("@dt_end", dt_start.AddYears(1).AddDays(-1));
                conn.Open();
                clsroomrate clsr = new clsroomrate();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    switch (Convert.ToDateTime(dr["data"]).DayOfWeek.ToString())
                    {
                        case "Saturday":
                            if (dr["workday"].ToString() == "True")
                            {
                                holiday_count++;
                            }
                            else
                            { }
                            break;
                        case "Sunday":
                            if (dr["workday"].ToString() == "True")
                            {
                                holiday_count++;
                            }
                            else
                            { }
                            break;
                        default:
                            if (dr["workday"].ToString() == "False")
                            {
                                no_workday_count++;
                            }
                            else
                            { }
                            break;
                    }
                }
                conn.Close();
            }
        }
        # endregion

        int total_year_holiday = Sun_and_saturday_count + no_workday_count - holiday_count; //月假日天
        int total_year_workday = mon_to_friday_count - no_workday_count + holiday_count;    //月工作天
        int total_year_67 = Sun_and_saturday_count - holiday_count;//(月)六日非國定假天數

        # region 1~5日
        string my_year = this.DropDownList1.SelectedValue + "/%";
        string strCmd2 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=1 or [duty]=2) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";

        ArrayList al = new ArrayList();
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd2, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "0";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_workday * 2).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        # region 1~5夜
        ArrayList al2 = new ArrayList();
        string strCmd3 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=3) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd3, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "1";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_workday * 1).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al2.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        # region 休日
        ArrayList al3 = new ArrayList();
        string strCmd4 = "SELECT count(clsroom) as year_c FROM timetable where datewithduty like @datewithduty and clsroom =@clsroom and ([duty]=1 or [duty]=2) and (DATEPART ( dw , date )=2 or DATEPART ( dw , date )=3 or DATEPART ( dw , date )=4 or DATEPART ( dw , date )=5 or DATEPART ( dw , date )=6)";
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd4, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@clsroom", clsroomlist[i]);
                    cmd.Parameters.AddWithValue("@datewithduty", my_year);

                    clsroomrate clsr = new clsroomrate();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        clsr.clsroom = clsroomlist[i].ToString();
                        clsr.weak_duty = "1";
                        clsr.used_times = dr["year_c"].ToString();
                        clsr.can_times = (total_year_67 * 2).ToString();
                        clsr.clsuserate = Convert.ToString(100 * Convert.ToInt32(clsr.used_times) / Convert.ToInt32(clsr.can_times));
                        al3.Add(clsr);
                    }
                    conn.Close();
                }
            }
        }
        # endregion

        ArrayList total_sum = new ArrayList();
        for (int i = 0; i < clsroomlist.Count; i++)
        {
            forgridused aa1 = new forgridused();
            aa1.教室 = ((clsroomrate)(al[i])).clsroom;
            aa1.時段 = "1~5日";
            aa1.使用時段次數 = ((clsroomrate)(al[i])).used_times;
            aa1.可使用時段次數 = ((clsroomrate)(al[i])).can_times;
            aa1.使用率 = ((clsroomrate)(al[i])).clsuserate;
            total_sum.Add(aa1);

            forgridused aa2 = new forgridused();
            aa2.教室 = "";
            aa2.時段 = "1~5夜";
            aa2.使用時段次數 = ((clsroomrate)(al2[i])).used_times;
            aa2.可使用時段次數 = ((clsroomrate)(al2[i])).can_times;
            aa2.使用率 = ((clsroomrate)(al2[i])).clsuserate;
            total_sum.Add(aa2);

            forgridused aa3 = new forgridused();
            aa3.教室 = "";
            aa3.時段 = "休日";
            aa3.使用時段次數 = ((clsroomrate)(al3[i])).used_times;
            aa3.可使用時段次數 = ((clsroomrate)(al3[i])).can_times;
            aa3.使用率 = ((clsroomrate)(al3[i])).clsuserate;
            total_sum.Add(aa3);
        }
        this.GridView1.DataSource = total_sum;
        this.GridView1.DataBind();
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial1", " var gg1 = new Array(" + ((forgridused)total_sum[0]).使用率 + ", " + ((forgridused)total_sum[3]).使用率 + ", " + ((forgridused)total_sum[6]).使用率 + ", " + ((forgridused)total_sum[9]).使用率 + ", " + ((forgridused)total_sum[12]).使用率 + ", " + ((forgridused)total_sum[15]).使用率 + ", " + ((forgridused)total_sum[18]).使用率 + ", " + ((forgridused)total_sum[21]).使用率 + ", " + ((forgridused)total_sum[24]).使用率 + ", " + ((forgridused)total_sum[27]).使用率 + ", " + ((forgridused)total_sum[30]).使用率 + ", " + ((forgridused)total_sum[33]).使用率 + ", " + ((forgridused)total_sum[35]).使用率 + ", " + ((forgridused)total_sum[38]).使用率 + ", " + ((forgridused)total_sum[41]).使用率 + ", " + ((forgridused)total_sum[44]).使用率 + ", " + ((forgridused)total_sum[47]).使用率 + ");", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial2", " var gg2 = new Array(" + ((forgridused)total_sum[1]).使用率 + ", " + ((forgridused)total_sum[4]).使用率 + ", " + ((forgridused)total_sum[7]).使用率 + ", " + ((forgridused)total_sum[10]).使用率 + ", " + ((forgridused)total_sum[13]).使用率 + ", " + ((forgridused)total_sum[16]).使用率 + ", " + ((forgridused)total_sum[19]).使用率 + ", " + ((forgridused)total_sum[22]).使用率 + ", " + ((forgridused)total_sum[25]).使用率 + ", " + ((forgridused)total_sum[28]).使用率 + ", " + ((forgridused)total_sum[31]).使用率 + ", " + ((forgridused)total_sum[34]).使用率 + ", " + ((forgridused)total_sum[36]).使用率 + ", " + ((forgridused)total_sum[39]).使用率 + ", " + ((forgridused)total_sum[42]).使用率 + ", " + ((forgridused)total_sum[45]).使用率 + ", " + ((forgridused)total_sum[48]).使用率 + ");", true);
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "initial3", " var gg3 = new Array(" + ((forgridused)total_sum[2]).使用率 + ", " + ((forgridused)total_sum[5]).使用率 + ", " + ((forgridused)total_sum[8]).使用率 + ", " + ((forgridused)total_sum[11]).使用率 + ", " + ((forgridused)total_sum[14]).使用率 + ", " + ((forgridused)total_sum[17]).使用率 + ", " + ((forgridused)total_sum[20]).使用率 + ", " + ((forgridused)total_sum[23]).使用率 + ", " + ((forgridused)total_sum[26]).使用率 + ", " + ((forgridused)total_sum[29]).使用率 + ", " + ((forgridused)total_sum[32]).使用率 + ", " + ((forgridused)total_sum[35]).使用率 + ", " + ((forgridused)total_sum[37]).使用率 + ", " + ((forgridused)total_sum[40]).使用率 + ", " + ((forgridused)total_sum[43]).使用率 + ", " + ((forgridused)total_sum[46]).使用率 + ", " + ((forgridused)total_sum[49]).使用率 + ");", true);
        this.Label4.Text = "月報表";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=myexcel.xls");//xls
        Response.ContentType = "application/ms-excel";
        string HTML = GetControlsHTML(this.GridView1);

        // strip out hyperlink code (<a href....) 
        System.Text.RegularExpressions.Regex Stripper =
            new System.Text.RegularExpressions.Regex(@"<a[\s]+[^>]*?href[\s]?=[\s\""\']*(.*?)[\""\']*.*?>");
        HTML = Stripper.Replace(HTML, "");
        Stripper = new System.Text.RegularExpressions.Regex(@"&lt;/a>");
        HTML = Stripper.Replace(HTML, "");

        Response.Write(HTML);
        Response.End();
    }
    public string GetControlsHTML(Control c)
    {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        c.RenderControl(hw);
        return sw.ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=mydoc.doc");//xls
        Response.ContentType = "application/ms-excel";
        string HTML = GetControlsHTML(this.GridView1);

        // strip out hyperlink code (<a href....) 
        System.Text.RegularExpressions.Regex Stripper =
            new System.Text.RegularExpressions.Regex(@"<a[\s]+[^>]*?href[\s]?=[\s\""\']*(.*?)[\""\']*.*?>");
        HTML = Stripper.Replace(HTML, "");
        Stripper = new System.Text.RegularExpressions.Regex(@"&lt;/a>");
        HTML = Stripper.Replace(HTML, "");

        Response.Write(HTML);
        Response.End();
    }
}