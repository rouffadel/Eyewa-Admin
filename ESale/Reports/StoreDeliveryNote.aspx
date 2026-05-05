<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="StoreDeliveryNote.aspx.cs" Inherits="Reports_StoreDeliveryNote" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" Runat="Server">
<asp:Label ID="lblTitle" Text="Customer Delivery Note Report" runat="server" CssClass="TitleClass"></asp:Label>
    <asp:ToolkitScriptManager ID="SM" runat="server" />
<script type="text/jscript" src="../js/jquery-1.9.0.min.js"></script>
   <script src="../js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/sorttable.js"></script>

    <script src="../js/JSON.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
   
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
<link href="../js/jquery-ui.css" rel="Stylesheet" type="text/css" />
   <script type="text/javascript">
       $(document).ready(function() {
       
           $("#<%= txtFromDate.ClientID %>").datepicker({
               showOn: 'button',
               buttonText: 'Select From Date',
               buttonImageOnly: true,
               buttonImage: '../Images/Calendar_scheduleHS.png',
               dateFormat: 'dd-mm-yy',
               changeMonth: true,
               changeYear: true
               //constrainInput: true
           });
           $('#<%=txtToDate.ClientID%>').datepicker({
               showOn: 'button',
               buttonText: 'Select To Date',
               buttonImageOnly: true,
               buttonImage: '../Images/Calendar_scheduleHS.png',
               dateFormat: 'dd-mm-yy',
               changeMonth: true,
               changeYear: true
               //constrainInput: true
           });
           $(".ui-datepicker-trigger").mouseover(function() {

               $(this).css('cursor', 'pointer');

           });

       });
       

   
        function datevalidation(dd) {
            var matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(dd.value);
            if (matches == null) {
                alert('Select date from datepicker');
                dd.value = '';
            }
            else
                matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(dd.value);
        }
        function TotalDateChecking() {
            var day1, day2, day3, day4;
            var month1, month2, month3, month4;
            var year1, year2, year3, year4;

            value1 = document.getElementById('ctl00_ContentPlaceHolder2_txtFromDate').value;
            value2 = document.getElementById('ctl00_ContentPlaceHolder2_txtToDate').value;

            day1 = value1.substring(0, value1.indexOf("-"));
            month1 = value1.substring(value1.indexOf("-") + 1, value1.lastIndexOf("-"));
            year1 = value1.substring(value1.lastIndexOf("-") + 1, value1.length);

            day2 = value2.substring(0, value2.indexOf("-"));
            month2 = value2.substring(value2.indexOf("-") + 1, value2.lastIndexOf("-"));
            year2 = value2.substring(value2.lastIndexOf("-") + 1, value2.length);

            date1 = year1 + "/" + month1 + "/" + day1;
            date2 = year2 + "/" + month2 + "/" + day2;


            firstDate = Date.parse(date1);
            secondDate = Date.parse(date2);


            msPerDay = 24 * 60 * 60 * 1000
            var performance = Math.round((secondDate.valueOf() - firstDate.valueOf()) / msPerDay) + 1;

            if (performance <= 0) {
                alert('To Date should be greater than From Date');
                document.getElementById('ctl00_ContentPlaceHolder2_txtToDate').value = '';
            }

        }

    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMessage" Runat="Server">
<asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
          </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <center > <asp:Label ID="lblstatus" runat="server" CssClass="MessageClass" ></asp:Label></center>
    <asp:Panel ID="pnlCustomerDNReports" runat="server">
        <center>
            
            <table>
                    <tr>
                        <td><asp:Label ID="lblSupplier" runat="server" Text="Store" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlStore" runat="server" CssClass="DropDownClass"></asp:DropDownList></td>
                        <td><asp:Label ID="lblSTNNO" CssClass="Label" runat="server" Text="StoreDeliveryNote No"></asp:Label></td>
                         <td><asp:TextBox ID="txtStoreDeliveryNoteNo"  runat="server" CssClass="TextBoxStyle">
                            </asp:TextBox>
                          </td>
                    </tr> 
                    <tr>
                        <td><asp:Label ID="Label3" CssClass="Label" runat="server" Text="From Date"></asp:Label></td>
                        <td><asp:TextBox ID="txtFromDate" onchange="TotalDateChecking();datevalidation(this);" runat="server" CssClass="TextBoxStyle">
                            </asp:TextBox>
                            <asp:Image ID="imgDeliveryNoteFromDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="imgDeliveryNoteFromDate" Enabled="true" EnabledOnClient="true"
                                Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                            </td>
                        <td><asp:Label ID="Label4" CssClass="Label" runat="server" Text="To Date"></asp:Label></td>
                        <td><asp:TextBox ID="txtToDate" onchange="TotalDateChecking();datevalidation(this);" runat="server" CssClass="TextBoxStyle">
                            </asp:TextBox>
                            <asp:Image ID="imgDeliveryNoteToDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="imgDeliveryNoteToDate" Enabled="true" EnabledOnClient="true" Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                             </td>
                    </tr>                   
                    <tr style="display:none;">
                        <td><asp:Label ID="lblSTNDate" ForeColor="Black" runat="server" Text="Store DeliveryNote Date"></asp:Label></td>
                        <td><asp:TextBox ID="txtStoreDeliveryNoteDate" onchange="TotalDateChecking();datevalidation(this);" runat="server" CssClass="TextBoxStyle">
                            </asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStoreDeliveryNoteDate"
                                PopupButtonID="Image1" Enabled="true" EnabledOnClient="true"
                                Format="dd-MM-yyyy">
                            </asp:CalendarExtender>
                            </td>
                            <td><asp:Label ID="Label6" runat="server" Text="Category" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlCategory" runat="server" CssClass="DropDownClass" 
                                AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
                         
                    </tr>
                    
                    <tr>
                        
                        <td><asp:Label ID="Label5" runat="server" Text="Brand" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlBrand" runat="server" CssClass="DropDownClass" 
                                AutoPostBack="True" onselectedindexchanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList></td>                       
                        <td><asp:Label ID="Label7" runat="server" Text="Model No" CssClass="Label"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlProduct" runat="server" CssClass="DropDownClass" 
                                onselectedindexchanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList></td>
                        <td  align="right">                     
                        
                    </td> 
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                        <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="BtnEmptyStyle"
                            ToolTip="show report" onclick="btnReport_Click"   />
                        <td align="left" >
                        <asp:ImageButton ID="imgbtnClear" ImageUrl="~/images/clearBtn.png" AlternateText="Clear"
                            runat="server" ToolTip="clear fields" onclick="imgbtnClear_Click"   />
                    </td>
                    </tr>
                    <tr>
                        <td></td>
                <td>
                    <asp:RadioButton ID="rbtnSummary" runat="server" Checked="true" 
                        CssClass="Label" GroupName="SDN" 
                        oncheckedchanged="rbtnSummary_CheckedChanged" AutoPostBack="true"/>
                        <asp:Label ID="Label1" runat="server" Text="Summary" CssClass="Label"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="rbtnDetailed" runat="server"  
                        Checked="false" CssClass="Label" GroupName="SDN" 
                        oncheckedchanged="rbtnDetailed_CheckedChanged" AutoPostBack="true" />
                        <asp:Label ID="Label2" runat="server" Text="Detailed" CssClass="Label"></asp:Label>
                </td>
                    <td></td>
                </tr>
                   
                </table>
            <br />
            <br />
            <asp:Panel ID="pnlgrid" runat="server">
                <div class="gridclass">
                    <asp:GridView runat="server" ID="gvCustomerDNReport" AutoGenerateColumns="false"
                        Style="margin-top: 0px" ShowFooter="True">
                        <Columns>
                           <%-- <asp:BoundField DataField="Sno" HeaderText="S.No." />--%>
                            
                              <asp:TemplateField HeaderText="S.No." FooterStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblsrno" runat="server" Text='<%#Eval("Sno")%>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                            <asp:BoundField DataField="SubOrganisationName" HeaderText="Sub Organisation" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                            <asp:BoundField DataField="CustomerDeliveryNoteNo" HeaderText="DeliveryNote No" />
                            <asp:BoundField DataField="CustomerDeliveryNoteDate" HeaderText="DeliveryNote Date" />
                            <asp:TemplateField HeaderText="Net Product Value" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblINetProductValue" runat="server" Text='<%#Eval("NetProductValue")%>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFNetProductValue" runat="server" Text="NetProductValue"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                    
                           <%-- <asp:BoundField DataField="NetProductValue" HeaderText="Net Product Value" />--%>
                            
                           <%--<asp:BoundField DataField="HandlingCharges" HeaderText="Handling Charges" />--%>
                           
                             <asp:TemplateField HeaderText="Handling Charges" FooterStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblIHandlingCharges" runat="server" Text='<%#Eval("HandlingCharges")%>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFHandlingCharges" runat="server" Text="NetProductValue"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                             <%--<asp:BoundField DataField="GrossValue" HeaderText="Gross Value"  /> 
                             <asp:BoundField DataField="TotalDiscount" HeaderText="TotalDiscount"  />--%>
                             
                          <%--  <asp:BoundField DataField="TotalValue" HeaderText="Total Value"  />--%>
                            <asp:TemplateField HeaderText="Total Value" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblITotalValue" runat="server" Text='<%#Eval("TotalValue")%>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFTotalValue" runat="server" Text="NetProductValue"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="StatusName" HeaderText="Status" />
                        </Columns>
                       
                        <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
              
               
            </asp:Panel>
        </center>
        
    </asp:Panel>
    <asp:Panel ID="panelPrint" runat="server">
     <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="BtnEmptyStyle" OnClick="btnprint_Click"
                    Visible="false" />
                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="BtnEmptyStyle"
                    Visible="false"/>
                    <asp:Button ID="btnExportToPdf" runat="server" Text="ExportToPdf" 
                    ToolTip="export to pdf" Visible="false" CssClass="BtnEmptyStyle"/>
    </asp:Panel>
    <asp:ModalPopupExtender ID="Mp1" runat="server" TargetControlID="btnprint" PopupControlID="panelPrint"></asp:ModalPopupExtender>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

