//---------------------------------------------------------------------------- 
//程式功能	產生 DataNumber 泛型集合物件
//備註說明	輸入 MaxCnt 即自動產生指定範圍的連續數字資料
//---------------------------------------------------------------------------- 
using System.Collections.Generic;

public class ODS_DataNumber_DataReader
{
	public List<DataNumber> GetDataNumber(int MinCnt, int MaxCnt)
	{
		int iCnt = 1;

		List<DataNumber> DataNumberData = new List<DataNumber>();

		for (iCnt = MinCnt; iCnt <= MaxCnt; iCnt++)
		{
			DataNumberData.Add(new DataNumber(iCnt.ToString(), iCnt));
		}

		return DataNumberData;
	}
}

public class DataNumber
{
	private string M_Name;
	private int M_Value;

	public DataNumber(string _M_Name, int _M_Value)
	{
		M_Name = _M_Name;
		M_Value = _M_Value;
	}

	public string DataNumber_Name
	{
		get
		{
			return M_Name;
		}

		set
		{
			M_Name = value;
		}
	}

	public int DataNumber_Value
	{
		get
		{
			return M_Value;
		}
		set
		{
			M_Value = value;
		}
	}
}