using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data.OleDb;



#region CommonFunctions
	public class CommonFunctions
	{
        #region GetFileData(string FilePath)
        //public static string GetFileData(string FilePath)
        //{
        //    string FileData = "";
        //    try
        //    {

        //        using (StreamReader sr = new StreamReader(FilePath))
        //        {
        //            String line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                FileData += line + "\n";
        //            }
        //        }
        //    }
        //    catch (Exception Ex) { throw Ex; }
        //    return FileData;

        //}
        //#endregion

        //#region WriteFile(string FilePath, string FileData)
		
        //public static void WriteFile(string FilePath, string FileData)
        //{
        //    try
        //    {
        //        using (StreamWriter sw = new StreamWriter(FilePath))
        //        {
        //            sw.Write(FileData);
        //        }
        //    }
        //    catch (Exception Ex) { throw Ex; }
        //}
        #endregion

		#region GetDisplayDate(string date)
		public static string GetDisplayDate(string date)
		{
			string Ret = "";
			if (date != "")
			{
				try
				{
					Ret = Convert.ToDateTime(date).ToString("dd MMM yyyy");
				}
				catch { }
			}
			return Ret;
		}
		#endregion

		#region IsValidDate(string date)
		public static bool IsValidDate(string date)
		{
			try
			{
				if (date != null && date != "")
				{ Convert.ToDateTime(date); return true; }
				else { return false; }
			}
			catch { return false; }
		}
		#endregion

		#region IsValidTime(string time)
		public static bool IsValidTime(string time)
		{
			try
			{
				if (time != null && time != "")
				{ Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + time); return true; }
				else { return false; }
			}
			catch { return false; }
		}
		#endregion

		#region isValidInt(string val)
		public bool isValidInt(string val)
		{
			try
			{
				if (val != null && val != "")
				{ Convert.ToInt32(val); return true; }
				else { return false; }
			}
			catch { return false; }
		}
		#endregion

		#region isValidBool(string val)
		public static bool isValidBool(string val)
		{
			try
			{
				if (val != null && val != "")
				{ Convert.ToBoolean(val); return true; }
				else { return false; }
			}
			catch { return false; }
		}
		#endregion

		#region isValidDouble(string val)
		public bool isValidDouble(string val)
		{
			try
			{
				if (val != null && val != "")
				{ Convert.ToDouble(val); return true; }
				else { return false; }
			}
			catch { return false; }
		}
		#endregion

		#region ReplaceNextLine(string MainString, string ReplaceData)
		public static string ReplaceNextLine(string MainString, string ReplaceData)
		{
			MainString = MainString.Replace("\n", ReplaceData);
			MainString = MainString.Replace("\r", "");
			return MainString;
		}
		#endregion	

        #region ConverttoValidDate(string date)
        public static string converttodate(string date)
        {
            string[] _date = date.Split('-');
            if (_date[0].Length == 1)
            {
                _date[0] = 0 + _date[0];
            }
            date = _date[1] + "-" + _date[0] + "-" + _date[2];
            date = _date[2] + "/" + _date[1] + "/" + _date[0];
            
            return date;
        }
        #endregion

        #region GetExcelConnectionString(string Extension)
        public static string GetExcelConnectionString(string Extension)
        {
            string eSaleConstr = "";
            if (string.Compare(Extension, ".XLSX", true) == 0) //Excel 2007
                eSaleConstr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
            else
                eSaleConstr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            return eSaleConstr;
        }
        #endregion

        #region errorLogs

        public void WriteError(string errorMessage, string Traceinfo, string Username, string Mode)//,int i,int j,string username,
        {
            //try
            //{
                //DateTime.Today.Date,DateTime.Today.Day,DateTime.Today.Year


                //string path = "~/ErrorLog/" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".txt";
                //if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                //{
                //    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                //}
                //using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                //{
                //    //w.WriteLine("\r\nLog Entry : ");
                //    //w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                //    //string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                //    //              ". Error Message:" + errorMessage +
                //    //              ". USERNAME:" + Username +
                //    //". LINE NO:" + i +
                //    //". USERNAME:" + username + 
                //    //". DATE:" + System.DateTime.Now.ToShortDateString() +
                //    //".ERROR:" + Traceinfo;
                //    w.WriteLine("Error in: " + System.Web.HttpContext.Current.Request.Url.ToString());
                //    w.WriteLine("Error Message:" + errorMessage);
                //    w.WriteLine("ERROR:" + Traceinfo);

                //    w.WriteLine("MODE:" + Mode);
                //    w.WriteLine("====================================================================================================================");
                //    w.Flush();
                //    w.Close();
                //}
            //}
            //catch (Exception ex)
            //{
            //    WriteError(ex.Message);
            //}

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

        #endregion
    }

	#endregion

#region CommonMails
public class CommonMails
{
    #region SendTextMail
    public static void SendTextMail(string to,string from, string subject, string body)
    {
        
        MailMessage mailObj = new MailMessage();
        //mailObj.From = "admin@vtechnocrat.com";  //"VTECHNOCRAT";
        mailObj.From = from;
        mailObj.To = to;
        mailObj.Subject = subject;
        mailObj.Body = body;
        mailObj.BodyFormat = MailFormat.Text;
        SmtpMail.Send(mailObj);
    }
    #endregion

    #region SendHTMLMail
    public static void SendHTMLMail(string to,string from, string subject, string body)
    {
        MailMessage mailObj = new MailMessage();
        //mailObj.From = "admin@vtechnocrat.com";  //"VTECHNOCRAT";
        mailObj.From = from;
        mailObj.To = to;
        mailObj.Subject = subject;
        mailObj.Body = body;
        mailObj.BodyFormat = MailFormat.Html;
        SmtpMail.Send(mailObj);
    }
    #endregion

    //public static String AdminEmailId = "admin@vtechnocrat.com";

#endregion



}
