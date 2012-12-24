//---------------------------------------------------------------------------- 
//程式功能	線上考試(限定身份) > 填寫考生資料 > 試題清單 > 回答試題
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class _B003111 : System.Web.UI.Page
{
	static CheckBox[] cb_tmp;		// 複選選項物件
	static RadioButton[] rb_tmp;	// 單選選項物件
	static string[] ti_sid;			// 答案選項編號
	static bool[] ti_correct;		// 答案選項為正確答案
	static int correct_num = 1;		// 正確答案數目 (複選判斷用)

    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";
		int tp_sid = -1, tu_sid = -1, tq_sort = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限，不存入登入紀錄
			//Check_Power("B003", false);

			if (Request["tu_sid"] != null && Request["tp_sid"] != null && Request["tq_sort"] != null)
			{
				if (int.TryParse(Request["tu_sid"], out tu_sid) && int.TryParse(Request["tp_sid"], out tp_sid)
					&& int.TryParse(Request["tq_sort"], out tq_sort))
				{
					lb_tu_sid.Text = tu_sid.ToString();
					lb_tp_sid.Text = tp_sid.ToString();
					lb_tq_sort.Text = tq_sort.ToString();
				}
				else
					mErr = "資料格式錯誤!\\n";
			}
			else
				mErr = "資料取得錯誤!\\n";
		}

		if (mErr == "")
		{
			// 取得題目及建立答案選項
			GetData();
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"B003.aspx\");", true);
    }

	// Check_Power() 檢查使用者權限並存入登入紀錄
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

	// 取得資料，並建立選項
	private void GetData()
	{
		string SqlString = "", mErr = "", msel = "";
		int icnt = 0, tq_item = 0, tp_question = 0;
		bool mcheck = false;
		SqlDataReader Sql_Reader;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				Sql_Command.Connection = Sql_Conn;

				#region 讀取考生資料
				SqlString = "Select Top 1 tu_name, tu_no, b_time, e_time From Ts_User Where tp_sid = @tp_sid And tu_sid = @tu_sid;";
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.Clear();
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
				Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);

				Sql_Reader = Sql_Command.ExecuteReader();

				if (Sql_Reader.Read())
				{
					lb_tu_name.Text = Sql_Reader["tu_name"].ToString().Trim();
					lb_tu_no.Text = Sql_Reader["tu_no"].ToString().Trim();
					lb_b_time.Text = DateTime.Parse(Sql_Reader["b_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
					lb_e_time.Text = DateTime.Parse(Sql_Reader["e_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
				}
				else
					mErr = "找不到考生資料!\\n";

				Sql_Reader.Close();
				#endregion

				#region 讀取試卷資料
				if (mErr == "")
				{
					SqlString = "Select Top 1 tp_title, tp_question From Ts_Paper Where tp_sid = @tp_sid;";
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						lb_tp_title.Text = Sql_Reader["tp_title"].ToString().Trim();
						tp_question = int.Parse(Sql_Reader["tp_question"].ToString());
						lb_tp_question.Text = tp_question.ToString();
					}
					else
						mErr = "找不到試卷資料!\\n";

					Sql_Reader.Close();
				}
				#endregion

				if (mErr == "")
				{
					if (tp_question >= int.Parse(lb_tq_sort.Text))
					{
						#region 讀取題目資料
						SqlString = "Select Top 1 tq_sid, tq_desc, tq_type, tq_item, tq_score From Ts_Question Where tp_sid = @tp_sid And tq_sort = @tq_sort";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
						Sql_Command.Parameters.AddWithValue("tq_sort", lb_tq_sort.Text);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							lb_tq_sid.Text = Sql_Reader["tq_sid"].ToString();
							lb_tq_desc.Text = Sql_Reader["tq_desc"].ToString().Trim().Replace("\r\n", "<br>");
							lb_tq_score.Text = Sql_Reader["tq_score"].ToString();

							lb_tq_type.Text = Sql_Reader["tq_type"].ToString();
							if (lb_tq_type.Text == "0")
								lt_tq_type.Text = "以下項目，請圈選出 <b>1</b> 個正確答案。(單選)";
							else if (lb_tq_type.Text == "1")
								lt_tq_type.Text = "以下項目，有不定數量的正確答案。(複選)";
							else
								lt_tq_type.Text = "以下項目中，請圈選 " + lb_tq_type.Text + " 個正確答案。(複選)";

							lb_tq_item.Text = Sql_Reader["tq_item"].ToString();
							tq_item = int.Parse(lb_tq_item.Text);
						}
						else
							mErr = "找不到題目!\\n";

						Sql_Reader.Close();
						#endregion

						#region 取得問卷項目資料
						if (mErr == "")
						{
							ti_sid = new string[tq_item];			// 答案選項編號
							ti_correct = new bool[tq_item];			// 答案選項為正確答案
							correct_num = 0;						// 正確答案數目 (複選判斷用)

							SqlString = "Select i.ti_sid, i.ti_desc, i.ti_correct, IsNull(a.tu_sid, -1) as tu_sid From Ts_Item i";
							SqlString += " Left Outer Join Ts_UAns a On a.tu_sid = @tu_sid And i.tp_sid = a.tp_sid And i.tq_sid = a.tq_sid And i.ti_sid =a.ti_sid";
							SqlString += " Where i.tp_sid = @tp_sid And i.tq_sid = @tq_sid Order by i.ti_sort;";

							Sql_Command.Parameters.Clear();
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
							Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
							Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);

							Sql_Reader = Sql_Command.ExecuteReader();

							switch (lb_tq_type.Text)
							{
								case "0":
									// 單選
									rb_tmp = new RadioButton[tq_item];

									while (Sql_Reader.Read())
									{
										if (Sql_Reader["tu_sid"].ToString() == "-1")
										{
											mcheck = false;
											msel = "<img src=\"../images/ico/normal.gif\">&nbsp;";
										}
										else
										{
											mcheck = true;
											msel = "<img src=\"../images/ico/correct-r.gif\" title=\"上次勾選的答案\">&nbsp;";
										}

										ti_sid[icnt] = Sql_Reader["ti_sid"].ToString();
										if (Sql_Reader["ti_correct"].ToString() == "1")
										{
											ti_correct[icnt] = true;
											correct_num++;
										}
										else
											ti_correct[icnt] = false;

										rb_tmp[icnt] = new RadioButton();
										rb_tmp[icnt].Text = msel + Sql_Reader["ti_desc"].ToString().Trim() + "<br>";
										rb_tmp[icnt].Checked = mcheck;
										rb_tmp[icnt].EnableViewState = true;
										rb_tmp[icnt].EnableTheming = true;
										rb_tmp[icnt].GroupName = "rb_group";
										pl_tq_type.Controls.Add(rb_tmp[icnt]);

										icnt++;
									}
									break;

								default:
									// 複選
									cb_tmp = new CheckBox[tq_item];

									while (Sql_Reader.Read())
									{
										if (Sql_Reader["tu_sid"].ToString() == "-1")
										{
											mcheck = false;
											msel = "<img src=\"../images/ico/normal.gif\">&nbsp;";
										}
										else
										{
											mcheck = true;
											msel = "<img src=\"../images/ico/correct-r.gif\" title=\"上次勾選的答案\">&nbsp;";
										}

										ti_sid[icnt] = Sql_Reader["ti_sid"].ToString();
										if (Sql_Reader["ti_correct"].ToString() == "1")
										{
											ti_correct[icnt] = true;
											correct_num++;
										}
										else
											ti_correct[icnt] = false;

										cb_tmp[icnt] = new CheckBox();
										cb_tmp[icnt].Text = msel + Sql_Reader["ti_desc"].ToString().Trim() + "<br>";
										cb_tmp[icnt].Checked = mcheck;
										cb_tmp[icnt].EnableViewState = true;
										cb_tmp[icnt].EnableTheming = true;
										pl_tq_type.Controls.Add(cb_tmp[icnt]);

										icnt++;
									}
									break;
							}
							Sql_Reader.Close();
						}
						#endregion
					}
					else
						mErr = "End";		// 已答完所有題目
				}

				Sql_Conn.Close();
			}
		}

		if (mErr != "")
		{
			if (mErr == "End")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"考題已全部答完!\\n\");parent.location.reload(true);\");", true);
			else
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
		}
	}

	// 確定完成 (存檔)
	protected void lk_ok_Click(object sender, EventArgs e)
	{
		int tq_item = int.Parse(lb_tq_item.Text);
		int icnt = 0, scnt = 0, ckint = 0, sel_num = 0, tq_score = 0, tq_sort = 0, tp_question = 0;
		string sti_sid = "", mErr = "", SqlString = "";

		#region 判斷問卷型式及選擇的答案
		if (lb_tq_type.Text == "0")
		{
			// 單選
			scnt = 1;	// 項目選擇上限

			// 檢查是否有選擇
			for (icnt = 0; icnt < tq_item; icnt++)
			{
				if (rb_tmp[icnt].Checked)
				{
					sti_sid = ti_sid[icnt];
					if (ti_correct[icnt])	// 答對
						sel_num++;
					ckint++;
				}
			}
		}
		else if (lb_tq_type.Text == "1")
		{
			// 複選全部
			scnt = tq_item;	// 項目選擇上限

			for (icnt = 0; icnt < tq_item; icnt++)
			{
				if (cb_tmp[icnt].Checked)
				{
					sti_sid += ti_sid[icnt] + ";";
					if (ti_correct[icnt])	// 答對
						sel_num++;
					ckint++;
				}
			}	
		}
		else
		{
			// 限制數量複選
			scnt = int.Parse(lb_tq_type.Text);	// 項目選擇上限

			for (icnt = 0; icnt < tq_item; icnt++)
			{
				if (cb_tmp[icnt].Checked)
				{
					sti_sid += ti_sid[icnt] + ";";
					if (ti_correct[icnt])	// 答對
						sel_num++;
					ckint++;
				}
			}
		}
		#endregion

		if (ckint > scnt || ckint < 1)
		{
			// 判斷選擇項目數量
			if (scnt == 1)
				mErr = "請選擇出 1 個正確答案!\\n";
			else if (lb_tq_type.Text == "1")
				mErr = "請由題目中圈選出正確答案!\\n";
			else if (ckint != scnt)
				mErr = "請選擇出 " + scnt.ToString() + " 個正確答案!\\n";
		}
		else
		{
			// 判斷是否得分
			if (correct_num == sel_num)
				tq_score = int.Parse(lb_tq_score.Text);		// 答對得分
			else
				tq_score = 0;

			string[] stmp = sti_sid.Split(';');

			#region 答案存檔
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					#region 先刪除原答題資料
					Sql_Conn.Open();
					SqlString = "Delete Ts_UAns Where tu_sid = @tu_sid And tp_sid = @tp_sid And tq_sid = @tq_sid;";
					SqlString += "Delete Ts_UQuest Where tu_sid = @tu_sid And tp_sid = @tp_sid And tq_sid = @tq_sid;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
					#endregion

					#region 存入 考生答題紀錄
					Sql_Conn.Open();

					for (icnt = 0; icnt < ckint; icnt++)
					{
						SqlString = "Insert Into Ts_UAns (tu_sid, tp_sid, tq_sid, ti_sid) Values";
						SqlString += " (@tu_sid, @tp_sid, @tq_sid, @ti_sid);";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
						Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
						Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);
						Sql_Command.Parameters.AddWithValue("ti_sid", stmp[icnt]);

						Sql_Command.ExecuteNonQuery();
					}
					Sql_Conn.Close();
					#endregion

					#region  存入考生題目紀錄
					Sql_Conn.Open();

					SqlString = "Insert Into Ts_UQuest (tu_sid, tp_sid, tq_sid, tuq_score) Values";
					SqlString += " (@tu_sid, @tp_sid, @tq_sid, @tuq_score);";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);
					Sql_Command.Parameters.AddWithValue("tuq_score", tq_score);
					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
					#endregion

					Sql_Conn.Open();

					// 統計考生試卷得分及紀錄目前時間，並統計試卷總得分、題目總得分及排名
					SqlString = "Execute dbo.p_Ts_User_Calc @tp_sid, @tu_sid, @tq_sid, 1;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();

					Sql_Conn.Close();
				}
			}
			#endregion
		}

		if (mErr == "")
		{
			tp_question = int.Parse(lb_tp_question.Text);
			tq_sort = int.Parse(lb_tq_sort.Text) + 1;
			if (tq_sort > tp_question)
			{
				// 答題結束
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"考題已全部答完!\\n\");parent.location.reload(true);", true);
			}
			else
			{
				// 續續答下一題
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "location.replace(\"B003111.aspx?tu_sid=" + lb_tu_sid.Text + "&tp_sid=" + lb_tp_sid.Text + "&tq_sort=" + tq_sort.ToString() + "\");", true);
			}
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
