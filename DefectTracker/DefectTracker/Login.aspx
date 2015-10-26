<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DefectTracker.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <title>Login Page</title>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/signin.css" rel="stylesheet" />
        
    
    
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/Custom/login.js"></script>

</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <form class="form-signin" role="form" runat="server" method="post">
                <h3 class="form-signin-heading">Please Sign In</h3>
                <code>
                    <label runat="server" visible="False" id="lblmsg"></label>
                </code>
                <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User Name" runat="server" required="yes" />
                <asp:TextBox ID="txtPWD" TextMode="Password" CssClass="form-control" placeholder="Password" runat="server" required="yes" />
                <asp:CheckBox ID="chkRememberMe" runat="server" />
                &nbsp; <strong>Remember me</strong>
                <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="btnSubmit" runat="server" Text="Sign in" OnClick="btnSubmit_Click" />
                <div>
                    <ul class="pager">
                        <li><a href="ChangePassword.aspx" onclick="ChangePassword();return false;">Change Password</a></li>
                        <li><a href="ForgotPassword.aspx" onclick="ForgotPassword();return false;">Forgot Password  </a></li>
                    </ul>
                </div>
            </form>
        </div>
        <!-- /container -->
    </div>


    <div class="modal fade" id="cpModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="cpModalLabel">Modal title</h4>
                </div>

                <div class="modal-body">

                    <div class="row" id="cperrorPanel">
                        <div class="col-md-12">
                            <!--Error Messsage Panel start -->
                            <div class="panel-group" id="cpaccordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <a style="color: Red" data-toggle="collapse" data-parent="#accordion" href="#cpcollapseOne">Errors</a>
                                    </div>
                                    <div id="cpcollapseOne" class="panel-collapse collapse">
                                        <div class="panel-body alert-warning text-left">
                                            <div id="cpmsgPanel">
                                                <div id="cpmessageDetails"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <form role="form" id="cpForm" class="form-horizontal" method="post">
                        
                        <div class="form-group">
                            <label class="col-sm-4 control-label">Registered UserId</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" placeholder="Enter registered UserId" id="registeredUserName" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label">Current Password</label>
                            <div class="col-sm-6">
                                <input type="password" class="form-control" placeholder="Enter current password"  id="currentPassword" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label">New Password</label>
                            <div class="col-sm-6">
                                <input type="password" class="form-control" placeholder="Enter new password" id="newPassword" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label">Confirm Password</label>
                            <div class="col-sm-6">
                                <input type="password" class="form-control" placeholder="Enter confirm password" id="confirmPassword" />
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                            <button type="submit" class="btn btn-info">Submit</button>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>


    <div class="modal fade" id="fpModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="fpModalLabel">Modal title</h4>
                </div>

                <div class="modal-body">

                    <div class="row" id="fperrorPanel">
                        <div class="col-md-12">
                            <!--Error Messsage Panel start -->
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <a style="color: Red" data-toggle="collapse" data-parent="#accordion" href="#fpcollapseOne">Errors</a>
                                    </div>
                                    <div id="fpcollapseOne" class="panel-collapse collapse">
                                        <div class="panel-body alert-warning text-left">
                                            <div id="msgPanel">
                                                <div id="fpmessageDetails"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <form role="form" id="fpForm" class="form-horizontal" method="post">

                        <div class="form-group">
                            <label class="col-sm-4 control-label">Registered Email Id</label>
                            <div class="col-sm-7">
                                <input type="text" class="form-control" placeholder="Enter registerd email id" id="txtEmail" />
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                            <button type="submit" class="btn btn-info">Submit</button>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>
</body>
</html>
