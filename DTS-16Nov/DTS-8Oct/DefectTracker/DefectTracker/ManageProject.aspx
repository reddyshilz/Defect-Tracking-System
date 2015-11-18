<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProject.aspx.cs" Inherits="DefectTracker.ManageProject" %>

<%@ Register Src="~/UserControls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/UserControls/Header.ascx" TagPrefix="uc1" TagName="Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Manage Project</title>
    <link href= "Content/bootstrap.css" rel="stylesheet" />
    <link href= "Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/datepicker.css" rel="stylesheet" />
    <link href= "Content/Site.css" rel="stylesheet" />
    <link href= "Content/eComSite.css" rel="stylesheet" />
    
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/Custom/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="Scripts/Custom/docs.min.js"></script>
    
 
    <script src="Scripts/Custom/manageProject.js"></script>

</head>
<body>

<form id="form1" method="get">
    <div>
        <uc1:Header runat="server" ID="Header" />
    </div>
   <div class="container">
       
       <div class="row">
           <div class="col-xs-6"><h3> Manage Projects </h3></div>
           <div class="pull-right"><button class="btn btn-info" onclick="AddProject();return false;">Add Project</button> &nbsp; &nbsp;</div>
       </div>

    <div class="row">
       <table class="table table-hover">           
           <thead>
               <tr>
               <th>ID</th>
               <th>Name</th>
               <th>Description</th>
               <th>Start Date</th>
               <th>End Date</th>
               <th>IsActive</th>              
               <th>Actions</th>
             </tr>
           </thead>
           <% foreach (var project in Projects)
              { %>
           <tr>
               <td style="vertical-align:middle"> <%=project.Id%></td>
               <td style="vertical-align:middle"> <%=project.Name%></td>
               <td style="vertical-align:middle"><%=project.Description%> </td>
               <td style="vertical-align:middle"><%=project.StartDate.ToString("MM/dd/yyyy")%> </td>
               <td style="vertical-align:middle"><%=project.EndDate.ToString("MM/dd/yyyy")%> </td>
                 <td style="vertical-align:middle"><%=project.IsActive%> </td>              
               <td style="vertical-align:middle"><a href="#" onclick="getdata('<%=project.Id%>');return false;"><span class="glyphicon glyphicon-edit edit"></span></a> &nbsp;&nbsp;<a href="#" onclick="deleteProject('<%=project.Id%>');return false;"> <span class="glyphicon glyphicon-trash delete"></span></a></td>
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
                      
                         <input type="hidden" class="form-control" id="Id" />

                        <div class="form-group">
                            <label class="col-sm-4 control-label">Name</label>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" placeholder="Enter Project Name" id="Name" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label">Description</label>
                            <div class="col-sm-8">
                                <input type="text" class="form-control" placeholder="Enter Project Description" id="Description" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-4 control-label">Start Date</label>
                            <div class="col-sm-8">
                                <div class="input-group date">
                                    <input type='text' class="form-control" placeholder="Enter Project Start Date" id='StartDate' />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar pull-left"></span>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label">End Date</label>
                            <div class="col-sm-8">
                                <div class="input-group date">
                                    <input type='text' class="form-control" placeholder="Enter Project End Date"  id='EndDate' />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar pull-left"></span>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4 control-label">IsActive</label>
                            <div class="col-sm-8" style="align-items: center">
                                <input type="checkbox" id="IsActive" />
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
          
              <input type="hidden" id="deleteProjectId" value=""/>
         
   <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="submit" class="btn btn-danger">Delete</button>
      </div>
    </div>
   </form>
  </div>
</div>
</div>

</body>
</html>

