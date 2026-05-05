<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Product.aspx.cs" Inherits="Screens_Product" Title="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function validDecimal() {

            var reg = /(^100([.]0{1,2})?)$|(^\d{1,2}([.]\d{1,2})?)$/;
            var val = document.getElementById("ctl00_ContentPlaceHolder1_txtMaxDiscPer").value;
            if (reg.test(val) == false) {
                alert("Enter valid Decimal no upto two decimal places and below hundred");
                document.getElementById("ctl00_ContentPlaceHolder1_txtMaxDiscPer").value = "0.00";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMaxDiscPer").focus();
                return false;
            }
            return true;

        }


        function checkDecimals(fieldName, fieldValue) {
            decallowed = 2;
            if (isNaN(fieldValue) || fieldValue == "") {

                //$("#dvSummary>ul").append("<li>Enter valid Numeric value upto 2 decimal points.</li>");
                alert("Enter Valid Numeric Value upto 2 decimal places.");
                fieldValue = "";
                fieldName.select();
                fieldName.focus();
                return false;
            }
            else {
                if (fieldValue.indexOf('.') == -1) fieldValue += ".";
                dectext = fieldValue.substring(fieldValue.indexOf('.') + 1, fieldValue.length);

                if (dectext.length > decallowed) {

                    // $("#dvSummary>ul").append("<li>Enter a number with up to " + decallowed + " decimal places.</li> ");
                    alert("Enter a number up to " + decallowed + " decimal places.");
                    fieldName.value = "";
                    fieldName.select();
                    fieldName.focus();
                    return false;
                }
                else {

                    return true;
                }
            }
        }
        function CheckValue(aa) {
            var validationsummary = $("#<%=ValidationSummary1.ClientID%>");

            var elementvalue = aa.value;
            if (isNaN(elementvalue)) {
                aa.value = "";
                // $("#dvSummary #<%=ValidationSummary1.ClientID%>").append("<ul>Please Enter numeric Value.</ul>");
                alert("Please Enter Numeric Value.");
                return false;
            }
            else {
                if (checkDecimals(aa, aa.value) == true)
                    return true;
                else
                    return false;
            }
        }
        function check() {
            var productelement = $("#<%=txtProductValue.ClientID%>");
            CheckValue(productelement);
            var discountelement = $("#<%=txtDefaultSellingDiscount.ClientID%>");
            CheckValue(discountelement);
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
                                    <strong>Product</strong>
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
                                                    Category:</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="DDLCategorySearch" runat="server"
                                                        class="form-control select"
                                                        OnSelectedIndexChanged="DDLCategorySearch_SelectedIndexChanged" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Brand:</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlBrandSearch" runat="server" OnSelectedIndexChanged="ddlBrandSearch_SelectedIndexChanged"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Model No:</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtProductSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                                        <div id="gridClass" class="table-responsive">
                                            <asp:GridView ID="gvProduct" runat="server" DataKeyNames="ProductID"  AutoGenerateColumns="false"
                                                OnRowCommand="gvProduct_RowCommand" CssClass="table datatable table-bordered table-striped table-actions"
                                                OnRowCreated="gvProduct_RowCreated" OnRowDeleting="gvProduct_RowDeleting"
                                                OnRowEditing="gvProduct_RowEditing">
                                                <Columns>
                                                    <asp:BoundField DataField="CategoryName"
                                                        HeaderText="Category" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="BrandName"
                                                        HeaderText="Brand" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="ProductName"
                                                        HeaderText="Model No" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="ProductValue"
                                                        HeaderText="Selling Price" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="SellingDefaultDiscount"
                                                        HeaderText="Selling Discount" Visible="false" />
                                                    <asp:BoundField DataField="MaxDiscount"
                                                        HeaderText="Max Discount" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" Visible="true" />
                                                    <asp:TemplateField ItemStyle-Width="90">
                                                        <ItemTemplate>
                                                            <%--    <asp:ImageButton ID="lnkbtnView" runat="server"
                                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex%>" CommandName="View"
                                                                ImageUrl="../Images/hammer_screwdriver.png" Style="width: 16px" Text="View" />
                                                            <asp:ImageButton ID="lnkbtnEdit" runat="server"
                                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex%>" CommandName="EditRow"
                                                                ImageUrl="../Images/pencil.png" Text="Edit" />
                                                            <asp:ImageButton ID="lnkbtnDel" runat="server"
                                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex%>"
                                                                CommandName="Deleting" ImageUrl="../Images/cross.png"
                                                                OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                Text="Delete" />--%>
                                                            <asp:LinkButton ID="lnkbtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                CommandName="View"
                                                                Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRow" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                Style="text-decoration: none;" CssClass="fae fa-pencil" ToolTip="Edit"></asp:LinkButton>
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
                                                Category:</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="DDLCategory" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="DDLCategory"
                                                InitialValue="0" ErrorMessage="Please Select Category" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Brand:</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlBrand" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="ddlBrand"
                                                InitialValue="0" ErrorMessage="Please Select Brand" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Model No:</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtProductName" runat="server" />
                                                </div>
                                                <asp:HiddenField ID="HDProductID" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtProductName" InitialValue=""
                                                    ErrorMessage="Please Enter Model No" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Selling Price:</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtProductValue" runat="server" onchange="return CheckValue(this);" />
                                                </div>
                                                <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="txtProductValue" InitialValue=""
                                                    ErrorMessage="Please Enter Selling Price" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row "  style="display:none">
                                    <div class="col-md-5" visible="false">
                                        <div class="form-group" visible="false">
                                            <label class="col-md-3 control-label" visible="false">
                                                Default Discount%(Selling):
                                            </label>
                                            <div class="col-md-8" visible="false">
                                                <div class="input-group" visible="false">
                                                    <asp:TextBox CssClass="form-control" ID="txtDefaultSellingDiscount" runat="server"
                                                        Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" visible="true">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Maximum Discount % :</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtMaxDiscPer" runat="server" Visible="true"
                                                        Text="0.00" onblur="return validDecimal()" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                IsActive:</label>
                                            <div class="col-md-8">
                                                <div class="input-group">

                                                    <asp:CheckBox ID="chkActive" Text="Active" CssClass="Label" runat="server" Checked="true" />

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:Label ID="lblBarCode" runat="server" CssClass="col-md-3 control-label">Bar Code:</asp:Label>&nbsp;
                                            <div class="col-md-8">
                                                <asp:Image ID="imgBarcode" runat="server" Style="width: 100px; height: 30px;" /><br />
                                                &nbsp; <asp:Label ID="lblProduct" runat="server"></asp:Label><br />
                                                &nbsp; <asp:Label ID="lblPrice" runat="server"></asp:Label>&nbsp;                                                        
                                            </div>
                                        </div>
                                    </div>
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

