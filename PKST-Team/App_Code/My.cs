//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	使用 VB 的 My 物件
//---------------------------------------------------------------------------- 

using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.ApplicationServices;

public class My
{
	static private Computer my_Computer = new Computer();
	static private WindowsFormsApplicationBase my_Application = new WindowsFormsApplicationBase();
	static private User my_User = new User();

	public static ServerComputer Computer
	{
		get
		{
			return my_Computer;
		}
	}

	public static WindowsFormsApplicationBase Application
	{
		get
		{
			return my_Application;
		}
	}

	public static User User
	{
		get
		{
			return my_User;
		}
	}
}
