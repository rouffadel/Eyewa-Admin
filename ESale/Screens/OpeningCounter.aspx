<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="OpeningCounter.aspx.cs" Inherits="Screens_OpeningCounter" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
    <asp:ToolkitScriptManager ID="tsm" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblTitle" Text="Opening Counter" runat="server" CssClass="TitleClass"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTab" Runat="Server">
    <script type="text/javascript">
        function Check() {
            var storeid = document.getElementById("ctl00_ContentPlaceHolder1_ddlStore").value;
            if (storeid == "0") {
                alert("Select any Store");
                return false;
            }
            var openvalue = document.getElementById("ctl00_ContentPlaceHolder1_txtOpeningCounterValue").value;
            if (openvalue == "") {
                alert("Enter any Numeric value in OPen value .");
                return false;
            }
            if (isNaN(openvalue)) {
                alert("Enter any Numeric value.");
                document.getElementById("ctl00_ContentPlaceHolder1_txtOpeningCounterValue").value = "";
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMessage" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblStatus" CssClass="MessageClass" runat="server"> </asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlAdd" runat="server">
                <center>
                    <table>
                        <tr>
                            <td>
                                 <asp:Label ID="Label2" runat="server" Text="Store" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStore" runat="server" CssClass="DropDownClass" ></asp:DropDownList>
                                 <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStore" 
                        InitialValue="0"  ErrorMessage="Please Select Organization" Text="*" ></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                            <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalenderFromDate" runat="server" PopupButtonID="imgFromDate"
                                TargetControlID="txtDate" Enabled="true" EnabledOnClient="true" Format="dd-MM-yyyy">
                            </asp:CalendarExtender>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Opening Counter Value" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpeningCounterValue" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOpeningCounterValue" 
                          ErrorMessage="Please Opening Counter value" Text="*" ></asp:RequiredFieldValidator>
                                 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/images/saveBtn.png" CausesValidation="true" OnClientClick="return Check();" 
                                    onclick="imgSave_Click" />
                            </td>
                        </tr>
                        
                    </table>
                    
                            <center>
                            <div id="dvSummary"  align="center">
                                <asp:ValidationSummary CssClass="validationSummary" ID="ValidationSummary1" ValidationGroup="r" runat="server" />
                            </div>
                        </center>
                       
                </center>
            </asp:Panel>
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

