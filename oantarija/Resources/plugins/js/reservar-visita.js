$('#cardMa').click(function () {
    var codigo = '<div class="blue">';
    codigo += '<p class="white-text flow-text">Reserva para turno mañana</p>';
    codigo += '<img src="images/maniana_white.png" width="20%">';
    codigo += '</div>';
    $('#cabeceraModal').html(codigo);

    codigo = '<select id="selectHoraInicio">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="8">8 am</option>';
    codigo += '<option value="9">9 am</option>';
    codigo += '<option value="10">10 am</option>';
    codigo += '</select>';
    $('#campoHoraInicio').html(codigo);


    codigo = '<select id="selectHoraFin">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="9">9 am</option>';
    codigo += '<option value="10">10 am</option>';
    codigo += '<option value="11">11 am</option>';
    codigo += '</select>';
    $('#campoHoraFin').html(codigo);

    $('select').material_select();
    $('#modalReseva').modal('open');
});
$('#cardMe').click(function () {
    var codigo = '<div class="yellow darken-2">';
    codigo += '<p class="white-text flow-text">Reserva para turno medio día</p>';
    codigo += '<img src="images/mediodia_white.png" width="20%">';
    codigo += '</div>';
    $('#cabeceraModal').html(codigo);
    codigo = '<select id="selectHoraInicio">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="11">11 am</option>';
    codigo += '<option value="12">12 pm</option>';
    codigo += '<option value="13">1 pm</option>';
    codigo += '</select>';
    $('#campoHoraInicio').html(codigo);


    codigo = '<select id="selectHoraFin">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="12">12 pm</option>';
    codigo += '<option value="13">1 pm</option>';
    codigo += '<option value="14">2 pm</option>';
    codigo += '</select>';
    $('#campoHoraFin').html(codigo);

    $('select').material_select();
    $('#modalReseva').modal('open');
});
$('#cardTa').click(function () {
    var codigo = '<div class="deep-orange darken-1">';
    codigo += '<p class="white-text flow-text">Reserva para turno tarde</p>';
    codigo += '<img src="images/tarde_white.png" width="20%">';
    codigo += '</div>';
    $('#cabeceraModal').html(codigo);
    codigo = '<select id="selectHoraInicio">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="14">2 pm</option>';
    codigo += '<option value="15">3 pm</option>';
    codigo += '<option value="16">4 pm</option>';
    codigo += '</select>';
    $('#campoHoraInicio').html(codigo);


    codigo = '<select id="selectHoraFin">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="15">3 pm</option>';
    codigo += '<option value="16">4 pm</option>';
    codigo += '<option value="17">5 pm</option>';
    codigo += '</select>';
    $('#campoHoraFin').html(codigo);

    $('select').material_select();
    $('#modalReseva').modal('open');
});
$('#cardNo').click(function () {
    var codigo = '<div class="blue-grey darken-1">';
    codigo += '<p class="white-text flow-text">Reserva para turno noche</p>';
    codigo += '<img src="images/noche_white.png" width="20%">';
    codigo += '</div>';
    $('#cabeceraModal').html(codigo);

    codigo = '<select id="selectHoraInicio">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="17">5 pm</option>';
    codigo += '<option value="18">6 pm</option>';
    codigo += '<option value="19">7 pm</option>';
    codigo += '</select>';
    $('#campoHoraInicio').html(codigo);


    codigo = '<select id="selectHoraFin">';
    codigo += '<option value="" disabled selected>(Seleccionar hora)</option>';
    codigo += '<option value="18">6 pm</option>';
    codigo += '<option value="19">7 pm</option>';
    codigo += '<option value="20">8 pm</option>';
    codigo += '</select>';
    $('#campoHoraFin').html(codigo);

    $('select').material_select();
    $('#modalReseva').modal('open');
});

//Agregar o quitar personas a reserva de visita
var cantPer = 1;
$('#plusPers').click(function () {
    cantPer++;
    $('#CantidadPersonas').html(cantPer);
});
$('#minusPers').click(function () {
    if (cantPer == 1) {
        Materialize.toast('Una visita necesita almenos 1 persona!', 4000);
    }
    else {
        cantPer--;
        $('#CantidadPersonas').html(cantPer);
    }
});

//Selects Hora Inicio y Hora Fin
$('#selectHoraInicio').change(function () {

    verificarHoras();
});
$('#selectHoraFin').change(function () {

    verificarHoras();
});

$('#selectTema').change(function () {
    var tema = $('#selectTema').val();

});

$('#chkVehiculo').change(function () {
    if ($('#chkVehiculo').is(':checked')) {
        alert('si');
    }
    else {
        alert('no');
    }

});



function verificarHoras() {
    var flag = true;
    var hi = $('#selectHoraInicio').val();
    var hf = $('#selectHoraFin').val();
    if (hi != null && hf != null) {
        if (hf < hi) {
            Materialize.toast('La hora de fin no puede ser mayor a la hora de inicio!', 4000);
            flag = false;
        }
        if (hf == hi) {
            Materialize.toast('La hora de inicio y la hora de fin no pueden ser iguales!', 4000);
            flag = false;
        }
    }
    return flag;
};


$('#aceptar').click(function () {
    if (verificarHoras()) {
        alert(verificarHoras());
    }
    else {
        alert(verificarHoras());
    }

});


//BuscarFecha
$('#btnFecha').click(function(){
    var bfecha = $('#selectFecha').val();
    $('#campoFecha').html(bfecha);
    Materialize.toast('Turnos disponilbes para '+bfecha,4000);
});


//Cargar Fecha Actual
$(document).ready(function () {
    mostrarFechaActual();
});
function mostrarFechaActual() {
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    $('#selectFecha').val(today);

    var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
    var f = new Date();
    var fecha = f.getDate() + " " + meses[f.getMonth()] + ", " + f.getFullYear();
    $('#campoFecha').html(fecha);
};


//Imprimir Comprobantes

function printElem(divId) {
    var content = document.getElementById(divId).innerHTML;
    var mywindow = window.open('', 'Print', 'height=600,width=800');

    mywindow.document.write('<html><head><title>Print</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write(content);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus()
    mywindow.print();
    mywindow.close();
    return true;
}