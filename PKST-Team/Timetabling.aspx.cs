using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

public partial class _2 : System.Web.UI.Page
{
    int i = 1;
    bool day_flag;
    bool insert_flag;
    bool insert_schefrom_db;
    bool do_flag = true;
    string lab1, lab2, lab3, lab4, lab5; // for Lab時間判斷
    DateTime startdate, endate; // 課程起訖日


    protected void Page_Load(object sender, EventArgs e)
    {
        //if (IsPostBack)
        //{
            //{
            //    string strJS = "alert('儲存檔案?')";
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", strJS, true);
            //}
            if (Request.QueryString["classmemory"] != null)
            {
                try
                {
                    getclslablist(Convert.ToString(Request.QueryString["classmemory"]));
                    lab1 = clslablist[0].ToString();
                    lab2 = clslablist[1].ToString();
                    lab3 = clslablist[2].ToString();
                    lab4 = clslablist[3].ToString();
                    lab5 = clslablist[4].ToString();
                }
                catch
                {
                    lab1 = "Unchecked";
                    lab2 = "Unchecked";
                    lab3 = "Unchecked";
                    lab4 = "Unchecked";
                    lab5 = "Unchecked";
                }

                string choosedcls = Request.QueryString["classmemory"];

                ArrayList registerclsdatelist = new ArrayList();
                registerclsdatelist = getregisterclsdate((choosedcls));

                startdate = Convert.ToDateTime(registerclsdatelist[0]);
                Response.Write(startdate);

                endate = Convert.ToDateTime(registerclsdatelist[1]);
                Response.Write(endate);

                insert_flag = haveregisterinsql(Request.QueryString["classmemory"]);
                if (!insert_flag)
                {
                    genclearsch();
                    insert_flag = true;
                }

                comparelist();
            }
        //}
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        string choosedcls = Request.QueryString["classmemory"];
        //讓DropDownList1停在上頁所選
        this.DropDownList1.SelectedValue = choosedcls;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Timetabling.aspx?classmemory=" + this.DropDownList1.SelectedValue.ToString());
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        day_flag = true;
        # region 不顯示本月以外日期
        if (e.Day.IsOtherMonth)
        {
            e.Cell.Controls.Clear();
            day_flag = false;
        }
        # endregion
        
        if (day_flag)
        {
            string mymy_year = e.Day.Date.Year.ToString();
            string mymy_month = e.Day.Date.Month.ToString();
            string mymy_day=e.Day.Date.Day.ToString();
            DateTime d1 = Convert.ToDateTime(e.Day.Date.Year.ToString() + "-" + e.Day.Date.Month.ToString() + "-1");
            DateTime d2 = d1.AddMonths(1).AddDays(-1);

            if (do_flag)
            {
                insert_schefrom_db = havescheindb(Request.QueryString["classmemory"], d1, d2);
            }

            //第一次做havescheindb判斷後，如果判斷出本班本月沒資料，就不再做判斷了
            if (!insert_schefrom_db)
            {
                do_flag = false;
            }

            if (insert_schefrom_db)
            {
                string my_date = e.Day.Date.ToString("yyyy-MM-dd");
                day_flag = true;
                string label1 = "";
                string label2 = "";
                string label3 = "";
                # region 判斷非上課周期內標上' '
                if (day_flag)
                {
                    if (e.Day.Date > endate || e.Day.Date < startdate)
                    {
                        insertText(e, my_date, System.Drawing.Color.Black, true);
                        day_flag = false;
                        i++;
                    }
                }
                # endregion

                # region insertSche由資料庫叫課程代號和'H''L'到課表裡

                if (day_flag)
                {
                    for(int j=1;j<=3;j++)
                    {                       
                        string my_date_withduty = Convert.ToString(mymy_year + "/" + mymy_month + "/" + mymy_day + "!" + j);                      
                        switch(j)
                        {
                            case 1:
                            label1=calltimetable(Request.QueryString["classmemory"].ToString(), my_date_withduty);
                                break;
                            case 2:
                           label2=calltimetable(Request.QueryString["classmemory"].ToString(), my_date_withduty);
                               break;
                            case 3:
                               label3 = calltimetable(Request.QueryString["classmemory"].ToString(), my_date_withduty);
                               break;
                            default:
                                break;
                        }
                    }
                    insertSche(e, my_date,label1,label2,label3);
                    day_flag = false;
                    i++;
                }
                # endregion
            }
            else
            {
            string my_date = e.Day.Date.ToString("yyyy-MM-dd");
            string my_year = e.Day.Date.Year.ToString();
            getholidylist(my_year + "-01-01");
            getsshiftworkdaylist(my_year + "-01-01");

            # region 判斷非上課周期內標上' '
            if (day_flag)
            {
                if (e.Day.Date > endate || e.Day.Date < startdate)
                {
                    insertText(e, my_date, System.Drawing.Color.Black, true);
                    day_flag = false;
                    i++;
                }
            }
            # endregion

            # region 判斷六日補上班給日曆天標上' '
            if (day_flag)
            {
                if (e.Day.Date.DayOfWeek.ToString() == "Sunday" || e.Day.Date.DayOfWeek.ToString() == "Saturday")
                {
                    for (i = 0; i < shiftworkdaylist.Count; i++)
                    {
                        if (my_date == Convert.ToDateTime(shiftworkdaylist[i]).ToString("yyyy-MM-dd"))
                        {
                            insertText(e, my_date, System.Drawing.Color.White, false);
                            day_flag = false;
                            i++;
                            break;
                        }
                    }
                }
            }
            # endregion

            # region 判斷六日給日曆天標上'H'
            if (day_flag)
            {
                if (e.Day.Date.DayOfWeek.ToString() == "Sunday" || e.Day.Date.DayOfWeek.ToString() == "Saturday")
                {
                    insertH(e, my_date);
                    day_flag = false;
                    i++;
                }
            }
            # endregion

            # region 判斷假日給日曆天標上'H'
            if (day_flag)
            {
                for (i = 0; i < holidylist.Count; i++)
                {
                    if (my_date == Convert.ToDateTime(holidylist[i]).ToString("yyyy-MM-dd"))
                    {
                        insertH(e, my_date);
                        day_flag = false;
                        i++;
                        break;
                    }
                }
            }
            # endregion

            # region 判斷Lab天標上'L'
            if (day_flag)
            {
                switch (e.Day.Date.DayOfWeek.ToString())
                {
                    case "Monday":
                        if (lab1 == "Checked")
                        {
                            insertL(e, my_date);
                            day_flag = false;
                            i++;
                        }
                        break;
                    case "Tuesday":
                        if (lab2 == "Checked")
                        {
                            insertL(e, my_date);
                            day_flag = false;
                            i++;
                        }
                        break;
                    case "Wednesday":
                        if (lab3 == "Checked")
                        {
                            insertL(e, my_date);
                            day_flag = false;
                            i++;
                        }
                        break;
                    case "Thursday":
                        if (lab4 == "Checked")
                        {
                            insertL(e, my_date);
                            day_flag = false;
                            i++;
                        }
                        break;
                    case "Friday":
                        if (lab5 == "Checked")
                        {
                            insertL(e, my_date);
                            day_flag = false;
                            i++;
                        }
                        break;
                    default:
                        break;
                }
            }
            # endregion

            # region 非國定假日及Lab時間，日曆天不標記

            if (day_flag)
            {
                insertText(e, my_date, System.Drawing.Color.White, false);
                day_flag = false;
                i++;
            }
            # endregion
            }
        }
    }


    // 標'資料'方法  (已有資料由資料庫叫出)
    public void insertSche(DayRenderEventArgs e, string my_date, string tb1txt,string tb2txt,string tb3txt)
    {
        Label lbl = new Label();
        lbl.Text = "<br/>";
        e.Cell.Controls.Add(lbl);

        TextBox tb1 = new TextBox();
        tb1.ID = my_date + "!1";
        tb1.Width = 20;
        tb1.Text = tb1txt;
        e.Cell.Controls.Add(tb1);

        Label lbl2 = new Label();
        lbl2.Text = "<br/>";
        e.Cell.Controls.Add(lbl2);

        TextBox tb2 = new TextBox();
        tb2.ID = my_date + "!2";
        tb2.Width = 20;
        tb2.Text = tb2txt;
        e.Cell.Controls.Add(tb2);

        Label lbl3 = new Label();
        lbl3.Text = "<br/>";
        e.Cell.Controls.Add(lbl3);

        TextBox tb3 = new TextBox();
        tb3.ID = my_date + "!3";
        tb3.Width = 20;
        tb3.Text = tb3txt;
        e.Cell.Controls.Add(tb3);
    }
    // 標'L'方法
    public void insertL(DayRenderEventArgs e, string my_date)
    {
        Label lbl = new Label();
        lbl.Text = "<br/>";
        e.Cell.Controls.Add(lbl);

        TextBox tb1 = new TextBox();
        tb1.ID = my_date + "!1";
        tb1.Width = 20;
        tb1.Text = "";
        e.Cell.Controls.Add(tb1);

        Label lbl2 = new Label();
        lbl2.Text = "<br/>";
        e.Cell.Controls.Add(lbl2);

        TextBox tb2 = new TextBox();
        tb2.ID = my_date + "!2";
        tb2.Width = 20;
        tb2.Text = "";
        e.Cell.Controls.Add(tb2);

        Label lbl3 = new Label();
        lbl3.Text = "<br/>";
        e.Cell.Controls.Add(lbl3);

        TextBox tb3 = new TextBox();
        tb3.ID = my_date + "!3";
        tb3.Width = 20;
        tb3.Text = "L";
        e.Cell.Controls.Add(tb3);
        savetext("L", tb3.ID);
    }
    // 標'H'方法
    public void insertH(DayRenderEventArgs e, string my_date)
    {
        Label lbl = new Label();
        lbl.Text = "<br/>";
        e.Cell.Controls.Add(lbl);

        TextBox tb1 = new TextBox();
        tb1.ID = my_date + "!1";
        tb1.Width = 20;
        tb1.Text = "H";
        e.Cell.Controls.Add(tb1);
        savetext("H", tb1.ID);

        Label lbl2 = new Label();
        lbl2.Text = "<br/>";
        e.Cell.Controls.Add(lbl2);

        TextBox tb2 = new TextBox();
        tb2.ID = my_date + "!2";
        tb2.Width = 20;
        tb2.Text = "H";
        e.Cell.Controls.Add(tb2);
        savetext("H", tb2.ID);

        Label lbl3 = new Label();
        lbl3.Text = "<br/>";
        e.Cell.Controls.Add(lbl3);

        TextBox tb3 = new TextBox();
        tb3.ID = my_date + "!3";
        tb3.Width = 20;
        tb3.Text = "H";
        e.Cell.Controls.Add(tb3);
        savetext("H", tb3.ID);

    }
    // 標' '方法
    public void insertText(DayRenderEventArgs e, string my_date, System.Drawing.Color clr, bool rwflag)
    {
        Label lbl = new Label();
        lbl.Text = "<br/>";
        e.Cell.Controls.Add(lbl);

        TextBox tb1 = new TextBox();
        tb1.ID = my_date + "!1";
        tb1.Width = 20;
        tb1.BackColor = clr;
        tb1.ReadOnly = rwflag;
        e.Cell.Controls.Add(tb1);

        Label lbl2 = new Label();
        lbl2.Text = "<br/>";
        e.Cell.Controls.Add(lbl2);

        TextBox tb2 = new TextBox();
        tb2.ID = my_date + "!2";
        tb2.Width = 20;
        tb2.BackColor = clr;
        tb2.ReadOnly = rwflag;
        e.Cell.Controls.Add(tb2);

        Label lbl3 = new Label();
        lbl3.Text = "<br/>";
        e.Cell.Controls.Add(lbl3);

        TextBox tb3 = new TextBox();
        tb3.ID = my_date + "!3";
        tb3.Width = 20;
        tb3.BackColor = clr;
        tb3.ReadOnly = rwflag;
        e.Cell.Controls.Add(tb3);
    }
    
    ArrayList arr = new ArrayList();
    //建立對應課程號碼矩陣
    public void comparelist()
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        //string strCmd = "select CourseName from special_model where id_class='" + id_class+"'";
        //string strCmd = "select CourseName,rtrim(TeacherName),Length from special_model where id_class='WB1289'";
        string strCmd = "select CourseName from special_model where id_class=@id_class";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@id_class", Convert.ToString(Request.QueryString["classmemory"]));
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                arr.Add("buffer");
                while (dr.Read())
                {
                    arr.Add(dr[0].ToString());
                }
                conn.Close();
            }
        }
    }
    
    //由timetable中叫出CourseName,TeacherName的方法
    public string calltimetable(string id_class, string my_date_withduty)
    {
        string labelgg = "";
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select CourseName,TeacherName from timetable where id_class=@id_class and datewithduty=@my_date_withduty order by duty";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@id_class", id_class);
                cmd.Parameters.AddWithValue("@my_date_withduty", my_date_withduty);
                conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        //Response.Write(arr.IndexOf(dr["CourseName"]).ToString());
                        
                        if(dr["CourseName"].ToString()=="H")
                        {
                            labelgg = "H";
                        }
                        else if (dr["CourseName"].ToString() == "L")
                        {
                            labelgg = "L";
                        }
                        else if (arr.IndexOf(dr["CourseName"]).ToString()=="-1")
                        {
                            labelgg = "";
                        }
                        else
                        {
                            labelgg = arr.IndexOf(dr["CourseName"]).ToString();
                        }
                        //Response.Write(labelgg);       
                    }
                conn.Close();
            }
        }
        return labelgg;
    }

    // 抓取本年度假日方法
    ArrayList holidylist = new ArrayList();
    public ArrayList getholidylist(string my_year)
    {
        string strSQL = "select data from yearcalendar where data > @my_year_start and workday='false'";
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@my_year_start", SqlDbType.Date);
        p1.Direction = ParameterDirection.Input;
        p1.Value = my_year;
        cmd.Parameters.Add(p1);

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            holidylist.Add(dr["data"]);
        }
        conn.Close();
        conn.Dispose();
        return holidylist;
    }
    // 抓取本年度補上班日方法
    ArrayList shiftworkdaylist = new ArrayList();
    public ArrayList getsshiftworkdaylist(string my_year)
    {
        string strSQL = "select data from yearcalendar where data > @my_year_start and workday='true'";
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@my_year_start", SqlDbType.Date);
        p1.Direction = ParameterDirection.Input;
        p1.Value = my_year;
        cmd.Parameters.Add(p1);

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            shiftworkdaylist.Add(dr["data"]);
        }
        conn.Close();
        conn.Dispose();
        return shiftworkdaylist;
    }
    // 抓取該月該班Lab方法
    ArrayList clslablist = new ArrayList();
    public ArrayList getclslablist(string my_class)
    {
        string strSQL = "select lab1,lab2,lab3,lab4,lab5 from classdetail where id_class=@my_class";
        string strConn = "Data Source=.;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@my_class", SqlDbType.NVarChar);
        p1.Direction = ParameterDirection.Input;
        p1.Value = my_class;
        cmd.Parameters.Add(p1);

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            clslablist.Add(dr["lab1"]);
            clslablist.Add(dr["lab2"]);
            clslablist.Add(dr["lab3"]);
            clslablist.Add(dr["lab4"]);
            clslablist.Add(dr["lab5"]);
        }
        conn.Close();
        conn.Dispose();
        return clslablist;
    }

    // 判斷該月分該班是否已塞入空白課表的方法
    public bool haveregisterinsql(string id_class)
    {
        bool flagwithdata = false;
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select top 1 datewithduty from timetable where id_class=@id_class";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@id_class", id_class);
                conn.Open();

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        flagwithdata = true;
                    }
                }
                catch
                {
                    flagwithdata = false;
                }
                conn.Close();
            }
        }

        return flagwithdata;
    }
    // 判斷該月分該班是否已塞入預設假日和LAB的方法
    public bool havescheindb(string id_class, DateTime datelike, DateTime datelike2)
    {
        bool flagwithdata = false;
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select Count(distinct CourseName) from timetable where id_class=@id_class and (date between  @datelike and @datelike2)";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@id_class", id_class);
                cmd.Parameters.AddWithValue("@datelike", datelike);
                cmd.Parameters.AddWithValue("@datelike2", datelike2);
                conn.Open();

                try
                {
                    int countsche;
                    countsche = Convert.ToInt32(cmd.ExecuteScalar());
                    if (countsche == 0)
                    {
                        flagwithdata = false;
                    }
                    else
                    {
                        flagwithdata = true;
                    }
                }
                catch
                {
                    flagwithdata = false;
                }
                conn.Close();
            }
        }

        return flagwithdata;
    }

    // 抓出課程起訖日
    ArrayList startenddate = new ArrayList();
    public ArrayList getregisterclsdate(string id_class)
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "select startdate,enddate from classdetail where id_class=@id_class";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@id_class", id_class);
                conn.Open();

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        startenddate.Add(dr[0]);
                        startenddate.Add(dr[1]);
                    }
                }
                catch
                {

                }
                conn.Close();
            }
        }

        return startenddate;
    }

    //預存空白課表
    protected void genscheofthismonth_Click1(object sender, EventArgs e)
    {
        genclearsch();
    }
    //預存空白課表方法
    public void genclearsch()
    {
        getregisterclsdate(Request.QueryString["classmemory"]);
        string clsmemory = Request.QueryString["classmemory"];
        DateTime ii_start = Convert.ToDateTime(startenddate[0]);
        DateTime ii_end = Convert.ToDateTime(startenddate[1]);

        DateTime i = ii_start;
        while (i <= ii_end)
        {
            string datestring = Convert.ToString(i.ToShortDateString());
            for (int j = 1; j <= 3; j++)
            {
                switch (j)
                {
                    case 1:
                        insertscheofthismonth(datestring + "!1", clsmemory, datestring, "1");
                        break;
                    case 2:
                        insertscheofthismonth(datestring + "!2", clsmemory, datestring, "2");
                        break;
                    case 3:
                        insertscheofthismonth(datestring + "!3", clsmemory, datestring, "3");
                        break;
                }
            }
            i = i.AddDays(1);
        }
    }
    //for預存空白課表
    public void insertscheofthismonth(string datewithduty, string clsmemory, string datestring, string my_duty)
    {
        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        string strCmd = "insert into [PKST].[dbo].[timetable] ([id_class],[datewithduty],[date],[duty]) values (@clsmemory,@datewithduty,@datestring,@my_duty)";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@datewithduty", datewithduty);
                cmd.Parameters.AddWithValue("@clsmemory", clsmemory);
                cmd.Parameters.AddWithValue("@datestring", datestring);
                cmd.Parameters.AddWithValue("@my_duty", my_duty);
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
    //提供空白課表第一次自動儲存時存"H","L"
    public void savetext(string label, string datewithduty)
    {
        string id_class = this.DropDownList1.Text;
        string dateid = datewithduty;
        string[] sp = dateid.Split('!');
        string sep_dateid_date = sp[0];
        string sep_dateid_duty = sp[1];


        string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            string strCmd = "UPDATE [PKST].[dbo].[timetable] set [CourseName]=@CourseName where [date]=@sep_dateid_date and [duty]=@sep_dateid_duty and [id_class]=@id_class";
            using (SqlCommand cmd = new SqlCommand(strCmd, conn))
            {
                cmd.Parameters.AddWithValue("@CourseName", label);

                cmd.Parameters.AddWithValue("@sep_dateid_date", sep_dateid_date);
                cmd.Parameters.AddWithValue("@sep_dateid_duty", sep_dateid_duty);
                cmd.Parameters.AddWithValue("@id_class", id_class);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }


}