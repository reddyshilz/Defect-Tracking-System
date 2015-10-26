<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="DefectTracker.ManageUser" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/UserControls/Header.ascx" TagPrefix="uc1" TagName="Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage User</title>
     <link href= "Content/bootstrap.css" rel="stylesheet" />
    <link href= "Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/datepicker.css" rel="stylesheet" />
    <link href= "Content/Site.css" rel="stylesheet" />
    <link href= "Content/eComSite.css" rel="stylesheet" />
    
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/Custom/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="Scripts/Custom/docs.min.js"></script>

     <script src="Scripts/Custom/manageUser.js"></script>
</head>
<body>
    <form id="form1" method="get" >
    <div>
        <uc1:Header runat="server" ID="Header" />
    </div>
   <div class="container">
       
       <div class="row">
           <div class="col-xs-6"><h3> Manage Users </h3></div>
           <div class="pull-right"><button class="btn btn-info" onclick="AddUser();return false;">Add User</button> &nbsp; &nbsp;</div>
       </div>

    <div class="row">
       <table class="table table-hover" id="myTable">           
           <thead>
               <tr>
               <th>UserId</th>
               <th>First Name</th>
               <th>Last Name</th>
               <th>Email Id</th>
               <th>Role</th>                                            
               <th>Actions</th>
             </tr>
           </thead>
           <% foreach (var user in UserList)
              { %>
           <tr>
               <td style="vertical-align:middle"> <%=user.UserId%></td>
               <td style="vertical-align:middle"> <%=user.FirstName%></td>
               <td style="vertical-align:middle"><%=user.LastName%> </td>
               <td style="vertical-align:middle"><%=user.EmailId%> </td>
               <td style="vertical-align:middle"><%=user.Role%> </td>                
               <td style="vertical-align:middle; width: 100px"><a href="#" onclick="getdata(<%=user.Id%>);return false;"><span class="glyphicon glyphicon-edit edit"></span></a> &nbsp;&nbsp;<a href="#" onclick="AssignProject('<%=user.Id%>','<%=user.FirstName%>','<%=user.LastName%>');return false;"> <span class="glyphicon glyphicon-plus edit"></span></a> &nbsp;&nbsp;<a href="#" onclick="deleteUser('<%=user.Id%>');return false;"> <span class="glyphicon glyphicon-trash delete"></span></a></td>
           </tr>
           <%} %>
            
       </table>
        
       
     
    </div><!-- /.row -->

   
</div><!-- /.container -->

    <div>
        <uc1:Footer runat="server" id="Footer" />
    </div>
    
    </form>

<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Modal title</h4>
      </div>
   
      <div class="modal-body">

           <div class="row" id="errorPanel">
                        <div class="col-md-12">
                            <!--Error Messsage Panel start -->
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <a style="color: Red" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Errors</a>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse">
                                        <div class="panel-body alert-warning text-left">
                                            <div id="msgPanel">
                                                <div id="messageDetails"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

           <form role="form" id="myForm" class="form-horizontal" method="post">
             <div class="form-group" >
                <label class="col-sm-4 control-label">User Id</label>
                <div class="col-sm-8">
                     <input type="hidden" class="form-control" id="Id" />
                    <input type="text" class="form-control" id="UserId" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-sm-4 control-label">First Name</label>
                <div class="col-sm-8">
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
     </form>
    </div>
  
  </div>
</div>
</div>

<div class="modal fade bs-example-modal-sm" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel1"></h4>
      </div>
        <form id="deleteForm"  role="form" class="form-horizontal" method="post">
      <div class="modal-body">
          <p style="text-align: center"></p>
          
              <input type="hidden" id="deleteUserId" value=""/>
         
   <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="submit" class="btn btn-danger">Delete</button>
      </div>
    </div>
   </form>
  </div>
</div>
</div>
    
    
<div class="modal fade" id="apModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="apModalLabel">Modal title</h4>
      </div>
   
      <div class="modal-body">
           <form role="form" id="apForm" class="form-horizontal" method="post">
             <div>
                   <table id="projectTable" class="table table-striped">
                       <thead>
                           <tr>
                               <th>Assign Projects</th>
                           </tr>
                       </thead>
                   </table>
               </div> 
        
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-info">Save changes</button>
          </div>
     </form>
    </div>
  
  </div>
</div>
</div>
</body>
</html>
