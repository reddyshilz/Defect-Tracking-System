<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefectList.aspx.cs" Inherits="DefectTracker.DefectList" %>

<%@ Register Src="~/UserControls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/UserControls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Defect List</title>
       <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/datepicker.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/eComSite.css" rel="stylesheet" />
    <style type="text/css">
        #btnSend {
            width: 14px;
        }
    </style>
</head>
<body>
     <div>
        <uc1:Header runat="server" ID="Header" />
    </div>
    <form id="form1" >
    
   <div class="container">       
       <div class="row">
           <div class="col-xs-6"><h3> Defect List </h3></div>
           <div class="pull-right"><button class="btn btn-info" onclick="return false;">Add Defect</button> &nbsp; &nbsp;</div>
       </div>
       <div class="row">
           <h4>Page Under Construction!!!</h4>
          
           
        </div>
  </div><!-- /.container -->

    <div>
        <uc1:Footer runat="server" id="Footer" />
    </div>
    </form>
</body>
</html>
