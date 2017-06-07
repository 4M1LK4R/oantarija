//Restringir Numeros
$(document).ready(function () {
    ListarReservas();
    ListarHorarios();
    ListarTemas();
    ListarTG();
    $('#cantidad').numeric();
    $('#cantidadProponer').numeric();
})

function Nuevo() {
    LimpiarCampos();
    $('#modalDatos').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">NUEVA RESERVA</p>';
    $('#cabeceraModal').html(codigo);
    //ListarCampos();
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
function EvaluarVacios() {
    if ($('#selectFechaModal').val() == '') {
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
    if ($('#cantidad').val() == '') {
        Materialize.toast('El campo cantidad no puede estar vacio!', 8000);
        return false;
    }
    if ($('#cantidad').val() > 50) {
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
function Guardar() {

    var i = $('#id').val();
    var fech = $('#selectFechaModal').val();
    var hor = $('#selectHorario').val();
    var tem = $('#selectTema').val();
    var can = $('#cantidad').val();
    var tipgru = $('#selectTG').val();
    var usu = $('#usu').val();
    $.getJSON("/Reserva/GuardarReserva", { id: i, fecha: fech, horario: hor, temas: tem.toString(), cantidad: can, tg: tipgru, usuario: usu, estado: true }, function (e) {
        if (e != "") {
            if (e.err == "proponer") {
                $('#idReservaProponer').val(e.iid);
                $('#Disponible').val((50 - e.can));
                var cadena = '<h5>Ya existe una reserva para la <b>Fecha: </b>' + e.fech + ' en el <br><b>Horario: </b>' + e.hor + '</h5>';
                cadena += '<br><b>Con:</b>';
                cadena += '<br><b>Tema(s): </b>' + e.tms;
                cadena += '<br><b>Nro Personas: </b>' + e.can;
                cadena += '<br><h4>Desea sumarse a la visita?</h4>';
                cadena += '<br><b>Cupos disponibles: <b>' + (50 - e.can);
                $('#ContenidoProponer').html(cadena);
                $('#cantidadProponer').val('');
                $('#modalProponer').modal('open');
            }
            else {
                Materialize.toast(e, 8000);
            }
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);

            LimpiarCampos();
            $('#modalDatos').modal('close');
            ListarReservas();
        }
    });
    //ListarReservas();
};

$('#aceptarProponer').click(function () {
    var id = $('#idReservaProponer').val();
    var disponible = $('#Disponible').val();
    var cantidad = $('#cantidadProponer').val();
    if (cantidad <= disponible) {
        $.getJSON("/Reserva/AdicionarReserva", { idre: id, usu: $('#usu').val(), cantidad: $('#cantidadProponer').val() }, function (e) { });
        $('#modalProponer').modal('close');
        $('#modalDatos').modal('close');
        Materialize.toast('Se Adiciono correctamente!', 8000);
        ListarReservas();
    }
    else {
        Materialize.toast('Solo se pueden sumar ' + disponible + ' personas!', 8000);
    }
});
$('#cancelarProponer').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalProponer').modal('close');
});


function Sumarse(iid, can) {
    if (can != 50) {
        $('#idReservaProponer').val(iid);
        $('#Disponible').val((50 - can));
        var cadena = '<br><h4>Desea sumarse a la visita?</h4>';
        cadena += '<br><b>Cupos disponibles: <b>' + (50 - can);
        $('#ContenidoProponer').html(cadena);
        $('#cantidadProponer').val('');
        $('#modalProponer').modal('open');
    }
    else {
        Materialize.toast('Ya no se pueden adicionar personas a la reserva!', 8000);
    }
};


function Editar(id) {
    var o = { id: id };
    $.getJSON("/Reserva/GetReserva", o, function (obj) {
        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR RESERVA</p>';
        $('#cabeceraModal').html(codigo);
        $('#id').val(id);
        $('#selectFechaModal').val(obj.fech);
        $('#selectHorario').val(obj.hor);

        var array = obj.tms.split(",");
        var arrayAux = [];

        for (var i = 0; i < array.length; i++) {
            var elem = parseInt(array[i]);
            arrayAux.push(elem);
        }

        $('#selectTema').val(arrayAux);

        $('#cantidad').val(obj.can);
        $('#selectTG').val(obj.tg);
        $('#usu').val(obj.usu);
        $('select').material_select();
        //Activar Campos
        Materialize.updateTextFields();
    });
    $('#campo_estado').show();
    $('#modalDatos').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#cantidad').val('');
    $('#selectFechaModal').val('');
    $('#selectHorario').val('');
    $('#selectTG').val('');
    $('#cantidad').val('');
    $('select').material_select();
};
function ListarReservas() {
    $.getJSON("/Reserva/ListarReservasFechaUsuario", { fecha: $('#selectFecha').val(), usu: $('#usu').val() }, function (cadena) {
        $("#tabla").html(cadena);
        CrearDataTable();
    });
    $('#btnListar').show();
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
//Funciones para eliminar
function ModalConfirmar(id) {
    $('#idEliminar').val(id);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">Esta seguro que desea Eliminar la Reserva?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('La Reserva fue eliminada exitosamente!', 8000);
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

//BuscarFecha
$('#btnFecha').click(function () {
    var bfecha = $('#selectFecha').val();
    ListarReservas();
    $('#campoFecha').html(bfecha);
    Materialize.toast('Turnos disponilbes para ' + bfecha, 4000);
    $('#btnListar').show();
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
//Imprimir Comprobante
function printElem(divId) {
    var content = document.getElementById(divId).innerHTML;
    var mywindow = window.open('', 'Print', 'height=600,width=800');
    mywindow.document.write('<html><head><title>Comprobante OAN</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write(content);
    mywindow.document.write('</body></html>');
    mywindow.document.close();
    mywindow.focus()
    mywindow.print();
    mywindow.close();
    return true;
}