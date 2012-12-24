//---------------------------------------------------------------------------- 
//程式功能	公用函數
//程式名稱	/App_Code/Common_Func.cs
//---------------------------------------------------------------------------- 
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class Common_Func
{
	// Check_ID() 檢查帳號密碼，儲存紀錄，傳回權限
	// 傳入參數 mg_id	使用者帳號
	//			mg_pass	登入密碼
	//			ip_addr	使用者 IP
	// 傳回數值	*開頭	*錯誤訊息
	//			其它	管理者編號；管理者姓名；權限字串
	public string Check_ID(string mg_id, string mg_pass, string ip_addr)
	{
		string SqlString = "";
		string mCheck = "", mErr = "", mg_sid = "", mg_name = "";
		StringBuilder mg_power = new StringBuilder();
		Decoder dcode = new Decoder();

		// 取得使用者資料 
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 mg_sid, mg_name, mg_id, mg_pass From Manager Where mg_id = @mg_id";
			Sql_Conn.Open();
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				SqlDataReader Sql_Reader;

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("@mg_id", mg_id);

				Sql_Reader = Sql_Command.ExecuteReader();
				if (Sql_Reader.Read())
				{
					// 再次確認帳號及密碼，以防有人使用 SQL 隱碼攻擊侵入 
					if (Sql_Reader["mg_id"].ToString().Trim() == mg_id)
					{
						// 密碼欄位需解密後再行核對
						if (mg_pass == dcode.DeCode(Sql_Reader["mg_pass"].ToString().Trim()))
						{
							// 建立 Session
							mg_sid = Sql_Reader["mg_sid"].ToString().Trim();
							mg_name = Sql_Reader["mg_name"].ToString().Trim();
							Sql_Command.Dispose();
							Sql_Reader.Close();
							Sql_Reader.Dispose();

							// 取得執行權限，置入 mg_power
							// 清除 SqlString 字串
							SqlString.Remove(0, SqlString.Length);

							if (mg_sid.ToString() == "0")
							{
								// 若為系統總管理者，擁有全部的功能執行權限
								SqlString = "Select fi_no2 From Func_Item2 Where is_visible <> 0";
							}
							else
							{
								// 一般使用者，由人員系統權限 Func_Power 資料表取得可執行的權限，以及系統管理用的功能
								SqlString = "Select fi_no2 From Func_Power Where mg_sid = @mg_sid And is_enable = 1";
								SqlString = SqlString + " Union ";
								SqlString = SqlString + "Select fi_no2 From Func_Item2 Where is_visible = 2";
							}

							// 取得權限，並填入 mg_power
							Sql_Command.Connection = Sql_Conn;
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("@mg_sid", mg_sid);
							Sql_Reader = Sql_Command.ExecuteReader();
							while (Sql_Reader.Read())
							{
								mg_power.Append(Sql_Reader["fi_no2"].ToString() + ";");
							}
							Sql_Command.Dispose();
							Sql_Reader.Close();

							if (mg_power.ToString() == "")
								mErr = "沒有任何的執行權限，請用其它帳號重新登入!\\n";
							else
							{	// 存入使用者登入紀錄，並更新最後更新紀錄
								SqlString = "Insert Into Mg_Log (mg_sid, fi_no2, lg_time, lg_ip) Values";
								SqlString += " (@mg_sid, '0001', getdate(), @lg_ip);";
								SqlString += "Update Manager Set last_date = getdate() Where mg_sid = @mg_sid";

								Sql_Command.Parameters.Clear();
								Sql_Command.Connection = Sql_Conn;
								Sql_Command.CommandText = SqlString;
								Sql_Command.Parameters.AddWithValue("@mg_sid", mg_sid);
								Sql_Command.Parameters.AddWithValue("@lg_ip", ip_addr);

								Sql_Command.ExecuteNonQuery();

								// 刪除一年前所有使用者的登入資料
								SqlString = "Delete Mg_Log Where lg_time < DateAdd(yy, -1,getdate())";
								Sql_Command.Parameters.Clear();
								Sql_Command.Connection = Sql_Conn;
								Sql_Command.CommandText = SqlString;
								Sql_Command.Parameters.AddWithValue("@mg_sid", mg_sid);
								Sql_Command.ExecuteNonQuery();
							}
						}
						else
							mErr = "帳號、密碼有誤!\\n";		// 不想讓使用者清楚知道是密碼錯誤，所以帳號、密碼兩者都寫
					}
					else
						mErr = "請使用正確的方式登入!\\n";

					Sql_Command.Dispose();
					Sql_Reader.Close();
					Sql_Reader.Dispose();
				}
				else
					mErr = "帳號、密碼有誤!\\n";
			}
		}

		if (mErr == "")
		{
			// 以 \t\n 為間隔
			mCheck = mg_sid + "\t\n" + mg_name + "\t\n" + mg_power;
		}
		else
		{
			mCheck = "*" + mErr;
		}

		return mCheck;
	}

	// Check_Power() 檢查權限並存檔
	// 輸入參數	mg_sid		管理者代碼
	//			mg_name		管理者姓名
	//			mg_power	權限字串
	//			f_power		現行程權限代碼
	//			bl_save		是否要存入使用紀錄
	// 傳回數值	 0	正確
	//			-1	不明原因錯誤
	//			-2	登入資料錯誤 (不正常的方式進入)
	//			-3	無指定功能的使用權限	
	public int Check_Power(string mg_sid, string mg_name, string mg_power, string f_power, string lg_ip, bool bl_save)
	{
		String_Func sfc = new String_Func();
		int mfg = -1;
		string SqlString = "";
		SqlConnection Sql_Conn;
		SqlCommand Sql_Command;

		if (sfc.IsInteger(mg_sid))
		{
			if (mg_name == "")
				mfg = -2;
			else
			{
				if (mg_power.Contains(f_power))
					mfg = 0;
				else
					mfg = -3;
			}
		}
		else
			mfg = -2;

		// 存入使用記錄
		if (mfg == 0 && bl_save)
		{
			SqlString = "Insert Into Mg_Log (mg_sid, fi_no2, lg_time, lg_ip) Values";
			SqlString = SqlString + " (@mg_sid, @fi_no2, getdate(), @lg_ip)";
			Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString);
			Sql_Conn.Open();
			Sql_Command = new SqlCommand(SqlString, Sql_Conn);
			Sql_Command.Parameters.AddWithValue("@mg_sid", mg_sid);
			Sql_Command.Parameters.AddWithValue("@fi_no2", f_power);
			Sql_Command.Parameters.AddWithValue("@lg_ip", lg_ip);

			Sql_Command.ExecuteNonQuery();

			Sql_Command.Dispose();
			Sql_Conn.Close();
		}

		return mfg;
	}

	// CleanSQL() 清除字串中有 SQL 隱碼攻擊的字串 「'」「 」「;」「--」 「|」「\t」「\n」
	public string CleanSQL(string mString)
	{
		if (mString == null)
			mString = "";
		else
		{
			mString = mString.Replace("'", "''");
			mString = mString.Replace(" ", "");
			mString = mString.Replace(";", "");
			mString = mString.Replace("--", "");
			mString = mString.Replace("|", "");
			mString = mString.Replace("\t", "");
			mString = mString.Replace("\n", "");
		}
		return mString;
	}

	// CheckSQL() 檢查字串中有 SQL 隱碼攻擊的字串 「'」「;」「--」「|」「\t」「\n」
	public bool CheckSQL(string mString)
	{
		bool mfg = false;

		if (mString == null)
			mfg = true;
		else
		{
			mfg = (mString.Contains("'")
				|| mString.Contains(" ")
				|| mString.Contains(";")
				|| mString.Contains("--")
				|| mString.Contains("|")
				|| mString.Contains("\t")
				|| mString.Contains("\n"));
		}

		return mfg;
	}

	// 心情圖示
	// 輸入參數：int	code	心情代碼
	// 傳回數值：string	image	圖示路徑
	//			 string name	圖示名稱
	public struct ImageSymbol
	{
		private int _code;
		private string _image;
		private string _name;

		public int code
		{
			set
			{
				SetCode(value);
			}
			get
			{
				return _code;
			}
		}

		public string image
		{
			get
			{
				return _image;
			}
		}

		public string name
		{
			get
			{
				return _name;
			}
		}

		private void SetCode(int code)
		{
			_code = code;
			_image = "~/images/symbol/";

			switch (code)
			{
				case 0:
					_image += "S00.gif";
					_name = "微笑";
					break;
				case 1:
					_image += "S01.gif";
					_name = "俏皮";
					break;
				case 2:
					_image += "S02.gif";
					_name = "得意";
					break;
				case 3:
					_image += "S03.gif";
					_name = "害羞";
					break;
				case 4:
					_image += "S04.gif";
					_name = "哭泣";
					break;
				case 5:
					_image += "S05.gif";
					_name = "禁言";
					break;
				case 6:
					_image += "S06.gif";
					_name = "氣憤";
					break;
				case 7:
					_image += "S07.gif";
					_name = "鄙視";
					break;
				case 8:
					_image += "S08.gif";
					_name = "無言";
					break;
				case 9:
					_image += "S09.gif";
					_name = "害怕";
					break;
				case 10:
					_image += "S10.gif";
					_name = "真棒";
					break;
				case 11:
					_image += "S11.gif";
					_name = "傷心";
					break;
				case 12:
					_image += "S12.gif";
					_name = "握手";
					break;
				case 13:
					_image += "S13.gif";
					_name = "豬頭";
					break;
				case 14:
					_image += "S14.gif";
					_name = "大便";
					break;
				case 15:
					_image += "S15.gif";
					_name = "電話連絡";
					break;
				case 16:
					_image += "S16.gif";
					_name = "OK";
					break;
				case 17:
					_image += "S17.gif";
					_name = "禮物";
					break;
				default:
					_image += "S00.gif";
					_name = "微笑";
					break;
			}
		}
	}
}