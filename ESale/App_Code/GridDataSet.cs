using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ESaleEntity;
/// <summary>
/// Summary description for GridDataSet
/// </summary>
public class GridDataSet
{
    public int PageNumber { get; set; }
    public int TotalRecords { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public List<ESales> eSales { get; set; }
    public List<ESales> eSalePriscription { get; set; }
    public List<ESaleList> eSaleslist { get; set; }
    public List<ESupplierDeliveryNote> eSupplier { get; set; }
    public List<ESupplierDeliveryNoteList> eSupplierlist { get; set; }
    public List<ESupplierDeliveryNoteCalList> eSupplierlistCals { get; set; }
    public List<EStoreDeliveryNote> eStore { get; set; }
    public List<EStoreDeliveryList> eStoreList { get; set; }
    
}
