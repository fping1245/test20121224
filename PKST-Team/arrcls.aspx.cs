using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Collections;

public partial class arrcls : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ComboBox cb = new ComboBox();
        //cb.ID = "a";
        //cb.AutoCompleteMode = ComboBoxAutoCompleteMode.Suggest;
        //cb.DropDownStyle = ComboBoxStyle.DropDownList;
        //this.Panel1.Controls.Add(cb);

        ListItem ls = new ListItem();
        ls.Value = "1";
        
        ComboBox cb = new ComboBox();
        cb.DataSource = ls;
        cb.ID = "a";
        cb.AutoCompleteMode = ComboBoxAutoCompleteMode.Suggest;
        cb.DropDownStyle = ComboBoxStyle.DropDown;
        this.Panel1.Controls.Add(cb);
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        // DateTime dt=Calendar1.SelectedDate;
        // Response.Write(dt.ToString());
        //if (e.Day.Date == dt)
        //{
        //ComboBox a1 = new ComboBox();

        //a1.ID = 'a' + i.ToString();
        //a1.AutoCompleteMode = ComboBoxAutoCompleteMode.Suggest;
        //a1.DropDownStyle = ComboBoxStyle.DropDownList;
        //i++;
        //        e.Cell.Controls.Add(tb);
        //    }
        //Response.Write(e.Day.Date.ToString());
        DateTime dt = Calendar1.SelectedDate;
        if (e.Day.Date == dt)
        {
            ArrayList al=new ArrayList();
            al.Add(1);
            al.Add(2);
            al.Add(3);
            ComboBox cb = new ComboBox();
            cb.DataSource = al;
            cb.ID = "a";
            cb.AutoCompleteMode = ComboBoxAutoCompleteMode.Suggest;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            e.Cell.Controls.Add(cb);
        }
    }
}