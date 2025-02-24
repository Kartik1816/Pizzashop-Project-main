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



// Search Box



function changeColor(item){
    let categories = ['sandwich','pizza','burger','pasta','salads','dessert','sides','dips'];
    categories.forEach(function(category){
        document.getElementById(`blue-${category}`).classList.add('d-none');
        document.getElementById(`gray-${category}`).classList.remove('d-none');
        document.getElementById(`span-${category}`).classList.remove('active-category');
    });
    document.getElementById(`blue-${item}`).classList.remove('d-none');
    document.getElementById(`gray-${item}`).classList.add('d-none');
    document.getElementById(`span-${item}`).classList.add('active-category');
}




//email and password validation with regular expression using  jquaery


var isEmailValid = true;
var isPasswordValid = true;

function validateEmail () {
  var email = $('#email').val();
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

function validatePassword () {
  var password = $('#password').val();
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


  
  $('#email').on('input', function () {
    $('#login-error').text(''); 
    validateEmail();
    localStorage.setItem('email',$('#email').val());
  });
  
  $('#password').on('input', function () {
    validatePassword();
    $('#login-error').text('');
  });




$(document).ready(function () {
  
   var email = document.cookie.split(';').find(cookie => cookie.includes('email'));
    if (email)
   {
      window.location.href = '/Dashboard';
    }
  $('#login-form').submit(function (e) {

    validateEmail();
    validatePassword();
    
    e.preventDefault();
    if (!isEmailValid || !isPasswordValid) {
     
      return;
    }
    var email = $('#email').val();    
    var password = $('#password').val();
    var rememberMe=$('#remember-me').is(':checked');
    
   
  
  $.ajax({
    url: '/api/validate',
    type: 'POST',
    data: JSON.stringify({
      email: email,
      password: password
    }),
    contentType : 'application/json',
    success: function (data) {
      console.log(data);
             
      if (data.success) {
        console.log(data.token);
        document.cookie = `token=${data.token};max-age=${60*60*24*7};path=/;Samesite=Lax`;
        window.location.href='/Dashboard';
        // localStorage.setItem('token',data.token);
        
        if(rememberMe){
          var date=new Date();
          date.setTime(date.getTime()+(7*24*60*60*1000))
          document.cookie=`email=${email};expires=${date.toUTCString()}`
        }
        
      } else {
        $('#login-error').text(data.message);
      }
    },
    error: function (err) {
      $('#login-error').text('An error occurred while processing your request');
    }
  });

});

});

