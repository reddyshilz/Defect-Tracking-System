<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="DefectTracker.UserControls.Header" %>
<div class="container">  
    <div class="row">
        <div class="col-lg-4 col-md-4">
            <a href="#"><img class="img-responsive img-home-portfolio" src="../Images/logo.png" style="height:80px"></a>
        </div>
        <div class="col-lg-4 col-md-4">
                 
        </div>
        <div class="pull-right">
            <div style="margin-right:15px">
                <form runat="server">
                    <div class="form-group">
                        <strong>Hi <%=HttpContext.Current.User.Identity.Name%></strong> <asp:Button CssClass="btn btn-danger btn-xs" runat="server" ID="btnSignout" Text="Sign out" onclick="btnSignout_Click" />
                    </div>
                    </form>
            </div>
        </div>
    </div>
        <div class="row">
            <nav class="navbar navbar-inverse" role="navigation">
                <div class="container">
                    <div class="navbar-header">                                               
                        <a class="navbar-brand" href="#">Defect Tracker</a>
                    </div>
                    <!-- Collect the nav links, forms, and other content for toggling -->
                    <% if(Session["Role"].ToString()=="PM") {%>
                    <div class="collapse navbar-collapse navbar-ex1-collapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="../DefectList.aspx">Manage Defects</a></li>  
                            <li><a href="../ManageProject.aspx">Manage Projects</a></li>                           
                            <li><a href="../ManageUser.aspx">Manage Users</a></li>
                        </ul>
                    </div><!-- /.navbar-collapse -->
                    <% } %>
                </div><!-- /.container -->
            </nav>
        </div>
    </div>
