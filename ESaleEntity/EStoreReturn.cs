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
    public class EStoreReturn
    {
        int _OrganisationId;

        public int OrganisationId
        {
            get { return _OrganisationId; }
            set { _OrganisationId = value; }
        }
        int _StoreId;

        public int StoreId
        {
            get { return _StoreId; }
            set { _StoreId = value; }
        }
        int _StoreReturnId;

        public int StoreReturnId
        {
            get { return _StoreReturnId; }
            set { _StoreReturnId = value; }
        }
        int _StoreReturnDetailId;

        public int StoreReturnDetailId
        {
            get { return _StoreReturnDetailId; }
            set { _StoreReturnDetailId = value; }
        }
        string _ReturnNumber;

        public string ReturnNumber
        {
            get { return _ReturnNumber; }
            set { _ReturnNumber = value; }
        }
        string _ReturnDate;

        public string ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        decimal _TotalQuantity;

        public decimal TotalQuantity
        {
            get { return _TotalQuantity; }
            set { _TotalQuantity = value; }
        }
        decimal _TotalSellingPrice;

        public decimal TotalSellingPrice
        {
            get { return _TotalSellingPrice; }
            set { _TotalSellingPrice = value; }
        }
        string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        string _GridData;

        public string GridData
        {
            get { return _GridData; }
            set { _GridData = value; }
        }
        int _LoginId;

        public int LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }
        string _FromDate;

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        string _ToDate;

        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        int _OrganisationUser;

        public int OrganisationUser
        {
            get { return _OrganisationUser; }
            set { _OrganisationUser = value; }
        }
        int _CategoryId;

        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        int _BrandId;

        public int BrandId
        {
            get { return _BrandId; }
            set { _BrandId = value; }
        }
        int _ProductId;

        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }
        string _Transaction;

        public string Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
        string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        int _ProductID;

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        string _BrandName;

        public string BrandName
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }
        int _BrandID;

        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        int _CategoryID;

        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        DataSet ds;
        DLogin SDN;
        Hashtable ht;
        string Result = string.Empty;
        string WhereCondition=string.Empty;
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
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (LoginId == 1)
                    Transaction = "ddlStore";
                else
                {
                    if (LoginId != 1 && OrganisationUser != 0)
                    {
                        Transaction = "ddlStoreForUser";
                        WhereCondition = " and L.LoginID =" + LoginId;
                    }
                    else
                    {
                        Transaction = "ddlStoreForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginId;
                    }
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
        //public DataSet ddlCategory()
        //{
        //    try
        //    {
        //        ds = new DataSet();
        //        SDN = new DLogin();
        //        ht = new Hashtable();
        //        WhereCondition = string.Empty;
        //        string WhereCondition2 = string.Empty;
        //        if (LoginId == 1)
        //            Transaction = "ddlCategory";
        //        else
        //        {
        //            if (LoginId != 1 && OrganisationUser != 0)
        //            {
        //                Transaction = "ddlCategoryForUser";
        //                WhereCondition = " and L.LoginID =" + LoginId;
        //            }
        //            else
        //            {
        //                Transaction = "ddlCategoryForOrgUser";
        //                WhereCondition2 = " and L.LoginID =" + LoginId;
        //            }
        //        }
        //        if(StoreId!=0)
        //            WhereCondition += " and ST.StoreId =" + StoreId;
        //        ht.Add("@WhereCondition", WhereCondition);
        //        ht.Add("@WhereCondition2", WhereCondition2);
        //        ht.Add("@Transaction", Transaction);
        //        ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return ds;
        //}
        public DataSet ddlBrand()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                if (LoginId == 1)
                    Transaction = "ddlBrand";
                else
                {
                    if (LoginId != 1 && OrganisationUser != 0)
                    {
                        Transaction = "ddlBrandForUser";
                        WhereCondition = " and L.LoginID =" + LoginId;
                    }
                    else
                    {
                        Transaction = "ddlBrandForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginId;
                    }
                }
                if (StoreId != 0)
                    WhereCondition += " and ST.StoreId =" + StoreId;
                if (BrandName != "")
                    WhereCondition += " and B.BrandName like '" + BrandName + "%'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                if (CategoryId != 0)
                    WhereCondition = " and P.CategoryID =" + CategoryId;
                if (BrandId != 0)
                    WhereCondition += " and P.BrandID =" + BrandId;
                if (LoginId == 1)
                    Transaction = "ddlProduct";
                else
                {
                    if (LoginId != 1 && OrganisationUser != 0)
                    {
                        Transaction = "ddlProductForUser";
                        WhereCondition += " and L.LoginID =" + LoginId;
                    }
                    else
                    {
                        Transaction = "ddlProductForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginId;
                    }
                }
                if (StoreId != 0)
                    WhereCondition += " and ST.StoreId =" + StoreId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                if (ProductId != 0)
                    WhereCondition = " and P.ProductID=" + ProductId;
                if (StoreId != 0)
                WhereCondition += " and ST.StoreId =" + StoreId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "ProductValues");
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                if (OrganisationId != 0)
                    WhereCondition = " and  SR.OrganisationID =" + OrganisationId;
                if (StoreId != 0)
                    WhereCondition += " and SR.StoreID =" + StoreId;
                if (ReturnNumber != "")
                    WhereCondition += " and SR.ReturnNumber like '" + ReturnNumber + "%'";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SR.ReturnDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginId == 1)
                    Transaction = "GetGrid";
                else
                {
                    if (LoginId != 1 && OrganisationUser != 0)
                    {
                        Transaction = "GetGridForUser";
                        WhereCondition += " and L.LoginID =" + LoginId;
                    }
                    else
                    {
                        Transaction = "GetGridForOrgUser";
                        WhereCondition2 = " and L.LoginID =" + LoginId;
                    }
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                if (StoreReturnId != 0)
                    WhereCondition = " and SR.StoreReturnId =" + StoreReturnId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "GetGridForviewEdit");
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                ht.Add("@OrganisationID", OrganisationId);
                ht.Add("@StoreId", StoreId);
                ht.Add("@StoreReturnId", 0);
                ht.Add("@StoreReturnDetailId", 0);
                ht.Add("@StoreReturnNumber", ReturnNumber);
                ht.Add("@StoreReturnDate", ReturnDate);
                ht.Add("@TotalQuantity", 0);
                ht.Add("@TotalSellingPrice", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "Insert");
                ht.Add("@LoginID", LoginId);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_StoreReturn", ht);

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
                ht.Add("@OrganisationID", OrganisationId);
                ht.Add("@StoreId", StoreId);
                ht.Add("@StoreReturnId", StoreReturnId);
                ht.Add("@StoreReturnDetailId", 0);
                ht.Add("@StoreReturnNumber", "");
                ht.Add("@StoreReturnDate", "");
                ht.Add("@TotalQuantity", TotalQuantity);
                ht.Add("@TotalSellingPrice", TotalSellingPrice);
                ht.Add("@Remarks", Remarks);
                ht.Add("@Transaction", "InsertDetails");
                ht.Add("@LoginID", LoginId);
                ht.Add("@GridData", GridData);
                ds = SDN.GetTransaction("SP_StoreReturn", ht);
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
                ht.Add("@StoreId", StoreId);
                ht.Add("@StoreReturnId", StoreReturnId);
                ht.Add("@StoreReturnDetailId", 0);
                ht.Add("@StoreReturnNumber", "");
                ht.Add("@StoreReturnDate", "");
                ht.Add("@TotalQuantity", 0);
                ht.Add("@TotalSellingPrice", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "Delete");
                ht.Add("@LoginID", LoginId);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_StoreReturn", ht);
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
                ht.Add("@StoreReturnId", StoreReturnId);
                ht.Add("@StoreReturnDetailId",StoreReturnDetailId);
                ht.Add("@StoreReturnNumber", "");
                ht.Add("@StoreReturnDate", "");
                ht.Add("@TotalQuantity", 0);
                ht.Add("@TotalSellingPrice", 0);
                ht.Add("@Remarks", "");
                ht.Add("@Transaction", "DeleteDetails");
                ht.Add("@LoginID", LoginId);
                ht.Add("@GridData", "");
                ds = SDN.GetTransaction("SP_StoreReturn", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);

            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }

        public DataSet GetStoreReturnGrid()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                Transaction = string.Empty;
                string WhereCondition2 = string.Empty;
                if (StoreId != 0)
                    WhereCondition = " and S.StoreID =" + StoreId;
                if (ReturnNumber != "")
                    WhereCondition += " and SR.ReturnNumber like '" + ReturnNumber + "%'";
                if (CategoryId != 0)
                    WhereCondition += " and SRD.CategoryID =" + CategoryId;
                if (BrandId != 0)
                    WhereCondition += " and SRD.BrandID=" + BrandId;
                if (ProductId != 0)
                    WhereCondition += " and SRD.ProductID=" + ProductId;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SR.ReturnDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginId == 1)
                    Transaction = "StoreReturnReport";
                else
                {

                    if (OrganisationUser == 0)
                    {
                        Transaction = "StoreReturnReportForOrgUser";
                        WhereCondition2 = "and L.LoginID =" + LoginId;
                    }
                    else
                    {
                        Transaction = "StoreReturnReportkForUser";
                        WhereCondition += " and L.LoginID =" + LoginId;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetStoreReturnDetailGrid()
        {
            try
            {
                ds = new DataSet();
                SDN = new DLogin();
                ht = new Hashtable();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                Transaction = string.Empty;
                if (StoreId != 0)
                    WhereCondition = " and SR.StoreID =" + StoreId;
                if (ReturnNumber != "")
                    WhereCondition += " and SR.ReturnNumber like '" + ReturnNumber + "%'";
                if (CategoryId != 0)
                    WhereCondition += " and SRD.CategoryId =" + CategoryId;
                if (BrandId != 0)
                    WhereCondition += " and SRD..BrandId=" + BrandId;
                if (ProductId != 0)
                    WhereCondition += " and SRD..ProductId=" + ProductId;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and SR.ReturnDate between '" + FromDate + "' and '" + ToDate + "'";
                if (LoginId == 1)
                    Transaction = "DetailGrid";
                else
                {

                    if (OrganisationUser == 0)
                    {
                        Transaction = "DetailGridForOrgUser";
                        WhereCondition2 += " and L.LoginID =" + LoginId;
                    }
                    else
                    {
                        Transaction = "DetailGridForUser";
                        WhereCondition += " and L.LoginID =" + LoginId;
                    }
                }
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);

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
                if (StoreReturnId != 0)
                    WhereCondition = " and SR.StoreReturnId =" + StoreReturnId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "GetGridForPrint");
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
                WhereCondition = string.Empty;
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
                string WhereCondition2 = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and P.ProductName like '" + ProductName + "%'";               
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@Transaction", "GetProductName");
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);
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
                string WhereCondition2 = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and P.ProductName ='" + ProductName + "'";              
                if (StoreId != 0)
                    WhereCondition += " and S.StoreID =" + StoreId;
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductIDValue");
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);

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
                string WhereCondition2 = string.Empty;
                if (ProductName != "")
                    WhereCondition = " and P.ProductName ='" + ProductName + "'";
                //if (CategoryID != 0)
                //    WhereCondition += " and CategoryID=" + CategoryID;
                //if (BrandID != 0)
                //    WhereCondition += " and BrandID= " + BrandID;
                if (StoreId != 0)
                    WhereCondition += " and S.StoreID =" + StoreId;
                ht.Add("@WhereCondition2", WhereCondition2);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductIDValue");
                ds = SDN.GetTransaction("SP_GetDataStoreReturn", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

      
    }
}
