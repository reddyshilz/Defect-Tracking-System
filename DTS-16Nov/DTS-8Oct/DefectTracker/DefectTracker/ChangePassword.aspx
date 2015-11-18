<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DefectTracker.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/signin.css" rel="stylesheet" />
</head>
<body>
      <div class="container-fluid">     
      <form class="form-signin"  runat="server"  method="post">
        <h3 class="form-signin-heading">Change password</h3>
        <code><label runat="server" Visible="False" id="lblmsg"></label></code>
          <div class="row">
            <div class="col-sm-4">               
                <label class="col-sm-4 control-label">Current Password</label>
            </div>
            <div class="col-sm-8">
                <input type="password" class="form-control" id="txtCurrPwd" />
            </div>             
        </div>  
        <div class="row">
            <div class="col-sm-4">               
                <label class="col-sm-4 control-label">New Password</label>
            </div>
            <div class="col-sm-8">
                <input type="password" class="form-control" id="txtNewPwd" />
            </div>             
        </div>          
        <div class="row">
            <div class="col-sm-4">               
                <label class="col-sm-4 control-label">Confirm Password</label>
            </div>
            <div class="col-sm-8">
                <input type="password" class="form-control" id="txtConfPwd" />
            </div>             
        </div> 
        <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />          
      </form>
    </div> <!-- /container -->   
</body>
</html>
