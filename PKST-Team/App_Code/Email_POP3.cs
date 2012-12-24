//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	POP3 通訊處理 (Post Office Protocol Version 3)
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

public class Email_POP3
{
	private string _pop3_host = "";						// POP3 主機名稱 例：msa.hinet.net
	private int _pop3_port = 110;						// POP3 通訊埠 例：110
	private string _pop3_id = "";						// 登入帳號
	private string _pop3_pw = "";						// 登入密碼

	private TcpClient _pop3_client = new TcpClient();	// TCP/IP Client 元件

	private NetworkStream _netstream;
	private StreamReader _st_reader;
	private StreamWriter _st_writer;

	private string _st_line = "";						// 最後一行接收到的資料
	private bool _st_read_flag;							// 最後讀取的狀態 true : 正確， false : 失敗

	private int _mail_num = -1;							// 郵件數量 (-1 表示尚未執行 Stat 指令或是執行失敗)
	private int _mail_total = -1;						// 郵件大小 (-1 表示尚未執行 Stat 指令或是執行失敗)
	private int[] _mail_size;							// 個別郵件大小
	private string[] _mail_uidl;						// 個別郵件的唯一識別碼

	public string POP3_Host
	{
		set
		{
			this._pop3_host = value;
		}
		get
		{
			return _pop3_host;
		}
	}

	public int POP3_Port
	{
		set
		{
			this._pop3_port = value;
		}
		get
		{
			return _pop3_port;
		}
	}

	public string POP3_ID
	{
		set
		{
			this._pop3_id = value;
		}
		get
		{
			return _pop3_id;
		}
	}

	public string POP3_PW
	{
		set
		{
			this._pop3_pw = value;
		}
		get
		{
			return _pop3_pw;
		}
	}

	public string Get_Line
	{
		get
		{
			return _st_line;
		}
	}

	public bool Get_Read_Flag
	{
		get
		{
			return _st_read_flag;
		}
	}

	public int Mail_Num
	{
		get
		{
			return _mail_num;
		}
	}

	public int Mail_Total
	{
		get
		{
			return _mail_total;
		}
	}

	public int Mail_Size(int aCnt)
	{
		int intSize = -2;

		if (_mail_size != null)
		{
			if (_mail_size.Length > aCnt && aCnt > -1)
				intSize = _mail_size[aCnt];
			else
				intSize = -1;
		}

		return intSize;
	}

	public string Mail_UIDL(int aCnt)
	{
		string strUIDL = "-2";

		if (_mail_uidl != null)
		{
			if (_mail_uidl.Length > aCnt && aCnt > -1)
			{
				strUIDL = _mail_uidl[aCnt];
			}
			else
				strUIDL = "-1";
		}

		return strUIDL;
	}

	public int Connect()
	{
		int ckint = 1;

		if (_pop3_host == "")
			ckint = -2;
		else if (_pop3_port < 1 || _pop3_port > 65534)
			ckint = -3;
	
		if (ckint == 1)
		{
			try
			{
				_pop3_client.Connect(_pop3_host, _pop3_port);
				if (_pop3_client.Connected)
				{
					_netstream = _pop3_client.GetStream();
					_st_reader = new StreamReader(_netstream, Encoding.Default);
					_st_writer = new StreamWriter(_netstream, Encoding.Default);
					if (POP3_Receive())
					{
						ckint = 0;
					}
					else
					{
						_netstream.Close();
						_st_reader.Close();
						_st_writer.Close();
						_pop3_client.Close();

						_netstream.Dispose();
						_st_reader.Dispose();
						_st_writer.Dispose();
						ckint = -4;
					}
				}
				else
					ckint = -1;
			}
			catch
			{
				ckint = -1;
			}
		}
		return ckint;
	}

	public int Login()
	{
		int ckint = 0;

		if (_pop3_client.Connected)
		{
			if (POP3_Send("USER " + _pop3_id))
			{
				if (!POP3_Receive())
					ckint = -2;
			}
			else
				ckint = -2;

			if (ckint == 0)
			{
				if (POP3_Send("PASS " + _pop3_pw))
				{
					if (!POP3_Receive())
						ckint = -3;
				}
				else
					ckint = -3;
			}
		}
		else
			ckint = -1;

		return ckint;
	}

	public string POP3_Read()
	{
		try
		{
			_st_line = _st_reader.ReadLine();
			_st_read_flag = true;
		}
		catch
		{
			_st_line = "";
			_st_read_flag = false;
		}

		return _st_line;
	}

	public bool POP3_Receive()
	{
		bool ckbool = false;
		string str_read = "";

		try
		{
			str_read = _st_reader.ReadLine();
			if (str_read.Substring(0, 3) == "+OK")
				ckbool = true;
			else
				ckbool = false;
		}
		catch
		{
			ckbool = false;
		}

		_st_line = str_read;
		_st_read_flag = ckbool;

		return ckbool;
	}

	public bool POP3_Send(string str_send)
	{
		bool ckbool = false;

		try
		{
			// 將串流中的資料清空
			_st_reader.DiscardBufferedData();

			_st_writer.WriteLine(str_send);
			_st_writer.Flush();

			ckbool = true;
		}
		catch
		{
			ckbool = false;
		}

		return ckbool;
	}

	public bool POP3_Send_Check(string str_send)
	{
		bool ckbool = false;
		string str_read = "";

		try
		{
			// 將串流中的資料清空
			_st_reader.DiscardBufferedData();

			_st_writer.WriteLine(str_send);
			_st_writer.Flush();

			str_read = _st_reader.ReadLine();
			if (str_read.Substring(0, 3) == "+OK")
				ckbool = true;
			else
				ckbool = false;
		}
		catch
		{
			ckbool = false;
		}

		_st_line = str_read;
		_st_read_flag = ckbool;

		return ckbool;
	}

	public int Stat()
	{
		int ckint = 0, mailnum = 0, mailsize = 0;

		if (_pop3_client.Connected)
		{
			if (POP3_Send("STAT"))
			{
				if (POP3_Receive())
					ckint = 0;
				else
					ckint = -2;
			}
			else
				ckint = -2;
		}
		else
			ckint = -1;

		#region 資料存入 _mail_num 及 _mail_total
		if (ckint < 0)
		{
			_mail_num = -1;
			_mail_total = -1;
		}
		else
		{
			string[] strtmp = _st_line.Split(' ');

			if (int.TryParse(strtmp[1], out mailnum))
				_mail_num = mailnum;
			else
			{
				_mail_num = -1;
				ckint = -3;
			}

			if (int.TryParse(strtmp[2], out mailsize))
				_mail_total = mailsize;
			else
			{
				_mail_total = -1;
				ckint = -3;
			}
			strtmp = null;
		}
		#endregion

		return ckint;
	}

	public int ListAll()
	{
		int ckint = 0, icnt = 0, mcnt = 0, lcnt = 0, scnt = 0;
		string tmpstr = "", recstr = "";

		_mail_size = null;

		if (_pop3_client.Connected)
		{
			if (POP3_Send("LIST"))
			{
				if (POP3_Receive())
				{
					try
					{
						do
						{
							tmpstr = _st_reader.ReadLine();

							if (tmpstr.Substring(0, 1) != ".")
							{
								recstr += tmpstr + "#";
							}
						} while (tmpstr != ".");
					}
					catch
					{
						ckint = -13;
					}

					if (ckint == 0 && recstr.Length > 0)
					{
						recstr = recstr.Substring(0, recstr.Length - 1);

						string[] _tmp_size = recstr.Split('#');

						mcnt = _tmp_size.Length;

						// 填入數值
						_mail_size = new int[mcnt];

						for (icnt = 0; icnt < mcnt; icnt++)
						{
							lcnt = _tmp_size[icnt].IndexOf(" ") + 1;
							scnt = 0;

							if (int.TryParse(_tmp_size[icnt].Substring(lcnt, _tmp_size[icnt].Length - lcnt), out scnt))
								_mail_size[icnt] = scnt;
							else
								_mail_size[icnt] = 0;
						}
						_tmp_size = null;
					}
				}
				else
					ckint = -12;
			}
			else
				ckint = -12;
		}
		else
			ckint = -1;

		if (ckint < 0)
			_mail_size = null;

		return ckint;
	}

	public int UIDLAll()
	{
		int ckint = 0, icnt = 0, lcnt = 0, mcnt = 0;
		string recstr = "", tmpstr = "";

		if (_pop3_client.Connected)
		{
			if (POP3_Send("UIDL"))
			{
				if (POP3_Receive())
				{
					try
					{
						do
						{
							tmpstr = _st_reader.ReadLine();

							if (tmpstr.Substring(0, 1) != ".")
							{
								recstr += tmpstr + "#";
							}
						} while (tmpstr != ".");
					}
					catch
					{
						ckint = -24;
					}

					if (ckint == 0 && recstr.Length > 0)
					{
						recstr = recstr.Substring(0, recstr.Length - 1);

						_mail_uidl = recstr.Split('#');

						mcnt = _mail_uidl.Length;

						// 去除編號
						for (icnt = 0; icnt < mcnt; icnt++)
						{
							lcnt = _mail_uidl[icnt].IndexOf(" ") + 1;
							_mail_uidl[icnt] = _mail_uidl[icnt].Substring(lcnt, _mail_uidl[icnt].Length - lcnt);
						}
					}
				}
				else
					ckint = -22;
			}
			else
				ckint = -22;
		}
		else
			ckint = -1;

		if (ckint < 0)
			_mail_uidl = null;

		return ckint;
	}

	public int StatAll()
	{
		int ckint = 0;
		ckint = Stat();

		#region 取得個別郵件的大小及UIDL
		if (ckint == 0 && _mail_num > 0)
		{
				ckint = ListAll();
		}
		#endregion

		#region 取得個別郵件的UIDL
		if (ckint == 0 && _mail_num > 0)
		{
			ckint = UIDLAll();
		}
		#endregion

		return ckint;
	}

	public bool Dele_Mail(int mail_cnt)
	{
		return POP3_Send_Check("DELE " + mail_cnt.ToString());
	}

	public int Dele_UIDL(string sUIDL)
	{
		string tmpstr = "", mUIDL = "";
		int icnt = 0, mcnt = 0, ckint = -9;

		if (_pop3_client.Connected)
		{
			if (POP3_Send_Check("UIDL"))
			{
				do
				{
					try
					{
						tmpstr = _st_reader.ReadLine();

						icnt = tmpstr.IndexOf(" ");
						if (icnt > 0)
						{
							mUIDL = tmpstr.Substring(icnt + 1);

							// 核對識別碼是否相符
							if (mUIDL == sUIDL)
							{
								int.TryParse(tmpstr.Substring(0, icnt), out mcnt);		// 取得臨時郵件序號
								if (mcnt > 0)
								{
									// 刪除郵件
									if (Dele_Mail(mcnt))
										ckint = 0;
									else
										ckint = -5;
								}
								else
									ckint = -4;

								tmpstr = ".";
							}
						}
					}
					catch
					{
						tmpstr = ".";
						ckint = -3;
					}
				} while (tmpstr != ".");

				// 將串流中的資料清空
				_st_reader.DiscardBufferedData();
			}
			else
			{
				ckint = -2;
			}
		}
		else
		{
			ckint = -1;
		}

		return ckint;
	}

	public string Get_Topic(int mail_cnt)
	{
		StringBuilder sb_topic = new StringBuilder();
		string tmpstr = "";

		if (_pop3_client.Connected)
		{
			// 將串流中的資料清空
			_st_reader.DiscardBufferedData();

			if (POP3_Send_Check("TOP " + mail_cnt.ToString() + " 0"))
			{
				try
				{
					do
					{
						sb_topic.Append(tmpstr);
						tmpstr = _st_reader.ReadLine() + "\r\n";
					} while (tmpstr != ".\r\n");
				}
				catch
				{
					sb_topic.Append("\r\n<Error 3>");
				}
			}
			else
				sb_topic.Append("\r\n<Error 2>");

			// 將串流中的資料清空
			_st_reader.DiscardBufferedData();
		}
		else
			sb_topic.Append("\r\n<Error 1>");

		return sb_topic.ToString();
	}

	public string Get_Mail(int mail_cnt)
	{
		StringBuilder sb_content = new StringBuilder();
		string tmpstr = "";

		if (_pop3_client.Connected)
		{
			// 將串流中的資料清空
			_st_reader.DiscardBufferedData();

			if (POP3_Send_Check("RETR " + mail_cnt.ToString()))
			{
				try
				{
					do
					{
						sb_content.Append(tmpstr);
						tmpstr = _st_reader.ReadLine() + "\r\n";
					} while (tmpstr != ".\r\n");
				}
				catch
				{
					sb_content.Append("\r\n<Error 3>");
				}
			}
			else
				sb_content.Append("\r\n<Error 2>");

			// 將串流中的資料清空
			_st_reader.DiscardBufferedData();
		}
		else
			sb_content.Append("\r\n<Error 1>");

		return sb_content.ToString();
	}

	public bool Close()
	{
		bool ckbool = false;

		try
		{
			if (_pop3_client.Connected)
			{
				POP3_Send("QUIT");		// 送出結束指令

				#region 釋放相關資源
				_st_reader.Close();
				_st_reader.Dispose();

				_st_writer.Close();
				_st_writer.Dispose();

				_netstream.Close();
				_netstream.Dispose();
				#endregion

				_pop3_client.Close();
				_pop3_client = null;

				ckbool = true;
			}
		}
		catch
		{
			ckbool = false;
		}

		return ckbool;
	}
}
