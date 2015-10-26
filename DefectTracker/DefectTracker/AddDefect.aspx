<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDefect.aspx.cs" Inherits="DefectTracker.AddDefect" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/UserControls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
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
  <form role="form" id="myForm" class="form-horizontal" method="post">
       <div>
        <uc1:Header runat="server" ID="Header" />
    </div>
      <div class="container">
       
       <div class="row">
           <div class="col-xs-6"><h3> Manage Projects </h3></div>
        </div>
             <div class="form-group" >
                <label class="col-sm-2 control-label">User Id</label>
                <div class="col-sm-10">
                     <input type="hidden" class="form-control" id="Id" />
                    <input type="text" class="form-control" id="UserId" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-sm-2 control-label">First Name</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" id="FirstName" />
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-sm-4 control-label">Last Name</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="LastName" />
                </div>
            </div>                
                
            <div class="form-group">
                <label class="col-sm-4 control-label">EmailId</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="EmailId" />
                </div>
            </div>  
           
            <div class="form-group">
                <label class="col-sm-4 control-label">Role</label>
                <div class="col-sm-8">                    
                      <select  class="form-control"  id="ddlrole" >
                          <option value=""></option>
                      </select>
                </div>
            </div> 
        
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-info">Save changes</button>
          </div>
    </div>
        <div>
        <uc1:Footer runat="server" id="Footer" />
    </div>
     </form>
    
</body>
</html>
