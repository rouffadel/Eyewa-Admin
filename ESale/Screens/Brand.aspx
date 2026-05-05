<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Brand.aspx.cs" Inherits="Screens_Brand" Title="Brand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery.js"></script>

    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Brand</strong>
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

                            <div id="panelSearchBrand" runat="server">
                                <div class="panel-body">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Brand Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtSearchBrand" runat="server" CssClass="form-control"></asp:TextBox>
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
                                    <div id="panelGridBrand" runat="server">
                                        <div id="gridClass" class="table-responsive">
                                            <asp:GridView ID="GridViewBrand" runat="server" AutoGenerateColumns="false"
                                                DataKeyNames="BrandID" OnRowCommand="GridViewBrand_RowCommand" OnRowCreated="GridViewBrand_RowCreated"
                                                OnRowDeleting="GridViewBrand_RowDeleting" OnRowEditing="GridViewBrand_RowEditing"
                                                CssClass="table datatable table-bordered table-striped table-actions">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" DataField="BrandName" HeaderText="Brand Name"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="90">
                                                        <ItemTemplate>
                                                            <%--<asp:ImageButton ID="lnkbtnView" runat="server" Text="View" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                CommandName="View" ImageUrl="../images/hammer_screwdriver.png" ValidationGroup="False"
                                                                Style="width: 16px"></asp:ImageButton>
                                                            <asp:ImageButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                CommandName="Edits" ImageUrl="../images/pencil.png" ValidationGroup="False" Style="width: 16px">
                                                            </asp:ImageButton>
                                                            <asp:ImageButton ID="lnkbtnDel" runat="server" Text="Delete" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                CommandName="Deletes" ImageUrl="../images/cross.png" ValidationGroup="False"
                                                                Style="width: 16px"
                                                                OnClientClick="return confirm('Do you want to Delete the record?');"></asp:ImageButton>--%>
                                                            <asp:LinkButton ID="lnkbtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                CommandName="View"
                                                                Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edits" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                Style="text-decoration: none;" CssClass="fae fa-pencil" ToolTip="Edit"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDel" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                CommandName="Deletes" ValidationGroup="False" runat="server" ToolTip="Delete"
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
                            <div id="panelAddBrand" runat="server" class="page-content-wrap">


                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Brand Name</label>
                                            <div class="col-md-8">
                                               <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtAddBrand" runat="server" />
                                                </div>
                                            </div>
                                             <asp:HiddenField ID="HiddenFieldBrandId" runat="server" />
                                           <asp:RequiredFieldValidator ID="rfvAddBrand" runat="server" ValidationGroup="r" ControlToValidate="txtAddBrand"
                                                Text="*" ErrorMessage="Brand Name Can not be Empty">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                IsActive
                                            </label>
                                            <div class="col-md-8">
                                                <div class="input-group">

                                                    <asp:CheckBox ID="chkActive" CssClass="Label" runat="server" Checked="true" />

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>  
                                <div class="row top"></div>                    
                                <center>
                                 <table id="dvSummary" align="center">
                                    <tbody>
                                        <tr>
                                            <td>
                                               <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="r"
                                            CssClass="validationSummary" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                   
                                </center>
                            </div>
                            <div class="panel-footer" id="dvFooter" runat="server">
                                <div class="form-group">

                                    <div class="col-md-2" id="dvisave" runat="server">
                                        <asp:Button ID="imgAddBrand" ValidationGroup="r" runat="server" CssClass="btn btn-success btn-block pull-right"
                                            Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false"
                                            OnClick="imgAdd_Click" />
                                    </div>

                                    <div class="col-md-2" id="dvupdate" runat="server">
                                        <asp:Button ID="imgUpdateBrand" ValidationGroup="r" Visible="false" runat="server"
                                            CssClass="btn btn-success btn-block pull-left"
                                            Text="Update" CausesValidation="true"
                                            Style="margin-bottom: 5px;" OnClick="imgUpdate_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvclear" runat="server">
                                        <asp:Button ID="imgClearBrand" runat="server" CssClass="btn btn-warning btn-block pull-left"
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
