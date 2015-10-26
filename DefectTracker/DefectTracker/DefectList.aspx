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

    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/Custom/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="Scripts/Custom/docs.min.js"></script>
    <link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css"/>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
     <script src="Scripts/Custom/manageDefect.js"></script>
    
</head>
<body>
     <div>
        <uc1:Header runat="server" ID="Header" />
    </div>
    <form id="form1" method="get" >
    
   <div class="container">       
       <div class="row">
           <div class="col-xs-6"><h3> Defect List </h3></div>
           <div class="pull-right"><button class="btn btn-info" onclick=" AddDefect();return false;">Add Defect</button> &nbsp; &nbsp;</div>
       </div>      
       <div class="row">
       <table  id="myTable" class="table table-striped">           
           <thead>
               <tr>
               <th>Project Name</th>
               <th>Defect Title</th>
               <th>Status</th>
               <th>Defect Type</th>
               <th>Created By</th>                                            
               <th>Assigned To</th>
               <th>Actions</th>
             </tr>
           </thead>
           <tbody>
           <% foreach (var defect in defectList)
              { %>
           <tr>
               <td style="vertical-align:middle"> <%=defect.ProjectName%></td>
               <td style="vertical-align:middle"> <%=defect.Title%></td>
               <td style="vertical-align:middle"><%=defect.Status%> </td>
               <td style="vertical-align:middle"><%=defect.DefectType%> </td>
               <td style="vertical-align:middle"><%=defect.CreatedBy%> </td>  
                 <td style="vertical-align:middle"><%=defect.AssignedTo%> </td>                
               <td style="vertical-align:middle; width: 100px"><a href="#" onclick="getdata(<%=defect.Id%>);return false;"><span class="glyphicon glyphicon-edit edit"></span></a> &nbsp;&nbsp;<a href="#" onclick="deleteDefect('<%=defect.Id%>');return false;"> <span class="glyphicon glyphicon-trash delete"></span></a></td>
           </tr>
           <%} %>
               </tbody>
            
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
                              <input type="hidden" id="deleteDefectId" value=""/>         
                            <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <button type="submit" class="btn btn-danger">Delete</button>
                             </div>
                    </div>
                    </form>
                </div>
                </div>
            </div>

           <form role="form" id="defectForm" class="form-horizontal" method="post">
             <div class="form-group" >
                <label class="col-sm-4 control-label">Title</label>
                <div class="col-sm-8">
                     <input type="hidden" class="form-control" id="Id" />
                    <input type="text" class="form-control" id="Title" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-sm-4 control-label">Defect Type</label>
                <div class="col-sm-8">
                    <select  class="form-control"  id="ddlType" >
                          <option value=""></option>
                      </select>
                </div>

            </div>
                
            <div class="form-group">
                <label class="col-sm-4 control-label">Stage of origin</label>
                <div class="col-sm-8">
                    <select  class="form-control"  id="ddlOrigin" >
                          <option value=""></option>
                      </select>
                </div>
            </div> 
                 
            <div class="form-group">
                <label class="col-sm-4 control-label">Status</label>
                <div class="col-sm-8">
                    <select  class="form-control"  id="ddlStatus" >
                            <option value=""></option>
                        </select>
                </div>
            </div>   
             <div class="form-group">
                <label class="col-sm-4 control-label">Priority</label>
                <div class="col-sm-8">
                    <select  class="form-control"  id="ddlPriority" >
                          <option value=""></option>
                      </select>
                </div>
            </div>      
                
           <div class="form-group">
                <label class="col-sm-4 control-label">Project</label>
                <div class="col-sm-8">
                    <select  class="form-control"  id="ddlProject" >
                          <option value=""></option>
                      </select>
                </div>
            </div> 
           
            <div class="form-group">
                <label class="col-sm-4 control-label">Created By</label>
                <div class="col-sm-8">                    
                    <input type="text" class="form-control" id="CreatedBy" disabled="disabled" />
                </div>
            </div> 
        
             <div class="form-group">
                <label class="col-sm-4 control-label">AssignTo</label>
                <div class="col-sm-8">
                    <select  class="form-control"  id="ddlAssignTo" >
                          <option value=""></option>
                      </select>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-sm-4 control-label">Description</label>
                <div class="col-sm-8">    
                    <textarea rows="3" class="form-control" id="Description" placeholder="Description"></textarea>                    
                </div>
            </div> 

            <div class="form-group">
                <label class="col-sm-4 control-label">HowFixed</label>
                <div class="col-sm-8">    
                    <textarea rows="3" class="form-control" id="HowFixed" placeholder="How fixed?"></textarea>                    
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
</body>
</html>
