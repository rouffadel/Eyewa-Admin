using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;


	#region MySql
	public class MySql
    {
        #region Private Vars
        SqlConnection _Con;
        bool _IsCommandExecuted = false;
        #endregion

        #region Constructor
        public MySql()
        { DefaultCon(); }
        #endregion

        #region Public Proporties
        public SqlConnection Con
        { get { return _Con; } }
        #endregion

        #region Public Methods
        public DataSet ExecuteQueries(string Queries)
        {
            DataSet _DT = new DataSet();
            SqlCommand Com = new SqlCommand(Queries, _Con);
            _Con.Open();
        //    using (_Con)
            {
                SqlDataAdapter DA = new SqlDataAdapter(Com);
                try { DA.Fill(_DT); _IsCommandExecuted = true; }
                catch (Exception Ex) { throw Ex; }
                finally
                {
                    DA.Dispose();
                    Com.Dispose();
                    _Con.Close();
                }
            }
            return _DT;
        }
        public Hashtable ExecuteSingleRecord(string Query)
        {
            Hashtable HT = new Hashtable();
            SqlCommand Com = new SqlCommand(Query, _Con);
            
            _Con.Open();
          //  using (_Con)
            {
                try
                {
                    SqlDataReader dr = Com.ExecuteReader();
                    if(dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        { HT.Add(dr.GetName(i), dr[i]); }
                    }
                    _IsCommandExecuted = true;
                }
                catch (Exception Ex) { throw Ex; }
                finally
                {
                    Com.Dispose();
                    _Con.Close();
                }
            }
            return HT;
        }
        public bool HasRecords(string Query)
        {
            bool ret = false;
            SqlCommand Com = new SqlCommand(Query, _Con);

            _Con.Open();
            //using (_Con)
            {
                try
                {
                    SqlDataReader dr = Com.ExecuteReader();
                    ret = dr.HasRows;
                    _IsCommandExecuted = true;
                }
                catch (Exception Ex) { throw Ex; }
                finally
                {
                    Com.Dispose();
                    _Con.Close();
                }
            }
            return ret;
        }
        public bool ExecuteNonQuery(string Query)
        {
            bool ret = false;
            SqlCommand Com = new SqlCommand(Query, _Con);
            _Con.Open();
            //using (_Con)
            {
                try 
                { 
                  ret = (Com.ExecuteNonQuery() > 0);
                  _IsCommandExecuted = true;
                }
                catch (Exception Ex) { throw Ex; }
                finally
                {
                    Com.Dispose();
                    _Con.Close();
                }
            }
            return ret;
        }
        public string ExecuteScalar(string Query)
        {
            string ret = string.Empty;
            SqlCommand Com = new SqlCommand(Query, _Con);
            _Con.Open();
            //using (_Con)
            {
                try { object obj = Com.ExecuteScalar(); if (obj != null) ret = obj.ToString(); _IsCommandExecuted = true; }
                catch (Exception Ex) { throw Ex; }
                finally
                {
                    Com.Dispose();
                    _Con.Close();
                    
                }
            }
            return ret;
        }
		public DataSet ExecuteSP(string SPName, Hashtable Params)
		{
			/*******
			 * Pass Hashtable Parameters as @ParameterName as Keys and Value as hashtable Value
			 * http://www.csharp-station.com/Tutorials/AdoDotNet/Lesson07.aspx
			 * *******/
			DataSet DS=new DataSet();
			SqlCommand Com=new SqlCommand();
			Com.Connection=_Con;
			_Con.Open();
			//using(_Con)
			{
				try
				{
					Com.CommandType=CommandType.StoredProcedure;
					Com.CommandText=SPName;
					foreach(DictionaryEntry DE in Params)
					{
						SqlParameter PS=new SqlParameter();
						PS.ParameterName=DE.Key.ToString();
						PS.Value=DE.Value;
						Com.Parameters.Add(PS);
					}
					SqlDataAdapter SDA=new SqlDataAdapter(Com);
					SDA.Fill(DS);
				}
				catch (Exception Ex) { throw Ex; }
				finally 
				{
					Com.Dispose();
					_Con.Close();
				}
			}
			return DS;
		}
		public DataSet ExecuteSP(string SPName, SqlParameterCollection Params)
		{
			/*******
			 * Pass Hashtable Parameters as @ParameterName as Keys and Value as hashtable Value
			 * http://www.csharp-station.com/Tutorials/AdoDotNet/Lesson07.aspx
			 * *******/
			DataSet DS=new DataSet();
			SqlCommand Com=new SqlCommand();
			Com.Connection=_Con;
			_Con.Open();
			//using(_Con)
			{
				try
				{
					Com.CommandType=CommandType.StoredProcedure;
					Com.CommandText=SPName;
					foreach(SqlParameter PS in Params)
					{Com.Parameters.Add(PS); }
					SqlDataAdapter SDA=new SqlDataAdapter(Com);
					SDA.Fill(DS);
				}
				catch (Exception Ex) { throw Ex; }
				finally 
				{
					Com.Dispose();
					_Con.Close();
				}
			}
			return DS;
		}

        public DataSet ExecuteGetSP(string SPName, string Cond,string str1)
        {
            //int i;
            DataSet DS = new DataSet();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Con.Open();
            try
            {
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = SPName;
                string[] strParams = Cond.Split('$');
                string[] strParamsNames = str1.Split('$');
                for (int i = 0; i < strParams.Length; i++)
                {
                    Cmd.Parameters.Add(strParamsNames[i].ToString(), SqlDbType.NVarChar, 10);
                    Cmd.Parameters[i].Value = strParams[i];
                }
                    //Cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 10);
                    //Cmd.Parameters[0].Value = strParams[0];
                    //Cmd.Parameters.Add("@EMailId", SqlDbType.NVarChar, 10);
                    //Cmd.Parameters[1].Value = strParams[1];
                    //Cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar, 10);
                    //Cmd.Parameters[2].Value = strParams[2];
          
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(DS);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cmd.Dispose();
                Con.Close();
            }
            return DS;
        }

        public DataSet ExecuteGetVendorsSP(string SPName, string Cond, string str1)
        {
            //int i;
            DataSet DS = new DataSet();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Con;
            Con.Open();
            try
            {
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = SPName;
                string[] strParams = Cond.Split('$');
                string[] strParamsNames = str1.Split('$');
                for (int i = 0; i < strParams.Length; i++)
                {
                    Cmd.Parameters.Add(strParamsNames[i].ToString(), SqlDbType.NVarChar, 10);
                    Cmd.Parameters[i].Value = strParams[i];
                }

                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(DS);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cmd.Dispose();
                Con.Close();
            }
            return DS;
        }
        //public int GetMaxID(string ColumnName, string TableName)
        //{
        //    //int ret = 0;
        //    //string tid = ExecuteScalar("Select Max(" + ColumnName + ") as MAXID from " + TableName);
        //    //if (CommonFunctions.isValidInt(tid)) { ret = Convert.ToInt32(tid); }
        //    //return ret;
        //}
        public void Close()
        {
            if (_Con.State == ConnectionState.Open)
            { _Con.Close(); }
        }
		
		
		
        #endregion

        #region Private Methods
        private void DefaultCon()
        { _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["eSaleConstr"].ConnectionString); }
        #endregion
    }
    
	#endregion

	#region MySqlFunctions
	public class MySqlFunctions
	{
		#region GetInsString(string MainString)
		public static string GetInsString(string MainString)
		{
			MainString = MainString.Replace("'NULL'", "NULL");
			return MainString;
		}
		#endregion

		#region ReplaceSingleQuote(string text)
		public static string ReplaceSingleQuote(string text)
		{
			if (text != null)
			{ return text.Trim().Replace("'", "''"); }
			else { return text; }
		}
		#endregion
	}
	#endregion
