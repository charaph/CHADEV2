using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class ConnectDB
{
    private string DEF_SERVER = "";
    private string DEF_DB = "";
    private string DEF_USER = "";
    private string DEF_PW = "";
    private string MDB = "";

    public ConnectDB()
    {
        //INI FILE
        if (DEF_SERVER == "")
        {
            string[] connection = ReadIniConnection();
            DEF_SERVER = connection[0];
            DEF_USER = connection[1];
            DEF_PW = connection[2];
            DEF_DB = connection[3];
            MDB = DEF_DB;
        }
    }

    public string[] ReadIniConnection(string section = CONFIG.CONNECTION_SECTION)
    {
        string[] strings = new string[1];
        var ConnectIni = new IniFile(CONFIG.DATABASE_FILE);
        Array.Resize(ref strings, 4);

        strings[0] = ConnectIni.Read("Server", section);
        strings[1] = ConnectIni.Read("Uid", section);
        strings[2] = ConnectIni.Read("Pwd", section);
        strings[3] = ConnectIni.Read("Database", section);

        return strings;
    }

    public string[] ReadTxtConnection()
    {
        string[] strings = new string[1];
        ObjSet.TextFile FILE = new ObjSet.TextFile(CONFIG.DATABASE_FILE);
        Array.Resize(ref strings, 4);

        strings[0] = FILE.ReadFile(1);
        strings[1] = FILE.ReadFile(2);
        strings[2] = FILE.ReadFile(3);
        strings[3] = FILE.ReadFile(4);

        return strings;
    }

    //CONNECT DEFAULT
    public bool ChkConnectDB(string ServerKey)
    {
        bool ChkConnect = false;
        SqlConnection newConn = new SqlConnection();
        try
        {
            if (ServerKey.Trim() == "")
            {
                newConn = SqlStrConn();
            }
            else
            {
                newConn = SqlStrConn(ServerKey);
            }
            ChkConnect = true;

        }
        catch (Exception)
        {
            ChkConnect = false;
        }

        return ChkConnect;
    }

    public SqlConnection SqlStrCon(string ServerKey = "")
    {
        if (string.IsNullOrEmpty(ServerKey) == true)
        {
            ServerKey = "";
        }
        SqlConnection newConn = new SqlConnection();
        try
        {
            if (ServerKey == "")
            {
                newConn = SqlStrConn();
            }
            else
            {
                newConn = SqlStrConn(ServerKey);
            }

        }
        catch (Exception ex)
        {
            new Function().msgError("Connection Error : " + ex.ToString());
        }

        return newConn;
    }

    public SqlConnection SqlStrConn()
    {
        string Connection = "";
        try
        {
            Connection = "Data Source=" + DEF_SERVER + ";Initial Catalog=" + DEF_DB + ";User ID=" + DEF_USER + ";Password=" + DEF_PW + "; pooling=false; MultipleActiveResultSets=True; TimeOut=250;";
            SqlConnection connMaster = new SqlConnection(Connection);
            if (connMaster.State == ConnectionState.Open)
            {
                connMaster.Close();
            }

            connMaster.Open();
            connMaster.Close();
        }
        catch (Exception)
        {
            Connection = "";
        }

        return new SqlConnection(Connection);
    }

    //CONNECT WITH KEY
    public SqlConnection SqlStrConn(string serverKey)
    {
        string _serverDB = serverKey;
        string Connection = "";
        try
        {
            //new Function().msgInfo(DEF_SERVER);
            Connection = "Data Source=" + DEF_SERVER + ";Initial Catalog=" + DEF_DB + ";User ID=" + DEF_USER + ";Password=" + DEF_PW + "; pooling=false; MultipleActiveResultSets=True; TimeOut=250;";
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("SELECT  TOP (1) ID, SearchKey, DBName, ServerIP, SVUserName, SVPassword ");
            sbQuery.Append("FROM    " + MDB + " ");
            sbQuery.Append("WHERE   (SearchKey = '");
            sbQuery.Append(_serverDB);
            sbQuery.Append("') ");
            //new Function().msgInfo(sbQuery.ToString());
            SqlConnection connMaster = new SqlConnection(Connection);
            if (connMaster.State == ConnectionState.Open)
            {
                connMaster.Close();
            }

            connMaster.Open();

            SqlCommand cmd = new SqlCommand(sbQuery.ToString(), connMaster);
            cmd.CommandTimeout = 50;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Dispose();

            if (dr.HasRows)
            {
                dr.Read();
                Connection = "Data Source=" + dr["ServerIP"].ToString() + ";Initial Catalog=" + dr["DBName"].ToString() + ";User ID=" + dr["SVUserName"].ToString() + ";Password=" + dr["SVPassword"].ToString() + "; pooling=false; MultipleActiveResultSets=True; TimeOut=250;";
            }
            else
            {
                Connection = "";
            }
            dr.Close();
            connMaster.Close();
        }
        catch (Exception)
        {
            Connection = "";
        }

        return new SqlConnection(Connection);
    }

}

//### SET TRANSACTIONS ###
class SQLTransactions
{
    private StringBuilder _sbPacket = new StringBuilder();

    public SQLTransactions(string strSQL)
    {
        StringBuilder sbPacket = new StringBuilder();
        sbPacket.Append("BEGIN TRANSACTION [Tran1]   ");
        sbPacket.Append("BEGIN TRY   ");
        sbPacket.Append(strSQL);
        sbPacket.Append("  COMMIT TRANSACTION [Tran1] ");
        sbPacket.Append("END TRY ");
        sbPacket.Append("BEGIN CATCH ");
        sbPacket.Append("  ROLLBACK TRANSACTION [Tran1] ");
        sbPacket.Append("   DECLARE @ErrorMessage NVARCHAR(4000);  ");
        sbPacket.Append("   DECLARE @ErrorSeverity INT;  ");
        sbPacket.Append("   DECLARE @ErrorState INT;  ");
        sbPacket.Append("   SELECT  @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();  ");
        sbPacket.Append("	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState ); ");
        sbPacket.Append("END CATCH  ");

        this._sbPacket = sbPacket;
    }

    public SQLTransactions(StringBuilder sbSQL)
    {
        StringBuilder sbPacket = new StringBuilder();
        sbPacket.Append("BEGIN TRANSACTION [Tran1]   ");
        sbPacket.Append("BEGIN TRY   ");
        sbPacket.Append(sbSQL.ToString());
        sbPacket.Append("  COMMIT TRANSACTION [Tran1] ");
        sbPacket.Append("END TRY ");
        sbPacket.Append("BEGIN CATCH ");
        sbPacket.Append("  ROLLBACK TRANSACTION [Tran1] ");
        sbPacket.Append("   DECLARE @ErrorMessage NVARCHAR(4000);  ");
        sbPacket.Append("   DECLARE @ErrorSeverity INT;  ");
        sbPacket.Append("   DECLARE @ErrorState INT;  ");
        sbPacket.Append("   SELECT  @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();  ");
        sbPacket.Append("	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState ); ");
        sbPacket.Append("END CATCH  ");

        this._sbPacket = sbPacket;
    }

    public StringBuilder GetStringBuilder()
    {
        return this._sbPacket;
    }

    public string GetString()
    {
        return this._sbPacket.ToString();
    }

}


//### QUERY CLASS ###
class DBQuery
{
    string _Conn = "";
    string _strQuery = "";
    //##### RECEIVE VALUES QUERY #####
    public DBQuery(string StringQuery, string ConnectionKey = "", bool Check_Transactions = true)
    {
        this._Conn = ConnectionKey;
        this._strQuery = StringQuery;
        try
        {
            if (StringQuery.Substring(0, 6).ToUpper() == "SELECT")
            {
                this._strQuery = StringQuery;
            }
            else
            {
                if (Check_Transactions == true)
                {
                    this._strQuery = new SQLTransactions(StringQuery).GetString();
                }
            }
        }
        catch (Exception)
        {
            if (Check_Transactions == true)
            {
                this._strQuery = new SQLTransactions(StringQuery).GetString();
            }
        }
    }

    public DBQuery(StringBuilder StringBuilderQuery, string ConnectionKey = "", bool Check_Transactions = true)
    {
        this._Conn = ConnectionKey;
        this._strQuery = StringBuilderQuery.ToString();
        try
        {
            if (StringBuilderQuery.ToString().Substring(0, 6).ToUpper() == "SELECT")
            {
                this._strQuery = StringBuilderQuery.ToString();
            }
            else
            {
                if (Check_Transactions == true)
                {
                    this._strQuery = new SQLTransactions(StringBuilderQuery.ToString()).GetString();
                }
            }
        }
        catch (Exception)
        {
            if (Check_Transactions == true)
            {
                this._strQuery = new SQLTransactions(StringBuilderQuery.ToString()).GetString();
            }
        }
    }

    //##### RETURN VALUES QUERY #####
    public bool runSQL()
    {
        try
        {
            SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);
            SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
            //new Function().msgInfo(this._strQuery);
            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
            return true;
        }
        catch (Exception ex)
        {
            ;
            new Function().msgError("ERROR # " + ex.ToString());
            return false;
        }
    }


    //public bool backupSQL()
    //{
    //    try
    //    {
    //        SqlConnection Conn = new ConnectDB().SqlStrCon();
    //        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
    //        //new Function().msgInfo(this._strQuery);
    //        Conn.Open();
    //        Cmd.ExecuteNonQuery();
    //        Conn.Close();
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        new Function().msgError("ERROR # " + ex.ToString());
    //        return false;
    //    }
    //}

    public string getString()
    {
        string SendStr = "";
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);

        Conn.Open();
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr.HasRows)
        {
            dr.Read();
            SendStr = dr.GetValue(0).ToString();
        }
        else
        {
            SendStr = "";
        }
        dr.Close();
        Conn.Close();

        return SendStr;
    }

    public int getInt()
    {
        int SendInt = 0;
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);

        Conn.Open();
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr.HasRows)
        {
            dr.Read();
            if (dr.GetValue(0).ToString() == "")
            {
                SendInt = 0;
            }
            else
            {
                SendInt = Convert.ToInt32(dr.GetValue(0));
            }
        }
        else
        {
            SendInt = 0;
        }
        dr.Close();
        Conn.Close();

        return SendInt;
    }

    public double getDouble()
    {
        double SendDbl = 0;
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);

        Conn.Open();
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr.HasRows)
        {
            dr.Read();
            if (dr.GetValue(0).ToString() == "")
            {
                SendDbl = 0;
            }
            else
            {
                SendDbl = Convert.ToDouble(dr.GetValue(0));
            }

        }
        else
        {
            SendDbl = 0;
        }
        dr.Close();
        Conn.Close();

        return SendDbl;
    }

    public bool getCheck()
    {
        bool SendChk = false;
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);

        Conn.Open();
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr.HasRows)
        {
            SendChk = true;
        }
        else
        {
            SendChk = false;
        }
        Conn.Close();

        return SendChk;
    }

    public SqlDataReader getDataReader()
    {
        SqlDataReader SendDR = null;
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        Conn.Open();
        SendDR = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return SendDR;
    }

    public int getCount()
    {
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);
        Conn.Open();
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr.Cast<object>().Count();
    }

    public DataTable getDataTable()
    {
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);
        Conn.Open();
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);

        DataTable dt = new DataTable();
        dt.Load(Cmd.ExecuteReader());
        Conn.Close();

        //Clone DataTable
        DataTable dtCloned = dt.Clone();
        int y = 0;
        foreach (DataColumn col in dt.Columns)
        {
            dtCloned.Columns[y].DataType = typeof(String);
            dtCloned.Columns[y].ReadOnly = false;
            y++;
        }
        foreach (DataRow row in dt.Rows)
        {
            dtCloned.ImportRow(row);
        }

        return dtCloned;
    }

    public DataSet getDataSet()
    {
        SqlConnection Conn = new ConnectDB().SqlStrCon(this._Conn);
        SqlCommand Cmd = new SqlCommand(this._strQuery, Conn);
        SqlDataAdapter da = new SqlDataAdapter(Cmd);
        DataSet ds = new DataSet();
        Conn.Open();
        da.Fill(ds, "setTable");
        Conn.Close();
        return ds;
    }
}



