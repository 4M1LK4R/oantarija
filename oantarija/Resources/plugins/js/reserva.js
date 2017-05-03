var est = true;
var v = true;
//Restringir Numeros
$(document).ready(function () {
    ListarReservas();
    ListarHorarios();
    ListarTemas();
    ListarTG();
    $('#cantidad').numeric();
    $('#cantidadProponer').numeric();
})
$('#activo').click(function () {
    est = true;
});
$('#inactivo').click(function () {
    est = false;
});
$('#vhiSi').click(function () {
    v = true;
});
$('#vhiNo').click(function () {
    v = false;
});
function CargarVehiculoEnChck(flag) {
    if (flag) {
        $('#vhiSi').prop('checked', true);
    }
    else {
        $('#vhiNo').prop('checked', true);
    }
}
function CargarEstadoEnChck(flag) {
    if (flag) {
        $('#activo').prop('checked', true);
    }
    else {
        $('#inactivo').prop('checked', true);
    }
}
function Nuevo() {
    LimpiarCampos();
    est = true;
    CargarEstadoEnChck(est);
    v = true;
    CargarVehiculoEnChck(v);
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
    //int id
    //string fecha
    //int horario
    //string temas
    //int cantidad
    //int tg
    //bool vehiculo
    //string usuario
    //bool estado
    alert(tem.toString());
    $.getJSON("/Reserva/GuardarReserva", { id: i, fecha: fech, horario: hor, temas: tem.toString(), cantidad: can, tg: tipgru, vehiculo: v, usuario: usu, estado: est }, function (e) {
        if (e != "") {
            if (e.err == "proponer") {
                alert('proponer');
                console.log(e);
                $('#idReservaProponer').val(e.iid);
                $('#Disponible').val((e.salcapacidad - e.can));
                //var cadena = '<p>Fecha: ' + e.fech + '</p>';
                //cadena += '<p>Horario: ' + e.hor + '</p>';
                //cadena += '<p>Sala: ' + e.sal + '</p>';
                //cadena += '<p>Personas: ' + e.can + '</p>';
                //cadena += '<p>Cupos disponibles: ' + (e.salcapacidad - e.can) + '</p>';
                $('#ContenidoProponer').html(cadena);
                //'La Fecha: ' + fech + ' en el Horario: ' + hor + ' en la Sala: ' + sal + ' con ' + can + ' Personas'
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
    ListarReservas();
};

$('#aceptarProponer').click(function () {
    var id = $('#idReservaProponer').val();
    var disponible = $('#Disponible').val();
    var cantidad = $('#cantidadProponer').val();
    if (cantidad < (disponible + 1)) {

        $.getJSON("/Reserva/AdicionarReserva", { id: id, cantidad: $('#cantidadProponer').val() }, function (e) { });
        Materialize.toast('Se Adiciono correctamente!', 8000);
        $('#modalProponer').modal('close');
        $('#modalDatos').modal('close');
    }
    else {
        Materialize.toast('Solo puede agregar ' + disponible + ' personas!', 8000);
    }
});
$('#cancelarProponer').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalProponer').modal('close');
});



function Editar(id) {
    var o = { id: id };
    $.getJSON("/Reserva/GetReserva", o, function (obj) {
        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR RESERVA</p>';
        $('#cabeceraModal').html(codigo);
        console.log(obj);

        $('#id').val(id);
        $('#selectFechaModal').val(obj.fech);
        $('#selectHorario').val(obj.hor);
        $('#selectTema').val([obj.tms]);
        alert($('#selectTema').val(obj.tms))
        $('#cantidad').val(obj.can);
        $('#selectTG').val(obj.tg);
        $('#usu').val(obj.usu);
        $('select').material_select();
        CargarEstadoEnChck(obj.est);
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
    $('#selectTema').val('');
    $('#cantidad').val('');
    $('select').material_select();
};
function ListarReservas() {
    $.getJSON("/Reserva/ListarReservasFechaUsuario", { fecha: $('#selectFecha').val(), usu: $('#usu').val() }, function (cadena) {
        $("#tabla").html(cadena);
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
function ModalConfirmar(id, nom) {
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
    $.getJSON("/Reserva/DeleteReserva", o, function (e) { });
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