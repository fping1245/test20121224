using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;

public partial class clssche : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    ArrayList my_memo = new ArrayList();
    public class my_sche
    {
        public string my_duty { get; set; }
        public string my_date { get; set; }
        public string my_am { get; set; }
        public string my_pm { get; set; }
        public string my_xm { get; set; }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=myexcel.xls");//xls
        Response.ContentType = "application/ms-excel";
        string HTML = GetControlsHTML(this.Calendar1);

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
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {

        bool other_month_flag = true;
        # region 不顯示本月以外日期
        if (e.Day.IsOtherMonth)
        {
            other_month_flag = false;
            e.Cell.Controls.Clear();
        }
        # endregion

        if (other_month_flag)
        {
            string my_am="";
            string my_pm="";
            string my_xm="";
            string strConn = "Data Source=.;Initial Catalog=PKST;User ID=sa";
            string strCmd = "select CourseName,date,duty from timetable where date=@date and id_class=@id_class";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@date", e.Day.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@id_class", this.DropDownList1.SelectedValue);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        switch (dr["duty"].ToString())
                        {
                            case "1":
                                my_am = dr["CourseName"].ToString();
                                break;
                            case "2":
                                my_pm = dr["CourseName"].ToString();
                                break;
                            case "3":
                                my_xm = dr["CourseName"].ToString();
                                break;
                        }
                    }
                    conn.Close();
                }
            }  
            //第一行
            Label lbl = new Label();
            lbl.Text = "<br/>------早------<br/>";
            lbl.Font.Size = 10;
            e.Cell.Controls.Add(lbl);

            Label lb4 = new Label();
            int mycount = (int)((my_am.Length) / 7);
            int myleave = (my_am.Length) % 7;
            ArrayList arr=new ArrayList();
            int j = 0;

            if (mycount > 0)
            {
                for (int i = 1; i <= mycount; i++)
                {
                    arr.Add(my_am.Substring(7 * (i - 1), 7));
                    j++;
                }
            }

            if (myleave != 0 & mycount > 0)
            {
            arr.Add(my_am.Substring(7*(mycount),my_am.Length - 7*(mycount)));
            j++;
            }
            else if (myleave != 0 & mycount == 0)
            {
                arr.Add(my_am);
                j++;
            }

            string mymyam="";
            for (int k = 0; k < j; k++)
            {
                if (k != j - 1)
                {
                    mymyam = mymyam + arr[k].ToString() + "<br/>";
                }
                else
                {   
                    mymyam = mymyam + arr[k].ToString();
                }
            }

            lb4.Text = mymyam;           
            lb4.Width = 100;
            lb4.Height =80;
            lb4.Font.Size = 10;
            lb4.ForeColor = System.Drawing.Color.Blue;
            e.Cell.Controls.Add(lb4);

            //第二行
            Label lb2 = new Label();
            lb2.Text = "<br/>------中------<br/>";
            lb2.Font.Size = 10;
            e.Cell.Controls.Add(lb2);

            Label lb5 = new Label();
            mycount = (int)((my_pm.Length) / 7);
            myleave = (my_pm.Length) % 7;
            ArrayList arr2 = new ArrayList();
            j = 0;

            if (mycount > 0)
            {
                for (int i = 1; i <= mycount; i++)
                {
                    arr2.Add(my_pm.Substring(7 * (i - 1), 7));
                    j++;
                }
            }

            if (myleave != 0 & mycount > 0)
            {
                arr2.Add(my_pm.Substring(7 * (mycount), my_pm.Length - 7 * (mycount)));
                j++;
            }
            else if (myleave != 0 & mycount == 0)
            {
                arr2.Add(my_pm);
                j++;
            }

            string mymypm = "";
            for (int k = 0; k < j; k++)
            {
                if (k != j - 1)
                {
                    mymypm = mymypm + arr2[k].ToString() + "<br/>";
                }
                else
                {
                    mymypm = mymypm + arr2[k].ToString();
                }
            }

            lb5.Text = mymypm; 
            lb5.Width = 100;    
            lb5.Height = 80;
            lb5.Font.Size = 10;
            lb5.ForeColor = System.Drawing.Color.Blue;
            e.Cell.Controls.Add(lb5);

            //第三行
            Label lb3 = new Label();
            lb3.Text = "<br/>------晚------<br/>";
            lb3.Font.Size = 10;
            e.Cell.Controls.Add(lb3);

            Label lb6 = new Label();
            mycount = (int)((my_xm.Length) / 7);
            myleave = (my_xm.Length) % 7;
            ArrayList arr3 = new ArrayList();
            j = 0;

            if (mycount > 0)
            {
                for (int i = 1; i <= mycount; i++)
                {
                    arr3.Add(my_xm.Substring(7 * (i - 1), 7));
                    j++;
                }
            }

            if (myleave != 0 & mycount > 0)
            {
                arr3.Add(my_xm.Substring(7 * (mycount), my_xm.Length - 7 * (mycount)));
                j++;
            }
            else if (myleave != 0 & mycount == 0)
            {
                arr3.Add(my_xm);
                j++;
            }

            string mymyxm = "";
            for (int k = 0; k < j; k++)
            {
                if (k != j - 1)
                {
                    mymyxm = mymyxm + arr3[k].ToString() + "<br/>";
                }
                else
                {
                    mymyxm = mymyxm + arr3[k].ToString();
                }
            }
            lb6.Text = mymyxm; 
            lb6.Width = 100;
            lb6.Height = 80;
            lb6.Font.Size= 10;
            lb6.ForeColor = System.Drawing.Color.Blue;
            e.Cell.Controls.Add(lb6);
        }



    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime startdate, endate;
        string choosedcls = this.DropDownList1.SelectedValue.ToString();

        ArrayList registerclsdatelist = new ArrayList();
        registerclsdatelist = getregisterclsdate((choosedcls));

        startdate = Convert.ToDateTime(registerclsdatelist[0]);
        //Response.Write("課程起始日:"+startdate.ToShortDateString()+"<br>");
        this.TextBox1.Text = startdate.ToShortDateString();

        endate = Convert.ToDateTime(registerclsdatelist[1]);
        //Response.Write("課程結束日:"+endate.ToShortDateString());
        this.TextBox2.Text = endate.ToShortDateString();
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
}