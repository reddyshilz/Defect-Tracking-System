<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="DefectTracker.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href= "Content/bootstrap.css" rel="stylesheet" />
    <link href= "Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/datepicker.css" rel="stylesheet" />
    <link href= "Content/Site.css" rel="stylesheet" />
    <link href= "Content/eComSite.css" rel="stylesheet" />
    
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/Custom/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="Scripts/Custom/docs.min.js"></script>
</head>
<body>
    <form class="form-horizontal" role="form">
        <div class="form-group">
            
            <input type="email" class="form-control" id="inputEmail" placeholder="Email"/>
        </div>
        <div class="form-group">
            
            <input type="password" class="form-control" id="inputPassword" placeholder="Password"/>
        </div>
        <div class="checkbox">
            <label/><input type="checkbox"/> Remember me</label/>
        </div>
        <button type="submit" class="btn btn-primary">Login</button>
    </form>
    <br>
    <div class="alert alert-info">
        <a href="#" class="close" data-dismiss="alert">×</a>
        <strong>Note:</strong> The inline form layout is rendered as default vertical form layout if the viewport width is less than 768px. Open the output in a new window and resize the screen to see how it works.
    </div>
       
</body>
</html>
