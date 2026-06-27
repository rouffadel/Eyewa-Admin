<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

    <!DOCTYPE html>
    <html lang="en">

    <head id="Head1" runat="server">
        <title>Eyewa ERP - Login</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />

        <link rel="icon" href="fevicon.ico" type="image/x-icon" />

        <!-- Fonts -->
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap"
            rel="stylesheet" />
        <!-- Icons -->
        <script src="https://unpkg.com/lucide@latest"></script>

        <!-- Custom CSS -->
        <link href="css_New/modern-login.css" rel="stylesheet" />
    </head>

    <body>
        <form id="Form1" runat="server">
            <div class="main-container animated fadeInDown">
                <!-- Sidebar / Left Column -->
                <div class="sidebar">
                    <div class="logo-section">
                        <div class="eye-logo">
                            <svg width="60" height="60" viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <circle cx="50" cy="50" r="48" fill="#5E35B1"/>
                                <path d="M50 30C30 30 15 50 15 50C15 50 30 70 50 70C70 70 85 50 85 50C85 50 70 30 50 30Z" fill="white"/>
                                <circle cx="50" cy="50" r="12" fill="#5E35B1"/>
                            </svg>
                        </div>
                        <div class="logo-text">
                            <h1>EYEWA ERP</h1>
                            <p>Optical Store Management</p>
                        </div>
                    </div>

                    <div class="hero-text">
                        <h2>Manage your optical business with ease</h2>
                        <div class="accent-line"></div>
                    </div>

                    <p class="description">
                        All-in-one ERP solution for optical stores, chains and distributors.
                    </p>

                    <div class="hero-image-container">
                        <img src="Images/login_hero.png" alt="Eyewa Hero" class="hero-image" />
                    </div>

                    <div class="features-list">
                        <div class="feature-item">
                            <div class="feature-icon"><i data-lucide="layout-dashboard"
                                    style="width: 20px; height: 20px;"></i></div>
                            <div class="feature-content">
                                <h4>Smart Store Management</h4>
                                <p>Manage sales, inventory, customers and prescriptions.</p>
                            </div>
                        </div>
                        <div class="feature-item">
                            <div class="feature-icon"><i data-lucide="bar-chart-3"
                                    style="width: 20px; height: 20px;"></i></div>
                            <div class="feature-content">
                                <h4>Real-time Insights</h4>
                                <p>Get real-time reports and analytics for better decisions.</p>
                            </div>
                        </div>
                        <div class="feature-item">
                            <div class="feature-icon"><i data-lucide="shield-check"
                                    style="width: 20px; height: 20px;"></i></div>
                            <div class="feature-content">
                                <h4>Secure & Compliant</h4>
                                <p>ZATCA compliant, secure and trusted by optical stores.</p>
                            </div>
                        </div>
                    </div>

                    <div class="sidebar-footer">
                        &copy; 2025 Eyewa ERP. All rights reserved.
                    </div>
                </div>

                <!-- Form section / Right Column -->
                <div class="form-section">

                    <asp:Panel ID="pnlLogin" runat="server">
                        <div class="form-header">
                            <h2>Welcome Back!</h2>
                            <p>Sign in to your Eyewa ERP account</p>
                        </div>

                        <div class="form-group">
                            <label>Username / Email</label>
                            <div class="input-wrapper">
                                <i data-lucide="user" class="input-icon"></i>
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control"
                                    placeholder="Enter username or email"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvtxtLogin" runat="server" ControlToValidate="txtLogin"
                                ErrorMessage="Please enter User Name" Text="*" Style="color: red; font-size: 11px;"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label>Password</label>
                            <div class="input-wrapper">
                                <i data-lucide="lock" class="input-icon"></i>
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control"
                                    placeholder="Enter password"></asp:TextBox>
                                <i data-lucide="eye" class="input-toggle" id="togglePassword"></i>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvtxtpassword" runat="server"
                                ControlToValidate="txtpassword" ErrorMessage="Please enter Password" Text="*"
                                Style="color: red; font-size: 11px;" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-options">
                            <label class="remember-me">
                                <input type="checkbox" />
                                <span>Remember me</span>
                            </label>
                            <asp:Button ID="lbluser" runat="server" CssClass="btn-link" Text="Forgot Password?"
                                OnClick="lnkforgotpassword_Click" />
                        </div>

                        <asp:Button ID="btnLogin" runat="server" CssClass="btn-signin" Text="Sign In"
                            OnClick="imgbtnLogin_Click" ValidationGroup="G1" />

                        <div class="divider">
                            <span>or</span>
                        </div>

                        <div class="btn-otp">
                            <i data-lucide="smartphone" style="width: 18px; height: 18px;"></i>
                            Login with OTP
                        </div>

                        <div class="form-footer">
                            Don't have an account? <a href="#">Contact your administrator</a>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlgetpassword" runat="server" Visible="false">
                        <div class="form-header">
                            <h2>Reset Password</h2>
                            <p>Enter your details to retrieve your password</p>
                        </div>

                        <div class="form-group">
                            <label>Username</label>
                            <div class="input-wrapper">
                                <i data-lucide="user" class="input-icon"></i>
                                <asp:TextBox ID="txtLoginId" runat="server" CssClass="form-control"
                                    placeholder="Enter username"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginId"
                                ErrorMessage="Required User Name" ValidationGroup="Y" Style="color: red;">*
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label>Login Type</label>
                            <div class="input-wrapper">
                                <i data-lucide="layers" class="input-icon"></i>
                                <asp:TextBox ID="txtLoginType" runat="server" CssClass="form-control"
                                    placeholder="Enter login type"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvLoginType" runat="server"
                                ControlToValidate="txtLoginType" ErrorMessage="Required Login Type" ValidationGroup="Y"
                                Style="color: red;">*</asp:RequiredFieldValidator>
                        </div>

                        <div style="display: flex; gap: 15px;">
                            <asp:Button ID="Button1" runat="server" Text="Get Password" CssClass="btn-signin"
                                style="flex: 2;" ValidationGroup="Y" />
                            <asp:Button ID="Close" runat="server" Text="Cancel" CssClass="btn-otp" style="flex: 1;"
                                OnClick="Close_Click" />
                        </div>
                    </asp:Panel>

                    <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="validation-label"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="false"
                        ShowSummary="true" ValidationGroup="G1"
                        Style="color: red; font-size: 12px; margin-top: 10px;" />
                </div>
            </div>

            <div class="page-footer">
                Powered by <span>FADEL</span>
            </div>
        </form>

        <script>
            // Initialize Lucide icons
            lucide.createIcons();

            // Password visibility toggle
            const togglePassword = document.querySelector('#togglePassword');
            const password = document.querySelector('#<%=txtpassword.ClientID %>');

            if (togglePassword && password) {
                togglePassword.addEventListener('click', function (e) {
                    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
                    password.setAttribute('type', type);

                    // Toggle icon
                    if (type === 'text') {
                        this.setAttribute('data-lucide', 'eye-off');
                    } else {
                        this.setAttribute('data-lucide', 'eye');
                    }
                    lucide.createIcons();
                });
            }
        </script>
    </body>

    </html>