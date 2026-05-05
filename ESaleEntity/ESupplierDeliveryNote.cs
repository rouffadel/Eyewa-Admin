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
    public class ESupplierDeliveryNote
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
        public string PWhereCondition
        {
            get { return WhereCondition; }
            set { WhereCondition = value; }
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
        private string _SDNNo;

        public string SDNNo
        {
            get { return _SDNNo; }
            set { _SDNNo = value; }
        }
        private string _SDNDate;

        public string SDNDate
        {
            get { return _SDNDate; }
            set { _SDNDate = value; }
        }
        private string _PaymentDueDate;

        public string PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
        }
        private int _SupplierDeliveryNoteID;

        public int SupplierDeliveryNoteID
        {
            get { return _SupplierDeliveryNoteID; }
            set { _SupplierDeliveryNoteID = value; }
        }
        private int _SupplierDeliveryNoteDetailID;

        public int SupplierDeliveryNoteDetailID
        {
            get { return _SupplierDeliveryNoteDetailID; }
            set { _SupplierDeliveryNoteDetailID = value; }
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
        private decimal _NetProductValue;

        public decimal NetProductValue
        {
            get { return _NetProductValue; }
            set { _NetProductValue = value; }
        }
        private decimal _GrossValue;
        public decimal GrossValue
        {
            get { return _GrossValue; }
            set { _GrossValue = value; }
        }
        private decimal _ProductValue;
        public decimal ProductValue
        {
            get { return _ProductValue; }
            set { _ProductValue = value; }
        }
        //private int _BranidID;
        //public int BrandID
        //{
        //    get { return _BrandID; }
        //    set { _BrandID = value; }
        //}
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
        private decimal _TotalValue;

        public decimal TotalValue
        {
            get { return _TotalValue; }
            set { _TotalValue = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _City;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _ContactNumber;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
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
        public int _store;
        public int store
        {
            get { return _store; }
            set { _store = value; }
        }
        private string _GridData;

        public string GridData
        {
            get { return _GridData; }
            set { _GridData = value; }
        }
        private string _GridDataDeliveryNote;

        public string GridDataDeliveryNote
        {
            get { return _GridDataDeliveryNote; }
            set { _GridDataDeliveryNote = value; }
        }
        private string _StoreDeliveryNoteNo;
        public string StoreDeliveryNoteNo
        {
            get { return _StoreDeliveryNoteNo; }
            set { _StoreDeliveryNoteNo = value; }
        }
        private string _GridData1;

        public string GridData1
        {
            get { return _GridData1; }
            set { _GridData1 = value; }
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
        private string _SupplierDeliveryNoteNo;

        public string SupplierDeliveryNoteNo
        {
            get { return _SupplierDeliveryNoteNo; }
            set { _SupplierDeliveryNoteNo = value; }
        }
        string _Vatid;
        public string Vatid
        {
            get { return _Vatid; }
            set { _Vatid = value; }
        }
        private string _SupplierDeliveryNoteDate;

        public string SupplierDeliveryNoteDate
        {
            get { return _SupplierDeliveryNoteDate; }
            set { _SupplierDeliveryNoteDate = value; }
        }
        string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
       

      
        //Methods
        public DataSet ddlOrganisation()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@WhereCondition", "");              
                ht.Add("@Transaction", "ddlOrganisation");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        public DataSet ddlSupplier()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@WhereCondition", "");               
                ht.Add("@Transaction", "ddlSupplier");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                ht.Add("@WhereCondition", "");               
                ht.Add("@Transaction", "ddlCategory");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                ht.Add("@WhereCondition", "");                
                ht.Add("@Transaction", "ddlBrand");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                if (CategoryID != 0)
                    WhereCondition = " and P.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID =" + BrandID;
                ht.Add("@WhereCondition", WhereCondition);               
                ht.Add("@Transaction", "ddlProduct");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                if (ProductID != 0)
                    WhereCondition = " and P.ProductID=" + ProductID;
                ht.Add("@WhereCondition", WhereCondition);                
                ht.Add("@Transaction", "ProductValues");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                if (OrganisationID != 0)
                    WhereCondition = " and  SDN.OrganisationID =" + OrganisationID;
                if (SupplierID != 0)
                    WhereCondition += " and S.SupplierID =" + SupplierID;
                if (SDNNo != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteNo like '" + SDNNo + "%'";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteDate between '" + FromDate + "' and '" + ToDate + "'";
                ht.Add("@WhereCondition", WhereCondition);                
                ht.Add("@Transaction", "SDNGRID");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                if (SupplierDeliveryNoteID != 0)
                    WhereCondition = " and SDND.SupplierDeliveryNoteID =" + SupplierDeliveryNoteID;
                ht.Add("@WhereCondition", WhereCondition);                
                ht.Add("@Transaction", "SDNDGRIDFORVIEWEDIT");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetPurchaseDetailId(int id)
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SupplierDeliveryNoteID != 0)
                    WhereCondition = " SupplierDeliveryNoteDetailID =" + id;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "CheckSupDetailIdExistsOrNot");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

          public DataSet GridForViewEditTemp()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SupplierDeliveryNoteID != 0)
                    WhereCondition = " and SDND.SupplierDeliveryNoteID =" + SupplierDeliveryNoteID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "SDNDGRIDFORVIEWEDITTemp");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet ECheckDuplicatesVIEWEDIT(string categoryid,string brand,string product)
        {

            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SupplierDeliveryNoteID != 0)
                    WhereCondition = " and SDN.SupplierDeliveryNoteID =" + SupplierDeliveryNoteID + " and B.BrandName='" + brand + "' and P.ProductName='" + product + "' and SDND.CategoryID=" + Convert.ToInt32(categoryid);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "CheckDuplicatesVIEWEDIT");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                WhereCondition = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetPrintScreen()
        {
            ds = new DataSet();
            ht = new Hashtable();
            try
            {
                if (PWhereCondition != "")
                {
                    ht.Add("@WhereCondition", PWhereCondition);                  
                    ht.Add("@Transaction","GetPrintSupplierID");
                    ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        public DataSet GetPrintOrganisation()
        {
            ds = new DataSet();
            ht = new Hashtable();
            try
            {
                if (WhereCondition != "")
                {
                    ht.Add("@WhereCondition", OrganisationID);                    
                    ht.Add("@Transaction", "GetPrintOrganisationID");
                    ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetPrintGridTable()
        {
            ds = new DataSet();
            ht = new Hashtable();
            try
            {
                if (WhereCondition != "")
                {
                    ht.Add("@WhereCondition", WhereCondition);
                    ht.Add("@Transaction", "GetPrintGridTable");                   
                    ds = SDN.GetTransaction("SP_GetDataSDN", ht);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet InsertSDN()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@OrganisationID",OrganisationID);
                ht.Add("@SupplierID",SupplierID);
                ht.Add("@DeliveryNoteNo",SDNNo);
                ht.Add("@DeliveryNoteDate",SDNDate);
                ht.Add("@PaymentDueDate",PaymentDueDate);
                ht.Add("@SupplierDeliveryNoteID",0);
                ht.Add("@SupplierDeliveryNoteDetailID",0);
                ht.Add("@NetProductValue",0);
                ht.Add("@HandlingCharges",0);
                ht.Add("@TotalValue",0);
                ht.Add("@Remarks","");
                ht.Add("@Transaction","INSERTSDN");
                ht.Add("@LoginID",LoginID);
                ht.Add("@GridData","");               
                ds = SDN.GetTransaction("SP_SupplierDeliveryNote", ht);
                
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }

        public string InsertSDND()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@OrganisationID",0);
                ht.Add("@SupplierID",0);
                ht.Add("@DeliveryNoteNo","");
                ht.Add("@DeliveryNoteDate","");
                ht.Add("@PaymentDueDate","");
                ht.Add("@SupplierDeliveryNoteID",SupplierDeliveryNoteID);
                ht.Add("@SupplierDeliveryNoteDetailID",0);
                ht.Add("@NetProductValue",NetProductValue);
                ht.Add("@HandlingCharges",HandlingCharges);
                ht.Add("@TotalValue",TotalValue);
                ht.Add("@Remarks",Remarks);
                ht.Add("@Transaction","INSERTSDND");
                ht.Add("@LoginID",LoginID);
                ht.Add("@GridData",GridData);
                ht.Add("@store", store);
                ht.Add("@GridDataDeliveryNote", GridDataDeliveryNote);
                ht.Add("@StoreDeliveryNoteNo", StoreDeliveryNoteNo);
                ds = SDN.GetTransaction("SP_SupplierDeliveryNote", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {
                
                throw;
            }
            return Result;
        }

        public DataSet UpdateNewProductsValue()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@Transaction", "UpdateProductValue");
                ht.Add("@WhereCondition", "set ProductValue=" + ProductValue + " where ProductName='" + ProductName + "' and IsActive=1 and IsDeleted=0");
                ht.Add("@ProductName",ProductName);
                ht.Add("@ProductValue", ProductValue);
                ds = SDN.GetTransaction("SP_UpdateSPInProducts", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public string InsertSDNDTemp()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                WhereCondition = "INSERTSDNDTemp";
                ht.Add("@OrganisationID", 0);
                ht.Add("@SupplierID", 0);
                ht.Add("@DeliveryNoteNo", "");
                ht.Add("@DeliveryNoteDate", "");
                ht.Add("@PaymentDueDate", "");
                ht.Add("@SupplierDeliveryNoteID", SupplierDeliveryNoteID);
                ht.Add("@SupplierDeliveryNoteDetailID", 0);
                ht.Add("@NetProductValue", NetProductValue);
                ht.Add("@HandlingCharges", HandlingCharges);
                ht.Add("@TotalValue", TotalValue);
                ht.Add("@Remarks", Remarks);
                ht.Add("@Transaction", WhereCondition);
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", GridData);
                ds = SDN.GetTransaction("SP_SupplierDeliveryNote", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
        public string DeleteSDN()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@OrganisationID", 0);
                ht.Add("@SupplierID", 0);
                ht.Add("@DeliveryNoteNo", "");
                ht.Add("@DeliveryNoteDate", "");
                ht.Add("@PaymentDueDate", "");
                ht.Add("@SupplierDeliveryNoteID", SupplierDeliveryNoteID);
                ht.Add("@SupplierDeliveryNoteDetailID", 0);
                ht.Add("@NetProductValue", 0);
                ht.Add("@HandlingCharges", 0);
                ht.Add("@TotalValue", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "DELETESDN");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_SupplierDeliveryNote", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
        public string DeleteSDND()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@OrganisationID", 0);
                ht.Add("@SupplierID", 0);
                ht.Add("@DeliveryNoteNo", "");
                ht.Add("@DeliveryNoteDate", "");
                ht.Add("@PaymentDueDate", "");
                ht.Add("@SupplierDeliveryNoteID", SupplierDeliveryNoteID);
                ht.Add("@SupplierDeliveryNoteDetailID", SupplierDeliveryNoteDetailID);
                ht.Add("@NetProductValue", 0);
                ht.Add("@HandlingCharges", 0);
                ht.Add("@TotalValue", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "DELETESDND");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_SupplierDeliveryNote", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
        public string GetID()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                ht.Add("@OrganisationID", 0);
                ht.Add("@SupplierID", 0);
                ht.Add("@DeliveryNoteNo", "");
                ht.Add("@DeliveryNoteDate", "");
                ht.Add("@PaymentDueDate", "");
                ht.Add("@SupplierDeliveryNoteID", 0);
                ht.Add("@SupplierDeliveryNoteDetailID", 0);
                ht.Add("@NetProductValue", 0);
                ht.Add("@HandlingCharges", 0);
                ht.Add("@TotalValue", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "GetID");
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_SupplierDeliveryNote", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds.Tables[0].Rows[0]["Status"].ToString();
        }

        public DataSet GetInflowStock()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SupplierID != 0)
                    WhereCondition = " and S.SupplierID =" + SupplierID;
                if (SupplierDeliveryNoteNo != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteNo like '" + SupplierDeliveryNoteNo + "%'";
                if (SupplierDeliveryNoteDate != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteDate = '" + SupplierDeliveryNoteDate+"'";
                if (PaymentDueDate != "")
                    WhereCondition += " and SDN.PaymentDueDate ='" + PaymentDueDate+"'";
                if (CategoryID != 0)
                    WhereCondition += " and SDND.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and SDND.BrandID =" + BrandID;
                if (ProductID != 0)
                    WhereCondition += " and SDND.ProductID=" + ProductID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteDate between '" + FromDate + "' and '" + ToDate + "'";
                ht.Add("@WhereCondition", WhereCondition);               
                ht.Add("@Transaction", "GetIncomingStockReport");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);

            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;                        
        }
        public DataSet GetAvailableStockGrid()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SupplierID != 0)
                    WhereCondition = " and S.SupplierID =" + SupplierID;
                if (CategoryID != 0)
                    WhereCondition += " and C.CategoryID ="+CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and B.BrandID =" + BrandID;
                if (ProductID != 0)
                    WhereCondition += " and P.ProductID =" + ProductID;
                if (FromDate != "" && ToDate != "")
                {
                    WhereCondition += "and Convert(date,S.CreatedDate,105)  between Convert(date,'" + FromDate + "',105) and Convert(date,'" + ToDate + "',105)";
                }
                ht.Add("@WhereCondition", WhereCondition);               
                ht.Add("@Transaction", "AvailableStock");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetSupplierStockGrid()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (SupplierID != 0)
                    WhereCondition = " and S.SupplierID =" + SupplierID;
                if (SupplierDeliveryNoteNo != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteNo like '" + SupplierDeliveryNoteNo + "%'";
                if (SupplierDeliveryNoteDate != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteDate = '" + SupplierDeliveryNoteDate + "'";
                if (PaymentDueDate != "")
                    WhereCondition += " and SDN.PaymentDueDate ='" + PaymentDueDate + "'";
                if (CategoryID != 0)
                    WhereCondition += " and SDND.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and SDND.BrandID =" + BrandID;
                if (ProductID != 0)
                    WhereCondition += " and SDND.ProductID=" + ProductID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SDN.SupplierDeliveryNoteDate between '" + FromDate + "' and '" + ToDate + "'";
                ht.Add("@WhereCondition", WhereCondition);               
                ht.Add("@Transaction", "SupplierStock");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetBrandAutoComplete()
        {
            try
            {
                 ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (BrandName != "")
                    WhereCondition = " and BrandName like '" + BrandName + "%'";
                ht.Add("@WhereCondition",WhereCondition);                
                ht.Add("@Transaction", "ddlBrand");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition1 = string.Empty;
                if (BrandName != "")
                    WhereCondition = " and BrandName like '" + BrandName + "%'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetBrandID");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetProductAutoComplete()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition1 = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and ProductName like '" + ProductName + "%'";
                if (CategoryID != 0)
                    WhereCondition += " and CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and BrandID =" + BrandID;
                
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlProductAutoComplete");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);
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
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and P.ProductName ='" + ProductName + "'";
                if (CategoryID != 0)
                    WhereCondition += " and P.CategoryID=" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID= " + BrandID;
                ht.Add("@WhereCondition", WhereCondition);
         
                ht.Add("@Transaction", "GetProductIDValue");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);

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
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and ProductName ='" + ProductName + "'";
                //if (CategoryID != 0)
                //    WhereCondition += " and CategoryID=" + CategoryID;
                //if (BrandID != 0)
                //    WhereCondition += " and BrandID= " + BrandID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductCategoryBrandIDValue");
                ds = SDN.GetTransaction("SP_GetDataSDN", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
    }
    public class ESupplierDeliveryNoteList
    {
        public string CategoryName { set; get; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductValue { get; set; }
        public int Quantity { get; set; }
        public Decimal BuyingPriceperPice { get; set; }
        public Decimal GrossValue { get; set; }
        public Decimal NetBuyingPrice { get; set; }
        public int SupplierDeliveryNoteDetailID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        
         
    }
    public class ESupplierDeliveryNoteCalList
    {
        public float TotalGrossValue { get; set; }
    }
}
