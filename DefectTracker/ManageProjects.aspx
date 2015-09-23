<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageProjects.aspx.cs" Inherits="ManageProjects" %>

<%@ Register Src="~/UserControls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/UserControls/Header.ascx" TagPrefix="uc1" TagName="Header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Manage Project</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/datepicker.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/eComSite.css" rel="stylesheet" />
    
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/Custom/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="Scripts/Custom/docs.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var dateFormat = "mm-dd-yyyy";

            $("#StartDate").datepicker({ viewMode: 'date', format: dateFormat, "autoclose": true });
            $("#EndDate").datepicker({ viewMode: 'date', format: dateFormat, "autoclose": true });

            $("#myForm").submit(function (e) {

                e.preventDefault();
                var projectData = new ProjectData;
                var projectJson = JSON.stringify(projectData);

                $.ajax({
                    type: "POST",
                    url: "ManageProjectAPI.aspx/SaveProject",
                    data: "{'project' : " + projectJson + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) { $("#myModal").modal("hide"); location.reload(); },
                    failure: function (response) { alert(response); }
                });

            });

            $("#deleteForm").submit(function (e) {
                e.preventDefault();
                var projectid = $("#deleteProjectId").val();
                $.ajax({
                    type: "POST",
                    url: "ManageProjectAPI.aspx/DeleteProject",
                    data: '{projectId: "' + projectid + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) { $("#myModal").modal("hide"); location.reload(); },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });

        });






        function getdata(projectId) {

            $.ajax({
                type: "POST",
                url: "ManageProjectAPI.aspx/GetProjectById",
                data: '{projectId: "' + projectId + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });

        }


        function deleteProject(projectId) {

            var deleteHeader = "Delete Confirmation ?";
            var deleteBody = "Do you want to delete the <br/> Project Id : " + projectId + "?";
            openConfirmModal(deleteHeader, deleteBody, projectId);
        }


        function openConfirmModal(deleteHeader, deleteBody, projectId) {

            $("#deleteModal .modal-header h4").html(deleteHeader);
            $("#deleteModal .modal-body p").html('<strong>' + deleteBody + '</strong>');
            $("#deleteProjectId").val(projectId);
            $('#deleteModal').modal({ backdrop: 'static', keyboard: false, show: "show" });

        }

        function OnSuccess(response) {
            var project = response.d;
            openModalPopup("Update Project", project);
        }

        function AddProject() {
            openModalPopup("Add Project", null);
        }

        function openModalPopup(header, data) {
            $("#myModal .modal-header h4").html(header);
            setProject(data);
            $("#myModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
        }

        var setProject = function (project) {
            if (project !== null) {

                $("#Id").val(project.Id);
                $("#Name").val(project.Name);
                $("#Description").val(project.Description);

                var sdate = new Date(parseInt(project.StartDate.substr(6)));
                var edate = new Date(parseInt(project.EndDate.substr(6)));

                $("#StartDate").datepicker("setDate", sdate);
                $("#EndDate").datepicker("setDate", edate);

                if (project.IsActive) {
                    $("#IsActive").prop('checked', true);
                }
                else {
                    $("#IsActive").prop('checked', false);
                }
            }
        }

        var ProjectData = function () {
            this.Id = $("#Id").val();
            this.Name = $("#Name").val();
            this.Description = $("#Description").val();
            this.StartDate = $("#StartDate").val();
            this.EndDate = $("#EndDate").val();
            this.IsActive = $("#IsActive").prop("checked");
        }


    </script>

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
           <form role="form" id="myForm" class="form-horizontal" method="post">
             <div class="form-group">
                <label class="col-sm-4 control-label">Project Id</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="Id" />
                </div>
            </div>

              <div class="form-group">
                <label class="col-sm-4 control-label">Name</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="Name" />
                </div>
            </div>
                
                  <div class="form-group">
                <label class="col-sm-4 control-label">Description</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="Description" />
                </div>
            </div>
                
                
                  <div class="form-group">
                <label class="col-sm-4 control-label">Start Date</label>
               <div class="col-sm-8">
                <div class="input-group date">
                   <input type='text' class="form-control"  id='StartDate'/>
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
                   <input type='text' class="form-control" id='EndDate'/>
                      <span class="input-group-addon">
                      <span class="glyphicon glyphicon-calendar pull-left"></span>
                   </span>
                </div>
              </div>
            </div>
                
               <div class="form-group">
                <label class="col-sm-4 control-label">IsActive</label>
                <div class="col-sm-8" style="align-items :center">
                    <input type="checkbox" id="IsActive"/>
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


