<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/signin.css" rel="stylesheet" />
</head>
<body>
<div class="container-fluid">
      <div class="row">  
      <form class="form-signin" role="form" runat="server"  method="post">
        <h3 class="form-signin-heading">Please Sign In</h3>
        <code><label runat="server" Visible="False" id="lblmsg"></label></code>
         <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User Name" runat="server"  required="yes"/>
         <asp:TextBox ID="txtPWD" TextMode="Password" CssClass="form-control" placeholder="Password" runat="server" required="yes"/>
          <asp:CheckBox ID="chkRememberMe" runat="server" /> &nbsp; <strong>Remember me</strong>
          <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="btnSubmit" runat="server" Text="Sign in" onclick="btnSubmit_Click" />
        <div>            
        <ul class="pager">
        <li><a href="ChangePassword.aspx">Change Password</a></li>
        <li><a href="ForgotPassword.aspx">Forgot Password  </a></li>             
        </ul>
        </div>
      </form>
    </div> <!-- /container -->
    </div>
</body>
</html>