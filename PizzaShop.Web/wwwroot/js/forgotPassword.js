

var isEmailValid = true;

function validateEmail () {
  var email = $('#forgot-email').val();
  if (email.length <= 0) {
    isEmailValid = false;
    $('#email-error').text('Email is required');
  } else if (!email.match(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)) {
    isEmailValid = false;
    $('#email-error').text('Email is invalid');
  } else {
    $('#email-error').text('');
    isEmailValid = true;
  }
}

$('#forgot-email').on('input', function () {
  $('#login-error').text(''); 
  validateEmail();
});

$(document).ready(function(){

  var storedEmail=localStorage.getItem('email')
 
  if (storedEmail)
  {
    $('#forgot-email').val(storedEmail);
  }

  $('#forgot-password-form').submit(function (e) {

    validateEmail();
    
    e.preventDefault();
    if (!isEmailValid) {
      return;
    }
    var email = $('#forgot-email').val();    
   

  $.ajax({
    url: '/api/forgotPassword',
    type: 'POST',
    data: JSON.stringify({
      Email: email,
    }),
    contentType : 'application/json',
    success: function (data) {
      console.log(data);
      if (data.success) {
        console.log(data.message);
        toastr.success(data.message);
      } else {
        toastr.error(data.message);
      }
    },
    error: function (err) {
      $('#email-error').text('An error occurred while processing your request');
    }
  });

});
});