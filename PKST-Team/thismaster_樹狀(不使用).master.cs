using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class thismaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 動態建立 TreeView 
        if (!IsPostBack)
        {
            BuildTreeNode();
        }
    }

    public void BuildTreeNode()
    {
        TreeNode RootNode = new TreeNode();         //根節點 
        string TmpValue = "";
        int iCnt = -1;
        string SqlString;

        RootNode.Text = "功能選單";
        RootNode.Value = "0";
        RootNode.ToolTip = "請點選下方的項目";
        //RootNode.NavigateUrl = "javascript:chgcont('Welcome.aspx')";

        tv_func.Nodes.Clear();
        tv_func.Nodes.Add(RootNode);

        //if (Session["mg_sid"].ToString() == "0")
        //{
        // 系統管理員
        SqlString = "Select f2.fi_no1, f1.fi_name1, f2.fi_no2, f2.fi_name2, f2.fi_url2";
        SqlString = SqlString + " From Func_Item2 f2";
        SqlString = SqlString + " Inner Join Func_Item1 f1 On f1.fi_no1 = f2.fi_no1";
        SqlString = SqlString + " Where f2.is_visible = 1 And f1.is_visible = 1";
        SqlString = SqlString + " Order by f1.fi_sort1, f2.fi_sort2";
        //}
        //else
        //{
        //    // 一般使用者
        //    SqlString = "Select f1.fi_no1, f1.fi_name1, f2.fi_no2, f2.fi_name2, f2.fi_url2";
        //    SqlString = SqlString + " From Func_Power p";
        //    SqlString = SqlString + " Inner Join Func_Item2 f2 On f2.fi_no1 = p.fi_no1 And f2.fi_no2 = p.fi_no2";
        //    SqlString = SqlString + " Inner Join Func_Item1 f1 On f1.fi_no1 = p.fi_no1";
        //    SqlString = SqlString + " Where p.is_enable = 1 And f2.is_visible = 1 And f1.is_visible = 1 And p.mg_sid = " + Session["mg_sid"].ToString();
        //    SqlString = SqlString + " Order by f1.fi_sort1, f2.fi_sort2";
        //}

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

                        // 設定節點所連結的網頁，透過 javascript 來控制另一個框架的網頁內容。
                        //ChildNode.NavigateUrl = "javascript:chgcont('" + Sql_Reader["fi_url2"].ToString().Trim() + "')";
                        ChildNode.NavigateUrl = "javascript:openswinlocal('" + Sql_Reader["fi_url2"].ToString().Trim() + "')";
                        RootNode.ChildNodes[iCnt].ChildNodes.Add(ChildNode);
                    }
                    Sql_Reader.Close();
                }
            }
            sql_conn.Close();
        }
    }
   
}
