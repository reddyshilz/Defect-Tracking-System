//This file contains all the Jquery ajax method to add, edit, assign defect, UI validation for defect page,
//modal popup open, close and call to web method to pariallly do only save, edit, assign defect function call
//written By praba & shilpa
var usrRole;
$(document).ready(function () {

    $('#myTable').dataTable({
        bJQueryUI: true,
        sPaginationType: "full_numbers"
    });
     usrRole = $('#hdnRole').val();

    //$("#ddlProject").change(function (e) {
    //    onProjectChange(e);
    //});

     $("#errorPanel").hide();
    //On sumit of add/edit defect in modal popup call the webmethod 
     //to save details to database, also show error happen in server side
    $("#defectForm").submit(function (e) {
        e.preventDefault();
        var defectData = new DefectData;
        if (validateDefect(defectData)) {
            var defectJson = JSON.stringify(defectData);                   
            $.ajax({
                type: "POST",
                url: "ManageDefectApi.aspx/SaveDefect",
                data: "{'defect' : " + defectJson + "}",                
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //success: function (response) { $("#myModal").modal("hide"); location.reload(); },
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

    //Jquery ajax call when delete click happen. Call the webmethod to delete the defect 
    //from DB. It will not delete the row from database, just mark that defect as deleted
    $("#deleteForm").submit(function (e) {
        e.preventDefault();       
        var defectid = $("#deleteDefectId").val();
        alert(defectid);
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/DeleteDefect",
            data: '{defectId: "' + defectid + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) { $("#myModal").modal("hide"); location.reload(); },
            failure: function (response) {
                alert(response.d);
            }
        });
    });

});

var selectedDefectId = 0;
var handleAsyncCounter = 0;
function handleAsync() {
    handleAsyncCounter++;
    if (handleAsyncCounter===27)
        getDefect(selectedDefectId);
}

//Get the defect data from db by calling different methods.
//This method get called when edit of defect happen
function getdata(defectId) {
    selectedDefectId = defectId;
    getStatus();
    getPriority();
    getDefectType();
    getStageOfOrigin();
    getProjects();
    getAssignTo();
    if (handleAsyncCounter === 27)
        getDefect(selectedDefectId);
   
}
//Jquery ajax method to call the webmethod to get defect details by defectId 
function getDefect(defectId) {
    hideErrorPanel();
    if (defectId === 0) return;
    $.ajax({
        type: "POST",
        url: "ManageDefectApi.aspx/GetDefectById",
        data: '{defectId: "' + defectId + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        }
    });
}

var status1 = [];
//Jquey ajax method to call the webmethod to get the status list to populate status dropdown
function getStatus() {    
    $("#ddlStatus").empty();
    $("#ddlStatus").append($("<option></option>").val("").html(""));
    if (status1.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetStatus",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                status1 = data.d;
                $.each(status1, function (key, value) {
                    $("#ddlStatus").append($("<option></option>").val(value.Id).html(value.Description));
                    handleAsync();
                });
             
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(status1, function (key, value) {
            $("#ddlStatus").append($("<option></option>").val(value.Id).html(value.Description));
        });
        
    }
}
var priority = [];
//jquery - ajax method call to populate priority dropdown
//call webmethod to get the priority list from database
function getPriority() {   
    $("#ddlPriority").empty();
    $("#ddlPriority").append($("<option></option>").val("").html(""));
    if (priority.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetPriority",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                priority = data.d;
                $.each(priority, function (key, value) {
                    $("#ddlPriority").append($("<option></option>").val(value.Id).html(value.Description));
                    handleAsync();
                });
                
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(priority, function (key, value) {
            $("#ddlPriority").append($("<option></option>").val(value.Id).html(value.Description));
        });
       
    }
}

var deftype = [];
//jquery - ajax method call to populate defecttype dropdown
//call webmethod to get the defecttype list from database
function getDefectType() {   
    $("#ddlType").empty();
    $("#ddlType").append($("<option></option>").val("").html(""));
    if (deftype.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetDefectType",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                deftype = data.d;
                $.each(deftype, function (key, value) {
                    $("#ddlType").append($("<option></option>").val(value.Id).html(value.Description));
                    handleAsync();
                });
                
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(deftype, function (key, value) {
            $("#ddlType").append($("<option></option>").val(value.Id).html(value.Description));
        });
        
    }
}
var deforigin = [];
//jquery - ajax method call to populate stageoforigin dropdown
function getStageOfOrigin() {    
    $("#ddlOrigin").empty();
    $("#ddlOrigin").append($("<option></option>").val("").html(""));
    if (deforigin.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetStageOfOrigin",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                deforigin = data.d;
                $.each(deforigin, function (key, value) {
                    $("#ddlOrigin").append($("<option></option>").val(value.Id).html(value.Description));
                    handleAsync();
                });
               
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(deforigin, function (key, value) {
            $("#ddlOrigin").append($("<option></option>").val(value.Id).html(value.Description));
        });
        
    }
}
var project = [];
//jquery - ajax method call to populate project dropdown
function getProjects() {   
    $("#ddlProject").empty();
    $("#ddlProject").append($("<option></option>").val("").html(""));
    if (project.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetUserProjects",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                project = data.d;

                $.each(project, function (key, value) {
                    $("#ddlProject").append($("<option></option>").val(value.Id).html(value.Name));
                    handleAsync();
                });               
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(project, function (key, value) {
            $("#ddlProject").append($("<option></option>").val(value.Id).html(value.Name));
        });       
    }
}
var assignUserList = [];
//jquery - ajax method call to populate assignto dropdown
function getAssignTo() {    
    $("#ddlAssignTo").empty();
    $("#ddlAssignTo").append($("<option></option>").val("").html(""));
    if (assignUserList.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetAssignedTo",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                assignUserList = data.d;

                $.each(assignUserList, function (key, value) {
                    $("#ddlAssignTo").append($("<option></option>").val(value.UserId).html(value.UserId));
                    handleAsync();
                });               
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(assignUserList, function (key, value) {
            $("#ddlAssignTo").append($("<option></option>").val(value.UserId).html(value.UserId));
        });       
    }
}


var projectAssignedUser = [];
//jquery - ajax method call to populate assignto dropdown on change of project
function onProjectChange(e) {   
    $("#ddlAssignTo").empty();
    $("#ddlAssignTo").append($("<option></option>").val("").html(""));
    if (projectAssignedUser.length === 0) {
    $.ajax({
        type: "POST",
        url: "ManageDefectApi.aspx/PopulateAssignedTo",
        data: "{'projectId' : " + $('#ddlProject').val() + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            projectAssignedUser = data.d;

            $.each(projectAssignedUser, function (key, value) {
                $("#ddlAssignTo").append($("<option></option>").val(value.UserId).html(value.UserId));
            });
                     
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    } else {
        $.each(projectAssignedUser, function (key, value) {
            $("#ddlAssignTo").append($("<option></option>").val(value.UserId).html(value.UserId));
        });
    }
    
};
//Validate required fields in defect page
var validateDefect = function (defect) {
    var message = "<b>Please Fix the below errors </b>";
    message = message + "<ul>";
    var isValid = true;
    if (defect.ProjectId.length === 0) {
        message += "<li>Project selection is required </li>";
        isValid = false;
    }
    if (defect.Title.length === 0) {
        message += "<li>Title is required </li>";
        isValid = false;
    }
    if (defect.DefectType.length === 0) {
        message += "<li>Defect Type is required </li>";
        isValid = false;
    }
    if (defect.StageOfOrigin.length === 0) {
        message += "<li>StageOfOrigin is required </li>";
        isValid = false;
    }
    if (defect.Status.length === 0) {
        message += "<li>Status is required </li>";
        isValid = false;
    }
    else if (defect.Priority.length === 0) {
        message += "<li>Priority is required </li>";
        isValid = false;
    }

    message = message + "</ul>";

    if (!isValid) {
        displayErrorPanel(message);
    }
    return isValid;
}
//Javascript to display error panel
function displayErrorPanel(message) {

    $("#errorPanel").show();
    $('#collapseOne').collapse('show');
    $("#messageDetails").html(message);
}
//Javascript to hide error panel
function hideErrorPanel() {

    $("#errorPanel").hide();
    $('#collapseOne').collapse('hide');
    $("#messageDetails").html();
}
//javascript method called, when onclick of delete button
function deleteDefect(Id) {
    var deleteHeader = "Delete Confirmation ?";
    var deleteBody = "Do you want to delete the <br/> Defect : " + Id + "?";
    openConfirmModal(deleteHeader, deleteBody, Id);
}
//Delete confirmation window
function openConfirmModal(deleteHeader, deleteBody, Id) {

    $("#deleteModal .modal-header h4").html(deleteHeader);
    $("#deleteModal .modal-body p").html('<strong>' + deleteBody + '</strong>');
    $("#deleteDefectId").val(Id);
    $('#deleteModal').modal({ backdrop: 'static', keyboard: false, show: "show" });

}
//javascript method get called, when adddefect button click happen
function AddDefect() {
    getStatus();
    getPriority();
    getDefectType();
    getStageOfOrigin();
    getProjects();
    getAssignTo();    
    var defectData = new DefectData;
    defectData.Id = 0;
    defectData.ProjectId = "";
    defectData.Title = "";
    defectData.DefectType = "";
    defectData.StageOfOrigin = "";
    defectData.Description = "";
    defectData.Status = "";
    defectData.Priority = "";
    defectData.AssignedTo = "";
    defectData.HowFixed = "";
    $("#ddlAssignTo").attr("disabled", "true");
    //$("#ddlStatus").append($("<option></option>").val(1).html("Open"));
    $("#ddlStatus").attr("disabled", "true");
    $("#ddlStatus").val('Open').html("Open");  
    openModalPopup("Log Defect in the system", defectData);
    hideErrorPanel();
}

//if getdata sucessfully then open modal popup to display edit data
function OnSuccess(response) {
    var defect = response.d;
    openModalPopup("Update Defect", defect);
    // $("#UserId").attr("disabled", "disabled");
}
function openModalPopup(header, data) {
    $("#myModal .modal-header h4").html(header);
    setDefect(data);
    $("#myModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
}
//Set the defect value to the UI
var setDefect = function (defect) {
    if (defect !== null) {
        $("#Id").val(defect.Id);
        $("#ddlProject").val(defect.ProjectId);
        $("#Title").val(defect.Title);
        $("#ddlType").val(defect.DefectType);
        $("#ddlOrigin").val(defect.StageOfOrigin);
        $("#Description").val(defect.Description);
        $("#ddlStatus").val(defect.Status);
        $("#ddlPriority").val(defect.Priority);
        $("#ddlAssignTo").val(defect.AssignedTo);
        $("#HowFixed").val(defect.HowFixed);
        $("#CreatedBy").val(defect.CreatedBy);
        $("#ddlProject").val(defect.ProjectId);
        $("#oldStatus").val(defect.Status);
        var sta = $('#oldStatus').val();
        
        if (sta == '')
        {
            $("#ddlAssignTo").attr("disabled", "true");
            $("#ddlStatus").attr("disabled", "true");
        }
        else if (sta == '1' )
        {
            $("#ddlAssignTo").attr("disabled", "true");       
            $("#ddlStatus").attr("disabled", "true");
            if(usrRole == 'PM')
            {
                $("#ddlAssignTo").removeAttr("disabled");
                $("#ddlStatus").removeAttr("disabled");
            }
        }
        else if (sta == '3' || sta=='4' || sta=='5')//status inprogress or fixed or closed
        {
            $("#ddlAssignTo").attr("disabled", "true");
        }
        else
        {            
           // $("#ddlAssignTo").removeAttr("disabled");
            $("#ddlStatus").removeAttr("disabled");
        }
       

    }
}
//read the defect data from UI
var DefectData = function () {
    this.Id = $("#Id").val();
    this.Title = $("#Title").val();
    this.DefectType = $("#ddlType").val();
    this.StageOfOrigin = $("#ddlOrigin").val();
    this.Description = $("#Description").val();
    this.Status = $("#ddlStatus").val();
    this.Priority = $("#ddlPriority").val();
    this.AssignedTo = $("#ddlAssignTo").val();
    this.HowFixed = $("#HowFixed").val();
    this.CreatedBy = $("#CreatedBy").val();
    this.ProjectId = $("#ddlProject").val();

}

