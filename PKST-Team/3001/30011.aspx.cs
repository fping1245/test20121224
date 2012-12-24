//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > TreeView
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _30011 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("3001", false);

			if (Request["al_sid"] != null)
			{
				if (int.TryParse(Request["al_sid"], out ckint))
					lb_al_sid.Text = ckint.ToString();
				else
					lb_al_sid.Text = "0";
			}
		
			// 建立 TreeView
			BuildTreeView();
		}
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

	// 建立 TreeView
	private void BuildTreeView()
	{
		TreeNode RootNode = new TreeNode();         //根節點 

		RootNode.Text = "根目錄";
		RootNode.Value = "0";
		RootNode.ToolTip = "";
		RootNode.NavigateUrl = "3001.aspx?al_sid=0";
		RootNode.Target = "_parent";

		if (lb_al_sid.Text == "0")
			RootNode.Select();

		tv_Al_List.Nodes.Clear();
		tv_Al_List.Nodes.Add(RootNode);
		tv_Al_List.ExpandAll();

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			Sql_Conn.Open();

			string SqlString = "";

			SqlString = "Select al_sid, up_al_sid, al_name, al_desc From Al_List Order by up_al_sid, al_sort";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				using (SqlDataAdapter Sql_Adapter = new SqlDataAdapter(Sql_Command))
				{
					DataTable dt_Al_List = new DataTable();

					Sql_Adapter.Fill(dt_Al_List);

					// 用遞迴方式建立 Nodes
					AddNodes(ref RootNode, ref dt_Al_List, 0);

					dt_Al_List.Clear();
					dt_Al_List.Dispose();
				}
			}
			Sql_Conn.Close();
		}
	}

	// 用遞迴方式建立 Nodes
	private void AddNodes(ref TreeNode pNode, ref DataTable dt_Al_List, int up_al_sid)
	{
		DataRow[] dRow = dt_Al_List.Select("up_al_sid = " + up_al_sid.ToString());

		// 如果有資料，則建立子節點
		if (dRow.GetUpperBound(0) > -1)
		{
			TreeNode subNode;

			foreach (DataRow sRow in dRow)
			{
				subNode = new TreeNode();

				if (sRow[0].ToString() == lb_al_sid.Text)
				{
					subNode.Select();
				}

				subNode.Text = sRow[2].ToString();
				subNode.Value = sRow[0].ToString();
				
				subNode.NavigateUrl = "3001.aspx?al_sid=" + sRow[0].ToString();
				subNode.Target = "_parent";
				subNode.ToolTip = sRow[3].ToString();
				pNode.ChildNodes.Add(subNode);

				AddNodes(ref subNode, ref dt_Al_List, int.Parse(sRow[0].ToString()));
			}
			dRow = null;
		}
	}
}
