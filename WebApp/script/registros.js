getRegister();

function appendRegister(register){
  $('#register_table > tbody').empty();
  $.each(register, function (i, user) {
    var rows = 
      "<tr>" +
      "<td><b>" + user.Company + "</b></td>" +
      "<td>" + user.Cedula + "</td>" +
      "<td>" + user.Name + "</td>" +
      "<td>" + user.Degree + "</td>" +
      "<td>" + user.Email + "</td>" +
      "<td>" + user.Phone + "</td>" +
      "</tr>";
    $('#register_table > tbody').append(rows);
  });
}

function noRegister(){
  $('#register_table > tbody').empty();
  var rows = 
    "<tr>" +
    '<td colspan = "6">No hay registros en el sistema</td>' +
    "</tr>";
  $('#register_table > tbody').append(rows);
}

function getRegister(){
  $.ajax({
    type: "GET",
    url: "http://localhost:58683/api/user",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    crossDomain: true,
    success: function (data) {
     appendRegister(data)
    },
    failure: function (data) {
      noRegister();
    },
    error: function (data) {
      noRegister();
    }
  });
}