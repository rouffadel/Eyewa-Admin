using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESaleEntity.DAL;
namespace ESaleEntity
{
    public class ESales
    {
        DLogin Sale;

        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string Transation;

        string _BrandName;

        public string BrandName
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }
        string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        private int _StoreID;

        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        private int _SalesID;

        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }
        private decimal _PaidAmount;
        public float PaidAmount { get; set; }

        private string _CustomerName;

        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _City;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _ZipCode;
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        private string _VatID;
        public string VatID
        {
            get { return _VatID; }
            set { _VatID = value; }
        }
        private string _ContactNum;
        public string ContactNum
        {
            get { return _ContactNum; }
            set { _ContactNum = value; }
        }
        private string _CustomerNo;

        public string CustomerNo
        {
            get { return _CustomerNo; }
            set { _CustomerNo = value; }
        }
        private string _OrganisationName;
        public string OrganisationName
        {
            get { return _OrganisationName; }
            set { _OrganisationName = value; }
        }
        private string _FromDate;

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private string _ToDate;

        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private int _CategoryID;

        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        private int _BrandID;

        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        private int _ProductID;

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        private string _GridData;

        public string GridData
        {
            get { return _GridData; }
            set { _GridData = value; }
        }
        private int _SalesDetailID;

        public int SalesDetailID
        {
            get { return _SalesDetailID; }
            set { _SalesDetailID = value; }
        }
        private int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        private float _GrossTotal;

        public float GrossTotal
        {
            get { return _GrossTotal; }
            set { _GrossTotal = value; }
        }
        private float _Discount;

        public float Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        private float _NetTotal;

        public float NetTotal
        {
            get { return _NetTotal; }
            set { _NetTotal = value; }
        }
        private int _SalesManID;

        public int SalesManID
        {
            get { return _SalesManID; }
            set { _SalesManID = value; }
        }
        private string _InvoiceNo;

        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        private string _InvoiceDate;

        public string InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private int _OrganisationiD;

        public int OrganisationiD
        {
            get { return _OrganisationiD; }
            set { _OrganisationiD = value; }
        }
        public float Balance { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string LoginName { get; set; }
        public string PaymentDate { get; set; }
        public float PaymentBalance { get; set; }
        public float ProductVal { get; set; }
        public string PaymentMode { get; set; }

        public string SPH_RightEye { get; set; }
        public string CYL_RightEye { get; set; }
        public string AXIS_RightEye { get; set; }
        public string ADD_RightEye { get; set; }
        public string SPH_LeftEye { get; set; }
        public string CYL_LeftEye { get; set; }
        public string AXIS_LeftEye { get; set; }
        public string ADD_LeftEye { get; set; }
        public string SPH_IPD { get; set; }
        public string CYL_IPD { get; set; }
        public string AXIS_IPD { get; set; }
        public string ADD_IPD { get; set; }
        public int OrganisationUser { get; set; }
        public string SerialNo { get; set; }
        public DataSet ddlOrganisation()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;

                ht.Add("@WhereCondition", "");
                ht.Add("@Transaction", "ddlOrganisation");
                ds = Sale.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet ddlStore()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string Wherecondition2 = string.Empty;
                if (LoginID == 1)
                    Transation = "ddlStore";
                else
                {
                    if (LoginID != 1 && OrganisationUser != 0)
                    {
                        Transation = "ddlStoreForUser";
                        WhereCondition = " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transation = "ddlStoreForOrgUser";
                        Wherecondition2 = " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@whereCondition2", Wherecondition2);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet ddlCategory()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                //if (StoreID != 0)
                //    WhereCondition = " and ST.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlCategory");
                ds = Sale.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet ddlBrand()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and ST.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlBrand");
                ds = Sale.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet ddlProduct()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (CategoryID != 0)
                    WhereCondition = " and P.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID =" + BrandID;
                //if (StoreID != 0)
                //    WhereCondition += " and ST.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlProduct");
                ds = Sale.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet ddlUser()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                Transation = string.Empty;
                //if (LoginID == 1)
                Transation = "ddlUser";
                //else
                //{
                //    WhereCondition = " and L.LoginID =" + LoginID;
                //    Transation = "ddlUserForUser";
                //}
                //if(LoginID!=1)
                //    WhereCondition = " and LoginID =" + LoginID;
                //    if (StoreID != 0)
                //        WhereCondition += " and StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet ddlSalesMan()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                Transation = string.Empty;

                if (LoginID == 1)
                {
                    Transation = "ddlSalesMan";
                    if (StoreID != 0)
                        WhereCondition = " and StoreID =" + StoreID;
                }
                else
                {
                    if (StoreID != 0)
                        WhereCondition = " and L.StoreID =" + StoreID;
                    Transation = "ddlSalesManForUser";
                    WhereCondition += " and L.LoginID=" + LoginID;
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetProductValue()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (ProductID != 0)
                    WhereCondition = " and P.ProductID =" + ProductID;
                if (StoreID != 0)
                    WhereCondition += " and ST.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductValue");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSalesGrid()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transation = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and S.StoreID =" + StoreID;
                if (CustomerName != "")
                    WhereCondition += " and S.CustomerName like '" + CustomerName + "%'";
                if (CustomerNo != "")
                    WhereCondition += " and S.CustomerNo like '" + CustomerNo + "%'";
                if (InvoiceNo != "")
                    WhereCondition += "and S.InvoiceNo like '%" + InvoiceNo + "%'";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and cast(S.CreatedDate as date) between'" + FromDate + "' and '" + ToDate + "'";
                if (SerialNo != "")
                    WhereCondition += " and REVERSE(SUBSTRING(REVERSE(S.InvoiceNo), 1,CHARINDEX('-', REVERSE(S.InvoiceNo)) - 1))  ='" + SerialNo + "'";
                if (LoginID == 1)
                    Transation = "GetSalesGrid";
                else
                {

                    if (OrganisationUser == 0)
                    {
                        Transation = "GetSalesGridForOrgUser";
                        WhereCondition2 += " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transation = "GetSalesGridForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("GetDataSalesNew", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSalesDetailsGrid()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " Where S.SaleID =" + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetSalesDetailGrid");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSalesDetailsGrid1()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition1 = string.Empty;
                if (SalesID != 0)
                {
                    WhereCondition = " Where S.SaleID =" + SalesID;
                    if (FromDate != "")
                        WhereCondition1 = " Where S.SaleID=" + SalesID + " and Convert(date,S.PaymentDate)= Convert(date,'" + FromDate + "')";
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", WhereCondition1);
                ht.Add("@Transaction", "GetSalesDetailGrid");
                ds = Sale.GetTransaction("SP_SalesReportPrintPopUP", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet InsertSales()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", 0);
                ht.Add("@LoginID", LoginID);
                ht.Add("@CustomerName", CustomerName);
                ht.Add("@CustomerNo", CustomerNo);
                ht.Add("@StoreID", StoreID);
                ht.Add("@GridData", "");
                ht.Add("@SalesDetailsID", 0);
                ht.Add("@GrossTotal", 0);
                ht.Add("@Discount", 0);
                ht.Add("@NetTotal", 0);
                ht.Add("@UserID", 0);
                ht.Add("@InvoiceNo", InvoiceNo);
                ht.Add("@InvoiceDate", InvoiceDate);
                ht.Add("@Remarks", "");
                ht.Add("@Balance", 0);
                ht.Add("@PaidAmount", 0);
                ht.Add("@PaymentMode1", "");
                ht.Add("@Transaction", "InsertSales");
                ds = Sale.GetTransaction("SP_Sales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet ECheckDuplicatesVIEWEDIT(string categoryid, string brand, string product)
        {

            try
            {
                ds = new DataSet();
                Sale = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " and SD.SalesID =" + SalesID + " and SD.BrandID=" + Convert.ToInt32(brand) + " and SD.ProductID=" + Convert.ToInt32(product) + " and SD.CategoryID=" + Convert.ToInt32(categoryid);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "CheckDuplicatesVIEWEDIT");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public string InsertSalesDetails()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", @SalesID);
                ht.Add("@LoginID", LoginID);
                ht.Add("@CustomerName", CustomerName);
                ht.Add("@CustomerNo", CustomerNo);
                ht.Add("@StoreID", StoreID);
                ht.Add("@GridData", GridData);
                ht.Add("@SalesDetailsID", 0);
                ht.Add("@GrossTotal", GrossTotal);
                ht.Add("@Discount", Discount);
                ht.Add("@NetTotal", NetTotal);
                ht.Add("@UserID", SalesManID);
                ht.Add("@InvoiceNo", "");
                ht.Add("@InvoiceDate", "");
                ht.Add("@Remarks", "");
                ht.Add("@Balance", Balance);
                ht.Add("@PaidAmount", PaidAmount);
                ht.Add("@PaymentMode1", PaymentMode);
                ht.Add("@Transaction", "InsertSalesDetails");
                ds = Sale.GetTransaction("SP_Sales", ht);
                result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public DataSet DeleteSales()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", SalesID);
                ht.Add("@LoginID", LoginID);
                ht.Add("@CustomerName", "");
                ht.Add("@CustomerNo", "");
                ht.Add("@StoreID", 0);
                ht.Add("@GridData", "");
                ht.Add("@SalesDetailsID", 0);
                ht.Add("@GrossTotal", 0);
                ht.Add("@Discount", 0);
                ht.Add("@NetTotal", 0);
                ht.Add("@UserID", 0);
                ht.Add("@InvoiceNo", "");
                ht.Add("@InvoiceDate", "");
                ht.Add("@Remarks", "");
                ht.Add("@Balance", 0);
                ht.Add("@PaidAmount", 0);
                ht.Add("@PaymentMode1", "");
                ht.Add("@Transaction", "DeleteSales");
                ds = Sale.GetTransaction("SP_Sales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet DeleteSalesDetails()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", 0);
                ht.Add("@LoginID", LoginID);
                ht.Add("@CustomerName", "");
                ht.Add("@CustomerNo", "");
                ht.Add("@StoreID", StoreID);
                ht.Add("@GridData", "");
                ht.Add("@SalesDetailsID", SalesDetailID);
                ht.Add("@GrossTotal", 0);
                ht.Add("@Discount", 0);
                ht.Add("@NetTotal", 0);
                ht.Add("@UserID", 0);
                ht.Add("@InvoiceNo", "");
                ht.Add("@InvoiceDate", "");
                ht.Add("@Remarks", "");
                ht.Add("@Balance", 0);
                ht.Add("@PaidAmount", 0);
                ht.Add("@PaymentMode1", "");
                ht.Add("@Transaction", "DeleteSalesDetails");
                ds = Sale.GetTransaction("SP_Sales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetPrint()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " and S.SaleID = " + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetPrintPopup");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }

      
        public DataSet GetEPriscriptionPrintPopup()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = "Where S.SaleID = " + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetPriscriptionPrintpopup");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }



        public DataSet GetBalanceGridReport()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transation = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and S.StoreID =" + StoreID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and S.InvoiceDate between '" + FromDate + "' and '" + ToDate + "'";
                if (InvoiceNo != "")
                    WhereCondition += " and S.InvoiceNo like '" + InvoiceNo + "%'";
                //WhereCondition += " and  CONVERT(nvarchar(100),S.CreatedDate,105) between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                {
                    Transation = "GetBalanceReport";
                }
                else
                {
                    if (OrganisationUser == 0)
                    {
                        Transation = "GetBalanceReportForOrgUser";
                        WhereCondition2 += " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transation = "GetBalanceReportForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("GetDataSalesNew", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSalesGridReport()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transation = string.Empty;
                string WhereCondition1 = string.Empty;
                string WhereCondition3 = string.Empty;
                //if (StoreID != 0)
                //    WhereCondition = " and S.StoreID =" + StoreID;
                //if (CustomerName != "")
                //    WhereCondition = " and S.CustomerName like '" + CustomerName + "%'";
                //if (CustomerNo != "")
                //    WhereCondition = " and S.CustomerNo like '" + CustomerNo + "%'";
                //if (FromDate != "" && ToDate != "")
                //    WhereCondition += " and S.InvoiceDate between '" + FromDate + "' and '" + ToDate + "'";
                //if (InvoiceNo != "")
                //    WhereCondition += " and S.InvoiceNo like '" + InvoiceNo + "%'";


                if (StoreID != 0)
                {
                    WhereCondition += " and S.StoreID =" + StoreID;
                    // WhereCondition3 += " and S.StoreID =" + StoreID;
                    WhereCondition2 += " and S.StoreId =" + StoreID;
                }
                //if (CategoryID != 0)
                //    WhereCondition1 += " and C.CategoryID =" + CategoryID;
                //if (BrandID != 0)
                //    WhereCondition1 += " and B.BrandID =" + BrandID;
                //if (ProductID != 0)
                //    WhereCondition1 += " and P.ProductID =" + ProductID;
                if (CustomerName != "")
                {
                    WhereCondition += " and S.CustomerName like '" + CustomerName + "%'";
                    WhereCondition2 += " and S.CustomerName like '" + CustomerName + "%'";
                }
                if (CustomerNo != "")
                {
                    WhereCondition += " and S.CustomerNo like '" + CustomerNo + "%'";
                    WhereCondition2 += " and S.CustomerNo like '" + CustomerNo + "%'";
                }
                if (FromDate != "" && ToDate != "")
                {
                    //WhereCondition += " and convert(date,S.InvoiceDate) like convert(date, '" + FromDate + "')";
                    //WhereCondition3 += " and convert(date,IP.CreatedDate) like convert(date,'" + FromDate + "' )";
                    //WhereCondition2 += " and Convert(date,IP.PaymentDate)= Convert(date,'" + FromDate + "') and Convert(date,S.InvoiceDate)<Convert(Date,'" + FromDate + "')";
                    WhereCondition += " and convert(date,S.InvoiceDate) between  Convert(date,'" + FromDate + "') and  Convert(date,'" + ToDate + "')";
                    WhereCondition3 += " and convert(date,IP.CreatedDate) between  Convert(date,'" + FromDate + "') and  Convert(date,'" + ToDate + "')";
                    WhereCondition2 += " and Convert(date,IP.PaymentDate) between  Convert(date,'" + FromDate + "') and  Convert(date,'" + ToDate + "') and Convert(date,S.InvoiceDate)<Convert(Date,'" + FromDate + "')";

                }
                if (InvoiceNo != "")
                {
                    // WhereCondition3 += " and S.InvoiceNo like '" + InvoiceNo + "%'";
                    WhereCondition += " and S.InvoiceNo like '" + InvoiceNo + "%'";
                }
                //WhereCondition += " and  CONVERT(nvarchar(100),S.CreatedDate,105) between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                {
                    Transation = "GetSalesReport";
                }
                else
                {
                    if (OrganisationUser == 0)
                    {
                        Transation = "GetSalesReportForOrgUser";
                        WhereCondition1 = " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transation = "GetSalesReportForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                        WhereCondition2 += " and L.LoginId =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", WhereCondition1);
                ht.Add("@WhereCondition3", WhereCondition3);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("[SP_GetDataSalesNew2]", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSales2Report()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transation = string.Empty;
                string WhereCondition1 = string.Empty;
                string WhereCondition3 = string.Empty;
                //if (StoreID != 0)
                //    WhereCondition = " and S.StoreID =" + StoreID;
                //if (CustomerName != "")
                //    WhereCondition = " and S.CustomerName like '" + CustomerName + "%'";
                //if (CustomerNo != "")
                //    WhereCondition = " and S.CustomerNo like '" + CustomerNo + "%'";
                //if (FromDate != "" && ToDate != "")
                //    WhereCondition += " and S.InvoiceDate between '" + FromDate + "' and '" + ToDate + "'";
                //if (InvoiceNo != "")
                //    WhereCondition += " and S.InvoiceNo like '" + InvoiceNo + "%'";


                if (StoreID != 0)
                {
                    WhereCondition += " and S.StoreID =" + StoreID;
                }
                if (CategoryID != 0)
                    WhereCondition1 += " and SD.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition1 += " and SD.BrandID =" + BrandID;
                if (ProductID != 0)
                    WhereCondition1 += " and SD.ProductID =" + ProductID;
                if (CustomerName != "")
                {
                    WhereCondition += " and S.CustomerName like '" + CustomerName + "%'";

                }
                if (CustomerNo != "")
                {
                    WhereCondition += " and S.CustomerNo like '" + CustomerNo + "%'";
                }
                if (FromDate != "" && ToDate != "")
                {
                    WhereCondition += " and convert(date,S.InvoiceDate) between convert(date, '" + FromDate + "') and Convert(date,'" + ToDate + "')";// and '" + ToDate + "'";                    
                }


                if (InvoiceNo != "")
                {
                    // WhereCondition3 += " and S.InvoiceNo like '" + InvoiceNo + "%'";
                    WhereCondition += " and S.InvoiceNo like '" + InvoiceNo + "%'";
                }
                //WhereCondition += " and  CONVERT(nvarchar(100),S.CreatedDate,105) between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                {
                    if (WhereCondition1 == "")
                    {
                        Transation = "SalesReport2";
                        if (StoreID != 0)
                        {
                            WhereCondition3 += " and S.StoreID =" + StoreID;
                        }
                        if (CustomerName != "")
                        {
                            WhereCondition3 += " and S.CustomerName like '" + CustomerName + "%'";
                        }

                        if (CustomerNo != "")
                        {
                            WhereCondition3 += " and S.CustomerNo like '" + CustomerNo + "%'";
                        }                        
                       
                        if (FromDate != "" && ToDate != "")
                        {
                            WhereCondition3 += " and convert(date,S.InvoiceDate) Not between convert(date, '" + FromDate + "') and Convert(date,'" + ToDate + "')";// and '" + ToDate + "'";                    
                        }
                        if (FromDate != "" && ToDate != "")
                        {
                            WhereCondition2 += " and convert(date,SD.PaymentDate) between convert(date, '" + FromDate + "') and Convert(date,'" + ToDate + "')";// and '" + ToDate + "'";                    
                        }
                    }
                    else
                    {
                        Transation = "SalesReport21";
                    }
                }
                else
                {
                    if (OrganisationUser == 0)
                    {
                        if (WhereCondition1 == "")
                            Transation = "SalesReport2ForOrgUser";
                        else
                            Transation = "SalesReport21ForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        if (WhereCondition1 == "")
                            Transation = "SalesReport2ForUser";
                        else
                            Transation = "SalesReport21ForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition3", WhereCondition3);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", WhereCondition1);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("[SP_GetDataSalesNew]", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSalesDetailsReport()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and S.StoreID =" + StoreID;
                if (CategoryID != 0)
                    WhereCondition += " and C.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and B.BrandID =" + BrandID;
                if (ProductID != 0)
                    WhereCondition += " and P.ProductID =" + ProductID;
                if (CustomerName != "")
                    WhereCondition += " and S.CustomerName like '" + CustomerName + "%'";
                if (CustomerNo != "")
                    WhereCondition += " and S.CustomerNo like '" + CustomerNo + "%'";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and convert(date,S.InvoiceDate) like convert(date,'" + FromDate + "')";// and '" + ToDate + "'";
                if (InvoiceNo != "")
                    WhereCondition += " and S.InvoiceNo like '" + InvoiceNo + "%'";
                if (LoginID == 1)
                {
                    Transation = "GetSalesDetailsReport";
                }
                else
                {
                    if (OrganisationUser == 0)
                    {
                        Transation = "GetSalesDetailsReportForOrgUser";
                        WhereCondition2 += " and L.LoginID=" + LoginID;
                    }
                    else
                    {
                        Transation = "GetSalesDetailsReportForUser";
                        WhereCondition += " and L.LoginID=" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("GetDataSalesNew", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetSalesDetails()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " and SaleID =" + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "SalesDetails");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetInvoiceDetails()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " and SaleID =" + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "InvoiceDetails");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet getPrescription()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " where SaleID =" + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetPrescriptionDetails");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet SavePayment()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", SalesID);
                ht.Add("@LoginID", 0);
                ht.Add("@CustomerName", "");
                ht.Add("@CustomerNo", "");
                ht.Add("@StoreID", "");
                ht.Add("@GridData", GridData);
                ht.Add("@SalesDetailsID", 0);
                ht.Add("@GrossTotal", 0);
                ht.Add("@Discount", 0);
                ht.Add("@NetTotal", 0);
                ht.Add("@UserID", 0);
                ht.Add("@InvoiceNo", "");
                ht.Add("@InvoiceDate", "");
                ht.Add("@Balance", 0);
                ht.Add("@Remarks", Remarks);
                ht.Add("@PaidAmount", 0);
                ht.Add("@PaymentMode1", "");
                ht.Add("@Transaction", "SavePayment");
                ds = Sale.GetTransaction("SP_Sales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet SavePrescription()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", SalesID);
                ht.Add("@LoginID", 0);
                ht.Add("@CustomerName", "");
                ht.Add("@CustomerNo", "");
                ht.Add("@StoreID", "");
                ht.Add("@GridData", GridData);
                ht.Add("@SalesDetailsID", 0);
                ht.Add("@GrossTotal", 0);
                ht.Add("@Discount", 0);
                ht.Add("@NetTotal", 0);
                ht.Add("@UserID", 0);
                ht.Add("@InvoiceNo", "");
                ht.Add("@InvoiceDate", "");
                ht.Add("@Remarks", "");
                ht.Add("@Balance", 0);
                ht.Add("@PaidAmount", 0);
                ht.Add("@PaymentMode1", "");
                ht.Add("@Transaction", "SavePrescription");
                ds = Sale.GetTransaction("SP_Sales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetProfitReport()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition1 = string.Empty;
                string WhereCondition2 = string.Empty;
                string WhereCondition3 = string.Empty;
                Transation = string.Empty;
                if (OrganisationiD != 0)
                {
                }
                if (StoreID != 0)
                {
                    WhereCondition += " and ST.StoreID=" + StoreID;
                    WhereCondition2 += " and  P.StoreID=" + StoreID;
                    WhereCondition3 += " and ST.StoreID=" + StoreID;

                }
                if (CategoryID != 0)
                {
                    WhereCondition += " and C.CategoryID =" + CategoryID;

                }
                if (BrandID != 0)
                {
                    WhereCondition += " and B.BrandID =" + BrandID;

                }
                if (ProductID != 0)
                {
                    WhereCondition += " and P.ProductID =" + ProductID;

                }
                if (FromDate != "" && ToDate != "")
                {
                    WhereCondition += " and cast(S.InvoiceDate as date) between '" + FromDate + "' and '" + ToDate + "'";
                    WhereCondition2 += " and cast(PP.PaymentDate as date) between '" + FromDate + "' and '" + ToDate + "'";
                    WhereCondition3 += " and cast(OL.CreatedDate as date) between '" + FromDate + "' and '" + ToDate + "'";
                }
                if (LoginID == 1)
                {
                    Transation = "GetProfitReport";
                }
                else
                {
                    if (OrganisationUser == 0)
                    {
                        Transation = "GetProfitReportForOrgUser";
                        WhereCondition1 += " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transation = "GetProfitReportForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                    // WhereCondition += " and L.LoginID =" + LoginID;

                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", WhereCondition1);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition3", WhereCondition3);
                ht.Add("@Transaction", Transation);
                ds = Sale.GetTransaction("SP_GetProfitReport1", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetProfitDetails()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                WhereCondition = string.Empty;
                if (ProductID != 0)
                    WhereCondition = "and SD.ProductID =" + ProductID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProfitDetails");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetPrintStore()
        {
            ht = new Hashtable();

            ds = new DataSet();
            Sale = new DLogin();
            try
            {
                if (StoreID == 0)
                {
                    ht.Add("@WhereCondition", "0");
                }
                else
                {
                    ht.Add("@WhereCondition", StoreID);
                }

                ht.Add("@Transaction", "GetprintStore");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetPrintSales()
        {
            ht = new Hashtable();
            ds = new DataSet();
            Sale = new DLogin();
            try
            {
                WhereCondition = string.Empty;
                if (SalesID != 0)
                    WhereCondition = " and S.SaleID =" + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetprintSales");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetPaymentSavePrint()
        {
            ht = new Hashtable();
            ds = new DataSet();
            Sale = new DLogin();
            try
            {
                WhereCondition = string.Empty;
                if (SalesID != 0)
                {
                    WhereCondition = SalesID.ToString();
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetPaymentSavePrint");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetPaymentPrint()
        {
            ht = new Hashtable();
            ds = new DataSet();
            Sale = new DLogin();
            try
            {
                WhereCondition = string.Empty;
                if (SalesID != 0)
                {
                    WhereCondition = SalesID.ToString();
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetPaymentPrint");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet CheckQuantity()
        {
            ds = new DataSet();
            ht = new Hashtable();
            Sale = new DLogin();
            try
            {
                WhereCondition = string.Empty;
                if (ProductID != 0 && ProductVal != 0)
                {
                    WhereCondition = "and ProductID=" + ProductID + "and ProductValue=" + ProductVal;

                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "CheckQuantity");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetProductIDandValue()
        {
            try
            {
                ds = new DataSet();
                Sale = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and P.ProductName ='" + ProductName + "'";
                if (CategoryID != 0)
                    WhereCondition += " and P.CategoryID=" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID= " + BrandID;
                if (StoreID != 0)
                    WhereCondition += " and ST.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductIDValue");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetBrandID()
        {
            try
            {
                ds = new DataSet();
                Sale = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition1 = string.Empty;
                if (BrandName != "")
                    WhereCondition = " and BrandName like '" + BrandName + "%'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetBrandID");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetLedgerGrid()
        {
            try
            {
                ds = new DataSet();
                Sale = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and S.StoreID=" + StoreID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and CONVERT(nvarchar(100),PP.CreatedDate,105) between '" + FromDate + "' and '" + ToDate + "'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetLedger");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetProductCategorybrandID()
        {
            try
            {
                ds = new DataSet();
                Sale = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and P.ProductName ='" + ProductName + "'";
                //if (CategoryID != 0)
                //    WhereCondition += " and CategoryID=" + CategoryID;
                //if (BrandID != 0)
                //    WhereCondition += " and BrandID= " + BrandID;
                if (StoreID != 0)
                    WhereCondition += " and ST.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductCategoryBrandIDValue");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetOrderLenseGrid()
        {
            try
            {
                ds = new DataSet();
                Sale = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                WhereCondition = " and SalesID =" + SalesID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetOrderLenseGrid");
                ds = Sale.GetTransaction("SP_GetDataSales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet SaveOrderLense()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                Sale = new DLogin();
                ht.Add("@SalesID", SalesID);
                ht.Add("@LoginID", 0);
                ht.Add("@CustomerName", "");
                ht.Add("@CustomerNo", "");
                ht.Add("@StoreID", "");
                ht.Add("@GridData", GridData);
                ht.Add("@SalesDetailsID", 0);
                ht.Add("@GrossTotal", 0);
                ht.Add("@Discount", 0);
                ht.Add("@NetTotal", 0);
                ht.Add("@UserID", 0);
                ht.Add("@InvoiceNo", "");
                ht.Add("@InvoiceDate", "");
                ht.Add("@Balance", 0);
                ht.Add("@Remarks", "");
                ht.Add("@PaidAmount", 0);
                ht.Add("@PaymentMode1", "");
                ht.Add("@Transaction", "SaveOrderLense");
                ds = Sale.GetTransaction("SP_Sales", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
    }
}
public class ESaleList
{
    public string CategoryName { set; get; }
    public string BrandName { get; set; }
    public string ProductName { get; set; }
    public float ProductValue { get; set; }
    public int Quantity { get; set; }
    public float SellingPrice { get; set; }
    public float TotalValue { get; set; }
    public float Discount { get; set; }
    public float NetValue { get; set; }
    public float TotalGrossValue { get; set; }
    public float Balance { get; set; }
    public float PaimAmount { get; set; }
    public string Remarks { get; set; }
    public float IPBalance { get; set; }
    public string PaymentDate { get; set; }
    public string PaymentMode { get; set; }
    public float PaymentAmount { get; set; }
    public string PaymentReceiptNum { get; set; }
    public float PaymentBalance { get; set; }

}