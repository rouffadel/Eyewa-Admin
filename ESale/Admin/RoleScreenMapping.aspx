<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="RoleScreenMapping.aspx.cs" Inherits="Admin_RoleScreenMapping" Title="Role Screen Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery.js"></script>

    <br />
    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Role Screen Mapping</strong>
                                </h3>

                            </div>
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
                                        <asp:Label ID="lblmsg" runat="server" Style="color: White;"></asp:Label>
                                    </div>
                                </div>
                            </center>
                            <br />

                            <div id="pnlAddRoleScreen" runat="server" visible="true">
                                <div class="panel-body">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Role</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlRoles" runat="server"
                                                        class="form-control select" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged"
                                                        AutoPostBack="true"
                                                        Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvddlRole" runat="server" ErrorMessage="Select Role."
                                                    InitialValue="0"
                                                    ControlToValidate="ddlRoles" ValidationGroup="r">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>                               
                                    <div id="pnlSelect" runat="server" visible="false">

                                        <asp:CheckBox ID="chkSelection" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkSelection_CheckedChanged" Visible="true"></asp:CheckBox>
                                        <asp:Label ID="Label1" runat="server" Text="Select All" />
                                    </div>

                                    <table border="0" align="center">
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="validSum1" runat="server" ValidationGroup="r" CssClass="validationSummary"
                                                    ShowMessageBox="false" ShowSummary="true" DisplayMode="List" Width="318px" />

                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row top"></div>
                                    <div id="pnlrepScreens" runat="server" visible="false" class="row top">
                                        <asp:Repeater ID="repScreens" runat="server" >
                                            <HeaderTemplate>
                                                <div id="gridClass">
                                                    <table border="0"  cellpadding="1" cellspacing="3" 
                                                       class="table table-bordered table-striped table-actions tableBorder">
                                                        <tr>

                                                            <th valign="top">
                                                                <asp:Label ID="lblRepScreenName" Text="Screen Name" runat="server"></asp:Label>
                                                            </th>

                                                            <%--<th valign="top">
                                <asp:Label ID="lblRepScreenCode" Text="Screen Url" runat="server"></asp:Label>
                            </th>--%>
                                                            <th valign="top">
                                                                <asp:Label ID="lblRepAdd" Text="View" runat="server"></asp:Label>
                                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_CheckedChanged"
                                                                    Visible="true" />
                                                            </th>
                                                            <th valign="top">
                                                                <asp:Label ID="lblRepDelete" Text="Add" runat="server"></asp:Label>
                                                                <asp:CheckBox ID="chkAdd" runat="server" AutoPostBack="true" OnCheckedChanged="chkAdd_CheckedChanged"
                                                                    Visible="true" />
                                                            </th>
                                                            <th valign="top">
                                                                <asp:Label ID="lblRepView" Text="Edit" runat="server"></asp:Label>
                                                                <asp:CheckBox ID="chkEdit" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_CheckedChanged"
                                                                    Visible="true" />
                                                            </th>
                                                            <th valign="top">
                                                                <asp:Label ID="lblAlert" Text="Delete" runat="server"></asp:Label>
                                                                <asp:CheckBox ID="chkDelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkEdit_CheckedChanged"
                                                                    Visible="true" />
                                                            </th>


                                                        </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="background-color: #F3F3F3">

                                                    <td align="left">
                                                        <%# DataBinder.Eval(Container.DataItem, "ScreenName")%>
                                                    </td>
                                                    <%-- <td align="left" >
                        <%# DataBinder.Eval(Container.DataItem, "ScreenUrl")%>
                    </td>--%>
                                                    <td align="center">
                                                        <input type="checkbox" name="AddChk" id="chkBoxView" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />
                                                    </td>
                                                    <td align="center">
                                                        <input type="checkbox" id="chkBoxAdd" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />

                                                    </td>
                                                    <td align="center">
                                                        <input type="checkbox" id="chkAlert" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />
                                                    </td>
                                                    <td align="center">
                                                        <input type="checkbox" id="chkBoxDelete" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />
                                                    </td>


                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr style="background-color: #fff">


                                                    <td align="left">
                                                        <%# DataBinder.Eval(Container.DataItem, "ScreenName")%>
                                                    </td>
                                                    <%--  <td align="left" >
                        <%# DataBinder.Eval(Container.DataItem, "ScreenUrl")%>
                    </td>--%>
                                                    <td align="center">
                                                        <input type="checkbox" name="AddChk" id="chkBoxView" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />
                                                    </td>
                                                    <td align="center">
                                                        <input type="checkbox" id="chkBoxAdd" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />

                                                    </td>
                                                    <td align="center">
                                                        <input type="checkbox" id="chkAlert" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />
                                                    </td>
                                                    <td align="center">
                                                        <input type="checkbox" id="chkBoxDelete" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "ScreenId")%>' />
                                                    </td>

                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </table> </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-2" id="save" runat="server">
                                            <asp:Button ID="btnSave" ValidationGroup="r" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                Text="Save" Style="margin-bottom: 5px;" CausesValidation="true"
                                                OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-2" id="dvclear" runat="server">
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning btn-block pull-left"
                                                alt="Clear" Text="Clear" Style="margin-bottom: 5px;"
                                                OnClick="btnClear_Click" />
                                        </div>
                                        <div class="col-md-2" id="dvcancel" runat="server">
                                            <asp:Button ID="btncan" runat="server" CssClass="btn btn-danger btn-block pull-left"
                                                Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncan_Click" />
                                        </div>
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


