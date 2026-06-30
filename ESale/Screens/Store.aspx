<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Store.aspx.cs"
    Inherits="Screens_Store" Title="Store" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
        style="background-color: Black;">

        <script src="../js/jquery.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">
            function CheckMail(aa) {
                //alert("mail");
                var email = document.getElementById('#<%=txtEmailID.ClientID%>');
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                if (!filter.test(aa.value)) {
                    alert('Please provide a valid email address');
                    email.focus;
                    return false;
                }
                return false;
            }
        </script>
        <style type="text/css">
            .bc {
                background: black;
            }
        </style>
        <script type="text/Javascript">
       function DisableButton(ID) {
           document.getElementbyId(ID).disabled = true;
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
                                        <strong>Store</strong>
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
                                                <span aria-hidden="true">×</span><span
                                                    class="sr-only">Close</span></button>
                                            <asp:Label ID="lblStatus" runat="server" Style="color: white;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="dvSuccess" runat="server"
                                        style="text-align: center; margin-top: 1%">
                                        <div class="alert alert-success notification" role="alert">
                                            <button type="button" class="close" data-dismiss="alert">
                                                <span aria-hidden="true">×</span><span
                                                    class="sr-only">Close</span></button>
                                            <asp:Label ID="lblSuccess" runat="server" Style="color: White;"></asp:Label>
                                        </div>
                                    </div>
                                </center>


                                <div id="pnlSearch" runat="server">
                                    <div class="panel-body">
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group row">
                                                    <label class="col-md-3 control-label">
                                                        Store Name</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span
                                                                    class="fa fa-search"></span></span>
                                                            <asp:TextBox ID="txtStoreNameSearch" runat="server"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group row">
                                                    <label class="col-md-3 control-label">
                                                        Contact No</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span
                                                                    class="fa fa-search"></span></span>
                                                            <asp:TextBox ID="txtContactNumberSearch" runat="server"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group row">
                                                    <label class="col-md-3 control-label">
                                                        Email Id</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span
                                                                    class="fa fa-search"></span></span>
                                                            <asp:TextBox ID="txtEmailIDSearch" runat="server"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group row">
                                                    <label class="col-md-3 control-label">
                                                    </label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <asp:Button ID="imgSearch" runat="server" Text="Search"
                                                                CssClass="btn btn-info" OnClick="imgSearch_Click" />
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnClearSearch" runat="server" Text="Clear"
                                                                CssClass="btn btn-warning"
                                                                OnClick="btnClearSearch_Click" />
                                                        </div>
                                                        <label class="help-block" style="display: none;">
                                                            Search</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row top"></div>
                                        <div id="pnlSearchGrid" runat="server" class="row top">
                                            <div id="gridClass" class="table-responsive">
                                                <asp:GridView ID="gvStore" runat="server" AutoGenerateColumns="false"
                                                    DataKeyNames="StoreID" OnRowCommand="gvStore_RowCommand"
                                                    OnRowEditing="gvStore_RowEditing"
                                                    OnRowDeleting="gvStore_RowDeleting"
                                                    OnRowCreated="gvStore_RowCreated"
                                                    CssClass="table datatable table-bordered table-striped table-actions">
                                                    <Columns>
                                                        <asp:BoundField DataField="StoreName" HeaderText="Store Name"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="StoreCode" HeaderText="Store Code"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="Address" HeaderText="Address"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="City" HeaderText="City"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="ZipCode" HeaderText="ZipCode"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="ContactPerson"
                                                            HeaderText="Contact Person" ItemStyle-CssClass="middle"
                                                            ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="ContactNumber"
                                                            HeaderText="Contact Number" ItemStyle-CssClass="middle"
                                                            ItemStyle-Width="30" />
                                                        <asp:BoundField DataField="EmailID" HeaderText="EmailID"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                        <asp:TemplateField ItemStyle-ForeColor="Gray"
                                                            ItemStyle-CssClass="middle" ItemStyle-Width="90"
                                                            HeaderStyle-ForeColor="gray">
                                                            <ItemTemplate>
                                                                <%-- <asp:ImageButton ID="lnkbtnView" runat="server"
                                                                    ImageUrl="../Images/hammer_screwdriver.png"
                                                                    Text="View"
                                                                    CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'
                                                                    CommandName="View" Style="width: 16px" />
                                                                <asp:ImageButton ID="lnkbtnEdit" runat="server"
                                                                    ImageUrl="../Images/pencil.png" Text="Edit"
                                                                    CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'
                                                                    CommandName="EditRow" />
                                                                <asp:ImageButton ID="lnkbtnDel" runat="server"
                                                                    ImageUrl="../Images/cross.png" Text="Delete"
                                                                    CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'
                                                                    OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                    CommandName="Deleting" />--%>

                                                                <asp:LinkButton ID="lnkbtnView" runat="server"
                                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                    CommandName="View" Style="text-decoration: none;"
                                                                    CssClass="fav fa-eye lni lni-eye" ToolTip="View">
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnEdit" runat="server"
                                                                    CommandName="EditRow"
                                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                    Style="text-decoration: none;"
                                                                    CssClass="fae fa-pencil lni lni-pencil"
                                                                    ToolTip="Edit">
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnDel"
                                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                    OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                    CommandName="Deleting" ValidationGroup="False"
                                                                    runat="server" ToolTip="Delete"
                                                                    Style="text-decoration: none;">
                                                                    <span class="fad fa-times lni lni-close"> </span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="pnlAdd" class="page-content-wrap" runat="server">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Organisation</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="DDLOrganisation" runat="server"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:RequiredFieldValidator ValidationGroup="r"
                                                    ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="DDLOrganisation" InitialValue="0"
                                                    ErrorMessage="Please Select Organization" Text="*">
                                                </asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Store Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtStoreName"
                                                            runat="server" />
                                                    </div>
                                                    <asp:HiddenField ID="HdStoreID" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="r"
                                                        ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtStoreName" InitialValue=""
                                                        ErrorMessage="Please Insert Store Name" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Store Code</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtStoreCode"
                                                            runat="server" />
                                                    </div>
                                                    <asp:RequiredFieldValidator ValidationGroup="r"
                                                        ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtStoreCode" InitialValue=""
                                                        ErrorMessage="Please Insert Store Code" Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Address</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtAddress"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    City</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox ID="txtCity" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Zip Code</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox ID="txtZipCode" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Contact No</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtContactNo"
                                                            runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Contact Person</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox ID="txtContactPerson" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Email Id</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span
                                                                class="fa fa-pencil"></span></span>
                                                        <asp:TextBox ID="txtEmailID" runat="server"
                                                            CssClass="form-control" onchange="return CheckMail(this);">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="col-md-3 control-label">
                                                    Logo</label>
                                                <div class="col-md-8">
                                                    <%-- <div class="input-group">--%>
                                                        <asp:Image ID="imgStore" runat="server"
                                                            Style="height: 100px; width: 100px;" />
                                                        <asp:FileUpload ID="fuStore" runat="server" />
                                                        <%-- </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top"></div>
                                    <table id="dvSummary" align="center">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:ValidationSummary CssClass="validationSummary"
                                                        ID="ValidationSummary1" ValidationGroup="r" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                    <div>
                                    </div>
                                </div>
                                <div class="panel-footer" id="dvFooter" runat="server">
                                    <div class="form-group row">

                                        <div class="col-md-2" id="dvisave" runat="server">
                                            <asp:Button ID="imgSave" ValidationGroup="r" runat="server"
                                                CssClass="btn btn-success btn-block pull-right" Text="Save"
                                                Style="margin-bottom: 5px;" CausesValidation="true"
                                                OnClientClick="this.disabled = true;" UseSubmitBehavior="false"
                                                OnClick="imgSave_Click" />

                                        </div>

                                        <div class="col-md-2" id="dvupdate" runat="server">
                                            <asp:Button ID="imgupdate" ValidationGroup="r" Visible="false"
                                                runat="server" CssClass="btn btn-success btn-block pull-left"
                                                Text="Update" CausesValidation="true" Style="margin-bottom: 5px;"
                                                OnClick="imgupdate_Click" />
                                        </div>
                                        <div class="col-md-2" id="dvclear" runat="server">
                                            <asp:Button ID="imgClear" runat="server"
                                                CssClass="btn btn-warning btn-block pull-left" alt="Clear" Text="Clear"
                                                Style="margin-bottom: 5px;" OnClick="imgClear_Click" />
                                        </div>
                                        <div class="col-md-2" id="dvcancel" runat="server">
                                            <asp:Button ID="btncan" runat="server"
                                                CssClass="btn btn-danger btn-block pull-left" Text="Cancel"
                                                Style="margin-bottom: 5px;" OnClick="btncan_Click" />
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