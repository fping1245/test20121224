using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;


/// <summary>
/// IDU 的摘要描述
/// </summary>
public class IDU
{
	public IDU()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    ArrayList ch = new ArrayList();
    public ArrayList dropdownclasshost()
    {
        string strSQL = "select distinct ClassMentor from Class where EndDate >getdate()";
        string strConn = "Data Source=192.168.32.37;Initial Catalog=TMS;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            ch.Add(dr["ClassMentor"]);
        }
        conn.Close();
        conn.Dispose();
        return ch;
    }

    ArrayList cb = new ArrayList();
    public ArrayList dropdownclass(string name)
    {
        string strSQL = "select ClassIDNo from Class where ClassMentor=@name AND EndDate >getdate()";
        string strConn = "Data Source=192.168.32.37;Initial Catalog=TMS;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@name", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = name;
        cmd.Parameters.Add(p1);

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            cb.Add(dr["ClassIDNo"]);
        }
        conn.Close();
        conn.Dispose();
        return cb;
    }

    string startdate;
    public string dropdowndstart(string ClassID)
    {
        string strSQL = "select StartDate from Class where ClassIDNo=@ClassID";
        string strConn = "Data Source=192.168.32.37;Initial Catalog=TMS;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@ClassID", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = ClassID;
        cmd.Parameters.Add(p1);


        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            startdate = Convert.ToDateTime((dr["StartDate"])).ToString("yyyy/MM/dd");
        }

        conn.Close();
        conn.Dispose();
        return startdate;
    }

    string enddate;
    public string dropdownend(string ClassID)
    {
        string strSQL = "select EndDate from Class where ClassIDNo=@ClassID";
        string strConn = "Data Source=192.168.32.37;Initial Catalog=TMS;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@ClassID", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = ClassID;
        cmd.Parameters.Add(p1);


        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            enddate = Convert.ToDateTime((dr["EndDate"])).ToString("yyyy/MM/dd");
        }

        conn.Close();
        conn.Dispose();
        return enddate;
    }

    string ClassRoom;
    public string dropdowndclassroom(string ClassID)
    {
        string strSQL = "select ClassRoom from Class where ClassIDNo=@ClassID";
        string strConn = "Data Source=192.168.32.37;Initial Catalog=TMS;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@ClassID", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = ClassID;
        cmd.Parameters.Add(p1);


        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            ClassRoom = (dr["ClassRoom"]).ToString();
        }

        conn.Close();
        conn.Dispose();
        return ClassRoom;
    }

    public void updatedate(string id_class, string id_classroom, string startdate, string enddate, string lab1, string lab2, string lab3, string lab4, string lab5)
    {
        string strSQL = "insert into classdetail (id_class,id_classroom,startdate,enddate,lab1,lab2,lab3,lab4,lab5) values(@id_class,@id_classroom,@startdate,@enddate,@lab1,@lab2,@lab3,@lab4,@lab5)";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);

        SqlParameter p2 = new SqlParameter("@id_classroom", SqlDbType.NVarChar, 50);
        p2.Direction = ParameterDirection.Input;
        p2.Value = id_classroom;
        cmd.Parameters.Add(p2);

        SqlParameter p3 = new SqlParameter("@startdate", SqlDbType.NVarChar, 50);
        p3.Direction = ParameterDirection.Input;
        p3.Value = startdate;
        cmd.Parameters.Add(p3);

        SqlParameter p4 = new SqlParameter("@enddate", SqlDbType.NVarChar, 50);
        p4.Direction = ParameterDirection.Input;
        p4.Value = enddate;
        cmd.Parameters.Add(p4);

        SqlParameter p5 = new SqlParameter("@lab1", SqlDbType.NVarChar, 50);
        p5.Direction = ParameterDirection.Input;
        p5.Value = lab1;
        cmd.Parameters.Add(p5);

        SqlParameter p6 = new SqlParameter("@lab2", SqlDbType.NVarChar, 50);
        p6.Direction = ParameterDirection.Input;
        p6.Value = lab2;
        cmd.Parameters.Add(p6);

        SqlParameter p7 = new SqlParameter("@lab3", SqlDbType.NVarChar, 50);
        p7.Direction = ParameterDirection.Input;
        p7.Value = lab3;
        cmd.Parameters.Add(p7);

        SqlParameter p8 = new SqlParameter("@lab4", SqlDbType.NVarChar, 50);
        p8.Direction = ParameterDirection.Input;
        p8.Value = lab4;
        cmd.Parameters.Add(p8);

        SqlParameter p9 = new SqlParameter("@lab5", SqlDbType.NVarChar, 50);
        p9.Direction = ParameterDirection.Input;
        p9.Value = lab5;
        cmd.Parameters.Add(p9);

        cmd.ExecuteNonQuery();

        conn.Close();
        conn.Dispose();

    }

    public void insertclasslist(string id_class, string CourseName, string TeacherPersonID, string TeacherName, string Length)
    {
        string strSQL = "insert into special_model (id_class,CourseName,TeacherPersonID,TeacherName,Length) values(@id_class,@CourseName,@TeacherPersonID,@TeacherName,@Length)";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);

        SqlParameter p2 = new SqlParameter("@CourseName", SqlDbType.NVarChar, 50);
        p2.Direction = ParameterDirection.Input;
        p2.Value = CourseName;
        cmd.Parameters.Add(p2);

        SqlParameter p22 = new SqlParameter("@TeacherPersonID", SqlDbType.NVarChar, 50);
        p22.Direction = ParameterDirection.Input;
        p22.Value = TeacherPersonID;
        cmd.Parameters.Add(p22);

        SqlParameter p3 = new SqlParameter("@TeacherName", SqlDbType.NVarChar, 50);
        p3.Direction = ParameterDirection.Input;
        p3.Value = TeacherName;
        cmd.Parameters.Add(p3);

        SqlParameter p4 = new SqlParameter("@Length", SqlDbType.NVarChar, 50);
        p4.Direction = ParameterDirection.Input;
        p4.Value = Length;
        cmd.Parameters.Add(p4);

        cmd.ExecuteNonQuery();

        conn.Close();
        conn.Dispose();

    }

    string pkststartdate;
    public string pkstdropdowndstart(string id_class)
    {
        string strSQL = "select StartDate from classdetail where id_class=@id_class";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);


        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            pkststartdate = Convert.ToDateTime((dr["StartDate"])).ToString("yyyy/MM/dd");
        }

        conn.Close();
        conn.Dispose();
        return pkststartdate;
    }

    string pkstenddate;
    public string pkstdropdownend(string id_class)
    {
        string strSQL = "select EndDate from classdetail where id_class=@id_class";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);


        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            pkstenddate = Convert.ToDateTime((dr["EndDate"])).ToString("yyyy/MM/dd");
        }

        conn.Close();
        conn.Dispose();
        return pkstenddate;
    }

    string pkstClassRoom;
    public string pkstdropdowndclassroom(string id_class)
    {
        string strSQL = "select id_classroom from classdetail where id_class=@id_class";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);

        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            pkstClassRoom = (dr["id_classroom"]).ToString();
        }

        conn.Close();
        conn.Dispose();
        return pkstClassRoom;
    }

    List<string> labtime = new List<string>();
    public List<string> pkstselectdate(string id_class)
    {
        string strSQL = "select lab1,lab2,lab3,lab4,lab5 from classdetail where id_class=@id_class";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);

        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            labtime.Add((dr["lab1"]).ToString());
            labtime.Add((dr["lab2"]).ToString());
            labtime.Add((dr["lab3"]).ToString());
            labtime.Add((dr["lab4"]).ToString());
            labtime.Add((dr["lab5"]).ToString());
        }
        conn.Close();
        conn.Dispose();
        return labtime;
    }

    public void pkstupdatedate(string id_class, string id_classroom, string startdate, string enddate, string lab1, string lab2, string lab3, string lab4, string lab5)
    {
        string strSQL = "update classdetail set id_classroom=@id_classroom,startdate=@startdate,enddate=@enddate,lab1=@lab1,lab2=@lab2,lab3=@lab3,lab4=@lab4,lab5=@lab5 WHERE id_class=@id_class";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);

        SqlParameter p2 = new SqlParameter("@id_classroom", SqlDbType.NVarChar, 50);
        p2.Direction = ParameterDirection.Input;
        p2.Value = id_classroom;
        cmd.Parameters.Add(p2);

        SqlParameter p3 = new SqlParameter("@startdate", SqlDbType.NVarChar, 50);
        p3.Direction = ParameterDirection.Input;
        p3.Value = startdate;
        cmd.Parameters.Add(p3);

        SqlParameter p4 = new SqlParameter("@enddate", SqlDbType.NVarChar, 50);
        p4.Direction = ParameterDirection.Input;
        p4.Value = enddate;
        cmd.Parameters.Add(p4);

        SqlParameter p5 = new SqlParameter("@lab1", SqlDbType.NVarChar, 50);
        p5.Direction = ParameterDirection.Input;
        p5.Value = lab1;
        cmd.Parameters.Add(p5);

        SqlParameter p6 = new SqlParameter("@lab2", SqlDbType.NVarChar, 50);
        p6.Direction = ParameterDirection.Input;
        p6.Value = lab2;
        cmd.Parameters.Add(p6);

        SqlParameter p7 = new SqlParameter("@lab3", SqlDbType.NVarChar, 50);
        p7.Direction = ParameterDirection.Input;
        p7.Value = lab3;
        cmd.Parameters.Add(p7);

        SqlParameter p8 = new SqlParameter("@lab4", SqlDbType.NVarChar, 50);
        p8.Direction = ParameterDirection.Input;
        p8.Value = lab4;
        cmd.Parameters.Add(p8);

        SqlParameter p9 = new SqlParameter("@lab5", SqlDbType.NVarChar, 50);
        p9.Direction = ParameterDirection.Input;
        p9.Value = lab5;
        cmd.Parameters.Add(p9);

        cmd.ExecuteNonQuery();

        conn.Close();
        conn.Dispose();

    }

    public void pkstinsertclasslist(string id_class)
    {
        string strSQL = "delete from special_model where id_class=@id_class";
        string strConn = "Data Source=192.168.32.34;Initial Catalog=PKST;user ID=sa";
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand(strSQL, conn);
        cmd.CommandType = CommandType.Text;

        conn.Open();

        SqlParameter p1 = new SqlParameter("@id_class", SqlDbType.NVarChar, 50);
        p1.Direction = ParameterDirection.Input;
        p1.Value = id_class;
        cmd.Parameters.Add(p1);

        cmd.ExecuteNonQuery();

        conn.Close();
        conn.Dispose();

    }


}