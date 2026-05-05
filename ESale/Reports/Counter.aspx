<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Counter.aspx.cs" Inherits="Reports_Counter" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
    <asp:ToolkitScriptManager ID="tsm" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblTitle" Text="Counter" runat="server" CssClass="TitleClass"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTab" Runat="Server">
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnprint" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblStore" runat="server" Text="Store" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStore" runat="server" CssClass="DropDownClass">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblUser" runat="server" Text="User" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUser" runat="server" CssClass="DropDownClass">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="From Date" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                            <asp:TextBox ID="txtStoreFromDate" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                            <asp:Image ID="imgStoreFromDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CStoreFromDate" runat="server" PopupButtonID="imgStoreFromDate"
                                TargetControlID="txtStoreFromDate" Enabled="true" EnabledOnClient="true" Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                        </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Lable5" runat="server" Text="To Date" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                            <asp:TextBox ID="txtStoreToDate" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                            <asp:Image ID="imgStoreToDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CStoreToDate" runat="server" PopupButtonID="imgStoreToDate"
                                TargetControlID="txtStoreToDate" Enabled="true" EnabledOnClient="true" Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                        </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="BtnEmptyStyle"
                                    Style="margin-left: -100px;" ToolTip="show report" 
                                    onclick="btnReport_Click"  />
                            </td>
                            <td align="left" colspan="2">
                            <asp:ImageButton ID="btnClear" ImageUrl="~/images/clearBtn.png" AlternateText="Clear"
                                Style="margin-left: -120px;" runat="server" ToolTip="clear fields" 
                                    onclick="btnClear_Click"  />
                        </td>
                        </tr>
                    </table>
                </center>
                <br />
                <div class="gridclass">
                    <asp:GridView ID="gvCounter" runat="server" AutoGenerateColumns="False" 
                        HorizontalAlign="Center" ShowFooter="True" 
                        onrowdatabound="gvCounter_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="SNo" HeaderText="SNo" />
                            <asp:BoundField DataField="StoreName" HeaderText="Store" />
                             <asp:TemplateField HeaderText="User Name" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lbltot" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OpenDateTime" HeaderText="Opening Date& Time" />                           
                            <asp:TemplateField HeaderText="Opening Value" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpenValue" runat="server" Text='<%#Eval("OpeningValue")%>' ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lblTotalOpenValue" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sales" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSales" runat="server" Text='<%#Eval("SalesValue")%>' ></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                               <asp:Label ID="lblTotalSales" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses" FooterStyle-HorizontalAlign="Right"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenses" runat="server" Text='<%#Eval("PettyExpenses")%>' ></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                               <asp:Label ID="lblTotalExpenses" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CloseDateTime" HeaderText="ClosingDate" />
                             <asp:TemplateField HeaderText="Closing Value" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCloseValue" runat="server" Text='<%#Eval("ClosingValue")%>' ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                               <asp:Label ID="lblTotalCloseValue" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField HeaderText="Withdraw" DataField="WithDrawal" />--%>
                            <asp:TemplateField HeaderText="Withdraw" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblwithdrawl" runat="server" Text='<%#Eval("WithDrawal")%>' ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                               <asp:Label ID="lbltotalwithdrawl" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Final Closing Value" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblfinalvalue" runat="server" Text='<%#Eval("FinalClosingValue")%>' ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                               <asp:Label ID="lbltotalfinalvalue" runat="server" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                          <%--  <asp:BoundField HeaderText="Final Closing Value" DataField="FinalClosingValue" />--%>
                        </Columns>
                        <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
                <br />
                <center>
                    <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="BtnEmptyStyle" Visible="false"
                        ToolTip="print report" onclick="btnprint_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="BtnEmptyStyle"
                        Visible="false" ToolTip="export report" onclick="btnExport_Click" />
                </center>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

