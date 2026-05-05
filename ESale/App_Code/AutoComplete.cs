using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data;
using ESaleEntity;
/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{

    public AutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public string[] AutoCompleteAjaxRequest(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetDataFromDataBase(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["BrandName"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
    public DataTable GetDataFromDataBase(string prefixText)
    {
        DataSet ds = new DataSet();
        ESupplierDeliveryNote ESDN = new ESupplierDeliveryNote();
        if (prefixText != "")
            ESDN.BrandName = prefixText;
        else
            ESDN.BrandName = "";
        ds = ESDN.GetBrandAutoComplete();
        //string connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\bookstore.mdb;Persist Security Info=False;";
        //DataTable _objdt = new DataTable();
        //string querystring = "select * from ProLanguage where LanguageName like '%" + prefixText + "%';";
        //OleDbConnection _objcon = new OleDbConnection(connectionstring);
        //OleDbDataAdapter _objda = new OleDbDataAdapter(querystring, _objcon);
        //_objcon.Open();
        //_objda.Fill(_objdt);
        //return _objdt;
        return ds.Tables[0];
    }
    [WebMethod]
    public string[] AutoCompleteBrandRequest(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetBrand(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["BrandName"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
    public DataTable GetBrand(string prefixText)
    {
        DataSet ds = new DataSet();
        EStoreReturn ESDN = new EStoreReturn();
        if (prefixText != "")
            ESDN.BrandName = prefixText;
        else
            ESDN.BrandName = "";
        ds = ESDN.ddlBrand();
        //string connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\bookstore.mdb;Persist Security Info=False;";
        //DataTable _objdt = new DataTable();
        //string querystring = "select * from ProLanguage where LanguageName like '%" + prefixText + "%';";
        //OleDbConnection _objcon = new OleDbConnection(connectionstring);
        //OleDbDataAdapter _objda = new OleDbDataAdapter(querystring, _objcon);
        //_objcon.Open();
        //_objda.Fill(_objdt);
        //return _objdt;
        return ds.Tables[0];
    }
    [WebMethod]
    public string[] AutoCompleteProductRequest(string prefixText, int count, string contextKey)
    {
        List<string> ajaxDataCollection = new List<string>();
        int categoryid = 0, brandid = 0;
        if (contextKey != null)
        {
            categoryid = Convert.ToInt32(contextKey.Split('~')[0]);
            brandid = Convert.ToInt32(contextKey.Split('~')[1]);
        }
        else
        {
            categoryid = 0;
            brandid = 0;
        }
            DataTable _objdt = new DataTable();
            _objdt = GetProduct(prefixText, categoryid, brandid);
            if (_objdt.Rows.Count > 0)
            {
                for (int i = 0; i < _objdt.Rows.Count; i++)
                {
                    ajaxDataCollection.Add(_objdt.Rows[i]["ProductName"].ToString());
                }
            }
        
        return ajaxDataCollection.ToArray();
    }
    public DataTable GetProduct(string prefixText, int categoryid, int brandid)
    {
        DataSet ds = new DataSet();
        ESupplierDeliveryNote ESDN = new ESupplierDeliveryNote();
        if (prefixText != "")
            ESDN.ProductName = prefixText;
        else
            ESDN.ProductName = "";
        if (categoryid != 0)
            ESDN.CategoryID = categoryid;
        else
            categoryid = 0;
        if (brandid != 0)
            ESDN.BrandID = brandid;
        else
            ESDN.BrandID = 0;
        
        //if (HttpCookie["BrandName"] != null)
        //    ESDN.BrandName = Convert.ToString(Session["BrandName"]);
        //else
        //    ESDN.BrandName = "";
        //if (HttpContext.Current.Session["CategoryID"] != null)
        //    ESDN.CategoryID = Convert.ToInt32(Session["CategoryID"]);
        //else
        //    ESDN.CategoryID = 0;
        ds = ESDN.GetProductAutoComplete();
        return ds.Tables[0];
    }
    [WebMethod]
    public string[] AutoCompleteExpenseRequest(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetExpense(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["ExpenseType"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
    public DataTable GetExpense(string prefixText)
    {
        DataSet ds = new DataSet();
        EPettyExpenses Eexpense = new EPettyExpenses();
        if (prefixText != "")
            Eexpense.ExpenseName = prefixText;
        else
            Eexpense.ExpenseName = "";
        //if (HttpCookie["BrandName"] != null)
        //    ESDN.BrandName = Convert.ToString(Session["BrandName"]);
        //else
        //    ESDN.BrandName = "";
        //if (HttpContext.Current.Session["CategoryID"] != null)
        //    ESDN.CategoryID = Convert.ToInt32(Session["CategoryID"]);
        //else
        //    ESDN.CategoryID = 0;
        ds = Eexpense.GetExpenseAutoComplete();
        return ds.Tables[0];
    }
    [WebMethod]
    public string[] ACStoreReturnProduct(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetProductFromStoreReturn(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["ProductName"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
    public DataTable GetProductFromStoreReturn(string prefixText)
    {
        DataSet ds = new DataSet();
        EStoreReturn ESDN = new EStoreReturn();
        if (prefixText != "")
            ESDN.ProductName = prefixText;
        else
            ESDN.ProductName = "";
        ds = ESDN.GetProductAutoComplete();       
        return ds.Tables[0];
    }
}

