$(document).ready(function () {

    var serviceBaseUrl = "http://pvamu-empwebapi.azurewebsites.net";
    //var serviceBaseUrl = "http://localhost:20323";
   
    $("#errorPanel").hide();
    $("#myForm").submit(function (e) {

        e.preventDefault();
        var userData = new UserData;
        if (validateUser(userData)) {
            var userJson = JSON.stringify(userData);

            $.ajax({
                type: "POST",
                url: "ManageUserApi.aspx/SaveUser",
                data: "{'user' : " + userJson + "}",
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

    $('#UserId').blur(function (e) {      
        $.ajax({
            type: "GET",
            url: serviceBaseUrl + "/api/employee/" + $('#UserId').val(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.FirstName == '' || !response.FirstName || response.FirstName == undefined) {
                    $("#FirstName").val('NA');
                    $("#LastName").val('NA');
                    $("#EmailId").val('NA');
                }
                else{
                $("#FirstName").val(response.FirstName);
                $("#LastName").val(response.LastName);
                $("#EmailId").val(response.EmailId);
                }
            },
            failure: function (response) { alert(response); }
        });
    });

    $("#apForm").submit(function (e) {

        e.preventDefault();
        var userData = new UserData;
        //var assignments = [{ UserId: "1", ProjectId: "10" }, { UserId: "1", ProjectId: "11" }];
        var assignmentsJson = JSON.stringify(assignedProjects);
        $.ajax({
            type: "POST",
            url: "ManageUserApi.aspx/AddRemoveProject",
            data: "{'assignments' : " + assignmentsJson + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) { $("#myModal").modal("hide"); location.reload(); },
            failure: function (response) { alert(response); }
        });

    });

    $("#deleteForm").submit(function (e) {
        e.preventDefault();
        var userid = $("#deleteUserId").val();
        $.ajax({
            type: "POST",
            url: "ManageUserApi.aspx/DeleteUser",
            data: '{userId: "' + userid + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) { $("#myModal").modal("hide"); location.reload(); },
            failure: function (response) {
                alert(response.d);
            }
        });
    });

});


function getdata(userId) {
    getRoles(userId);
}

function getUser(userId) {
    if (userId === 0) return;
    $.ajax({
        type: "POST",
        url: "ManageUserApi.aspx/GetUserById",
        data: '{userId: "' + userId + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        }
    });

}

var roles = [];

function getRoles(userId) {
    $("#ddlrole").empty();
    $("#ddlrole").append($("<option></option>").val("").html(""));
    if (roles.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageUserApi.aspx/GetRoles",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                roles = data.d;
                $.each(roles, function (key, value) {
                    $("#ddlrole").append($("<option></option>").val(value.RoleId).html(value.Description));
                });
                getUser(userId);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        $.each(roles, function (key, value) {
            $("#ddlrole").append($("<option></option>").val(value.RoleId).html(value.Description));
        });
        getUser(userId);
    }

}


var validateUser = function (user) {
    var message = "<b>Please Fix the below errors </b>";
    message = message + "<ul>";
    var isValid = true;
    if (user.UserId.length === 0) {
        message += "<li>User Id is required </li>";
        isValid = false;
    }
    if (user.FirstName.length === 0) {
        message += "<li>First name is required </li>";
        isValid = false;
    }
    if (user.LastName.length === 0) {
        message += "<li>Last name is required </li>";
        isValid = false;
    }
    if (user.EmailId.length === 0) {
        message += "<li>EmailID is required </li>";
        isValid = false;
    }
    else if (user.RoleId.length === 0) {
        message += "<li>Role is required </li>";
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


function deleteUser(userId) {

    var deleteHeader = "Delete Confirmation ?";
    var deleteBody = "Do you want to delete the <br/> User Id : " + userId + "?";
    openConfirmModal(deleteHeader, deleteBody, userId);
}


function openConfirmModal(deleteHeader, deleteBody, userId) {

    $("#deleteModal .modal-header h4").html(deleteHeader);
    $("#deleteModal .modal-body p").html('<strong>' + deleteBody + '</strong>');
    $("#deleteUserId").val(userId);
    $('#deleteModal').modal({ backdrop: 'static', keyboard: false, show: "show" });

}

function OnSuccess(response) {
    var user = response.d;
    openModalPopup("Update User", user);
    $("#UserId").attr("disabled", "disabled");
    $("#FirstName").attr("disabled", "disabled");
    $("#LastName").attr("disabled", "disabled");
    $("#EmailId").attr("disabled", "disabled");
}

function AddUser() {
    getRoles(0);
    var userData = new UserData;
    userData.Id = 0;
    userData.UserId = "";
    userData.FirstName = "";
    userData.LastName = "";
    userData.EmailId = "";
    userData.RoleId = "";
    openModalPopup("Add User", userData);
    $("#UserId").removeAttr("disabled");
}

var pojects = [];
var assignedProjects = [];

function AssignProject(id, firstName, lastName) {
    if (id === 0) return;
    $.ajax({
        type: "POST",
        url: "ManageUserApi.aspx/GetAssignedProject",
        data: '{userId: "' + id + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            assignedProjects = data.d;
            mapAssignProjects(id, firstName, lastName);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

var allocatedProjList = [];
function AssignProjectList(id) {
    if (id === 0) return;
    $.ajax({
        type: "POST",
        url: "ManageUserApi.aspx/GetAssignProjList",
        data: '{userId: "' + id + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            allocatedProjList = data.d;            
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function assignProjectModalPopup(header, data) {
    $("#apModal .modal-header h4").html(header);
    $("#apModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
}

function mapAssignProjects(id, firstName, lastName) {

    $("#projectTable tr").remove();
    var fullName = firstName + " " + lastName;

    if (pojects.length === 0) {
        $.ajax({
            type: "POST",
            url: "ManageProjectApi.aspx/GetProjectsForAssignment",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                pojects = data.d;
                var isChecked = "";
                $.each(pojects, function (key, value) {

                    for (var i = 0; i < assignedProjects.length; i++) {
                        if (value.Id === assignedProjects[i].ProjectId) {
                            isChecked = 'checked';
                            break;;
                        } else {
                            isChecked = "";
                        }
                    }
                    $('#projectTable').append('<tr><td><input type="checkbox" onchange="mapProject(' + id + "," + value.Id + ",this);\"" + isChecked + ' /> &nbsp &nbsp;' + value.Name + '</td></tr>');

                });
                assignProjectModalPopup("Add/Remove Projects for " + fullName, null);

            },
            failure: function (response) {
                alert(response.d);
            }
        });
    } else {
        var isChecked = "";
        $.each(pojects, function (key, value) {

            for (var i = 0; i < assignedProjects.length; i++) {
                if (value.Id === assignedProjects[i].ProjectId) {
                    isChecked = 'checked';
                    break;;
                } else {
                    isChecked = "";
                }
            }
            $('#projectTable').append('<tr><td><input type="checkbox" onchange="mapProject(' + id + "," + value.Id + ",this);\"" + isChecked + ' /> &nbsp &nbsp;' + value.Name + '</td></tr>');

        });
        assignProjectModalPopup("Add/Remove Projects for " + fullName, null);
    }
}

function mapProject(userId, projectId, cbxCtrl) {
    if (cbxCtrl.checked) {
        var ap = { "__type": "DefectTracker.Models.ProjectAssignment", "ProjectId": projectId, "UserId": userId };
        assignedProjects.push(ap);
    } else {
        for (var index = 0; index < assignedProjects.length; index++) {
            if (assignedProjects[index].ProjectId === projectId) {
                assignedProjects.splice(index, 1);
                break;
            }
        }
    }

}

function openModalPopup(header, data) {
    $("#myModal .modal-header h4").html(header);
    setUser(data);
    $("#myModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
}

var setUser = function (user) {
    if (user !== null) {
        $("#Id").val(user.Id);
        $("#UserId").val(user.UserId);
        $("#FirstName").val(user.FirstName);
        $("#LastName").val(user.LastName);
        $("#EmailId").val(user.EmailId);
        $("#ddlrole").val(user.RoleId);

    }
}

var UserData = function () {
    this.Id = $("#Id").val();
    this.UserId = $("#UserId").val();
    this.FirstName = $("#FirstName").val();
    this.LastName = $("#LastName").val();
    this.EmailId = $("#EmailId").val();
    this.RoleId = $("#ddlrole").val();

}

