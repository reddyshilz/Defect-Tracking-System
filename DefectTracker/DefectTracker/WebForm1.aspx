<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DefectTracker.WebForm1" %>

<!DOCTYPE html>   
<html lang="en">   
<head>   
<meta charset="utf-8">   
<title>Example of Employee Table with twitter bootstrap</title>   
<meta name="description" content="Creating a Employee table with Twitter Bootstrap. Learn with example of a Employee Table with Twitter Bootstrap.">  
<link href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet">   
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
<link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css"></style>
<script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
</head>  
<body style="margin:20px auto">  
<div class="container">
<div class="row header" style="text-align:center;color:green">
<h3>Bootstrap Table With sorting,searching and paging using dataTable.js (Responsive)</h3>
</div>
<table id="myTable" class="table table-striped" >  
     <thead>
               <tr>
               <th>Project Name</th>
               <th>Defect Title</th>
               <th>Status</th>
               <th>Defect Type</th>
               <th>Created By</th>                                            
               <th>Assigned To</th>
                   <th></th>
             </tr>
           </thead>
    <tbody>
           <% foreach (var defect in defectList)
              { %>
           <tr>
               <td > <%=defect.ProjectName%></td>
               <td > <%=defect.Title%></td>
               <td ><%=defect.Status%> </td>
               <td ><%=defect.DefectType%> </td>
               <td ><%=defect.CreatedBy%> </td>  
                 <td ><%=defect.AssignedTo%> </td>                
               <td><a href="#" onclick="getdata(<%=defect.Id%>);return false;"><span class="glyphicon glyphicon-edit edit"></span></a> &nbsp;&nbsp;<a href="#" onclick="deleteDefect('<%=defect.Id%>');return false;"> <span class="glyphicon glyphicon-trash delete"></span></a></td>
           </tr>
           <%} %>
        </tbody>
      </table>  
	  </div>
</body>  
<script>
    $(document).ready(function () {
        alert('test');
        $('#myTable').dataTable();
    });
</script>
</html>  