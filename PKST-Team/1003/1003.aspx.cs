//---------------------------------------------------------------------------- 
//程式功能 動態產生功能選單
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _1003 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
            // 檢查使用者權限並存入使用紀錄
			//Check_Power("1003", true);

			// 動態建立 TreeView 
			BuildTreeNode();
		}
	}

    // Check_Power() 檢查使用者權限並存入使用紀錄
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

	public void BuildTreeNode()
	{
		TreeNode RootNode = new TreeNode();         //根節點 
		string TmpValue = "";
		int iCnt = -1;
		string SqlString;

		RootNode.Text = "範例選單";
		RootNode.Value = "0";
		RootNode.ToolTip = "請點選下方的範例項目";
		RootNode.NavigateUrl = "";

		tv_func.Nodes.Clear();
		tv_func.Nodes.Add(RootNode);

		SqlString = "Select f2.fi_no1, f1.fi_name1, f2.fi_no2, f2.fi_name2, f2.fi_url2";
		SqlString = SqlString + " From Func_Item2 f2";
		SqlString = SqlString + " Inner Join Func_Item1 f1 On f1.fi_no1 = f2.fi_no1";
		SqlString = SqlString + " Where f2.is_visible = 1 And f1.is_visible = 1";
		SqlString = SqlString + " Order by f1.fi_sort1, f2.fi_sort2";

		using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			sql_conn.Open();

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, sql_conn))
			{
				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					while (Sql_Reader.Read())
					{
						if (TmpValue != Sql_Reader["fi_no1"].ToString())
						{
							iCnt = iCnt + 1;
							TmpValue = Sql_Reader["fi_no1"].ToString();
							TreeNode ParentNode = new TreeNode();
							ParentNode.Text = Sql_Reader["fi_name1"].ToString().Trim();
							ParentNode.Value = Sql_Reader["fi_no1"].ToString();
							ParentNode.ToolTip = "請展開後，點選子功能";
							ParentNode.NavigateUrl = "";
							RootNode.ChildNodes.Add(ParentNode);
						}
						// 建立新的子節點
						TreeNode ChildNode = new TreeNode();

						// 設定節點顯示文字
						ChildNode.Text = Sql_Reader["fi_name2"].ToString().Trim();

						// 設定節點編號資料
						ChildNode.Value = Sql_Reader["fi_no2"].ToString();

						// 設定節點所連結的網頁，此處僅用 javascript 的 alert() 彈出訊息
						ChildNode.NavigateUrl = "javascript:alert('連結到：" + Sql_Reader["fi_url2"].ToString().Trim() + "')"; 
						RootNode.ChildNodes[iCnt].ChildNodes.Add(ChildNode);				
					}
					
					Sql_Reader.Close();
				}
			}
			sql_conn.Close();
		}
	}
}
