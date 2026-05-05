using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for CheckUrl
/// </summary>
public class CheckUrl
{
    public CheckUrl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string QueryString = string.Empty;
    public string Action = string.Empty;

    public CheckUrl(string Url)
    {
        QueryString = Url.Substring(Url.IndexOf(".aspx") + 5);
    }

    /// <summary>
    /// Each And EVery Request would pass from a single root Method
    /// </summary>
    /// <returns></returns>
    public string ExecuteRequest()
    {
        SortedList hTable = MakeActionTable();
        string reponseValue = string.Empty; 
        if (hTable != null)
        {
           
            RemoteCallHandler rch = new RemoteCallHandler();
            reponseValue = rch.ExetureRequest(hTable);
             
        }
        return reponseValue;
    }

    /// <summary>
    /// This Method is Used to retrive the session value from URL 
    /// </summary>
    /// <returns></returns>
    public string GetSession()
    {
        SortedList hTable = MakeActionTable();
        if (hTable != null)
        {
            if (hTable.Contains("SessionID"))
                return hTable["SessionID"].ToString();
            else
                return "";
        }
        return "";
    }

    /// <summary>
    /// This method would split all the querystring variables in to a sorted list.,., 
    /// </summary>
    /// <returns></returns>
    private SortedList MakeActionTable()
    {
        SortedList entryTable = null;

        if (QueryString != string.Empty)
        {
            string[] strSplitter = QueryString.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
            if (QueryString != string.Empty)
            {
                entryTable = new SortedList();
            }

            int i = 0;
            foreach (string splitVal in strSplitter)
            {
                if (splitVal.Contains("="))
                {
                    ArrayList al = new ArrayList();
                    al.AddRange(splitVal.Split(new char[] { '=' }));
                    entryTable.Add(al[0].ToString(), al[1].ToString());
                    i++;
                }
            }
        }
        return entryTable;
    }


  
    public string ExetureRequestReport()
    {
        SortedList hTable = MakeActionTable();
        string reponseValue = string.Empty;
        if (hTable != null)
        {

            RemoteCallHandler rch = new RemoteCallHandler();
            reponseValue = rch.ExetureRequestReport(hTable);

        }
        return reponseValue;
    }
}
