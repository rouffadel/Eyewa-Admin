using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Web.Security;





    /// <summary>
    /// Summary description for GlobalDeclarations
    /// </summary>
    public class GlobalDeclarations
    {
        SqlCommand cmd = new SqlCommand();
        MySql objSQLHelper = new MySql();
       public static bool addPermission ;
       public static bool viewPermission;
        public GlobalDeclarations()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void WriteError(string errorMessage, string Traceinfo, string Username, string Mode)//,int i,int j,string username,
        {
            try
            {
                //DateTime.Today.Date,DateTime.Today.Day,DateTime.Today.Year
                string path = "~/ErrorLog/" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();

                }
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    //w.WriteLine("\r\nLog Entry : ");
                    //w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    //string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                    //              ". Error Message:" + errorMessage +
                    //              ". USERNAME:" + Username +
                    //". LINE NO:" + i +
                    //". USERNAME:" + username + 
                    //". DATE:" + System.DateTime.Now.ToShortDateString() +
                    //".ERROR:" + Traceinfo;
                    w.WriteLine("Error in: " + System.Web.HttpContext.Current.Request.Url.ToString());
                    w.WriteLine("Error Message:" + errorMessage);
                    w.WriteLine("ERROR:" + Traceinfo);
                    w.WriteLine("USERNAME:" + Username);

                    w.WriteLine("MODE:" + Mode);
                    w.WriteLine("====================================================================================================================");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }

        }

        private void WriteError(string p)
        {
            string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-mm-yy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {

                string err = p;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }
        }



        //public void WriteError(string p)
        //{
        //    string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-mm-yy") + ".txt";
        //    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
        //    {
        //        File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
        //    }
        //    using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
        //    {

        //        string err = p;
        //        w.WriteLine(err);
        //        w.WriteLine("__________________________");
        //        w.Flush();
        //        w.Close();
        //    }
        //}



        #region SMS Function
        // Sending an sms alert to the parents using HttpWebRequest ......
        public string SmsFunction(string Numbers, string SmsText)
        {
            string smsresult, SMSURL, SMSUserName, SMSPassword, SMSFrom, TO, SMSText, FullUri;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@Transaction", "GETSMSDETAILS");
                string wherecondition = "sd.CURRENTPREFRENCE=1";
                ht.Add("@wherecondition", wherecondition);
                DataSet ds = new DataSet();
                // objSQLHelper = new SQLHelper();
                ds = objSQLHelper.ExecuteSP("SP_GetDataNew", ht);
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
                SMSURL = SMSURL.Replace("$username", SMSUserName);
                SMSURL = SMSURL.Replace("$password", SMSPassword);
                FullUri = SMSURL + "&to=" + TO + "&text=" + SMSText + "&from=" + SMSFrom;
                smsresult = GetPageContent(FullUri);
                return smsresult;
            }
            catch (Exception ex)
            {
                throw (ex);  
            }
            finally
            {
              
            }
        }
        public void CheckPermission(string RoleId, string ScreenCode)
        {
            
            try
            {
                objSQLHelper = new MySql();
                Hashtable ht = new Hashtable();
                string whereCondition = "r.ROLEID like '" + RoleId + "' and sm.SCREENCODE  like '" + ScreenCode + "'";
                ht.Add("@WhereCondition", whereCondition);
                ht.Add("@Transaction", "GetAllScreensForLeftForm");
                //objDStandards = new DStandards();
                object obj = new object();
                obj = objSQLHelper.ExecuteSP("SP_GetDataScreenMaster", ht);
                //obj = objDStandards.DCheckPermissions(ht);
                DataSet ds = (DataSet)obj;
                addPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADD"].ToString());
                viewPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["VIEW"].ToString());

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
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
        public string SendEmail(MailMessage mm)
        {
            string EmailFrom, EmailPassword, EmailSMTP;
            int EmailPort;
            try
            {
                objSQLHelper = new MySql();
                Hashtable ht = new Hashtable();
                ht.Add("@Transaction", "Emaildetails");
                string wherecondition = " Currentprefrence=1";
                ht.Add("@wherecondition", wherecondition);
                DataSet ds = new DataSet();

                ds = objSQLHelper.ExecuteSP("SP_GetDataNew", ht);
                EmailFrom = ds.Tables[0].Rows[0]["EmailFrom"].ToString();
                EmailPassword = ds.Tables[0].Rows[0]["EmailPassword"].ToString();
                EmailSMTP = ds.Tables[0].Rows[0]["EmailSMTP"].ToString();
                EmailPort =Convert.ToInt32(ds.Tables[0].Rows[0]["EmailPort"].ToString());
                mm.From = new MailAddress(EmailFrom);               
                mm.IsBodyHtml = false;
                mm.Priority = MailPriority.High;                
                SmtpClient sC = new SmtpClient();
                sC.Host =EmailSMTP;                
             // SmtpDeliveryMethod sdm  = new SmtpDeliveryMethod();              
                sC.DeliveryMethod = SmtpDeliveryMethod.Network;
                sC.UseDefaultCredentials = false;
                sC.EnableSsl = false;
                sC.Port = EmailPort;
                sC.Credentials = new NetworkCredential(EmailFrom,EmailPassword);
                sC.Send(mm);
                return "Success";
            }
            catch (Exception ex)
            { throw (ex); }

        }

        #region ConverttoValidDate(string date)
        public static string converttodate(string date)
        {
            try
            {

                string[] _date = date.Split('-');
                if (_date[0].Length == 1)
                {
                    _date[0] = 0 + _date[0];
                }
                date = _date[1] + "-" + _date[0] + "-" + _date[2];
                return date;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public string GetImageURL(string Location,string ImageId)
        {
            string url = (HttpContext.Current.Request.Url).ToString();
            url = url.Substring(0, (url.LastIndexOf("/")));
            url = url.Substring(0, (url.LastIndexOf("/")));
            string ImageUrl = url + "/" + Location + "/" + ImageId;
            //string strURL = ConfigurationSettings.AppSettings["ApplicationURL"].ToString();           
            //string ImageUrl = strURL + Location + "/"+ImageId ;
            ImageUrl = ImageUrl.Replace("\\", "/");

            return ImageUrl;
        }

        public string GetMarkerURL(string MarkerId)
        {
            string url = (HttpContext.Current.Request.Url).ToString();
            url=url.Substring(0,(url.LastIndexOf("/")));
            url = url.Substring(0, (url.LastIndexOf("/")));
            //string strURL = ConfigurationSettings.AppSettings["ApplicationURL"].ToString();
            string MarkerImagepath = ConfigurationSettings.AppSettings["MarkerImagepath"].ToString();
            //string MarkerUrl = strURL + MarkerImagepath+"/"+MarkerId;
            string MarkerUrl = url +"/" +MarkerImagepath + "/" + MarkerId;           
            //string MarkerUrl = "~\\Admin\\Images\\MarkerImage\\" + MarkerId;           
            MarkerUrl = MarkerUrl.Replace("\\", "/");
            return MarkerUrl;
        }
        public string GetLandMarksURL(string ImageId)
        {
            string url = (HttpContext.Current.Request.Url).ToString();
            url = url.Substring(0, (url.LastIndexOf("/")));
            url = url.Substring(0, (url.LastIndexOf("/")));
            //string strURL = ConfigurationSettings.AppSettings["ApplicationURL"].ToString();
            string LandmarkURL = ConfigurationSettings.AppSettings["ClientLandmarkImages"].ToString();
            
            //string ImageUrl = strURL +LandmarkURL +"/" + ImageId;
            string ImageUrl = url + "/" + LandmarkURL + "/" + ImageId;
            ImageUrl = ImageUrl.Replace("\\", "/");
            return ImageUrl;
        }
    }

