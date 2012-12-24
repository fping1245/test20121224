﻿//---------------------------------------------------------------------------- 
//程式功能	編碼函數模組 > Quoted Printable 編碼/解碼
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40032 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		// 檢查使用者權限但不存入登入紀錄
		//Check_Power("4003", false);
    }

	// 檢查使用者權限並存入登入紀錄
	private void Check_Power(string f_power, bool bl_save)
	{
		// 載入公用函數
		Common_Func cfc = new Common_Func();

		// 若 Session 不存在則直接顯示錯誤訊息
		try
		{
			if (cfc.Check_Power(Session["mg_sid"].ToString(), Session["mg_name"].ToString(), Session["mg_power"].ToString(), f_power, Request.ServerVariables["REMOTE_ADDR"], bl_save) > 0)
				Response.Redirect("../Error.aspx?ErrCode=1");
		}
		catch
		{
			Response.Redirect("../Error.aspx?ErrCode=2");
		}
	}

	// 編碼
	protected void bn_ecode_Click(object sender, EventArgs e)
	{
		QuotedPrintable qupt = new QuotedPrintable();

		qupt.CodePage = int.Parse(tb_codepage.Text);
		qupt.LineBreaks = int.Parse(tb_linebreaks.Text);
		qupt.DeQuotedCode = tb_dcode.Text.Trim();

		tb_ecode.Text = qupt.EnQuotedCode;
	}
	
	// 解碼
	protected void bn_dcode_Click(object sender, EventArgs e)
	{
		QuotedPrintable qupt = new QuotedPrintable();

		qupt.CodePage = int.Parse(tb_codepage.Text);
		qupt.EnQuotedCode = tb_ecode.Text.Trim();

		tb_dcode.Text = qupt.DeQuotedCode;
	}
}
