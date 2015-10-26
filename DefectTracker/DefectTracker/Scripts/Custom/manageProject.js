$(document).ready(function () {

    var dateFormat = "mm-dd-yyyy";
    var validationMessage = "";
    $("#errorPanel").hide();
    $("#StartDate").datepicker({ viewMode: 'date', format: dateFormat, "autoclose": true });
    $("#EndDate").datepicker({ viewMode: 'date', format: dateFormat, "autoclose": true });

    $("#myForm").submit(function (e) {

        e.preventDefault();
        var projectData = new ProjectData();
        if (validateProject(projectData)) {
            var projectJson = JSON.stringify(projectData);
            $.ajax({
                type: "POST",
                url: "ManageProjectApi.aspx/SaveProject",
                data: "{'project' : " + projectJson + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
               // success: function (response) { $("#myModal").modal("hide"); location.reload(); },
                success: function (response) {
                    if (response.d.StatusCode === 500) {
                        displayErrorPanel(response.d.ReasonPhrase);
                    } else {
                        $("#myModal").modal("hide"); location.reload();
                    }
                    
                },
                failure: function (response) { alert(response); }
            });
        }

    });

    $("#deleteForm").submit(function (e) {
        e.preventDefault();
        var projectid = $("#deleteProjectId").val();
        $.ajax({
            type: "POST",
            url: "ManageProjectApi.aspx/DeleteProject",
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

var validateProject = function (project) {
    var message = "<b>Please Fix the below errors </b>";
    message = message + "<ul>";
    var isValid = true;
    if (project.Name.length === 0) {
        message += "<li>Project Name is required </li>";
        isValid = false;
    }
    if (project.StartDate.length === 0) {
        message += "<li>Project start date is required </li>";
        isValid = false;
    }
    if (project.EndDate.length === 0) {
        message += "<li>Project end date is required </li>";
        isValid = false;
    }
    else if (project.StartDate.length > 0) {
        var dtCompare = compareDate(project.StartDate, project.EndDate);
        if (!dtCompare) {
            message += "<li> Project start date can not be greatr than end date </li>";
            isValid = false;
        }
    }
    message = message + "</ul>";

    if (!isValid) {
        displayErrorPanel(message);
    }
    return isValid;
}

function displayErrorPanel(message) {

    $("#errorPanel").show();
    $('#collapseOne').collapse('show');
    $("#messageDetails").html(message);
}

function compareDate(fromDate, toDate) {
    var dt1 = new Date(fromDate).valueOf();
    var dt2 = new Date(toDate).valueOf();
    return dt2 > dt1;
}

function getdata(projectId) {

    $.ajax({
        type: "POST",
        url: "ManageProjectApi.aspx/GetProjectById",
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
    $("#Name").attr("disabled", "disabled");
}

function AddProject() {
    var projectData = new ProjectData();
    projectData.Name = "";
    projectData.Description = "";
    projectData.StartDate = null;
    projectData.EndDate = null;
    projectData.IsActive = true;
    openModalPopup("Add Project", projectData);
    $("#Name").removeAttr("disabled");
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

        var sdate = "";
        var edate = "";

        if (project.StartDate !== null)
            sdate = new Date(parseInt(project.StartDate.substr(6)));
        if (project.EndDate !== null)
            edate = new Date(parseInt(project.EndDate.substr(6)));

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
