$(document).ready(function () {
    console.log($('#usu').val());

    if ($('#usu').val()!="") {
        $.getJSON("/Reserva/Notificacion", { usuario: $('#usu').val() }, function (nro) {
            Materialize.toast("Nro de reservas para hoy :" + nro, 8000);
        });
    }


});
