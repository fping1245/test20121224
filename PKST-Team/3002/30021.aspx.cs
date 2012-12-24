//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > TreeView
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;
using System.Web.UI.WebControls;

public partial class _30021 : System.Web.UI.Page
{
	private Decoder dcode = new Decoder();

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("3002", false);

			if (Request["fl_url"] != null)
				lb_fl_url.Text = dcode.DeCode(Request["fl_url"].Trim());
			else
				lb_fl_url.Text = Album.Root;

			lb_path.Text = Server.MapPath(Album.Root);
		
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
		RootNode.NavigateUrl = "3002.aspx?fl_url=" + Server.UrlEncode(dcode.EnCode(Album.Root));
		RootNode.Target = "_parent";

		if (lb_fl_url.Text == Album.Root)
			RootNode.Select();

		tv_Al_List.Nodes.Clear();
		tv_Al_List.Nodes.Add(RootNode);
		tv_Al_List.ExpandAll();

		AddNodes(ref RootNode, lb_path.Text);
	}

	// 用遞迴方式建立 Nodes
	private void AddNodes(ref TreeNode pNode, string up_fl_path)
	{
		string furl = "", ftext;

		// 如果有資料，則建立子節點
		if (Directory.Exists(up_fl_path))
		{
			TreeNode subNode;

			foreach (string fpath in Directory.GetDirectories(up_fl_path))
			{
				furl = fpath.Replace(lb_path.Text, "").Replace("\\", "/");

				// _thumb 為縮圖存放目錄，不顯示
				if (! furl.Contains("_thumb"))
				{
					subNode = new TreeNode();

					if (lb_fl_url.Text == (Album.Root + furl))
					{
						subNode.Select();
					}

					ftext = fpath.Replace(up_fl_path, "").Replace("\\", "");

					subNode.Text = ftext;
					subNode.Value = ftext;

					subNode.NavigateUrl = "3002.aspx?fl_url=" + Server.UrlEncode(dcode.EnCode(Album.Root + furl));
					subNode.Target = "_parent";
					subNode.ToolTip = furl;
					pNode.ChildNodes.Add(subNode);

					AddNodes(ref subNode, fpath);
				}
			}
		}
	}
}
