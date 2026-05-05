<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label CssClass="TitleClass" ID="lblTitle" Text="Change Password" runat="server"
        Visible="false"></asp:Label>

    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Change Password</strong>
                                </h3>
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
                            <div id="pnlsearchUser" runat="server">

                             <div class="panel-body">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Old Password</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtOldPwd" runat="server" ValidationGroup="r" TextMode="Password"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    New Password</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtNewPwd" runat="server" CssClass="form-control" ValidationGroup="r"
                                                            TextMode="Password"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Confirm Password</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtConfrmPwd" runat="server" ValidationGroup="r" TextMode="Password"
                                                            CssClass="form-control"></asp:TextBox>
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
                                                        <asp:Button ID="btnSubmit" runat="server" Text="ChangePassword" ValidationGroup="r"
                                                            CausesValidation="true"
                                                            CssClass="btn btn-info" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="true"
                                                            CssClass="btn btn-warning" OnClick="btnClear_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <table align="center" style="width: 370px">
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="validSum1" runat="server" ValidationGroup="r" CssClass="validationSummary"
                                                    DisplayMode="List" />

                                                <asp:RequiredFieldValidator ID="RfvOldPwd" runat="server" ErrorMessage="Old Password required."
                                                    ControlToValidate="txtOldPwd" ValidationGroup="r" Display="None">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RfvtxtNewPwd" runat="server" ErrorMessage="New Password required."
                                                    ControlToValidate="txtNewPwd" ValidationGroup="r" Display="None">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RfvtxtConfrmPwd" runat="server" ErrorMessage="Confirm Password required."
                                                    ControlToValidate="txtConfrmPwd" ValidationGroup="r" Display="None">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Password and Confirm Password Should Be Same"
                                                    ControlToCompare="txtNewPwd" ControlToValidate="txtConfrmPwd" ValueToCompare="="
                                                    ValidationGroup="r" Display="None"></asp:CompareValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ValidationGroup="r"
                                                    ErrorMessage="Password length should be minimum 6 characters" ValidationExpression="^[\w]{5,100}$"
                                                    ControlToValidate="txtNewPwd" Display="None"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>

                                    </table>

                                </div>
                            </div>
                        </div>
                    </form>

                </div>
            </div>
        </div>
    </form>


</asp:Content>

