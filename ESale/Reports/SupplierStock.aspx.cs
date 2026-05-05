using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESaleEntity;
using System.IO;
public partial class Reports_SupplierStock : System.Web.UI.Page
{
    ESupplierDeliveryNote Supplier;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            FillSupplier();
            FillCategory();
            FillBrand();
            txtFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            txtToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
    private void FillProduct()
    {
        try
        {
            Supplier = new ESupplierDeliveryNote();
            ds = new DataSet();
            int categoryid = Convert.ToInt32(ddlCategory.SelectedValue);
            int brandid = Convert.ToInt32(ddlBrand.SelectedValue);
            Supplier.CategoryID = categoryid;
            Supplier.BrandID = brandid;
            ds = Supplier.ddlProducts();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlProduct.DataSource = ds.Tables[0];
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductID";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("--Any--", "0"));

            }
            else
            {
                ddlProduct.Items.Insert(0, new ListItem("--Any--", "0"));

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void FillBrand()
    {
        Supplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = Supplier.ddlBrand();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBrand.DataSource = ds.Tables[0];
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        else
        {
            ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));

        }
    }

    private void FillCategory()
    {
        Supplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = Supplier.ddlCategory();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCategory.DataSource = ds.Tables[0];
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        else
        {
            ddlCategory.Items.Insert(0, new ListItem("--Any--", "0"));

        }
    }

    private void FillSupplier()
    {
        Supplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = Supplier.ddlSupplier();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.DataSource = ds.Tables[0];
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataValueField = "SupplierID";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        else
        {
            ddlSupplier.Items.Insert(0, new ListItem("--Any--", "0"));

        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        //lblstatus.Text = "";
        lblTitle.Text = "Supplier Stock";
        //pnlgrid.Visible = false;
        Session["ctrl"] = gvStock;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvStock);
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Supplier Stock Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=SupplierStock Report.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";

        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.HeaderRow.Style.Add("background-color", "#ccc");
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            GridViewRow row = gv.Rows[i];

            row.BackColor = System.Drawing.Color.White;
            if (i % 2 != 0)
            {
                gv.Rows[i].Style.Add("background-color", "#f2f2f2");
            }
            else
            {
                gv.Rows[i].Style.Add("background-color", "#ffffff");
            }

        }
        gv.Parent.Controls.Add(form);
        form.Attributes["runat"] = "server";
        form.Controls.Add(gv);
        this.Controls.Add(form);
        lblTitle.Visible = true;
        lblTitle.RenderControl(htextw);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            Supplier = new ESupplierDeliveryNote();
            ds = new DataSet();
            if (ddlSupplier.SelectedValue != "0")
                Supplier.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            else
                Supplier.SupplierID = 0;
            if (ddlCategory.SelectedValue != "0")
                Supplier.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Supplier.CategoryID = 0;
            if (ddlBrand.SelectedValue != "0")
                Supplier.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Supplier.BrandID = 0;
            if (ddlProduct.SelectedValue != "" && ddlProduct.SelectedValue != "0")
                Supplier.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            else
                Supplier.ProductID = 0;
            if (txtFromDate.Text != "")
                Supplier.FromDate = txtFromDate.Text;
            else
                Supplier.FromDate = "";
            if (txtToDate.Text != "")
                Supplier.ToDate = txtToDate.Text;
            else
                Supplier.ToDate = "";
            ds = Supplier.GetSupplierStockGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                gvStock.DataSource = ds;
                gvStock.DataBind();
                gvStock.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                lblStatus.Text = "No Records Found.";
                gvStock.DataSource = null;
                gvStock.DataBind();
                gvStock.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlBrand.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        ddlSupplier.SelectedValue = "0";
        ddlProduct.Items.Clear();
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBrand.SelectedValue != "0")
            FillProduct();
        else
            ddlProduct.Items.Clear();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue != "0")
            FillProduct();
        else
            ddlProduct.Items.Clear();
    }
    public decimal total = 0;
    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmount = new Label();
            lblAmount = (Label)e.Row.FindControl("lblAmount");
            if (lblAmount.Text.Trim() != string.Empty && lblAmount.Text != null)
                total = total + Convert.ToDecimal(lblAmount.Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalAmount = new Label();
            lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
            lblTotalAmount.Text = Convert.ToString(total);
        }
    }
}
