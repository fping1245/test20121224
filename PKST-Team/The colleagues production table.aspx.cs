using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class The_colleagues_production_table : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Panel6.Visible = false;
        this.Panel7.Visible = false;
        this.Panel8.Visible = false;
        this.Panel5.Visible = true;
        this.Label15.Text = this.DropDownList1.SelectedValue;
        this.Label17.Text = this.DropDownList4.SelectedValue;
        this.WordExcelButton1.Text = this.Label15.Text + this.Label16.Text + this.Label17.Text + this.Label18.Text;
        this.Panel2.Visible = false;
        this.Panel3.Visible = false;
        this.Panel4.Visible = false;
        this.Panel1.Visible = true;
        this.GridView1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.Panel5.Visible = false;
        this.Panel7.Visible = false;
        this.Panel8.Visible = false;
        this.Panel6.Visible = true;

        this.Label19.Text = this.DropDownList1.SelectedValue;      
        this.Label21.Text = this.DropDownList2.SelectedValue;
        this.Label23.Text = this.DropDownList4.SelectedValue;
        this.WordExcelButton2.Text = this.Label19.Text + this.Label20.Text + this.Label21.Text + this.Label22.Text + this.Label23.Text + this.Label24.Text;
        
        this.Panel1.Visible = false;
        this.Panel3.Visible = false;
        this.Panel4.Visible = false;
        this.Panel2.Visible =true;
        this.GridView2.DataBind();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        this.Panel5.Visible = false;
        this.Panel6.Visible = false;
        this.Panel8.Visible = false;
        this.Panel7.Visible = true;

        this.Label25.Text = this.DropDownList1.SelectedValue;
        this.Label27.Text = this.DropDownList3.SelectedValue;
        this.Label29.Text = this.DropDownList4.SelectedValue;
        this.WordExcelButton3.Text = this.Label25.Text + this.Label26.Text + this.Label27.Text + this.Label28.Text + this.Label29.Text + this.Label30.Text;

        this.Panel1.Visible = false;
        this.Panel2.Visible = false;
        this.Panel4.Visible = false;
        this.Panel3.Visible = true;
        this.GridView3.DataBind();
    }
    protected void Button5_Click1(object sender, EventArgs e)
    {
        this.Panel5.Visible = false;
        this.Panel6.Visible = false;
        this.Panel7.Visible = false;
        this.Panel8.Visible = true;

        this.WordExcelButton4.Text = this.Label31.Text;
        this.Panel1.Visible = false;
        this.Panel2.Visible = false;
        this.Panel3.Visible = false;
        this.Panel4.Visible = true;
        this.GridView4.DataBind();
    }
}