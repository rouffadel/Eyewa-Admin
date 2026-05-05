<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ClosingCounter.aspx.cs" Inherits="Screens_ClosingCounter" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
 <asp:ToolkitScriptManager ID="tsm" runat="server"></asp:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblTitle" Text="Closing Counter" runat="server" CssClass="TitleClass"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTab" Runat="Server">
    <script language="javascript">
        function Calculate(aa) {
            var closingcountervalue =parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtClosingCounterValue").value);
            var cashwithdrawalamount =parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtCashWithdrawal").value);
            
            if (isNaN(cashwithdrawalamount)) {
                alert("Please Enter Positive Numeric value.");
                aa.value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtClosingCounterBalance").value = "";
                return false;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_txtClosingCounterBalance").value = parseFloat(closingcountervalue - cashwithdrawalamount).toFixed(2);
            }
            return true;
        }
        function CheckFields() {
            var cashwithdrawalamount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtCashWithdrawal").value);
            var userid = document.getElementById("ctl00_ContentPlaceHolder1_ddlUser").value;
            if (cashwithdrawalamount == "") {
                alert("Please Enter Value or 0 in Cash Withdrawal amount .");
                return false;
            }
            if (userid == "0") {
                alert("Please Select User from User dropdown list.");
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
            <asp:Panel id="pnlAdd" runat="server">
                <center>
                <table>
                    <tr>
                        <td>
                             <asp:Label ID="Label4" runat="server" Text="Store" CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStore" runat="server" CssClass="DropDownClass" ></asp:DropDownList>                             
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcounter" runat="server" Text="Closing Counter " CssClass="Label" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                        </td>    
                    </tr>
                </table>
               </center>
               <br />
               <div class="gridclass">
                <asp:GridView ID="gvcounter" runat="server" AutoGenerateColumns="false" 
                       HorizontalAlign="Center" onrowdatabound="gvcounter_RowDataBound" ShowFooter="true" Width="560px">
                    <Columns>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="lblSection" runat="server" CssClass="Label" Text='<%#Eval("Reason")%>'></asp:Label>
                                <asp:HiddenField ID="HDCounterID" runat="server" Value='<%#Eval("CounterID")%>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSectionTag" runat="server" CssClass="Label" Text="Total" ></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit">
                            <ItemTemplate>
                                <asp:Label ID="lblCredit" runat="server" CssClass="Label" Text='<%#Eval("Credit")%>' ></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalCredit" runat="server" CssClass="Label"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit">
                            <ItemTemplate>
                                <asp:Label ID="lblDebit" runat="server" CssClass="Label" Text='<%#Eval("Debit")%>' ></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalDebit" runat="server" CssClass="Label"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" CssClass="Label"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalBal" runat="server" CssClass="Label"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
               </div>
                 <br />  
                <div id="dvCalculation" runat="server">
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblClosingcountervalue" runat="server" CssClass="Label" Text="Closing Counter Value"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClosingCounterValue" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="User"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="DropDownClass"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Closing Cash Withdrawal"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCashWithdrawal" runat="server" CssClass="TextBoxStyle"  onchange="return Calculate(this);"></asp:TextBox>
                                </td>
                             </tr>
                              <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Closing Counter Balance"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClosingCounterBalance" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </td>
                             </tr>
                        </table>
                    </center>
                    <center>
                        <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/images/saveBtn.png"  OnClientClick="return CheckFields();"
                            onclick="imgSave_Click" />
                    </center>
                </div>
            </asp:Panel>
         </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

