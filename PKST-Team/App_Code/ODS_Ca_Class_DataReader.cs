//---------------------------------------------------------------------------- 
//程式功能	產生 Ca_Class 泛型集合物件
//備註說明	重要程度分級代碼
//---------------------------------------------------------------------------- 
using System.Collections.Generic;

public class ODS_Ca_Class_DataReader
{
	public List<Ca_Class> GetCa_Class()
	{
		List<Ca_Class> Ca_ClassData = new List<Ca_Class>();

		Ca_ClassData.Add(new Ca_Class("重要", 1));
		Ca_ClassData.Add(new Ca_Class("普通", 2));
		Ca_ClassData.Add(new Ca_Class("不重要", 3));

		return Ca_ClassData;
	}
}

public class Ca_Class
{
	private string C_Name;
	private int C_Value;

	public Ca_Class(string _C_Name, int _C_Value)
	{
		C_Name = _C_Name;
		C_Value = _C_Value;
	}

	public string Ca_Class_Name
	{
		get
		{
			return C_Name;
		}

		set
		{
			C_Name = value;
		}
	}

	public int Ca_Class_Value
	{
		get
		{
			return C_Value;
		}
		set
		{
			C_Value = value;
		}
	}
}