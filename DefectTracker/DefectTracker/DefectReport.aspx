<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefectReport.aspx.cs" Inherits="DefectTracker.DefectReport" %>

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
    <link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css" />
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="Scripts/Custom/defectReport.js"></script>
</head>
<body>
    <form id="form1" method="post" runat="server">
        <div>
            <uc1:Header runat="server" ID="Header" />
        </div>

        <div class="container">
            
            <div class="row">
                
                <div class="col-xs-3">
                    <div class="form-group">
                        <label class="col-sm-4 control-label">Project</label>
                        <div class="col-sm-8">
                             <asp:DropDownList CssClass="form-control" ID="ddlProject" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 control-label">Priority</label>
                        <div class="col-sm-8">
                             <asp:DropDownList CssClass="form-control" ID="ddlPriority" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-xs-3">
                    <div class="form-group">
                        <label class="col-sm-4 control-label">Defect Type</label>
                        <div class="col-sm-8">
                             <asp:DropDownList CssClass="form-control" ID="ddlType" runat="server"></asp:DropDownList>
                        </div>
                    </div>  
                    
                    <div class="form-group">
                        <label class="col-sm-4 control-label">Status</label>
                        <div class="col-sm-8">
                            <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server"></asp:DropDownList>
                        </div>
                    </div>                 

                </div>
                <div class="col-xs-3">

                    <div class="form-group">
                        <label class="col-sm-4 control-label">Created By</label>
                        <div class="col-sm-8">
                            <asp:DropDownList CssClass="form-control" ID="CreatedBy" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                     <div class="form-group">
                        <div class="col-sm-8">
                            <asp:Button runat="server" CssClass="btn btn-info" Width="130" Text="Generate Report" OnClick="BtnSearchClick" />
                        </div>
                    </div>                    
                </div>    
                 <div class="col-xs-3">

                   <div class="form-group">
                        <div class="col-sm-8">
                            <asp:Button runat="server" CssClass="btn btn-info" Width="130" Text="Reset" OnClick="BtnResetClick" />
                        </div>
                    </div>                    
                </div>           
            </div>


            <div class="row">
                <table id="myTable" class="table table-striped">
                    <thead>
                        <tr>
                            <th>Project Name</th>
                            <th>Defect Title</th>
                            <th>Status</th>
                            <th>Defect Type</th>
                             <th>Priority</th>
                            <th>Created By</th>
                            <th>Assigned To</th>
                            <th>FixedBy</th>
                            <th>ClosedBy</th>
                          
                        </tr>
                    </thead>
                    <tbody>
                        <% foreach (var defect in DefectList)
                           { %>
                        <tr>
                            <td style="vertical-align: middle"><%=defect.ProjectName%></td>
                            <td style="vertical-align: middle"><%=defect.Title%></td>
                            <td style="vertical-align: middle"><%=defect.Status%> </td>
                            <td style="vertical-align: middle"><%=defect.DefectType%> </td>
                            <td style="vertical-align: middle"><%=defect.Priority%> </td>
                            <td style="vertical-align: middle"><%=defect.CreatedBy%> </td>
                            <td style="vertical-align: middle"><%=defect.AssignedTo%> </td>  
                            <td style="vertical-align: middle"><%=defect.FixedBy%> </td>   
                            <td style="vertical-align: middle"><%=defect.ClosedBy%> </td>   
                                                 
                        </tr>
                        <%} %>
                    </tbody>

                </table>

            </div>

        </div>

        <div>
            <uc1:Footer runat="server" ID="Footer" />
        </div>

    </form>
</body>
</html>
