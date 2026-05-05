<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" MasterPageFile="~/Admin/Admin.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        function HideDivs() {
            $("#ContentPlaceHolder1_Default").css("display", "none");
            $("#ctl00_ContentPlaceHolder1_Default").css("display", "none");
        }
</script>
    <center>
        <asp:Label Style="font-family: Georgia; font-size: xx-large; color: green; font-weight: bold; text-align: left;" ID="lblMsg"
            runat="server" Text=""></asp:Label><br />
        <br />
        <%-- <img src="../images/Logo.png" style="height:100px;"  />--%>
        <div>
            <asp:Image ID="imgStore" runat="server" Style="height: 50px;" AlternateText="Store Image" />
            <!-- <asp:Image ID="Image1" runat="server" Style="height: 50px;" AlternateText="Store Image" /> -->
            <asp:Image ID="Image2" runat="server" Style="height: 50px;" AlternateText="Store Image" />
        </div>
        <br />
        <form runat="server" >
           
            <div class="row" runat="server" id="Default">
                <br />
                <div class="row top" style="margin-top: 10px;">
                    <div class="col-md-4">
                   <%-- <div class="col-md-6">--%>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                Store</label>
                            <div class="col-md-8">                               
                                <asp:DropDownList ID="DrpStroe" runat="server"
                                    class="form-control select" AutoPostBack="true"  OnSelectedIndexChanged="DrpStroe_SelectedIndexChanged"
                                    Style="margin-bottom: 12px;">                                   
                                </asp:DropDownList>

                         <%--   </div>--%>
                        </div>
                    </div>
                        </div>
                  <%--  </div>
              
                <div class="row top" style="margin-top: 10px;">--%>
                    <div class="col-md-4">
                   <%-- <div class="col-md-6">--%>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                Month</label>
                            <div class="col-md-8">
                                <%--<select id="ddlmonth" class="form-control" ng-model="month" ng-change="changedates()">
                                        <option value="0">--Any--</option>
                                        <option value="1">January</option>
                                        <option value="2">February</option>
                                        <option value="3">March</option>
                                        <option value="4">April</option>
                                        <option value="5">May</option>
                                        <option value="6">June</option>
                                        <option value="7">July</option>
                                        <option value="8">August</option>
                                        <option value="9">September</option>
                                        <option value="10">October</option>
                                        <option value="11">November</option>
                                        <option value="12">December</option>
                                    </select>--%>
                                <asp:DropDownList ID="ddlmonth" runat="server"
                                    class="form-control select" AutoPostBack="true" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged"
                                    Style="margin-bottom: 12px;">
                                    <asp:ListItem Text="--Any--" Value="0" />
                                    <asp:ListItem Text="January" Value="1" />
                                    <asp:ListItem Text="February" Value="2" />
                                    <asp:ListItem Text="March" Value="3" />
                                    <asp:ListItem Text="April" Value="4" />
                                    <asp:ListItem Text="May" Value="5" />
                                    <asp:ListItem Text="June" Value="6" />
                                    <asp:ListItem Text="July" Value="7" />
                                    <asp:ListItem Text="August" Value="8" />
                                    <asp:ListItem Text="September" Value="9" />
                                    <asp:ListItem Text="October" Value="10" />
                                    <asp:ListItem Text="November" Value="11" />
                                    <asp:ListItem Text="December" Value="12" />
                                </asp:DropDownList>

                            </div>
                        </div>
                    <%--</div>--%>
                        </div>
                    <div class="col-md-4">
                    <%--<div class="col-md-6">--%>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                Year</label>
                            <div class="col-md-8">
                                <%-- <select id="ddlyear" class="form-control" ng-model="year" ng-change="changedates()">
                                <option value="0">--Any--</option>
                                <option value="2016">2016</option>
                                <option value="2017">2017</option>
                                <option value="2018">2018</option>
                            </select>--%>
                                <asp:DropDownList ID="ddlyear" runat="server"
                                    class="form-control select" AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"
                                    Style="margin-bottom: 12px;">
                                    <asp:ListItem Text="--Any--" Value="0" />
                                    <asp:ListItem Text="2016" Value="2016" />
                                    <asp:ListItem Text="2017" Value="2017" />
                                    <asp:ListItem Text="2018" Value="2018" />

                                </asp:DropDownList>
                            </div>
                        </div>
                    <%--</div>--%>
                        </div>
                </div>
                <br />
                <div class="row top" style="margin-top: 10px;">
                    <div class="col-md-2">
                        <div class="widget widget-default widget-item-icon">
                            <div>
                                <div class="widget-title" style="text-transform: none;">
                                    Today's Total
                                </div>
                                <div class="widget-int" id="TodayWorkOrderAmount">
                                    <asp:Label ID="lblTodaySales" runat="server" ></asp:Label>
                                     <br /></div>
                                    <div class="widget-title" style="text-transform: none;">
                                    Today's Collection
                                </div>
                                <div class="widget-int" id="Div7">
                                    <asp:Label ID="lblTodaycollec" runat="server" ></asp:Label>
                                    </div>
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="widget widget-default widget-item-icon">
                            <div>
                                <div class="widget-title" style="text-transform: none;">
                                    Week's Total
                                </div>
                                <div class="widget-int" id="WeekSalesAmount">
                                    <asp:Label ID="lblWeekSales" runat="server" ></asp:Label>
                                    </div>
                                    <br />
                                <div class="widget-title" style="text-transform: none;">
                                    Week's Collection
                                </div>
                                <div class="widget-int" id="Div5">
                                    <asp:Label ID="lblweekcollec" runat="server" ></asp:Label></div>
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="widget widget-default widget-item-icon">
                            <div>
                                <div class="widget-title" style="text-transform: none;">
                                    Month's Total
                                </div>
                                <div class="widget-int" id="MonthSalesAmount">
                                    <asp:Label ID="lblMonthSales" runat="server" ></asp:Label>
                                    <br />
                            </div>
                                    <div class="widget-title" style="text-transform: none;">
                                    Month's Collection
                                </div>
                                <div class="widget-int" id="Div6">
                                    <asp:Label ID="lblmonthcollec" runat="server" ></asp:Label><br />
                                </div>
                        </div>
                    </div>
                            </div>
                    <div class="col-md-2">
                        <div class="widget widget-default widget-item-icon">
                            <div>
                                <br />
                                <div class="widget-title" style="text-transform: none;">
                                    Today's Bills
                                </div>
                                <div class="widget-int" id="Div1">
                                    <asp:Label ID="TodayBill" runat="server" ></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="widget widget-default widget-item-icon">
                            <div>
                                <br />
                                <div class="widget-title" style="text-transform: none;">
                                    Week's Bills
                                </div>
                                <div class="widget-int" id="Div2">
                                    <asp:Label ID="weekbill" runat="server" ></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="widget widget-default widget-item-icon">
                            <div>
                                <br />
                                <div class="widget-title" style="text-transform: none;">
                                    Month's Bills
                                </div>
                                <div class="widget-int" id="Div3">
                                    <asp:Label ID="monthbill" runat="server" ></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>


            
                  </div>  
                <div id="Div4" class="table-responsive" runat="server" >
                <div class="col-md-12">
                <div class="col-md-8">
                    <%--<div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title-box">
                            <h3>Invoice</h3>
                        </div>--%>
                     <div class="col-md-2"></div>
                    <div class="col-md-8">
                    <asp:Literal ID="lt" runat="server"></asp:Literal>
                    <div id="chart_div" style="height: 300px;"></div>
                         </div></div>
               
                    <%-- </div>
                </div>--%>
                <%--</div>--%>
               <%-- <div class="row">
                <div class="col-md-8">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="panel-title-box">
                                        <h3>Invoices</h3>
                                        <span></span>
                                    </div>--%>
                   &nbsp;&nbsp;&nbsp;
                <div class="col-md-3">
                    <asp:Chart ID="Chart1" runat="server">
                         <%--BackColor="0, 0, 64" BackGradientStyle="LeftRight"
                        BorderlineWidth="0" Height="360px" Palette="None" PaletteCustomColors="Maroon"
                        Width="380px" BorderlineColor="64, 0, 64">--%>
                        <Titles>
                            <asp:Title ShadowOffset="10" Name="Items" />
                        </Titles>
                        <Legends>
                            <asp:Legend Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                        <Series>
                            <asp:Series Name="Default" />
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                        </ChartAreas>
                    </asp:Chart>

                </div>
                    
                   <%--                 </div>
                                </div>
                </div>
                    </div>--%>
            </div>
               </div>
                            

            <%--<div class="row" ng-show="false">
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="panel-title-box">
                                <h3>Invoice</h3>
                            </div>
                             <div class="panel-body padding-0">
                            <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px" Visible="false">
                                <Titles>
                                    <asp:Title ShadowOffset="3" Name="Items" />
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                </ChartAreas>
                            </asp:Chart>
                                 </div>
                        </div>
                    </div>
                </div>
            </div>--%>

            <%--<div class="row">
                <div class="col-md-8">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="panel-title-box">
                                <h3>Invoice</h3>
                            </div>
                            <ul class="panel-controls" style="margin-top: 2px;">
                                <li><a href="#" class="panel-fullscreen"><span class="fa fa-expand"></span></a></li>
                                <li><a href="#" class="panel-refresh"><span class="fa fa-refresh"></span></a></li>
                                <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><span
                                    runat="server" class="fa fa-cog"></span></a>
                                    <ul class="dropdown-menu">
                                        <li><a href="#" class="panel-collapse"><span class="fa fa-angle-down"></span>Collapse</a></li>
                                        <li><a href="#" class="panel-remove"><span class="fa fa-times"></span>Remove</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <div class="panel-body padding-0">
                              <div class="chart-holder" id="dvBarChart" style="height: 200px;">
                                    </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="panel-title-box">
                                        <h3>Invoices</h3>
                                        <span></span>
                                    </div>
                                    <ul class="panel-controls" style="margin-top: 2px;">
                                        <li><a href="#" class="panel-fullscreen"><span class="fa fa-expand"></span></a></li>
                                        <li><a href="#" class="panel-refresh"><span class="fa fa-refresh"></span></a></li>
                                        <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><span
                                            class="fa fa-cog"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#" class="panel-collapse"><span class="fa fa-angle-down"></span>Collapse</a></li>
                                                <li><a href="#" class="panel-remove"><span class="fa fa-times"></span>Remove</a></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="panel-body padding-0">
                                    <div class="chart-holder" id="dvcustomerdetails" style="height: 200px;">
                                    </div>
                                </div>
                            </div>
                        </div>
            </div>--%>
           </div>
            
        </form>



    </center>

</asp:Content>
