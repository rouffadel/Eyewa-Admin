using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleEntity.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace ESaleEntity
{
    public class EStoreDeliveryNote
    {
        DLogin storeDeliveryNote;
        string WhereCondition;
        string Transaction;
        DataSet ds;
        Hashtable ht;
        string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        string _BrandName;

        public string BrandName
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }
        private int _OrganisationID;

        public int OrganisationID
        {
            get { return _OrganisationID; }
            set { _OrganisationID = value; }
        }
        private int _StoreID;

        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        private string _DeliveryNoteNo;

        public string DeliveryNoteNo
        {
            get { return _DeliveryNoteNo; }
            set { _DeliveryNoteNo = value; }
        }
        private string _DeliveryNoteDate;

        public string DeliveryNoteDate
        {
            get { return _DeliveryNoteDate; }
            set { _DeliveryNoteDate = value; }
        }
        private string _PaymentDueDate;

        public string PaymentDueDate
        {
            get { return _PaymentDueDate; }
            set { _PaymentDueDate = value; }
        }
        private int _StoreDeliveryNoteID;

        public int StoreDeliveryNoteID
        {
            get { return _StoreDeliveryNoteID; }
            set { _StoreDeliveryNoteID = value; }
        }
        private int _StoreDeliveryNoteDetailID;

        public int StoreDeliveryNoteDetailID
        {
            get { return _StoreDeliveryNoteDetailID; }
            set { _StoreDeliveryNoteDetailID = value; }
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
        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private int _BrandID;

        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        public string StoreName { get; set; }
        public string  Address { get; set; }
        public string City { get; set; }
        public string Vatid { get; set; }
        public string ContactNum { get; set; }
        public string Email { get; set; }
        public string Zipcode { get; set; }
        public string OrganisationName { get; set; }
        public decimal TotalGrossValue { get; set; }
        public int OrganisationUser { get; set; }
        public DataSet ddlOrganisation()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                ht.Add("@WhereCondition", "");
                ht.Add("@Transaction", "ddlOrganisation");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
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
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transaction = string.Empty;
                
                if (LoginID == 1)
                {                   
                    Transaction="ddlStore";
                }
                else
                {
                    if (OrganisationUser != 0)
                    {
                        WhereCondition += " and L.LoginID =" + LoginID;
                        Transaction = "ddlStoreForUser";
                    }
                    else
                    {
                        Transaction = "ddlStoreForOrgUser";
                        WhereCondition2 += " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);
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
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;                
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlCategory");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
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
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlBrand");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
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
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                if (CategoryID != 0)
                    WhereCondition = " and P.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID =" + BrandID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlProduct");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
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
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                if (ProductID != 0)
                    WhereCondition = " and P.ProductID =" + ProductID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductValue");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet StorGrid()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                Transaction = string.Empty;
                string wherecondition2 = string.Empty;
                if (OrganisationID != 0)
                    WhereCondition = " and S.OrganisationID =" + OrganisationID;
                if (StoreID != 0)
                    WhereCondition += " and S.storeID= " + StoreID;
                if (DeliveryNoteNo != "")
                    WhereCondition += " and SDN.StoreDeliveryNoteNo like '" + DeliveryNoteNo + "%'";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SDN.StoreDeliveryNoteDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                {
                    Transaction = "StoreGrid";
                }
                else 
                {
                    if (OrganisationUser == 0)
                    {
                        Transaction = "StoreGridForOrgUser";
                        wherecondition2 = " and L.LoginID=" + LoginID;
                    }
                    else
                    {
                        Transaction = "StoreGridForUser";
                        WhereCondition += " and L.LoginID=" + LoginID;
                    }
                    
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@whereCondition2", wherecondition2);
                ht.Add("@Transaction", Transaction);
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }

        public DataSet StorGridForViewEdit()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                if (StoreDeliveryNoteID != 0)
                    WhereCondition = " and SDN.StoreDeliveryNoteID =" + StoreDeliveryNoteID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "StoreGridForViewEdit");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
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
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                ht.Add("@OrganisationID",OrganisationID);
                ht.Add("@StoreID",StoreID);
                ht.Add("@StoreDeliveryNoteNo",DeliveryNoteNo);
                ht.Add("@StoreDeliveryNoteDate",DeliveryNoteDate);
                ht.Add("@StoreDeliveryNoteDetailID", 0);
                ht.Add("@PaymentDueDate",PaymentDueDate);
                ht.Add("@StoreDeliveryNoteID",0);
                ht.Add("@NetProductValue",0);
                ht.Add("@HandlingCharges",0);
                ht.Add("@TotalValue",0);
                ht.Add("@Remarks","");
                ht.Add("@LoginID",LoginID);
                ht.Add("@GridData", "");
                ht.Add("@ProductID1", 0);
                ht.Add("@Transaction", "INSERTSDN");
                ds = storeDeliveryNote.GetTransaction("SP_StoreDeliveyNote", ht);
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
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                ht.Add("@OrganisationID",0);
                ht.Add("@StoreID", StoreID);
                ht.Add("@StoreDeliveryNoteID",StoreDeliveryNoteID);
                ht.Add("@StoreDeliveryNoteDetailID",0);
                ht.Add("@StoreDeliveryNoteNo","");
                ht.Add("@StoreDeliveryNoteDate","");
                ht.Add("@PaymentDueDate","");
                ht.Add("@NetProductValue",NetProductValue);
                ht.Add("@HandlingCharges",HandlingCharges);
                ht.Add("@TotalValue",TotalValue);
                ht.Add("@LoginID",LoginID);
                ht.Add("@GridData",GridData);
                ht.Add("@Remarks",Remarks);
                ht.Add("@ProductID1", 0);
                ht.Add("@Transaction", "INSERTSDND");
                ds = storeDeliveryNote.GetTransaction("SP_StoreDeliveyNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());

        }
        public string DeleteSDN()
        {
            string result = string.Empty;
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                ht.Add("@OrganisationID", 0);
                ht.Add("@StoreID", 0);
                ht.Add("@StoreDeliveryNoteID", StoreDeliveryNoteID);
                ht.Add("@StoreDeliveryNoteDetailID", 0);
                ht.Add("@StoreDeliveryNoteNo", "");
                ht.Add("@StoreDeliveryNoteDate", "");
                ht.Add("@PaymentDueDate", "");
                ht.Add("@NetProductValue", 0);
                ht.Add("@HandlingCharges", 0);
                ht.Add("@TotalValue", 0);
                ht.Add("@LoginID", LoginID);
                ht.Add("@GridData", "");
                ht.Add("@Remarks", "");
                ht.Add("@ProductID1", 0);
                ht.Add("@Transaction", "DELETESDN");
                ds = storeDeliveryNote.GetTransaction("SP_StoreDeliveyNote", ht);
                result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }

            catch (Exception)
            {

                throw;
            }
            return result;

        }
        public string DeleteSDND()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                ht.Add("@OrganisationID",0);
                ht.Add("@StoreID",StoreID);
                ht.Add("@StoreDeliveryNoteID",0);
                ht.Add("@StoreDeliveryNoteDetailID",StoreDeliveryNoteDetailID);
                ht.Add("@StoreDeliveryNoteNo","");
                ht.Add("@StoreDeliveryNoteDate","");
                ht.Add("@PaymentDueDate","");
                ht.Add("@NetProductValue",0);
                ht.Add("@HandlingCharges",0);
                ht.Add("@TotalValue",0);
                ht.Add("@LoginID",LoginID);
                ht.Add("@GridData","");
                ht.Add("@Remarks","");
                ht.Add("@ProductID1",ProductID);
                ht.Add("@Transaction", "DELETESDND");
                ds = storeDeliveryNote.GetTransaction("SP_StoreDeliveyNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());

        }
        public DataSet GetID()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                ht.Add("@OrganisationID",0);
                ht.Add("@StoreID",StoreID);
                ht.Add("@StoreDeliveryNoteID",0);
                ht.Add("@StoreDeliveryNoteDetailID", 0);
                ht.Add("@StoreDeliveryNoteNo","");
                ht.Add("@StoreDeliveryNoteDate","");
                ht.Add("@PaymentDueDate","");
                ht.Add("@NetProductValue",0);
                ht.Add("@HandlingCharges",0);
                ht.Add("@TotalValue",0);
                ht.Add("@LoginID",LoginID);
                ht.Add("@Remarks","");
                ht.Add("@GridData","");
                ht.Add("@ProductID1", ProductID);
                ht.Add("@Transaction", "GetID");
                ds = storeDeliveryNote.GetTransaction("SP_StoreDeliveyNote", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet GetStoreDeliveryNoteStock()
        {
            try
            {
                ds = new DataSet();
                storeDeliveryNote = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                Transaction = string.Empty;
                string WhereCondition2 = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and S.StoreID =" + StoreID;
                if (DeliveryNoteNo != "")
                    WhereCondition += " and SDN.StoreDeliveryNoteNo like '" + DeliveryNoteNo + "%'";
                if (CategoryID != 0)
                    WhereCondition += " and SDND.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and SDND.BrandID=" + BrandID;
                if (ProductID != 0)
                    WhereCondition += " and SDND.ProductID=" + ProductID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SDN.StoreDeliveryNoteDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                    Transaction = "StoreDeliveryNoteStock";
                else
                {

                    if (OrganisationUser == 0)
                    {
                        Transaction = "StoreDeliveryNoteStockForOrgUser";
                        WhereCondition2 = "and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transaction = "StoreDeliveryNoteStockForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction",Transaction);
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetStoreDetailReportGrid()
        {
            try
            {
                ds = new DataSet();
                storeDeliveryNote = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transaction = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and ST.StoreID =" + StoreID;
                if (DeliveryNoteNo != "")
                    WhereCondition += " and STN.StoreDeliveryNoteNo like '" + DeliveryNoteNo + "%'";
                if (CategoryID != 0)
                    WhereCondition += " and STND.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and STND.BrandID=" + BrandID;
                if (ProductID != 0)
                    WhereCondition += " and STND.ProductID=" + ProductID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and STN.StoreDeliveryNoteDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginID == 1)
                    Transaction = "DetailGrid";
                else
                {
                   
                    if (OrganisationUser == 0)
                    {
                        Transaction = "DetailGridForOrgUser";
                        WhereCondition2 += " and L.LoginID =" + LoginID;
                    }
                    else
                    {
                        Transaction = "DetailGridForUser";
                        WhereCondition += " and L.LoginID =" + LoginID;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetStoreAvailableStock()
        {
            try
            {
                ds = new DataSet();
                storeDeliveryNote = new DLogin();
                ht = new Hashtable();
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
                if (ToDate != "")
                {
                   // WhereCondition += "and Convert(date,S.CreatedDate,105) between Convert(date,'" + FromDate +"',105 )and Convert(date,'" + ToDate + "',105)"; 
                    WhereCondition += " and Convert(date,S.CreatedDate,105) <= Convert(date,'" + ToDate + "',105)"; 
                }
             
                if (LoginID == 1)
                {
                    ht.Add("@Transaction", "StoreAvailableStock");
                }
                else if (LoginID != 1 && OrganisationUser==0)
                {
                    ht.Add("@Transaction", "StoreAvailableStockForOrgUser");
                    WhereCondition2 += " and L.LoginId=" + LoginID;
                }
                else if (LoginID != 1 && OrganisationUser!=0)
                {
                    ht.Add("@Transaction", "StoreAvailableStockForUser");
                    WhereCondition += " and L.LoginId=" + LoginID;
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);

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
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and SDN.StoreDeliveryNoteID =" + StoreDeliveryNoteID;
                ht.Add("@WhereCondition", WhereCondition);               
                ht.Add("@Transaction", "PrintData");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;

        }

        public DataSet PrintGridStoreDetails()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and SDN.StoreDeliveryNoteID =" + StoreDeliveryNoteID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GridStoreDetails");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;

        }
        public DataSet PrintTotalCharges()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and SDN.StoreDeliveryNoteID =" + StoreDeliveryNoteID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "TotalCharges");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;

        }
        public DataSet GetProductDetails()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " P.ProductId =" + ProductID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductDetails");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;

        }
        public DataSet DataToPrint1()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and SDNd.StoreDeliveryNoteDetailID =" + StoreDeliveryNoteDetailID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "PrintData1");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;

        }

        public DataSet PrintGridStoreDetails1()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and SDND.StoreDeliveryNoteDetailID =" + StoreDeliveryNoteDetailID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GridStoreDetails");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;

        }
        public DataSet PrintTotalCharges1()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                storeDeliveryNote = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and sd.StoreDeliveryNoteDetailID =" + StoreDeliveryNoteDetailID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "TotalCharges1");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataForPrintStoreDeliveryNote", ht);
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
                storeDeliveryNote = new DLogin();
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
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);

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
                storeDeliveryNote = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition1 = string.Empty;
                if (BrandName != "")
                    WhereCondition = " and BrandName like '" + BrandName + "%'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetBrandID");
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);
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
                storeDeliveryNote = new DLogin();
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
                ds = storeDeliveryNote.GetTransaction("SP_GetDataStoreDeliveryNote", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }


        public string imagename { get; set; }
    }
    public class EStoreDeliveryList
    {
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string MyProperty { get; set; }
        public float ProductValue  { get; set; }
        public float  Quantity { get; set; }
        public float GrossValue  { get; set; }
        public float BuyingPrice { get; set; }
        public float TotalBuyingPrice { get; set; }

        public int ProductId { get; set; }
    }
}
