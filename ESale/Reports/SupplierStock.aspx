<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="SupplierStock.aspx.cs" Inherits="Reports_SupplierStock" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
    <asp:ToolkitScriptManager ID="tsm" runat="server">
    </asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
             <asp:Label ID="lblTitle" Text="Supplier Stock" runat="server" CssClass="TitleClass"></asp:Label>
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
     <Triggers >
        <asp:PostBackTrigger ControlID="btnprint" />
<asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
     </Triggers >
     <Triggers >
        <asp:PostBackTrigger ControlID="btnExport" />
     </Triggers>
        <ContentTemplate>      
            <center>
                <table>
                    <tr>
                        <td><asp:Label ID="lblSupplier" runat="server" Text="Supplier" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlSupplier" runat="server" CssClass="DropDownClass"></asp:DropDownList></td>
                        <td><asp:Label ID="Label2" runat="server" Text="Category" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlCategory" runat="server" CssClass="DropDownClass" 
                                AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" Text="Brand" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlBrand" runat="server" CssClass="DropDownClass" 
                                AutoPostBack="True" onselectedindexchanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList></td>
                        <td><asp:Label ID="Label3" runat="server" Text="Model No" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlProduct" runat="server" CssClass="DropDownClass"></asp:DropDownList></td>
                    </tr> 
                     <tr style="display:none;">
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
                    <tr>
                    <td colspan="2" align="right">                     
                        <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="BtnEmptyStyle"
                            ToolTip="show report" onclick="btnReport_Click"  />
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
                    <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        HeaderStyle-BackColor="#E5E5E5"    HorizontalAlign="Center" 
                        onrowdatabound="gvStock_RowDataBound" >
                        <Columns>
                            <asp:BoundField DataField="SNo" HeaderText="SNo" />
                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                            <asp:BoundField DataField="BrandName" HeaderText="Brand Name" />    
                            <asp:TemplateField HeaderText="Model No" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lblText" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:TemplateField HeaderText="Amount" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" ></asp:Label>                             
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns> 
                        <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />                       
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

