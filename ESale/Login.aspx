<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html >

<html lang="en" class="body-full-height">
<head id="Head1" runat="server">
    <title>ESales</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="icon" href="fevicon.ico" type="image/x-icon" />

    <link href="css_New/theme-default.css" rel="stylesheet" />
    <%--   <script type="text/javascript">
             if (window.parent.frames.length > 0) {
                 window.parent.location.href = "Login.aspx";
             }
    </script>--%>
    <%--<script language="javascript" type="text/javascript">
        function GetPasswordDiv() {
            var theForm = document.forms['frmMain'];
            document.getElementById("divGetPassword").style.display = '';
            document.getElementById("divLogin").style.display = 'none';

        }


        function Validation() {
            var EmailId = document.getElementById('<%=txtUserName.ClientID %>');
            if (EmailId.value == '') {
                alert("Please enter Email Address");
                EmailId.focus();
                return false;

            }
            var MobileNo = document.getElementById('<%=txtMobileNo.ClientID %>');
                 if (MobileNo.value == '') {
                     alert("Please enter Mobile No");
                     MobileNo.focus();
                     return false;
                 }

             }
             var image = new Array();
             image[0] = "images/SlideImages/img2.jpg";
             image[1] = "images/SlideImages/img1.jpg";
             image[2] = "images/SlideImages/img3.jpg";
             image[3] = "images/SlideImages/img4.jpg";
             image[4] = "images/SlideImages/img5.jpg";
             var k = image.length - 1;
             function Tick() {
                 var i = parseInt(localStorage.getItem("j"));
                 if (i == undefined || i == null)
                     i = 0;
                 if (isNaN(i))
                     i = 0;
                 // $("#ibody").css("background-image", "url(" + image[i] + ")");
                 // $("#ibody").css('background-image', 'url(' + image[i] + ')');
                 //$('myOjbect').css('background-image', 'url(' + imageUrl + ')');
                 $(".body").attr("style", "background-image:url(" + image[i] + ")");
                 //.style.backgroundimage = image[i];

                 i = i + 1;
                 if (i == image.length)
                     i = 0;
                 localStorage.setItem("j", i);
             }
             window.setInterval("Tick()", 3000);
    </script>--%>

</head>
<body>
    <form id="Form1" class="form-horizontal" runat="server">        
            <div class="panel-body" > 
                <center>    <!-- style="margin-left: 261px;"-->
                <!-- <div class="col-md-3"></div> -->
                <div class="col-md-3"> </div>
                <div class="col-md-3">
                    <img id="logo" src="Images/cityvision.png" width="290" alt="cityvision Logo" style="padding: 10px;
                        margin: 25px 0px;" />
                        </div>
                        <!-- <div class="col-md-3">
                    <img id="logo1" src="Images/gulfvision.png" width="290px" alt="gulfvision Logo" style="padding: 10px;
                        margin: 25px 0px;" />
                        </div>-->
                         <div class="col-md-3">
                    <img id="logo2" src="images/naimat al-basar1.png" width="290" alt="namiat al-basar Logo"
                        style="padding: 10px; margin: 25px 0px;" />
                        </div>
                        </center><%-- style="margin-left: 261px;"--%>
                </div>
                <div class="login-container" style=" margin-top: -48px;">

                    <div id="pnlLogin" runat="server" class="login-box animated fadeInDown">
                      <%--  <div class="login-logo"></div>--%>
                        <div class="login-body">
                            <div class="login-title"><strong>Welcome</strong>, Please login</div>
                            <%--   <form id="Form1" class="form-horizontal" runat="server">--%>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <%-- <input type="text" class="form-control" placeholder="Username"/>--%>
                                    <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtLogin" runat="server"
                                        ControlToValidate="txtLogin" ErrorMessage="Please enter Email Address" Text="*"
                                        Style="color: #FFF; font-weight: 600;"
                                        ValidationGroup="G1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <%-- <input type="password" class="form-control" placeholder="Password"/>--%>
                                    <asp:TextBox ID="txtpassword" TextMode="Password" runat="server" CssClass="form-control"
                                        placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtpassword" runat="server"
                                        ControlToValidate="txtpassword" ErrorMessage="Please enter Password" Text="*"
                                        Style="color: #FFF; font-weight: 600;"
                                        ValidationGroup="G1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6">
                                    <%-- <button class="btn btn-info btn-block">Log In</button>--%>
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-info btn-block" Text="Log In"
                                        OnClick="imgbtnLogin_Click" ValidationGroup="G1" />
                                </div>
                                <div class="col-md-6">
                                    <%--<a href="#" class="btn btn-link btn-block">Forgot your password?</a>--%>
                                    <%--<a href="">--%>
                                    <span class="xn-text">
                                        <asp:Button ID="lbluser" runat="server" class="btn btn-link btn-block" Text="Forgot your password?">
                                        </asp:Button></span>
                                    <%--OnClick="lnkforgotpassword_Click"--%>
                                </div>

                            </div>
                            <asp:Label ID="lblsuccess" runat="server" ForeColor="White" CssClass="btn btn-block"
                                Text=""></asp:Label>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="false"
                                            ShowSummary="true" CssClass="validationSummary" ValidationGroup="G1" Style="color: #FFF;
                                            font-weight: 600;" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                      <%--  <div class="login-footer">
                            <div class="pull-left">
                               &copy; 2014 AppName
                                 <div class="pull-right">
                               
                            </div>
                        </div>--%>
                    </div>

                    <div id="pnlgetpassword" runat="server" visible="false" class="login-box animated fadeInDown">               
                <div class="login-logo"></div>
                <div class="login-body">
                    <div class="login-title">Get Password</div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtLoginId" runat="server" ValidationGroup="a" CssClass="form-control"
                                placeholder="User Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginId"
                                ErrorMessage="Required User Name" ValidationGroup="Y">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtLoginType" runat="server" ValidationGroup="a" CssClass="form-control"
                                placeholder="Login Type"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLoginType"
                                ErrorMessage="Required Login Type" ValidationGroup="Y">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-4">

                            <asp:Button ID="Button1" runat="server" Text="Get Password" CssClass="btn btn-info btn-block"
                                Width="99px" ValidationGroup="Y" />
                               <%-- OnClick="btngetpassword_Click" --%>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="Close" runat="server" Text="Cancel" CssClass="btn btn-warning btn-block pull-left"
                                Width="99px"/>
                                <%--OnClick="Close_Click" --%>
                        </div>
                    </div>
                    <asp:Label ID="lblgotpwd" runat="server" ForeColor="White" CssClass="btn btn-block" Text=""></asp:Label>
                </div>
            </div>
                </div>


                <%--<div id="divGetPassword" style="display: none" runat="server" class="LoginBlockCtnr">

                    <br />
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="lblforgotmsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                    <h4 class="ForgotPwdTitle">Please enter your email id and mobile number below, we use
                        it to send you the details.</h4>
                    <ul class="LoginBlock">
                        <li class="GetPwdLoginUser">
                            <label>Enter&nbsp<span>Email ID&nbsp </span></label>
                            <asp:TextBox ID="txtUserName" runat="server" class="GetPwdUserName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server"
                                ControlToValidate="txtUserName" ErrorMessage="Please enter Email Address" Text="*"
                                ValidationGroup="G2"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regtxtUserName" runat="server"
                                ControlToValidate="txtUserName" ErrorMessage="Please enter valid Email Address"
                                Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="G2">*</asp:RegularExpressionValidator>

                        </li>

                        <li class="GetPwdLoginPwd">
                            <label>Enter&nbsp<span>Mobile No &nbsp</span></label>
                            <asp:TextBox ID="txtMobileNo" runat="server" class="GetPwdPassword" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtMobileNo" runat="server"
                                ControlToValidate="txtMobileNo" ErrorMessage="Please enter Mobile No" Text="*"
                                ValidationGroup="G2"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="regtxtMobileNo" ErrorMessage="Please enter valid Mobile No."
                                ControlToValidate="txtMobileNo" runat="server" ValidationExpression="^[0-9]{10}$"
                                ValidationGroup="G2">*</asp:RegularExpressionValidator>
                        </li>
                        <li class="LoginSubmit">

                            <asp:Button ID="btnGetPassword" Text="GetPassword" runat="server"
                                class="GetPwdBtn" OnClick="btnGetPassword_Click" ValidationGroup="G2" />
                            <asp:Button ID="btnGetPwdCancel" Text="Cancel" runat="server"
                                class="GetPwdCancelBtn" OnClick="btnGetPwdCancel_Click" />

                        </li>

                    </ul>

                    <table align="left">
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="false"
                                    ShowSummary="true" CssClass="validationSummary" ValidationGroup="G2" />
                            </td>
                        </tr>
                    </table>
                </div>--%> 
                <br />
                <div class="row" > <%--style=" margin-left: 387px;font-size: 15px;"--%>
                <center>
                   <%-- <a href="" class="FtrLinks">HelpDesk	</a>&nbsp; &nbsp;

                    <a href="" class="FtrLinks">Blog</a>&nbsp; &nbsp;

                    <a href="http://actinfotech.com" target="_blank" class="FtrLinks"> About Us</a>&nbsp; &nbsp;

                    <a href="" class="twitterLink">Twitter</a> <a href="" class="facebook">FaceBook</a>&nbsp; &nbsp;--%>

                    <a href="http://enhanceitservices.com" target="_blank" class="FtrLinks">Technology Powered
                        by Enhance IT Services	</a>
                        </center>
                </div>


    </form>
</body>
</html>
