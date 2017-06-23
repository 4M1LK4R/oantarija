var est = true;
$(document).ready(function () {
    ListarHorarios();
    ListarTemas();
    ListarTG();
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
    console.log(id);
    $.getJSON("/Visita/Get", o, function (obj) {
        
        if (o == 'vacio') {
            $('#id').val(id);
        }
        else {
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

        }

        

        

    });
    $('#campo_estado').show();
    $('#modalDatos').modal('open');
};

function LimpiarCampos() {
    //$('#id').val(0);
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





//Reserva Rapida

function Nuevo_r() {
    LimpiarCampos_r();
    est = true;
    CargarEstadoEnChck(est);
    $('#modalDatos_r').modal('open');
};



$('#aceptar_r').click(function () {
    if (EvaluarVacios_r()) {
        Guardar_r();
    }
});
$('#cancelar_r').click(function () {
    LimpiarCampos();
    $('#modalDatos_r').modal('close');
});
function EvaluarVacios_r() {
    if ($('#selectFechaModal_r').val() == '') {
        Materialize.toast('El campo fecha no puede estar vacio!', 8000);
        return false;
    }
    if ($('#selectHorario').val() == null) {
        Materialize.toast('Debe seleccionar un horario!', 8000);
        return false;
    }
    if ($('#selectTema').val() == null) {
        Materialize.toast('Debe seleccionar un tema!', 8000);
        return false;
    }
    if ($('#cantidad_r').val() == '') {
        Materialize.toast('El campo cantidad no puede estar vacio!', 8000);
        return false;
    }
    if ($('#cantidad_r').val() > 50) {
        Materialize.toast('El nro. maximo de personas es 50!', 8000);
        return false;
    }

    if ($('#selectTG').val() == null) {
        Materialize.toast('Debe seleccionar un tipo de grupo!', 8000);
        return false;
    }
    else {
        return true;
    }
};
function Guardar_r() {

    var i = $('#id_r').val();
    var fech = $('#selectFechaModal_r').val();
    var hor = $('#selectHorario').val();
    var tem = $('#selectTema').val();
    var can = $('#cantidad_r').val();
    var tipgru = $('#selectTG').val();
    var usu = $('#usu_r').val();
    $.getJSON("/Reserva/GuardarReserva", { id: i, fecha: fech, horario: hor, temas: tem.toString(), cantidad: can, tg: tipgru, usuario: usu, estado: true }, function (e) {
        if (e != "") {
            if (e.err == "proponer") {
                //$('#idReservaProponer').val(e.iid);
                //$('#Disponible').val((50 - e.can));
                //var cadena = '<h5>Ya existe una reserva para la <b>Fecha: </b>' + e.fech + ' en el <br><b>Horario: </b>' + e.hor + '</h5>';
                //cadena += '<br><b>Con:</b>';
                //cadena += '<br><b>Tema(s): </b>' + e.tms;
                //cadena += '<br><b>Nro Personas: </b>' + e.can;
                //cadena += '<br><h4>Desea sumarse a la visita?</h4>';
                //cadena += '<br><b>Cupos disponibles: <b>' + (50 - e.can);
                //$('#ContenidoProponer').html(cadena);
                //$('#cantidadProponer').val('');
                //$('#modalProponer').modal('open');
                Materialize.toast('Ya existe una reserva para esa fecha y horario!', 8000);
            }
            else {
                Materialize.toast(e, 8000);
            }
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);

            LimpiarCampos_r();
            $('#modalDatos_r').modal('close');
            //ListarReservas();
        }
    });
    //ListarReservas();
};

function LimpiarCampos_r() {
    $('#id_r').val(0);
    $('#cantidad_r').val('');
    $('#selectFechaModal_r').val('');
    $('#selectHorario').val('');
    $('#selectTG').val('');
    $('select').material_select();
};

function ListarHorarios() {
    $.getJSON("/Reserva/ListarHorarios", function (cadena) {
        $("#campoHorario").html(cadena);
        $('select').material_select();
    });
}
function ListarTemas() {
    $.getJSON("/Reserva/ListarTemas", function (cadena) {
        $('#campoTema').html(cadena);
        $('select').material_select();
    });
};
function ListarTG() {
    $.getJSON("/Reserva/ListarTG", function (cadena) {
        $('#campoTG').html(cadena);
        $('select').material_select();
    });
};





