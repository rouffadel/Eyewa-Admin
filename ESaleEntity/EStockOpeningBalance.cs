using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleEntity.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace ESaleEntity
{
    public class EStockOpeningBalance
    {
        DLogin SDN;
        DataSet ds;
        Hashtable ht;
        string Result, WhereCondition, Transaction;

        string _BrandName;

        public string BrandName
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }
       
        string _OrganisatonName;
        public string OrganisationName
        {
            get { return _OrganisatonName; }
            set { _OrganisatonName = value; }
        }
        string _SupplierName;
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        private int _StoreId;

        public int StoreId
        {
            get { return _StoreId; }
            set { _StoreId = value; }
        }
        private string _StoreName;

        public string StoreName
        {
            get { return _StoreName; }
            set { _StoreName = value; }
        }
        private int _OrganisationID;

        public int OrganisationID
        {
            get { return _OrganisationID; }
            set { _OrganisationID = value; }
        }
        private int _SupplierID;

        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        private string _SOBNo;

        public string SOBNo
        {
            get { return _SOBNo; }
            set { _SOBNo = value; }
        }
        private string _SOBDate;

        public string SOBDate
        {
            get { return _SOBDate; }
            set { _SOBDate = value; }
        }
       
        private int _StockOpeningBalanceId;

        public int StockOpeningBalanceId
        {
            get { return _StockOpeningBalanceId; }
            set { _StockOpeningBalanceId = value; }
        }
        private int _StockOpeningBalanceDetailId;

        public int StockOpeningBalanceDetailId
        {
            get { return _StockOpeningBalanceDetailId; }
            set { _StockOpeningBalanceDetailId = value; }
        }
        private int _CategoryID;

        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        private int _ProductID;

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        private decimal _TotalBuyingPrice;

        public decimal TotalBuyingPrice
        {
            get { return _TotalBuyingPrice; }
            set { _TotalBuyingPrice = value; }
        }
        private decimal _TotalSellingPrice;
        public decimal TotalSellingPrice
        {
            get { return _TotalSellingPrice; }
            set { _TotalSellingPrice = value; }
        }
        private decimal _ProductValue;
        public decimal ProductValue
        {
            get { return _ProductValue; }
            set { _ProductValue = value; }
        }
       
        private int _Qunatity;
        public int Quantity
        {
            get { return _Qunatity; }
            set { _Qunatity = value; }
        }
        private decimal _HandlingCharges;

        public decimal HandlingCharges
        {
            get { return _HandlingCharges; }
            set { _HandlingCharges = value; }
        }
        

        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        private string _GridData;

        public string GridData
        {
            get { return _GridData; }
            set { _GridData = value; }
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
        private int _BrandID;

        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
       
        string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        string _CategoryName;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public DataSet ddlOrganisation()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                ht.Add("@WhereCondition", "");
                ht.Add("@Transaction", "ddlOrganisation");
                ht.Add("@WhereCondition2", WhereCondition2);
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
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
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                ht.Add("@WhereCondition", "");
                ht.Add("@Transaction", "ddlCategory");
                ht.Add("@WhereCondition2", WhereCondition2);
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
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
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                ht.Add("@WhereCondition", "");
                ht.Add("@Transaction", "ddlBrand");
                ht.Add("@WhereCondition2", WhereCondition2);
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet ddlProducts()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (CategoryID != 0)
                    WhereCondition = " and P.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID =" + BrandID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "ddlProduct");
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet ProductValues()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (ProductID != 0)
                    WhereCondition = " and P.ProductID=" + ProductID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "ProductValues");
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet Grid()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (OrganisationID != 0)
                    WhereCondition = " and  SOB.OrganisationID =" + OrganisationID;
                if (StoreId != 0)
                    WhereCondition += " and SOB.StoreID =" + StoreId;
                if (SOBNo != "")
                    WhereCondition += " and SOB.StockOpeningBalanceNo like '" + SOBNo + "%'";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SOB.StockOpeningBalanceDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                    Transaction = "SOBGRID";
                else
                {
                    if (LoginID != 1 && OrganisationUser != 0)
                    {
                        Transaction = "SOBGRIDForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transaction = "SOBGRIDForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GridForViewEdit()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (StockOpeningBalanceId != 0)
                    WhereCondition = " and SOB.StockOpeningBalanceId =" + StockOpeningBalanceId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "SOBGridForViewEdit");
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet InsertSOB()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@OrganisationID", OrganisationID);
                ht.Add("@StoreId", StoreId);
                ht.Add("@StockOpeningBalanceId", 0);
                ht.Add("@StockOpeningBalanceDetailId", 0); 
                ht.Add("@StockOpeningBalanceNo", SOBNo);
                ht.Add("@StockOPeningBalanceDate", SOBDate);             
                ht.Add("@TotalBuyingPrice", 0);
                ht.Add("@TotalSellingPrice", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "INSERTSOB");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_StockOpeningBalance", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public string InsertSOBD()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                ht.Add("@OrganisationID", OrganisationID);
                ht.Add("@StoreId", StoreId);
                ht.Add("@StockOpeningBalanceId", StockOpeningBalanceId);
                ht.Add("@StockOpeningBalanceDetailId", 0); 
                ht.Add("@StockOpeningBalanceNo", "");
                ht.Add("@StockOPeningBalanceDate", "");
                ht.Add("@TotalBuyingPrice", TotalBuyingPrice);
                ht.Add("@TotalSellingPrice", TotalSellingPrice);
                ht.Add("@Remarks", Remarks);
                ht.Add("@Transaction", "INSERTSOBDETAILS");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", GridData);
                ds = SDN.GetTransaction("SP_StockOpeningBalance", ht);                
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
        public string DeleteSOB()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                ht.Add("@OrganisationID", 0);
                ht.Add("@StoreId", 0);
                ht.Add("@StockOpeningBalanceId", StockOpeningBalanceId);
                ht.Add("@StockOpeningBalanceDetailId", 0); 
                ht.Add("@StockOpeningBalanceNo", "");
                ht.Add("@StockOPeningBalanceDate", "");
                ht.Add("@TotalBuyingPrice", 0);
                ht.Add("@TotalSellingPrice", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "DELETESOB");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_StockOpeningBalance", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
        public string DeleteSOBD()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                ht.Add("@OrganisationID", 0);
                ht.Add("@StoreId", StoreId);
                ht.Add("@StockOpeningBalanceId", 0);
                ht.Add("@StockOpeningBalanceDetailId", StockOpeningBalanceDetailId);
                ht.Add("@StockOpeningBalanceNo", "");
                ht.Add("@StockOPeningBalanceDate", "");
                ht.Add("@TotalBuyingPrice", 0);
                ht.Add("@TotalSellingPrice", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "DeleteSOBDetails");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_StockOpeningBalance", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }

        public DataSet ddlStore()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (LoginID == 1)
                    Transaction = "ddlStore";
                else
                {
                    if (LoginID != 1 && OrganisationUser != 0)
                    {
                        Transaction = "ddlStoreForUser";
                        WhereCondition = " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transaction = "ddlStoreForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet DataToPrint()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (StockOpeningBalanceId != 0)
                    WhereCondition = " and SOB.StockOpeningBalanceId =" + StockOpeningBalanceId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "GetGridForPrint");
                ds = SDN.GetTransaction("SP_GetStockOpeningBalance", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public int OrganisationUser { get; set; }
    }
}
