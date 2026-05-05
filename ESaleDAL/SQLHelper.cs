using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Net;
using System.IO;
namespace ESaleDAL
{
    #region SQLHelper
    public class SQLHelper
    {

       
        #region Private Vars
        SqlConnection _Con;
        OleDbConnection _ConAccess;

        bool _IsCommandExecuted = false;
        #endregion

        #region Constructor
        public SQLHelper()
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
                try
                {
                    DA.Fill(_DT);
                    _IsCommandExecuted = true;
                }
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
        public object CheckPermission(string RoleId, string ScreenCode)
        {
            try
            {
                Hashtable ht = new Hashtable();
                string whereCondition = "  Org.OrganizationRoleId like '" + RoleId + "' and SM.ScreenCode  like '" + ScreenCode + "'";
                ht.Add("@WhereCondition", whereCondition);
                ht.Add("@Transaction", "GetAllScreensAndRoles");
                //objDStandards = new DStandards();
                object obj = new object();
                obj = ExecuteSP("sp_getdatascreenmaster", ht);
                //obj = objDStandards.DCheckPermissions(ht);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
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
                    if (dr.Read())
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

        public DataTable TableRetrieval(string query)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand Com = new SqlCommand();
                Com.CommandType = CommandType.Text;
                Com.Connection = _Con;
                Com.CommandText = query;
                _Con.Open();
                SqlDataAdapter adap = new SqlDataAdapter(Com);

                adap.Fill(_dt);

            }
            catch (SystemException ex)
            {
                throw (ex);

            }
            finally
            {

                _Con.Close();
            }
            return _dt;
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
            DataSet DS = new DataSet();
            SqlCommand Com = new SqlCommand();
            Com.Connection = _Con;
            _Con.Open();
            //using(_Con)
            {
                try
                {
                    Com.CommandType = CommandType.StoredProcedure;
                    Com.CommandText = SPName;
                    Com.CommandTimeout = 100000;

                    foreach (DictionaryEntry DE in Params)
                    {
                        SqlParameter PS = new SqlParameter();
                        PS.ParameterName = DE.Key.ToString();
                        PS.Value = DE.Value;
                        Com.Parameters.Add(PS);
                    }
                    SqlDataAdapter SDA = new SqlDataAdapter(Com);
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
            DataSet DS = new DataSet();
            SqlCommand Com = new SqlCommand();
            //Com.Connection = _Con;
            _Con.Open();
            //using(_Con)
            {
                try
                {
                    Com.CommandType = CommandType.StoredProcedure;
                    Com.CommandText = SPName;
                    Com.Connection = _Con;
                    foreach (SqlParameter PS in Params)
                    { Com.Parameters.Add(PS); }
                    SqlDataAdapter SDA = new SqlDataAdapter(Com);
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

        public DataSet ExecuteGetSP(string SPName, string Cond, string str1)
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


        public void Close()
        {
            if (_Con.State == ConnectionState.Open)
            { _Con.Close(); }
        }



        #endregion

        #region Private Methods
        private void DefaultCon()
        {
            _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["eSaleConstr"].ConnectionString);
            //string eSaleConstr = "Data Source=DEVSRV;Initial Catalog=ESM14OCT2010;User ID=sa;Password=eis@123";
            //_Con = new SqlConnection(eSaleConstr);
        }
        #endregion

        #region SMS Function
        // Sending an sms alert to the parents using HttpWebRequest ......
        public string SmsFunction(string Numbers, string SmsText)
        {
            string smsresult, SMSURL, SMSUserName, SMSPassword, SMSFrom, TO, SMSText, FullUri;
            Hashtable ht = new Hashtable();
            ht.Add("@Transaction", "GETSMSDETAILS");
            string wherecondition = "sd.CURRENTPREFRENCE=1";
            ht.Add("@wherecondition", wherecondition);
            DataSet ds = new DataSet();
            ds = ExecuteSP("Sp_GetData", ht);
            SMSURL = ds.Tables[0].Rows[0]["SMSURL"].ToString();
            SMSUserName = ds.Tables[0].Rows[0]["SMSUSERNAME"].ToString();
            SMSPassword = ds.Tables[0].Rows[0]["SMSPASSWORD"].ToString();
            SMSFrom = ds.Tables[0].Rows[0]["SMSFROM"].ToString();
            SMSText = SmsText;

            TO = Numbers;
            //SMSURL = ConfigurationManager.AppSettings["SMSURL"].ToString();
            //SMSUserName = ConfigurationManager.AppSettings["SMSUserName"].ToString();
            //SMSPassword = ConfigurationManager.AppSettings["SMSPassword"].ToString();
            //SMSFrom = ConfigurationManager.AppSettings["SMSFrom"].ToString();
            //SMSText = ConfigurationManager.AppSettings["SMSText"].ToString();           
            //"http://202.62.67.34/smpp.sms?username=shadaan&password=8345&to=917842730126&text=Hi Sabita&from=Enhance";
            FullUri = SMSURL + "username=" + SMSUserName + "&password=" + SMSPassword + "&to=" + TO + "&text=" + SMSText + "&from=" + SMSFrom;
            smsresult = GetPageContent(FullUri);

            return smsresult;
        }

        private static string GetPageContent(string FullUri)
        {
            try
            {
                CookieContainer Cookies = new CookieContainer(); ;
                HttpWebRequest Request;
                StreamReader ResponseReader;
                Request = ((HttpWebRequest)(WebRequest.Create(FullUri)));
                Request.CookieContainer = Cookies;
                ResponseReader = new StreamReader(Request.GetResponse().GetResponseStream());
                return ResponseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion
    }



    #endregion
}
