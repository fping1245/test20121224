//---------------------------------------------------------------------------- 
//程式功能	日期選擇
//備註說明	網頁內要有 fullwindow、div_calendar 的 div 區塊及 if_calendar 的 iframe 來開啟
//			需含入	/js/innerWindow.js
//					/js/innerCalendar.js
//---------------------------------------------------------------------------- 

using System;

public partial class _calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (! IsPostBack)
		{
			string mErr = "";
			DateTime ckdt;
			if (Request["ndt"] != null)
			{
				if (DateTime.TryParse(Request["ndt"], out ckdt))
				{
					cdr1.VisibleDate = ckdt;
				}
			}

			if (Request["rtobj"] == null)
				mErr = "未宣告傳回物件\\n";
			else
				lb_rtobj.Text = Request["rtobj"];

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_calendar();", true);
		}
    }

	// 選擇日期後，傳回資料並關閉視窗
	protected void cdr1_SelectionChanged(object sender, EventArgs e)
	{
		DateTime fDay = cdr1.SelectedDate;

		ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.rt_parent(\"" + lb_rtobj.Text + "\",\"" + fDay.ToString("yyyy/MM/dd") + "\");", true);
	}
}
