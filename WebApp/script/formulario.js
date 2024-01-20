var companyInput = $("#company");
var cedulaInput = $("#cedula");
var contactInput = $("#contact_id");
var tittleInput = $("#tittle_id");
var emailInput = $("#email_id");
var phoneInput = $("#phone_id");
var checkbox = $("#check_id");
var submitBtn=$("#submit_id");
var time_controller = 0;
var time_valid_controller = 0;
var errorForm = false;

function removeCharacter(tecla){
  return !(tecla.charCode < 48 || tecla.charCode > 57);
}

function createHtmlByUser(responseUser,lastUser,error){
  errorForm = error;
  var dialogHtml = '<h2 class="green_text" id="user_message">'+responseUser.Message+'.</h2>'

  if(error){
    dialogHtml = '<h2 class="red_text" id="user_message">'+responseUser.Message+'.</h2>'
  }

  for (var key in responseUser.User) {
    if (responseUser.User.hasOwnProperty(key) && lastUser.hasOwnProperty(key)) {
      var resKeyValue = responseUser.User[key];
      var lastKeyValue =  lastUser[key];
      if(resKeyValue == lastKeyValue){
        dialogHtml += '<p>'+lastKeyValue+'.</p>';
      } else{
        dialogHtml += '<p class="red_text">'+lastKeyValue+'.</p>';
      }
    }
  }
  return dialogHtml;
}
function appendDialogText(responseUser, originUser){
  var dialogHtml = createHtmlByUser(responseUser, originUser,responseUser.Error);
  $("#dialog").html("");
  dialogHtml+='<button class = "wee_btn" id="accept_btn" onclick="closeDialog();"> Aceptar </button>'
  $("#dialog").html(dialogHtml);
  window.dialog.showModal();
}

function closeDialog(){
  if(!errorForm){
    $(location).attr('href',"registros.html");
  } 
  validate();
  window.dialog.close();
}

cedulaInput.on("keyup", function(tecla) {
  var cedulaLength = cedulaInput.val().length;
  if(cedulaLength >=7){
    clearTimeout(time_controller);
    time_controller = setTimeout(callCedula, 500);
  }

  if(removeCharacter(tecla)){
    return false;
  }
});

cedulaInput.keypress(function(tecla){
  return removeCharacter(tecla);
});

phoneInput.keypress(function(tecla){
  return removeCharacter(tecla);
});

function validate(){
  clearTimeout(time_valid_controller);
  time_valid_controller = setTimeout(null, 500);

  var isValid = true;
  $("input").each(function() {
    var element = $(this);
    if(element.attr('id') != "check_id" && element.attr('id') != "submit_id"){
      if (element.val().trim() == "") {
        isValid = false;
      }
    }
  });

  if(checkbox.is(':checked') == false){
    isValid = false;
  }
  submitBtn.prop('disabled', !isValid)
}

function submitForm(){
  submitBtn.prop('disabled', true)
  var user = {
    "Company": companyInput.val(),
    "Cedula": cedulaInput.val(),
    "Name": contactInput.val(),
    "Degree": tittleInput.val(),
    "Email": emailInput.val(),
    "Phone": phoneInput.val()
  }
  saveUser(user);
  event.preventDefault();
}

function saveUser(user){
  var response;
  $.ajax({
    type:"POST", 
    url:"http://localhost:58683/api/user/",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    data:JSON.stringify(user),
    success:function(datos){
      response = datos;
      appendDialogText(response, user);
    },
    failure: function (datos) {
      if(datos.responseText != null){
        response = $.parseJSON(datos.responseText);
        appendDialogText(response, user);
      }
    },
    error: function (datos) {
      console.log(datos);
      if(datos.responseText == ""){
        var userFlag = {
          "User": {
            "Company": "",
            "Cedula": "",
            "Name": "",
            "Degree": "",
            "Email": "",
            "Phone": "",
          },
          "Message": "No se pudo obtener los datos",
          "Error": true,
        }
        appendDialogText(userFlag, user); 
      } else{
        response = $.parseJSON(datos.responseText);
        appendDialogText(response, user);
      }
    }
  });
}

function callCedula() {
  $.ajax({
    type:"GET",
    url:"http://localhost:58683/api/cedula/"+cedulaInput.val(),
    success: function (data) {
      if(data != null){
        contactInput.val(data.FullName);
        tittleInput.val(data.Degree);
        validate();
      }
    },
    failure: function (data) {
      contactInput.val("Cedula inexistente");
      tittleInput.val("Cedula inexistente");
    },
    error: function (data) {
      contactInput.val("Cedula inexistente");
      tittleInput.val("Cedula inexistente");
    }
  });
}
