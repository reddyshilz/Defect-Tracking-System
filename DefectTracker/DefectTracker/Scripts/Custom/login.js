//This file contains all the Jquery ajax method for login page to call the webmethod 
// to do validation , change password & forgot password functionality --Praba & Rekha
function ChangePassword() {

    $("#cperrorPanel").hide();
    $("#cpModal .modal-header h4").html("Change Password");
    $("#cpModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
}

function ForgotPassword() {
    $("#fperrorPanel").hide();
    $("#fpModal .modal-header h4").html("Forgot Password");
    $("#fpModal").modal({ backdrop: 'static', keyboard: false, show: "show" });
}

//jquery - ajax method call to do change password
//call webmethod to update the password info in DB
$(document).ready(function () {
  
    $("#cpForm").submit(function(e) {

        e.preventDefault();

        var cpModel = new CPModel();

        if (validateCPmodel(cpModel)) {
            var cpJson = JSON.stringify(cpModel);
            $.ajax({
                type: "POST",
                url: "Login.aspx/ChangePassword",
                data: "{'changePassword' : " + cpJson + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.StatusCode === 500) {
                        displaycpErrorPanel(response.d.ReasonPhrase);
                    } else {
                        $("#cpModal").modal("hide");
                    }
                },
                failure: function(response) { alert(response); }
            });
        }

    });
    //jquery - ajax method call to do forgot password
    //call webmethod to update the password info in DB & sent mail to user with new password
    $("#fpForm").submit(function(e) {
        e.preventDefault();

        var fpModel = new FPModel();

        if (validateFPmodel(fpModel)) {
            var fpJson = JSON.stringify(fpModel);
            $.ajax({
                type: "POST",
                url: "Login.aspx/ForgotPassword",
                data: "{'forgotPassword' : " + fpJson + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.StatusCode === 500) {
                        displayfpErrorPanel(response.d.ReasonPhrase);
                    } else {
                        $("#fpModal").modal("hide");
                    }
                },
                failure: function(response) {
                    console.log(response);
                    alert(response);
                }
            });
        }
    });


});
//jquery ajax call for required field validation
function validateCPmodel(cpmodel) {
    var message = "<b>Please Fix the below errors </b>";
    message = message + "<ul>";
    var isValid = true;
    if (cpmodel.RegisteredUserName.length === 0) {
        message += "<li>Registered UserId is required </li>";
        isValid = false;
    }
    if (cpmodel.CurrentPassword.length === 0) {
        message += "<li>Current Password is required </li>";
        isValid = false;
    }
    if (cpmodel.NewPassword.length === 0) {
        message += "<li>New Password is required </li>";
        isValid = false;
    }
    if (cpmodel.ConfirmPassword.length === 0) {
        message += "<li>Confirm Password is required </li>";
        isValid = false;
    }
    else if (cpmodel.NewPassword.length > 0) {
        if (cpmodel.NewPassword !== cpmodel.ConfirmPassword) {
            message += "<li>Confirm Password is not matching with New Password </li>";
            isValid = false;
        }
    }
    message = message + "</ul>";

    if (!isValid) {
        displaycpErrorPanel(message);
    }
    return isValid;
}

function displaycpErrorPanel(message) {

    $("#cperrorPanel").show();
    $('#cpcollapseOne').collapse('show');
    $("#cpmessageDetails").html(message);
}
//validate forgot password window for emailID
function validateFPmodel(cpmodel) {
    var message = "<b>Please Fix the below errors </b>";
    message = message + "<ul>";
    var isValid = true;
    if (cpmodel.RegisteredEmail.length === 0) {
        message += "<li>Registered Email Id is required </li>";
        isValid = false;
    }
    message = message + "</ul>";

    if (!isValid) {
        displayfpErrorPanel(message);
    }
    return isValid;
}
function displayfpErrorPanel(message) {
    $("#fperrorPanel").show();
    $('#fpcollapseOne').collapse('show');
    $("#fpmessageDetails").html(message);
}

var CPModel = function () {
    this.RegisteredUserName = $("#registeredUserName").val();
    this.CurrentPassword = $("#currentPassword").val();
    this.NewPassword = $("#newPassword").val();
    this.ConfirmPassword = $("#confirmPassword").val();
}

var FPModel = function () {
    this.RegisteredEmail = $("#txtEmail").val();
}