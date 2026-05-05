<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Supplier.aspx.cs" Inherits="Screens_Supplier" Title="Supplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function multiplication(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var buyingdiscount = parseFloat(aa.value);
            var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_txtProductValue").value);
            var netprice;
            if (!isNaN(buyingdiscount))
                netprice = productvalue - ((productvalue * buyingdiscount) / 100);
            else
                netprice = 0;
            document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_txtNetBuyingPrice").value = netprice.toFixed(2);
            return false;
        }
        function CheckSimilar(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var categoryid = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_ddlCategory").value;
            var productid = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_ddlProduct").value;
            var productvalue = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_txtProductValue").value;
            var grid = document.getElementById('<%=gvSupplierProduct.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvSupplierProduct.ClientID%>').rows.length;
            for (var i = 0; i < gridrowcount; i++) {

            }
        }
        function Similar(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var j = parseInt(id);
            var categoryid = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_ddlCategory").value;
            var productid = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_ddlProduct").value;
            var productvalue = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + id + "_txtProductValue").value;
            var grid = document.getElementById('<%=gvSupplierProduct.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvSupplierProduct.ClientID%>').rows.length;
            var cat, prod;
            for (var i = 1; i < gridrowcount; i++) {
                if ((i + 1) != j) {
                    if (i < 9) {
                        cat = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl0" + (i + 1) + "_ddlCategory").value;
                        prod = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl0" + (i + 1) + "_ddlProduct").value;
                        if (cat == categoryid && prod == productid) {
                            alert("Product Already Selected for this category,Select different product.");
                            return false;
                        }
                    }
                    else {
                        cat = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + (i + 1) + "_ddlCategory").value;
                        prod = document.getElementById("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl" + (i + 1) + "_ddlProduct").value;
                        if (cat == categoryid && prod == productid) {
                            alert("Product Already Selected for this category,Select different product.");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    </script>

    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Supplier</strong>
                                </h3>
                                <ul class="nav nav-tabs pull-right" role="tablist" style="margin-top: 0px;">
                                    <li id="lirole" runat="server">

                                        <asp:Button ID="lnkAdd" runat="server" CssClass="btn btn-primary pull-right"
                                            Text="Add" OnClick="lnkAdd_Click" UseSubmitBehavior="false" />
                                    </li>
                                </ul>
                            </div>
                            <!-- Screen Heading  End-->

                            <!-- Notifications Start-->
                            <center>
                                <div class="col-md-12" id="dvFailure" style="text-align: center; margin-top: 1%"
                                    runat="server">
                                    <div class="alert alert-danger notification" role="alert">
                                        <button type="button" class="close" data-dismiss="alert">
                                            <span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                        <asp:Label ID="lblStatus" runat="server" Style="color: white;"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12" id="dvSuccess" runat="server" style="text-align: center; margin-top: 1%">
                                    <div class="alert alert-success notification" role="alert">
                                        <button type="button" class="close" data-dismiss="alert">
                                            <span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                        <asp:Label ID="lblSuccess" runat="server" Style="color: White;"></asp:Label>
                                    </div>
                                </div>
                            </center>
                            <div id="pnlSearch" runat="server">
                                <div class="panel-body">


                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Supplier Name:</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSupplier" runat="server"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Contact No:</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtContactNoSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Mobile No:</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtMobileNoSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Email ID:</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtEmailIDSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                </label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <asp:Button ID="imgSearch" runat="server" Text="Search"
                                                            CssClass="btn btn-info" OnClick="imgSearch_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnClearSearch" runat="server" Text="Clear"
                                    CssClass="btn btn-warning" OnClick="btnClearSearch_Click" />
                                                    </div>
                                                    <label class="help-block" style="display: none;">
                                                        Search</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div id="pnlSearchGrid" runat="server">
                                        <div id="gridClass"  class="table-responsive">
                                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="false"
                                                DataKeyNames="SupplierID" OnRowCommand="gvSupplier_RowCommand"
                                                CssClass="table datatable table-bordered table-striped table-actions"
                                                OnRowCreated="gvSupplier_RowCreated" OnRowDeleting="gvSupplier_RowDeleting"
                                                OnRowEditing="gvSupplier_RowEditing">
                                                <Columns>
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="City" HeaderText="City" ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact Number" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="EmailID" HeaderText="Email ID" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:TemplateField ItemStyle-Width="90">
                                                        <ItemTemplate>
                                                            <%-- <asp:ImageButton ID="lnkbtnView" runat="server" ImageUrl="../Images/hammer_screwdriver.png"
                                        Text="View" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="View"
                                        Style="width: 16px" />
                                    <asp:ImageButton ID="lnkbtnEdit" runat="server" ImageUrl="../Images/pencil.png"
                                        Text="Edit" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="EditRow" />
                                    <asp:ImageButton ID="lnkbtnDel" runat="server" ImageUrl="../Images/cross.png"
                                        Text="Delete" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' OnClientClick="return confirm('Do you want to Delete the record?');"
                                        CommandName="Deleting" />--%>
                                                            <asp:LinkButton ID="lnkbtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                CommandName="View"
                                                                Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRow" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                Style="text-decoration: none;" CssClass="fae fa-pencil" ToolTip="Edit">
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDel" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                CommandName="Deleting" ValidationGroup="False" runat="server" ToolTip="Delete"
                                                                Style="text-decoration: none;"> 
                                                            <span class="fad fa-times"> </span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="pnlAdd" runat="server" class="page-content-wrap">

                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Supplier Name</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtSupplierName" runat="server" />
                                                </div>
                                                <asp:HiddenField ID="HDSupplierID" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtSupplierName" InitialValue=""
                                                    ErrorMessage="Please Enter Supplier Name" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Address</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtAddress" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row ">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                City</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Contact Person</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Contact No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtContactNo"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Mobile No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Zip Code</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtZipCode"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                VAT ID</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtVatID"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                EmailID</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtEmailID"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div   class="table-responsive">
                                    <asp:GridView ID="gvSupplierProduct" runat="server" AutoGenerateColumns="false" CellPadding="1"
                                        DataKeyNames="SupplierProductID" CssClass="table datatable table-bordered table-striped table-actions"
                                        OnRowDataBound="gvSupplierProduct_RowDataBound"
                                        OnRowCommand="gvSupplierProduct_RowCommand"
                                        OnRowDeleting="gvSupplierProduct_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="HDSupplierProductID" runat="server" Value='<%#Eval("SupplierProductID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control select" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtProductValue" runat="server" CssClass="form-control" Text='<%#Eval("ProductValue")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Selling Discount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSellingDiscount" runat="server" CssClass="form-control" Text='<%#Eval("SellingDefaultDiscount")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BuyingDiscount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBuyingDiscount" runat="server" CssClass="form-control" Text='<%#Eval("BuyingDiscount")%>'
                                                        onchange="return multiplication(this); return CheckSimilar(this);"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Buying price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNetBuyingPrice" runat="server" CssClass="form-control" Text='<%#Eval("NetBuyingPrice")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgDeleteRow" runat="server" ImageUrl="~/images/cross.png" CommandName="DeleteRow"
                                                        OnClientClick="return confirm('Do you want to Delete the record?');" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' >
                                                        <span class="fad fa-times"> </span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="row top"></div>
                                <table id="dvSummary" align="center">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary CssClass="validationSummary" ID="ValidationSummary1" ValidationGroup="r"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                            <div class="panel-footer" id="dvFooter" runat="server">
                                <div class="form-group">

                                    <div class="col-md-2" id="dvisave" runat="server">
                                        <asp:Button ID="imgSave" ValidationGroup="r" runat="server" CssClass="btn btn-success btn-block pull-right"
                                            Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false"
                                            OnClick="imgSave_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvupdate" runat="server">
                                        <asp:Button ID="imgupdate" ValidationGroup="r" Visible="false" runat="server"
                                            CssClass="btn btn-success btn-block pull-left"
                                            Text="Update" CausesValidation="true"
                                            Style="margin-bottom: 5px;" OnClick="imgupdate_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvclear" runat="server">
                                        <asp:Button ID="imgClear" runat="server" CssClass="btn btn-warning btn-block pull-left"
                                            alt="Clear" Text="Clear" Style="margin-bottom: 5px;"
                                            OnClick="imgClear_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvcancel" runat="server">
                                        <asp:Button ID="btncan" runat="server" CssClass="btn btn-danger btn-block pull-left"
                                            Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncan_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

