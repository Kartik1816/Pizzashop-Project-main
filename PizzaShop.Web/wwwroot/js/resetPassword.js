let pass=true;
function showPassword() {
  console.log('showPassword');
    var x = document.getElementById("password");
    if (pass) {
        x.type = "text";
        document.getElementById('showpassword').classList.add('d-none');
        document.getElementById('hidepassword').classList.remove('d-none');
        pass=false;

    } else {
        x.type = "password";
        document.getElementById('showpassword').classList.remove('d-none');
        document.getElementById('hidepassword').classList.add('d-none');
        pass=true;
    }
}

let confirm=true;
function confirmPassword() {
  console.log('showPassword');
    var x = document.getElementById("password1");
    if (pass) {
        x.type = "text";
        document.getElementById('showpassword1').classList.add('d-none');
        document.getElementById('hidepassword1').classList.remove('d-none');
        pass=false;

    } else {
        x.type = "password";
        document.getElementById('showpassword1').classList.remove('d-none');
        document.getElementById('hidepassword1').classList.add('d-none');
        pass=true;
    }
}

var isConfirmPasswordValid = true;
var isPasswordValid = true;

function validatePassword () {
    var password = $('#password').val();
    console.log("Validate Password function");
    
      if (password.length <= 0) {
        isPasswordValid = false;
        $('#password-error').text('Password is required');
      } else if (!password.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)) {
        isPasswordValid = false;
        $('#password-error').text('Password must contain at least one numeric digit, one uppercase and one lowercase letter, and at least 6 or more characters');   
      } else {
        $('#password-error').text('');
        isPasswordValid = true;
      }
  }
  function validateConfirmPassword () {
    var password = $('#password').val();
    var confirmPassword=$('#password1').val();
      if (confirmPassword.length <= 0) {
        isConfirmPasswordValid = false;
        $('#confirm-password-error').text('Password is required');
      } else if (password!=confirmPassword) {
        isConfirmPasswordValid = false;
        $('#password-does-not-match-error').text('New Password And Confirm Password does not match');   
      } else if(password===confirmPassword){
        $('#password-does-not-match-error').text('');
        isConfirmPasswordValid = true;
      }
  }

  $('#password').on('input',function(){
    $('#reset-error').text('');
    validatePassword();

  })

  $('#password1').on('input',function(){
    $('#reset-error').text('');
    validateConfirmPassword();
  })

$(document).ready(function () {
    console.log('ready');

    $('#reset-password-form').submit(function (e) {
        console.log('submit');

        e.preventDefault();
        var password = $('#password').val();
        var confirmPassword = $('#password1').val();
        var token = $('#token').val();
        var userid = $('#userid').val();
        if (password.length <= 0) {
            toastr.error('Password is required');
            return;
        }

        if (confirmPassword.length <= 0) {
            toastr.error('Confirm Password is required');
            return;
        }

        if (password !== confirmPassword) {
            toastr.error('Password and Confirm Password do not match');
            return;
        }
        console.log("password" + password);
        
        $.ajax({
            url: '/api/resetpassword',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ NewPassword: password,Token: token, UserId : userid }),
            success: function (response) {
              console.log(response);
                if (response.success) {
                    toastr.success(response.message);
                    setTimeout(function () {
                        window.location.href = '/Auth';
                    }, 2000);
                } else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                console.log(xhr);
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    toastr.error(xhr.responseJSON.message);
                } else {
                    toastr.error('An unexpected error occurred.');
                }
            }
        });
    });

    $('#reset-password-form-first-login').submit(function (e) {
      console.log('submit');

      e.preventDefault();
      var password = $('#password').val();
      var confirmPassword = $('#password1').val();
      if (password.length <= 0) {
          toastr.error('Password is required');
          return;
      }

      if (confirmPassword.length <= 0) {
          toastr.error('Confirm Password is required');
          return;
      }

      if (password !== confirmPassword) {
          toastr.error('Password and Confirm Password do not match');
          return;
      }
      console.log("passwordsdfsadfsdaf" + password);
      
      $.ajax({
          url: '/api/resetpasswordatFirstLogin',
          method: 'POST',
          contentType: 'application/json',
          data: JSON.stringify({ NewPassword: password}),
          success: function (response) {

            console.log(response);
              if (response.success) {
                  toastr.success(response.message);
                  setTimeout(function () {
                      window.location.href = '/Auth';
                  }, 2000);
              } else {
                  toastr.error(response.message);
              }
          },
          error: function (xhr) {
              console.log(xhr);
              if (xhr.responseJSON && xhr.responseJSON.message) {
                  toastr.error(xhr.responseJSON.message);
              } else {
                  toastr.error('An unexpected error occurred.');
              }
          }
      });
  });
});