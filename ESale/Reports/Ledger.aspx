<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Ledger.aspx.cs" Inherits="Reports_Ledger" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
    <asp:ToolkitScriptManager ID="tsm" runat="server"> </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
             <asp:Label ID="lblTitle" Text="Ledger Report" runat="server" CssClass="TitleClass"></asp:Label>
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
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnprint" />
        </Triggers>
      <ContentTemplate>
        <center>    
            <table>
                    <tr>
                        <td><asp:Label ID="lblStore" runat="server" Text="Store" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlStore" runat="server" CssClass="DropDownClass"></asp:DropDownList></td>                        
                         <td><asp:Label ID="lblFromDate" ForeColor="Black" runat="server" Text="From Date"></asp:Label></td>
                        <td><asp:TextBox ID="txtFromDate" onchange="TotalDateChecking();datevalidation(this);" runat="server" CssClass="TextBoxStyle">
                            </asp:TextBox>
                            <asp:Image ID="imgDeliveryNoteFromDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="imgDeliveryNoteFromDate" Enabled="true" EnabledOnClient="true"
                                Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                            </td>
                        <td><asp:Label ID="lblToDate" ForeColor="Black" runat="server" Text="To Date"></asp:Label></td>
                        <td><asp:TextBox ID="txtToDate" onchange="TotalDateChecking();datevalidation(this);" runat="server" CssClass="TextBoxStyle">
                            </asp:TextBox>
                            <asp:Image ID="imgDeliveryNoteToDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="imgDeliveryNoteToDate" Enabled="true" EnabledOnClient="true" Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                             </td>
                      </tr>
                      
                   
                    <tr >
                    <td></td>
                    <td></td>
                    <td  >                     
                        <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="BtnEmptyStyle" 
                            ToolTip="show report" onclick="btnReport_Click"  style="float:right;" />
                    </td>                
                    <td align="left" colspan="2">
                        <asp:ImageButton ID="imgbtnClear" ImageUrl="~/images/clearBtn.png" AlternateText="Clear"                        
                            runat="server" ToolTip="clear fields" onclick="imgbtnClear_Click"   />
                    </td>
                </tr>
                
                </table>
        </center>
         <asp:Panel ID="pnlGrid" runat="server">
                <div class="gridclass">
                    <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="false" 
                        HorizontalAlign="Center" onrowdatabound="gvLedger_RowDataBound" ShowFooter="true" Width="500px">
                        <Columns>
                          
                            <asp:TemplateField HeaderText="Sno" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSno" runat="server" Text='<%#Eval("SNo") %>' ></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lbltot" runat="server" CssClass="Label" Text="Total" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Credit">
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server" CssClass="Label"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lblTotalCredit" runat="server" CssClass="Label" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit">
                                <ItemTemplate>
                                    <asp:Label ID="lblDebit" runat="server" CssClass="Label"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalDebit" runat="server" CssClass="Label" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" CssClass="Label"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="Label" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </div>
                <center>
                 <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="BtnEmptyStyle" Visible="false"
                    ToolTip="print report" onclick="btnprint_Click"  />      
                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="BtnEmptyStyle"
                    Visible="false" ToolTip="export report" onclick="btnExport_Click"  /> 
               </center>
           </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

