using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ESaleEntity;

public partial class Admin_Welcome : System.Web.UI.Page
{
    CommonFunctions cf;
    //e EVObj;
    DataSet ds;
    EUsers user;




    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblToDayDate.InnerText = Date;
            //Hellouser.Text = "Welcome " + Session["LOGINNAME"].ToString();

            int storeid = Convert.ToInt32(Session["StoreID"]);
            string fileName = Convert.ToString(Session["StoreImage"]);
            if (Convert.ToString(Session["LoginID"]) == "1")
            {
                imgStore.ImageUrl = "~/images/cityvision.png";
                Image1.ImageUrl = "~/images/gulfvision.png";
                Image2.ImageUrl = "~/images/naimat al-basar.png";
                imgStore.Visible = true;
                Image1.Visible = true;
                Image2.Visible = true;
            }
            if (Convert.ToInt32(Session["LoginID"]) != 1)
            {
                if (storeid == 0)
                {
                    imgStore.ImageUrl = "~/images/cityvision.png";
                    Image1.ImageUrl = "~/images/gulfvision.png";
                    Image2.ImageUrl = "~/images/naimat al-basar.png";
                }
                else
                {
                    if (fileName != "")
                    {
                        imgStore.ImageUrl = "~/images/StoreImages/" + fileName;
                        imgStore.Visible = true;
                        Image1.Visible = false;
                        Image2.Visible = false;
                    }
                    else
                    {
                        imgStore.ImageUrl = "~/images/cityvision.png";
                        Image1.ImageUrl = "~/images/gulfvision.png";
                        Image2.ImageUrl = "~/images/naimat al-basar.png";
                    }

                }
            }


            if (!Page.IsPostBack)
            {
                
                if (Session["LOGINID"] == null)
                {
                    Response.Redirect("~/Login.aspx");

                }
                //ds = new DataSet();
                //user = new EUsers();
                //ds = user.EGetStoreImages();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    string filename = Convert.ToString(ds.Tables[0].Rows[0]["FileName"]);
                //    if(filename!="")
                //        Image1.ImageUrl = "~/images/StoreImages/1.png" ;
                //    filename = Convert.ToString(ds.Tables[0].Rows[1]["FileName"]);
                //    if (filename != "")
                //        Image2.ImageUrl = "~/images/StoreImages/1.png";
                //    filename = Convert.ToString(ds.Tables[0].Rows[2]["FileName"]);
                //    if (filename != "")
                //        Image3.ImageUrl = "~/images/StoreImages/1.png";
                //    filename = Convert.ToString(ds.Tables[0].Rows[3]["FileName"]);
                //    if (filename != "")
                //        Image4.ImageUrl = "~/images/StoreImages/1.png";
                //}
                //if (Session["LOGINTYPE"].ToString() == "Admin")
                //{
                   
                //}

                //if (Session["LOGINTYPE"].ToString() == "Dealer")
                //{
                    
                  

                //}


            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            cf = new CommonFunctions();
            cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {

        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        MySql objMySql = new MySql();
        Hashtable ht = new Hashtable();
        DataSet ds = new DataSet();
        try
        {
            if (Session["LOGINID"] == null)
            {

                Response.Redirect("Login.aspx", false);
            }
            else
            {
               
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            CommonFunctions cf = new CommonFunctions();
            cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {
            // ds.Dispose();
        }
    }



    
    

   
  
}

