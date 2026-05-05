<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <label id="lblTitle" runat="server" visible="false">Users</label>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        function StoreOrOrg() {
            if (Page_ClientValidate() == true) {
                var x = document.getElementById("ctl00_ContentPlaceHolder1_radioStoreUser").checked;
                var y = document.getElementById("ctl00_ContentPlaceHolder1_radioOrganisation").checked;
                if (x == false && y == false) {
                    alert("Select Store user or Organisation");
                    return false;
                }
                return true;
            }
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
                                    <strong>Users</strong>
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

                            <div id="panelAddUser" runat="server">

                                <div class="page-content-wrap">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Store User</label>
                                                <div class="col-md-8">
                                                    <asp:RadioButton ID="radioStoreUser" runat="server"
                                                        GroupName="radio" OnCheckedChanged="radioStoreUser_CheckedChanged"
                                                        AutoPostBack="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Organisation</label>
                                                <div class="col-md-8">
                                                    <asp:RadioButton ID="radioOrganisation" Text="" runat="server"
                                                        GroupName="radio" CssClass="Label" AutoPostBack="True"
                                                        OnCheckedChanged="radioOrganisation_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top" id="lblStoreName" visible="false" runat="server">
                                        <div class="col-md-5" >
                                            <div class="form-group">
                                                <label class="col-md-3 control-label" >
                                                    Store Name</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drdnStoreName" runat="server"
                                                        class="form-control select" 
                                                        Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvStoreName" runat="server" ControlToValidate="drdnStoreName"
                                                    ErrorMessage="Please Select Store Name" ValidationGroup="r" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    User Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtAddUser" runat="server" />
                                                    </div>
                                                    <asp:RequiredFieldValidator ValidationGroup="r" ID="rfvaddUser" runat="server" ControlToValidate="txtAddUser"
                                                        ErrorMessage="Please Enter User" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>                                    
                                    </div>
                                    <div class="row ">                                    
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Login ID</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtAddLogin" runat="server" />
                                                    </div>
                                                    <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtAddLogin" ErrorMessage="Please Enter Login" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                           <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                   Password</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" />
                                                    </div>
                                                   <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Role</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlRole" runat="server"
                                                        class="form-control select" 
                                                        Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>
                                                </div>
                                               <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="ddlRole" InitialValue="%" ErrorMessage="Please Select Role"
                                                    Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row top">                                    
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Email ID</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtAddEmail" runat="server" />
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
                                                        <asp:TextBox CssClass="form-control" ID="txtAddMobile" runat="server" MaxLength="10" />
                                                    </div>
                                                   <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="row ">                                    
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Contact No</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                        <asp:TextBox CssClass="form-control" ID="txtAddContact" runat="server" />
                                                    </div>                                                    
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                             <div class="row top">                                    
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                   Is Active</label>
                                                <div class="col-md-8">
                                                   <asp:CheckBox runat="server" ID="checkActive" />                                               
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                 
                                    <center>
                                        <div id="dvSummary" align="center">
                                            <asp:ValidationSummary CssClass="validationSummary" ID="ValidationSummary1" ValidationGroup="r"
                                                runat="server" />
                                        </div>
                                    </center>
                                </div>
                            </div>
                            <div ID="panelSearchUser" runat="server">
                             <div class="panel-body">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    User Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtSearchUserName" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtSearchUserName_TextChanged" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Store</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drdnSearchStore" runat="server"
                                                        class="form-control select"  AutoPostBack="True"
                                                OnSelectedIndexChanged="drdnSearchStore_SelectedIndexChanged"
                                                        Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>
                                                </div>                                              
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Login ID</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtSearchLoginName" runat="server" CssClass="form-control"  AutoPostBack="True" OnTextChanged="txtSearchLoginName_TextChanged"  ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Role</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlsearchrole" runat="server"
                                                        class="form-control select"   AutoPostBack="True" OnSelectedIndexChanged="ddlsearchrole_SelectedIndexChanged"
                                                        Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>
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
                                                        <asp:Button ID="searchuser" runat="server" Text="Search"
                                                            CssClass="btn btn-info" OnClick="searchuser_Click" />&nbsp;&nbsp;
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
                            <div ID="panelgrid" runat="server" class="table-responsive">
                                <div id="gridClass">
                                    <asp:GridView ID="grdRole" runat="server" AutoGenerateColumns="false"
                                       DataKeyNames="LoginID" OnRowCommand="grdRole_RowCommand" CssClass="table datatable table-bordered table-striped table-actions"
                                        OnRowDeleting="grdRole_RowDeleting" OnRowEditing="grdRole_RowEditing"
                                        OnRowCreated="grdRole_RowCreated" OnRowDataBound="grdRole_RowDataBound">

                                        <Columns>
                                            <asp:BoundField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"  DataField="UserName"  HeaderText="User Name">                                                
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" HeaderText="Store">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreName" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" HeaderText="Login Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLoginName" runat="server" Text='<%#Eval("LoginName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"
                                                DataField="Active"
                                                HeaderText="IsActive">
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30">
                                                <ItemTemplate>
                                                   <%-- <asp:ImageButton ID="lnkbtnView" runat="server" ImageUrl="../Images/hammer_screwdriver.png"
                                                        Text="View" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="View"
                                                        ValidationGroup="False" Style="width: 16px" />
                                                    <asp:ImageButton ID="lnkbtnEdit" runat="server" ImageUrl="../Images/pencil.png"
                                                        Text="Edit" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="Edit"
                                                        ValidationGroup="False" />
                                                    <asp:ImageButton ID="lnkbtnDel" runat="server" ImageUrl="../Images/cross.png"
                                                        Text="Delete" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' OnClientClick="return confirm('Do you want to Delete the record?');"
                                                        CommandName="Delete"
                                                        ValidationGroup="False" />--%>
                                                         <asp:LinkButton ID="lnkbtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                CommandName="View"
                                                                Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edits" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                Style="text-decoration: none;" CssClass="fae fa-pencil" ToolTip="Edit"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDel" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                CommandName="Delete" ValidationGroup="False" runat="server" ToolTip="Delete"
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
                            <div class="panel-footer" id="dvFooter" runat="server">
                                <div class="form-group">

                                    <div class="col-md-2" id="dvisave" runat="server">
                                        <asp:Button ID="imgSave" runat="server" CssClass="btn btn-success btn-block pull-right"
                                            Text="Save" Style="margin-bottom: 5px;" OnClientClick="return StoreOrOrg();this.disabled = true;"
                                                    OnClick="imgSave_Click"/>
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


