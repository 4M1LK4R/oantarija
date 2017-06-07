var est = true;
$(document).ready(function () {
    $('#cantidad').numeric();
});

$('#activo').click(function () {
    est = true;
});
$('#inactivo').click(function () {
    est = false;
});

function CargarEstadoEnChck(flag) {
    if (flag) {
        $('#activo').prop('checked', true);
    }
    else {
        $('#inactivo').prop('checked', true);
    }
};

function Nuevo() {
    LimpiarCampos();
    est = true;
    CargarEstadoEnChck(est);
    $('#modalDatos').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">REGISTRAR VISITA</p>';
    $('#cabeceraModal').html(codigo);

};

$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});

$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalDatos').modal('close');
});

//public ActionResult Guardar(int id, string hora_entrada, 
//string hora_salida, int cantidad, string proposito, 
//bool vehiculo, string tipo_v, string placa_v, 
//string color_v, string procedencia, string usuario, bool estado)


function EvaluarVacios() {
    if ($('#hInicio').val() == null) {
        Materialize.toast('Debe establecer una hora de inicio!', 8000);
        return false;
    }
    if ($('#hFin').val() == null) {
        Materialize.toast('Debe establecer una hora de fin!', 8000);
        return false;
    }
    if ($('#proposito').val() == '') {
        Materialize.toast('Debe indicar un proposito de la visita!', 8000);
        return false;
    }
    if ($('#cantidad').val() == '') {
        Materialize.toast('El campo cantidad no puede estar vacio!', 8000);
        return false;
    }

    if ($('#procedencia').val() == '') {
        Materialize.toast('Debe indicar la procendencia de la visita!', 8000);
        return false;
    }
    else {
        return true;
    }
};

function Guardar() {
    var o = {
        id: $('#id').val(),
        hora_entrada: $('#hInicio').val(),
        hora_salida: $('#hFin').val(),
        cantidad: $('#cantidad').val(),
        proposito: $('#proposito').val(),
        vehiculo: est,
        tipo_v: $('#tipo_v').val(),
        placa_v: $('#placa').val(),
        color_v: $('#color_v').val(),
        procedencia: $('#procedencia').val(),
        usuario: $('#usu').val(),
        estado: est
    };
    //Guardar(int id, string hora_entrada, string hora_salida, int cantidad, 
    //string proposito, bool vehiculo, string tipo_v, string placa_v, string 
    //color_v, string procedencia, string usuario, bool estado)
    $.getJSON("/Visita/Guardar", o , function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);

            LimpiarCampos();
            $('#modalDatos').modal('close');
            ListarReservas();
        }
    });
};

function Editar(id) {
    var o = { id: id };
    $.getJSON("/Visita/Get", o, function (obj) {
        $('#id').val(id);
        $('#hInicio').val(obj.hora_entrada);
        $('#hFin').val(obj.hora_salida);
        $('#cantidad').val(obj.cantidad_personas);
        $('#proposito').val(obj.proposito);
        $('#tipo_v').val(obj.tipo_vehiculo);
        $('#placa').val(obj.placa_vehiculo);
        $('#color_v').val(obj.color_vehiculo);
        $('#procedencia').val(obj.procedencia);
        CargarEstadoEnChck(obj.vehiculo);
        $('select').material_select();
        Materialize.updateTextFields();
    });
    $('#campo_estado').show();
    $('#modalDatos').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#hInicio').val();
    $('#hFin').val();
    $('#cantidad').val();
    $('#proposito').val();
    $('#tipo_v').val();
    $('#placa').val();
    $('#color_v').val();
    $('#procedencia').val();
    $('select').material_select();
};
function ListarReservas() {
    $.getJSON("/Reserva/ListarReservas", { fecha: $('#selectFecha').val() }, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
};
function ModalConfirmar(id) {
    $('#idEliminar').val(id);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">Esta seguro que desea Eliminar la Reserva?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
};

$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('La Visita fue eliminada exitosamente!', 8000);
    ListarReservas();
});

$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Reserva/DeleteReserva", o, function (e) {
        ListarReservas();
    });
};

$('#btnFecha').click(function () {
    var bfecha = $('#selectFecha').val();
    ListarReservas();
    $('#campoFecha').html(bfecha);
    Materialize.toast('Turnos disponilbes para ' + bfecha, 4000);
    $('#btnListar').show();
});

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
