using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for Login
/// </summary>
public class RemoteCallHandler
{
    MySql ms = new MySql();
    Hashtable ht = new Hashtable();
    string result = string.Empty;
    public RemoteCallHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Login Execute Request
    /// </summary>
    /// <param name="Entries"></param>
    /// <returns></returns>
    public string ExetureRequest(SortedList Entries)
    {
        string responseString = string.Empty;
        try
        {
            switch (Entries["m"].ToString().ToUpper())
            {
                //case "1":
                //    responseString = ValidateMachine(Entries["MACHINEID"].ToString(), Entries["BOOTPASSWORD"].ToString());
                //    break;
                //case "2":
                //    responseString = ValidateTerminalUser(Entries["TERMINALID"].ToString(), Entries["TERMINALPASSWORD"].ToString());
                //    break;
                //case "3":
                //    responseString = GetVendor(Entries["POSID"], Entries["DEALERID"], Entries["DATETIME"]);
                //    break;
                //case "4":
                //    responseString = GetDenomination(Entries["POSID"], Entries["DEALERID"], Entries["DATETIME"], Entries["VENDORID"], Entries["CATEGORY"].ToString());
                //    break;
                //case "5":
                //    responseString = GetPin(Entries["POSID"], Entries["DEALERID"], Entries["TERMINALNAME"], Entries["DATETIME"], Entries["VENDORID"], Entries["CATEGORY"], Entries["DENOMINATIONCODE"]);
                //    break;
                //case "6":
                //    responseString = GetPinNew(Entries["TERMINALID"].ToString(), Entries["TERMINALPASSWORD"].ToString(), Entries["TERMINALNAME"].ToString(), Entries["VENDORID"].ToString(), Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString());
                //    break;
                //case "1":
                //    responseString = GetUserNameAndPassword(Entries["DEALERCODE"].ToString(), Entries["TERMINALID"].ToString());
                //    break;
                //case "2":
                //    responseString = GetVendorNew(Entries["DEALERCODE"].ToString(), Entries["TERMINALID"].ToString(), Entries["USERNAME"].ToString(), Entries["PASSWORD"].ToString());
                //    break;
                //case "3":
                //    responseString = GetDenominationNew(Entries["DEALERCODE"].ToString(), Entries["TERMINALID"].ToString(), Entries["USERNAME"].ToString(), Entries["PASSWORD"].ToString(), Entries["VENDORID"].ToString(), Entries["CATEGORY"].ToString());
                //    break;
                //case "4":
                //    responseString = GetPinNew1(Entries["DEALERCODE"].ToString(), Entries["TERMINALID"].ToString(), Entries["USERNAME"].ToString(), Entries["PASSWORD"].ToString(), Entries["VENDORID"].ToString(), Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString());
                //    break;
                //case "5":
                //    responseString = GetTransactionTypes(Entries["POSID"], Entries["DEALERID"], Entries["TERMINALNAME"], Entries["DATETIME"], Entries["VENDORID"], Entries["CATEGORY"], Entries["DENOMINATIONCODE"]);
                //    break;
                case "MODE":
                    responseString = GetModes(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                    break;
                case "GETVENDORS":
                    responseString = GetVendorNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["CATEGORY"].ToString(), string.Empty);
                    //private string GetVendorNew(string DealerCode, string TerminalNo, string UserName, string Password)
                    break;
                case "GETCALLINGCARDVENDORS":
                    responseString = GetVendorNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["CATEGORY"].ToString(), Entries["DENOMINATIONCODE"].ToString());
                    break;
                case "GETCALLINGCARDPRODUCTS":
                    responseString = GetDenominationNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), string.Empty, Entries["CATEGORY"].ToString());
                    break;
                case "GETPRODUCTS":
                    responseString = GetDenominationNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["VENDORID"].ToString(), Entries["CATEGORY"].ToString());
                    break;
                case "GETPINFORTOPUP":
                    responseString = GetPinNew(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), Entries["VENDORID"].ToString(), Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString());
                    break;
                case "GETPINFORCALLINGCARDS":
                    responseString = GetPinNew(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), string.Empty, Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString());
                    break;
                case "GETPINFORSOLDCALLINGCARDS":
                    responseString = GetPinSoldForCalling(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), string.Empty, Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString(), Entries["PINDETAILID"].ToString());
                    break;
                case "GETPINFORSOLD":                                 
                        responseString = GetPinSold(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), Entries["VENDORID"].ToString(), Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString(), Entries["PINDETAILID"].ToString());                                    
                    break;
                //case "GETCUSTOMERAMOUNT":
                //    responseString = GETCUSTOMERAMOUNT(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                //    break;
            }
            return responseString;
        }
        catch (Exception ex)
        {
            responseString = "Invalid Request";
            return responseString;
        }
    }

    private string GetModes(string TerminalNo, string UserName, string Password)
    {
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                strWhereCond = "and D.DealerId=" + DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond);
                ht.Add("@Transaction", "GETCustomerAmount");
                ds = new DataSet();
                ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                result = "CALLINGCARD,TOPUPVOUCHER," + "€" + ds.Tables[0].Rows[0]["Amount"].ToString();
                return result;
            }
            else
                result = "DealerCode doesn't matched with UserName and Password";
        }
        else
            result = "Invalid DealerCode or TerminalNo";
        return result;
    }

    ////private string GETCUSTOMERAMOUNT(string TerminalNo, string UserName, string Password)
    ////{
    ////    string DealerId = string.Empty;
    ////    string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
    ////    ht.Add("@WhereCondition", strWhereCond);
    ////    ht.Add("@Transaction", "GetUserNameAndPasword");
    ////    DataSet ds = new DataSet();
    ////    ds = ms.ExecuteSP("SP_GetDataNew", ht);
    ////    if (ds.Tables[0].Rows.Count > 0)
    ////    {
    ////        if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
    ////        {
    ////            DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
    ////            strWhereCond = "and D.DealerId="+DealerId;
    ////            ht = new Hashtable();
    ////            ht.Add("@WhereCondition", strWhereCond);
    ////            ht.Add("@Transaction", "GETCustomerAmount");
    ////            ds = new DataSet();
    ////            ds = ms.ExecuteSP("SP_RemoteCalls", ht);
    ////            result ="CALLINGCARD,TOPUPVOUCHER," + "€"+ ds.Tables[0].Rows[0]["Amount"].ToString();
    ////            return result;
    ////        }
    ////        else
    ////            result = "Customer doesn't matched with UserName and Password";
    ////    }
    ////    else
    ////        result = "Invalid DealerCode or TerminalNo";
    ////    return result;
    ////}


    private string GetPinNew1(string DealerCode, string TerminalNo, string UserName, string Password, string VendorId, string DenominationCode, string category)
    {
        string DealerId = string.Empty;
        string VID = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "' and D.DealerCode='" + DealerCode + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                strWhereCond = "";
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond);
                ht.Add("@Transaction", "GetShortVendorName");
                ds = new DataSet();
                ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (category == "TOPUP")
                    { category = "1"; }
                    else
                    { category = "2"; }
                    VendorId = VendorId.Replace("%20", " ");
                    strWhereCond = " and v.vendorname = '" + VendorId + "' and category = '" + category + "' ";
                    ht = new Hashtable();
                    ht.Add("@WhereCondition", strWhereCond);
                    ht.Add("@Transaction", "GetDenomination");
                    ds = new DataSet();
                    ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["DenominationCode"].ToString() == DenominationCode)
                    {
                        VID = ds.Tables[0].Rows[0]["VendorId"].ToString();
                        ht = new Hashtable();
                        //ht.Add("@LoginName", UserName);
                        //ht.Add("@password", Password);
                        //ht.Add("@Terminal", TerminalNo);
                        //ht.Add("@Vendor", VendorId);
                        //ht.Add("@Denomination", DenominationCode);
                        //ht.Add("@Category", category);

                        ht.Add("@DealerID", DealerId);
                        ht.Add("@VendorID", VID);
                        ht.Add("@DenominationCode", DenominationCode);
                        ht.Add("@SessionLoginID", DealerId);
                        ht.Add("@SoldInfo", string.Empty);

                        ds = new DataSet();
                        ds = ms.ExecuteSP("SP_OnlineTransaction", ht);
                        //ds = ms.ExecuteSP("SP_RemoteCallForPin", ht);
                        if (ds.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            result = convertRowSetToString(ds);
                            result = result.Substring(0, result.Length);
                        }
                        else
                        {
                            result = ds.Tables[0].Rows[0]["Status"].ToString();
                        }
                        return result;
                    }
                    result = "Invalid DenominationCode";
                }

            }
            else
                result = "DealerCode doesn't matched with UserName and Password";
        }
        else
            result = "Invalid DealerCode or TerminalNo";
        return result;
    }

    private string GetDenominationNew(string DealerCode, string TerminalNo, string UserName, string Password, string VendorId, string category)
    {
        //string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "' and D.DealerCode='" + DealerCode + "'";
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString().ToUpper() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                strWhereCond = string.Empty;
                category = category.ToUpper().Trim().ToString();
                category = category.Replace("%20", "");
                if (category == "CALLINGCARD")
                {
                    strWhereCond = " and Category=1";
                    ht = new Hashtable();
                    ht.Add("@WhereCondition", strWhereCond);
                    ht.Add("@Transaction", "GetDenominationsForCallingCards");
                    ds = new DataSet();
                    ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = convertDataSetToString(ds);
                    }
                    else if (ds.Tables[0].Rows.Count == 0)
                        result = "No vouchers available";
                    result = result.Substring(0, result.Length - 1);
                    return result;
                }
                else if (category == "TOPUPVOUCHER")
                {
                    VendorId = VendorId.Replace("%20", " ");
                    strWhereCond = " and v.vendorname = '" + VendorId + "' and category = 2";
                    ht = new Hashtable();
                    ht.Add("@WhereCondition", strWhereCond);
                    ht.Add("@Transaction", "GetDenomination");
                    ds = new DataSet();
                    ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = convertDataSetToString(ds);
                    }
                    else if (ds.Tables[0].Rows.Count == 0)
                        result = "No vouchers available";
                    result = result.Substring(0, result.Length - 1);
                    return result;
                }
                else
                    return "Invalid Category";
                //strWhereCond = "";
                //ht = new Hashtable();
                //ht.Add("@WhereCondition", strWhereCond);
                //ht.Add("@Transaction", "GetShortVendorName");
                //ds = new DataSet();
                //ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    category = category.ToUpper().Trim().ToString();
                //    category = category.Replace("%20", "");
                //    //if (category == "TELEPHONECARDS")
                //    if (category == "CALLINGCARDS")
                //    { category = "1"; }
                //    else if (category == "TOPUPVOUCHER")
                //    { category = "2"; }
                //    else
                //        return "Invalid Category";
                //    VendorId = VendorId.Trim();
                //    VendorId = VendorId.Replace("%20", "");
                //    strWhereCond = " and v.vendorname = '" + VendorId + "' and category = '" + category + "' ";
                //    ht = new Hashtable();
                //    ht.Add("@WhereCondition", strWhereCond);
                //    ht.Add("@Transaction", "GetDenomination");
                //    ds = new DataSet();
                //    ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        result = convertDataSetToString(ds);
                //    }
                //    result = result.Substring(0, result.Length - 1);
                //    return result;
                //}

            }
            else
                result = "DealerCode doesn't matched with UserName and Password";
        }
        else
            result = "Invalid DealerCode or TerminalNo";
        return result;
    }

    private string GetVendorNew(string DealerCode, string TerminalNo, string UserName, string Password, string category, string Denomination)
    {
        //string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "' and D.DealerCode='" + DealerCode + "'";
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString().ToUpper() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                category = category.ToUpper().Trim().ToString();
                category = category.Replace("%20", "");
                if (category == "CALLINGCARD")
                {
                    strWhereCond = "and vd.SellingPrice='" + Denomination + "' and vd.Category=1 ";
                    ht = new Hashtable();
                    ht.Add("@WhereCondition", strWhereCond);
                    ht.Add("@Transaction", "GetVendorsForCallingCards");
                    ds = new DataSet();
                    ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = convertDataSetToString(ds);
                    }
                    result = result.Substring(0, result.Length - 1);

                }
                else if (category == "TOPUPVOUCHER")
                {
                    strWhereCond = "";
                    ht = new Hashtable();
                    ht.Add("@WhereCondition", strWhereCond);
                    ht.Add("@Transaction", "GetShortVendorName");
                    ds = new DataSet();
                    ds = ms.ExecuteSP("SP_RemoteCalls", ht);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = convertDataSetToString(ds);
                    }
                    result = result.Substring(0, result.Length - 1);
                    //return "$AIRTEL,HUTCH,RELIANCE^";
                    return result;
                }
                else
                    return "Invalid Category";
            }
            else
                result = "DealerCode doesn't matched with UserName and Password";
        }
        else
            result = "Invalid DealerCode or TerminalNo";
        return result;
    }

    private string GetUserNameAndPassword(string DealerCode, string TerminalNo)
    {

        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "' and D.DealerCode='" + DealerCode + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = convertRowSetToString(ds);
        }
        else
            result = "Invalid DealerCode or TerminalNo";
        return result;

    }
    private string GetPinNew(string LoginName, string password, string Terminal, string Vendor, string Denomination, string Category)
    {

        Category = Category.ToUpper().Trim().ToString();
        Category = Category.Replace("%20", "");
        //Vendor = Vendor.ToUpper().Trim().ToString();
        Vendor = Vendor.Replace("%20", "");
        Denomination = Denomination.Replace("%20", "");
        DataSet ds;
        if (Category == "CALLINGCARD")
        {
            string strWhereCond = " and Category=1 and vd.DenominationCode='"+Denomination+"'";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond);
            ht.Add("@Transaction", "GetProvidersForCallingCards");
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_RemoteCalls", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Vendor = ds.Tables[0].Rows[0]["VendorName"].ToString();
                Category = "1";
            }

        }
        else if (Category == "TOPUPVOUCHER")
        {
            Category = "2";
        }
        else
            return "Invalid Category";
        ht = new Hashtable();
        Denomination = Denomination.Replace("%20", " ");
        //string pinno = "";
        ht.Add("@LoginName", LoginName);
        ht.Add("@password", password);
        ht.Add("@Terminal", Terminal);
        ht.Add("@Vendor", Vendor);
        ht.Add("@Denomination", Denomination);
        ht.Add("@Category", Category);
        //ht.Add("@pinNo", pinno);

        //string strWhereCond1 = " and V.Category=" + Category + " and V.DenominationCode='" + Denomination + "' and Vn.VendorName='" + Vendor + "'";
        //ht.Add("@WhereCondition", strWhereCond1);
        //ht.Add("@Transaction", "GetPinNo");
        ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCallForPinWithOutPin", ht);
        if (ds.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
        {
            string strWhereCond1 = " and L.LoginName like '" + LoginName + "'";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond1);
            ht.Add("@Transaction", "GetDealerName");
            DataSet ds1 = new DataSet();
            ds1 = ms.ExecuteSP("SP_RemoteCalls", ht);

            result = convertRowSetToString(ds) + ds1.Tables[0].Rows[0]["DealerName"] + ",";
            result = result.Substring(0, result.Length - 1);

            if (Category == "2")
            {
                result = ds.Tables[0].Rows[0]["Status"] + "," + ds.Tables[0].Rows[0]["PinDetailId"] + "," + ds.Tables[0].Rows[0]["PinNo"] + "," + ds.Tables[0].Rows[0]["SerialNo"] + "," + ds.Tables[0].Rows[0]["CustomerServiceNo"] + "," + ds.Tables[0].Rows[0]["Info"] + "," + ds.Tables[0].Rows[0]["SellingPrice"] + "," + ds1.Tables[0].Rows[0]["DealerName"] + ","; 
                result = result.Substring(0, result.Length - 1);
               
            }
            
        }
        else
        {
            result = ds.Tables[0].Rows[0]["Status"].ToString();
        }
        return result;
    }


    private string GetPinSold(string LoginName, string password, string Terminal, string Vendor, string Denomination, string Category, string PinDetailId)
    {

        Category = Category.ToUpper().Trim().ToString();
        Category = Category.Replace("%20", "");
        //Vendor = Vendor.ToUpper().Trim().ToString();
        Vendor = Vendor.Replace("%20", "");
        DataSet ds;
        if (Category == "CALLINGCARD")
        {
            string strWhereCond = " and Category=1 and vd.DenominationCode='" + Denomination + "'";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond);
            ht.Add("@Transaction", "GetProvidersForCallingCards");
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_RemoteCalls", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Vendor = ds.Tables[0].Rows[0]["VendorName"].ToString();
                Category = "1";
            }

        }
        else if (Category == "TOPUPVOUCHER")
        {
            Category = "2";
        }
        else
            return "Invalid Category";
        ht = new Hashtable();
         Denomination = Denomination.Replace("%20", " ");
       
        ht.Add("@LoginName", LoginName);
        ht.Add("@password", password);
        ht.Add("@Terminal", Terminal);
        ht.Add("@Vendor", Vendor);
        ht.Add("@Denomination", Denomination);
        ht.Add("@Category", Category);
        ht.Add("@PinDetailId", PinDetailId);

        //string strWhereCond1 = " and V.Category=" + Category + " and V.DenominationCode='" + Denomination + "' and Vn.VendorName='" + Vendor + "'";
        //ht.Add("@WhereCondition", strWhereCond1);
        //ht.Add("@Transaction", "GetPinNo");
        ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCallForPin", ht);
        if (ds.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
        {
            
             string strWhereCond1 = " and L.LoginName like '"+LoginName+"'";
             ht = new Hashtable();
             ht.Add("@WhereCondition", strWhereCond1);
             ht.Add("@Transaction", "GetDealerName");
             DataSet ds1 = new DataSet();
             ds1 = ms.ExecuteSP("SP_RemoteCalls", ht);

             result = convertRowSetToString(ds) + ds1.Tables[0].Rows[0]["DealerName"] + ",";
            result = result.Substring(0, result.Length - 1);
        }
        else
        {
            result = ds.Tables[0].Rows[0]["Status"].ToString();
        }
        return result;
    }


    private string GetPinSoldForCalling(string LoginName, string password, string Terminal, string Vendor, string Denomination, string Category, string PinDetailId)
    {

        Category = Category.ToUpper().Trim().ToString();
        Category = Category.Replace("%20", "");
        //Vendor = Vendor.ToUpper().Trim().ToString();
        Vendor = Vendor.Replace("%20", "");
        DataSet ds;
        if (Category == "CALLINGCARD")
        {
            string strWhereCond = " and Category=1 and vd.DenominationCode='" + Denomination + "'";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond);
            ht.Add("@Transaction", "GetProvidersForCallingCards");
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_RemoteCalls", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Vendor = ds.Tables[0].Rows[0]["VendorName"].ToString();
                Category = "1";
            }

        }
        else if (Category == "TOPUPVOUCHER")
        {
            Category = "2";
        }
        else
            return "Invalid Category";
        ht = new Hashtable();
        Denomination = Denomination.Replace("%20", " ");

        ht.Add("@LoginName", LoginName);
        ht.Add("@password", password);
        ht.Add("@Terminal", Terminal);
        ht.Add("@Vendor", Vendor);
        ht.Add("@Denomination", Denomination);
        ht.Add("@Category", Category);
        ht.Add("@PinDetailId", PinDetailId);

        //string strWhereCond1 = " and V.Category=" + Category + " and V.DenominationCode='" + Denomination + "' and Vn.VendorName='" + Vendor + "'";
        //ht.Add("@WhereCondition", strWhereCond1);
        //ht.Add("@Transaction", "GetPinNo");
        ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCallForPin", ht);
        if (ds.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
        {

            string strWhereCond1 = " and L.LoginName like '" + LoginName + "'";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond1);
            ht.Add("@Transaction", "GetDealerName");
            DataSet ds1 = new DataSet();
            ds1 = ms.ExecuteSP("SP_RemoteCalls", ht);

            result = convertRowSetToString(ds) + ds1.Tables[0].Rows[0]["DealerName"] + ",";
            result = result.Substring(0, result.Length - 1);
        }
        else
        {
            result = ds.Tables[0].Rows[0]["Status"].ToString();
        }
        return result;
    }

    private string GetPin(object posID, object dealerName, object TerminalName, object dateTime, object vendorName, object category, object denominationCode)
    {
        string DealerID = string.Empty;
        string VendorId = string.Empty;

        //@DealerID int,      
        //@VendorID Int,    
        //@DenominationCode varchar(30),    
        //@SessionLoginID INT = -2,    
        //@SoldInfo NVarchar(2500) = NULL 
        TerminalName = ReplaceString(TerminalName.ToString());
        string strWhereCond = " and Terminals.TerminalNO ='" + TerminalName + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GETTERMINALS");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DealerID = ds.Tables[0].Rows[0]["DealerID"].ToString();  //Fetch the dealerID from the Terminal No
        }

        strWhereCond = "  where TerminalID ='" + posID + "' and DealerID ='" + DealerID + "'";
        ht = new Hashtable();
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetValidTerminal");
        ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCalls", ht);//Checkinhg If the posId  is correct
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            ht = new Hashtable();
            strWhereCond = " and vendors.vendorName ='" + vendorName + "'";
            ht.Add("@WhereCondition", strWhereCond);
            ht.Add("@Transaction", "GETVENDORS_FROMID");
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_GetDataNew", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                VendorId = ds.Tables[0].Rows[0]["VendorId"].ToString();//Fetch the vendorId from the vendorname
            }

            //( =%20,€=%EF%BF%BD)
            denominationCode = ReplaceString(denominationCode.ToString());
            ht = new Hashtable();
            ht.Add("@DealerID", DealerID);
            ht.Add("@VendorID", VendorId);
            ht.Add("@DenominationCode", denominationCode);
            ht.Add("@SessionLoginID", posID.ToString());
            ht.Add("@SoldInfo", string.Empty);
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_OnlineTransaction", ht);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString().ToUpper() == "SUCCESS")
                {
                    result = convertRowSetToString(ds);
                    result = result.Substring(0, result.Length);
                }
                else
                {
                    result = ds.Tables[0].Rows[0]["Status"].ToString();
                }
            }

            return result;
        }
        else { result = "INVALID PARAM VALUE"; }

        return result;
    }

    private string GetDenomination(object posID, object dealerID, object dateTime, object vendorID, string category)
    {
        string strWhereCond = "  where TerminalID ='" + posID + "' and DealerID ='" + dealerID + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetValidTerminal");//Checking If the posId  is correct
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCalls", ht);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (category == "TOPUP")
            { category = "1"; }
            else
            { category = "2"; }
            strWhereCond = " and v.vendorname = '" + vendorID + "' and category = '" + category + "' ";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond);
            ht.Add("@Transaction", "GetDenomination");
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_RemoteCalls", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = convertDataSetToString(ds);
            }

            return result;
            //return "$10,15,20,30^";
        }
        else { result = "INVALID PARAM VALUE"; }

        return result;
    }

    private string GetVendor(object posID, object dealerID, object dateTime)
    {
        string strWhereCond = "  where TerminalID ='" + posID + "' and DealerID ='" + dealerID + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetValidTerminal");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCalls", ht);//Checkinhg If the posId  is correct
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            strWhereCond = "";
            ht = new Hashtable();
            ht.Add("@WhereCondition", strWhereCond);
            ht.Add("@Transaction", "GetShortVendorName");
            ds = new DataSet();
            ds = ms.ExecuteSP("SP_RemoteCalls", ht);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = convertDataSetToString(ds);
            }
            //return "$AIRTEL,HUTCH,RELIANCE^";
            return result;
        }
        else { result = "INVALID PARAM VALUE"; }
        return result;

    }



    private string ValidateMachine(string MachineId, string BootPassword)
    {
        return "true"; // "false" 
    }

    private string ValidateTerminalUser(string TUserId, string TPassword)
    {
        string strWhereCond = " And loginname = '" + TUserId.Trim() + "' and password = '" + TPassword.Trim() + "' ";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "ValidateTerminalUser");
        DataSet ds = new DataSet();
        ds = ms.ExecuteSP("SP_RemoteCalls", ht);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return "1";
        }
        else
        {
            return "0";
        }

    }

    private string convertDataSetToString(DataSet ds)
    {
        string strResult = string.Empty;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            strResult += ds.Tables[0].Rows[i][0].ToString() + ",";
        }
        return strResult;
    }

    private string convertRowSetToString(DataSet ds)
    {
        string strResult = string.Empty;
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            strResult += ds.Tables[0].Rows[0][i].ToString() + ",";
        }

        return strResult;

    }
    private string ReplaceString(string MainString)
    {
        MainString = MainString.Replace("%20", " ");
        MainString = MainString.Replace("%EF%BF%BD", "€");
        return MainString;

        

    }

// Method for Reports
    public string ExetureRequestReport(SortedList Entries)
    {
        string responseString = string.Empty;
        try
        {
            switch (Entries["m"].ToString().ToUpper())
            {
                case "DAYREPORT":
                    responseString = GetDayReport(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                    break;
                case "WEEKREPORT":
                    responseString = GetweekReport(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());                   
                    break;
                case "MONTHREPORT":
                    responseString = GetMonthReport(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                  break;

                case "DAYPROFITREPORT":
                  responseString = GetDayFROFITReport(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                  break;
                case "WEEKPROFITREPORT":
                  responseString = GetweekFROFITReport(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                  break;
                case "MONTHPROFITREPORT":
                  responseString = GetMonthtFROFITReport(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                  break;
                case "TOPUPHISTORYREPORT":
                  responseString = GetTOPUpHistory(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                  break;
                //case "GETCALLINGCARDVENDORS":
                //    responseString = GetVendorNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["CATEGORY"].ToString(), Entries["DENOMINATIONCODE"].ToString());
                //    break;
                //case "GETCALLINGCARDPRODUCTS":
                //    responseString = GetDenominationNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), string.Empty, Entries["CATEGORY"].ToString());
                //    break;
                //case "GETPRODUCTS":
                //    responseString = GetDenominationNew(string.Empty, Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["VENDORID"].ToString(), Entries["CATEGORY"].ToString());
                //    break;
                //case "GETPINFORTOPUP":
                //    responseString = GetPinNew(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), Entries["VENDORID"].ToString(), Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString());
                //    break;
                //case "GETPINFORCALLINGCARDS":
                //    responseString = GetPinNew(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), string.Empty, Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString());
                //    break;
                //case "GETPINFORSOLDCALLINGCARDS":
                //    responseString = GetPinSoldForCalling(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), string.Empty, Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString(), Entries["PINDETAILID"].ToString());
                //    break;
                //case "GETPINFORSOLD":
                //    responseString = GetPinSold(Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString(), Entries["TERMINALID"].ToString(), Entries["VENDORID"].ToString(), Entries["DENOMINATIONCODE"].ToString(), Entries["CATEGORY"].ToString(), Entries["PINDETAILID"].ToString());
                //    break;
                ////case "GETCUSTOMERAMOUNT":
                ////    responseString = GETCUSTOMERAMOUNT(Entries["TERMINALID"].ToString(), Entries["LOGINID"].ToString(), Entries["LOGINPASSWORD"].ToString());
                ////    break;
            }
            return responseString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public string GetDayReport(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 = DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "DayReport");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringReports(ds1);
                }
                else
                {
                    result = "No Sales are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }
        }
        else
            result = "Invalid DealerCode or TerminalNo";
        return result;


        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "DayReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;
      }

    public string GetweekReport(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 = DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "WeeklyReport");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringReports(ds1);
                }
                else
                {
                    result = "No Sales are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }
        }
        else
        {
            result = "Invalid DealerCode or TerminalNo";
        }

        return result;

        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "WeeklyReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;
    }

    public string GetMonthReport(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 = DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "MonthlyReport");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringReports(ds1);
                }
                else
                {
                    result = "No Sales are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }

        }
        else
        {
            result = "Invalid DealerCode or TerminalNo";
        }
        return result;

        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "MonthlyReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;
    }

    public string GetDayFROFITReport(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 =DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "DAYProfitReportNEW");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringReports(ds1);
                }
                else
                {
                    result = "No Sales are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }
          
        }
        else
        {
            result = "Invalid DealerCode or TerminalNo";
        }
        return result;

        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "DayProfitReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;
   }

    public string GetweekFROFITReport(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 = DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "WEEKProfitReportNEW");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringReports(ds1);
                }
                else
                {
                    result = "No Sales are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }

        }
        else
        {
            result = "Invalid DealerCode or TerminalNo";
        }
        return result;

        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "WeeklyProfitReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;


    }
    public string GetMonthtFROFITReport(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 = DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "MONTHProfitReportNEW");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringReports(ds1);
                }
                else
                {
                    result = "No Sales are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }

        }
        else
        {
            result = "Invalid DealerCode or TerminalNo";
        }
        return result;

        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "MonthlyProfitReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;


    }
    private string convertRowSetToStringReports(DataSet ds)
    {
       
        string strResult = string.Empty;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                strResult += ds.Tables[0].Rows[i][j].ToString() + ",";
            }
            strResult = strResult.Substring(0, strResult.Length - 1);
            strResult = strResult + "%";

        }
        strResult =String.Format("{0:dd-MM-yyyy HH:mm:ss}", ds.Tables[2].Rows[0]["FromDate"]) + "%" + String.Format("{0:dd-MM-yyyy HH:mm:ss}", ds.Tables[2].Rows[0]["ToDate"])+"%"+ strResult + "Total-" + ds.Tables[1].Rows[0]["Total"].ToString();
        return strResult;

    }

    private string convertRowSetToStringTopUpReports(DataSet ds)
    {

        string strResult = string.Empty;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                strResult += ds.Tables[0].Rows[i][j].ToString() + ",";
            }
            strResult = strResult.Substring(0, strResult.Length - 1);
            strResult = strResult + "%";

        } 
        return strResult;

    }


    public string GetTOPUpHistory(string TerminalNo, string UserName, string Password)
    {
        string result = string.Empty;
        string DealerId = string.Empty;
        string strWhereCond = " and L.LoginTypeID=(select TypeId from LoginTypes where LoginType='Dealer') and T.TerminalNo='" + TerminalNo + "'";
        ht.Add("@WhereCondition", strWhereCond);
        ht.Add("@Transaction", "GetUserNameAndPasword");
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = ms.ExecuteSP("SP_GetDataNew", ht);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == UserName && ds.Tables[0].Rows[0]["Password"].ToString() == Password)
            {
                DealerId = ds.Tables[0].Rows[0]["DealerId"].ToString();
                string strWhereCond1 = DealerId;
                ht = new Hashtable();
                ht.Add("@WhereCondition", strWhereCond1);
                ht.Add("@Transaction", "GetTopUpHistory");
                ds1 = ms.ExecuteSP("SP_GetReportsData", ht);
                //DataTable mytable = new DataTable();
                //ds1.Merge(mytable);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = ds1.Tables[0].Columns.Add("SLNO");
                    col.SetOrdinal(0);

                    int j = 1;
                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        //dr["CreatedDt"] = String.Format("{0:dd-MM-yyyy HH:mm:ss}", ds1.Tables[0].Rows[0]["CreatedDt"]);
                        dr["SLNO"] = j.ToString();

                        j++;
                    }
                    result = convertRowSetToStringTopUpReports(ds1);
                }
                else
                {
                    result = "No Investments are Done..";

                }
            }
            else
            {
                result = "DealerCode doesn't matched with UserName and Password";
            }

        }
        else
        {
            result = "Invalid DealerCode or TerminalNo";
        }
        return result;

        //ht.Add("@WhereCondition", "");
        //ht.Add("@Transaction", "MonthlyReport");
        //DataSet ds = new DataSet();
        //ds = ms.ExecuteSP("SP_GetReportsData", ht);
        //return ds;
    }

}
