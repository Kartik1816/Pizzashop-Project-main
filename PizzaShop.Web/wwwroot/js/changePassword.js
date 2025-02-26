let currpass=true;
function showPassword() {
  console.log('showPassword');
    var x = document.getElementById("current-password");
    if (currpass) {
        x.type = "text";
        document.getElementById('showpassword').classList.add('d-none');
        document.getElementById('hidepassword').classList.remove('d-none');
        currpass=false;

    } else {
        x.type = "password";
        document.getElementById('showpassword').classList.remove('d-none');
        document.getElementById('hidepassword').classList.add('d-none');
        currpass=true;
    }
}

let newpass=true;
function confirmPassword() {
  console.log('showPassword');
    var x = document.getElementById("new-password");
    if (newpass) {
        x.type = "text";
        document.getElementById('showpassword1').classList.add('d-none');
        document.getElementById('hidepassword1').classList.remove('d-none');
        newpass=false;

    } else {
        x.type = "password";
        document.getElementById('showpassword1').classList.remove('d-none');
        document.getElementById('hidepassword1').classList.add('d-none');
        newpass=true;
    }
}

let confirmpass=true;
function newPassword() {
  console.log('showPassword');
    var x = document.getElementById("confirm-password");
    if (confirmpass) {
        x.type = "text";
        document.getElementById('showpassword2').classList.add('d-none');
        document.getElementById('hidepassword2').classList.remove('d-none');
        confirmpass=false;

    } else {
        x.type = "password";
        document.getElementById('showpassword2').classList.remove('d-none');
        document.getElementById('hidepassword2').classList.add('d-none');
        confirmpass=true;
    }
}


$(document).ready(function () {
    $("#cancel-button").click(function () {
        window.location.href = "/Dashboard";
    });
    $("#change-password-form").submit(function (e) {
        e.preventDefault();
        var currentPassword = $("#current-password").val();
        var newPassword = $("#new-password").val();
        var confirmPassword = $("#confirm-password").val();
        if (newPassword !== confirmPassword) {
            $("#error-message").text("New password and confirm password do not match.");
            return;
        }
        console.log("submitting form");
        
        $.ajax({
            type: "POST",
            url: "/user/changepassword",
            data: JSON.stringify({ OldPassword: currentPassword, NewPassword: newPassword, ConfirmPassword: confirmPassword }),
            contentType: "application/json",
            success: function (data) {
                console.log(data);
                console.log(data.value.message);
                if (data.value.success) {
                    toastr.success(data.value.message);
                    // window.location.href = "/home";
                } else {
                    toastr.error(data.value.message);
                    // $("#error-message").text(data.value.message);
                }
            }
        });
    });
});