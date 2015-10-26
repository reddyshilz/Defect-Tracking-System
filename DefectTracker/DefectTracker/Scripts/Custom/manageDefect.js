$(document).ready(function () {
    
    $('#myTable').dataTable({
        bJQueryUI: true,
        sPaginationType: "full_numbers"
    });
   
    $("#errorPanel").hide();
    $("#defectForm").submit(function (e) {

        e.preventDefault();
        var defectData = new DefectData;
        if (validateUser(defectData)) {
            var defectJson = JSON.stringify(defectData);

            $.ajax({
                type: "POST",
                url: "ManageDefectApi.aspx/SaveDefect",
                data: "{'defect' : " + defectJson + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) { $("#myModal").modal("hide"); location.reload(); },
                //success: function (response) {
                //    if (response.d.StatusCode === 500) {
                //        displayErrorPanel(response.d.ReasonPhrase);
                //    } else {
                //        $("#myModal").modal("hide"); location.reload();
                //    }
                //},
                failure: function (response) { alert(response); }
            });
        }

    });
  

    $("#deleteForm").submit(function (e) {
        e.preventDefault();
        var defectid = $("#deleteDefectId").val();
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


function getdata(defectId) {
    //getRoles(defectId);
    getDefect(defectId);
}

function getDefect(defectId) {
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

var status = [];

function getStatus() {
    $("#ddlStatus").empty();
    $("#ddlStatus").append($("<option></option>").val("").html(""));
    if (status.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetStatus",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                status = data.d;
                $.each(status, function (key, value) {
                    $("#ddlStatus").append($("<option></option>").val(value.Id).html(value.Description));
                });
                //getDefect(defectId);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(status, function (key, value) {
            $("#ddlStatus").append($("<option></option>").val(value.Id).html(value.Description));
        });
        //getDefect(defectId);
    }
}

function getPriority() {
    $("#ddlPriority").empty();
    $("#ddlPriority").append($("<option></option>").val("").html(""));
    if (status.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetPriority",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                status = data.d;
                $.each(status, function (key, value) {
                    $("#ddlPriority").append($("<option></option>").val(value.Id).html(value.Description));
                });
                //getDefect(defectId);;
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(status, function (key, value) {
            $("#ddlPriority").append($("<option></option>").val(value.Id).html(value.Description));
        });
        //getDefect(defectId);
    }
}

function getDefectType() {
    $("#ddlType").empty();
    $("#ddlType").append($("<option></option>").val("").html(""));
    if (status.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetDefectType",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                status = data.d;
                $.each(status, function (key, value) {
                    $("#ddlType").append($("<option></option>").val(value.Id).html(value.Description));
                });
                //getDefect(defectId);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(status, function (key, value) {
            $("#ddlType").append($("<option></option>").val(value.Id).html(value.Description));
        });
        //getDefect(defectId);
    }
}
function getStageOfOrigin() {
    $("#ddlOrigin").empty();
    $("#ddlOrigin").append($("<option></option>").val("").html(""));
    if (status.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetStageOfOrigin",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                status = data.d;
                $.each(status, function (key, value) {
                    $("#ddlOrigin").append($("<option></option>").val(value.Id).html(value.Description));
                });
               
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(status, function (key, value) {
            $("#ddlOrigin").append($("<option></option>").val(value.Id).html(value.Description));
        });
        
    }
}
function getProjects() {
    $("#ddlProject").empty();
    $("#ddlProject").append($("<option></option>").val("").html(""));
    if (status.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageDefectApi.aspx/GetUserProjects",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                status = data.d;
                $.each(status, function (key, value) {
                    $("#ddlProject").append($("<option></option>").val(value.Id).html(value.Description));
                });

            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(status, function (key, value) {
            $("#ddlProject").append($("<option></option>").val(value.Id).html(value.Description));
        });

    }
}


var validateUser = function (defect) {
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

function displayErrorPanel(message) {

    $("#errorPanel").show();
    $('#collapseOne').collapse('show');
    $("#messageDetails").html(message);
}

function deleteDefect(Id) {

    var deleteHeader = "Delete Confirmation ?";
    var deleteBody = "Do you want to delete the <br/> Defect : " + Id + "?";
    openConfirmModal(deleteHeader, deleteBody, Id);
}

function openConfirmModal(deleteHeader, deleteBody, Id) {

    $("#deleteModal .modal-header h4").html(deleteHeader);
    $("#deleteModal .modal-body p").html('<strong>' + deleteBody + '</strong>');
    $("#deleteDefectId").val(Id);
    $('#deleteModal').modal({ backdrop: 'static', keyboard: false, show: "show" });

}

function OnSuccess(response) {
    var defect = response.d;
    openModalPopup("Update Defect", defect);
   // $("#UserId").attr("disabled", "disabled");
}

function AddDefect() {
    getStatus();
    getPriority();
    getDefectType();
    getStageOfOrigin();
    getProjects();
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
    defectData.CreatedBy = "";
    
    openModalPopup("Log Defect in the system", defectData);
   // $("#CreatedBy").removeAttr("disabled");
}


function openModalPopup(header, data) {
    $("#myModal .modal-header h4").html(header);
    setDefect(data);
    $("#myModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
}

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
        $("#ddlAssignTo").val(defect.AssignTo);
        $("#HowFixed").val(defect.HowFixed);
        $("#CreatedBy").val(defect.CreatedBy);
        $("#ddlProject").val(defect.ProjectId);

    }
}

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

