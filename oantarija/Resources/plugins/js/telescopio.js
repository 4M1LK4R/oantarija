var est = true;
$(document).ready(function () {
    ListarTelescopios();
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
    $('#campo_estado').hide();
    $('#modalTelescopio').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVO TELESCOPIO</p>';
    $('#cabeceraModal').html(codigo);
};

$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});

$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalTelescopio').modal('close');
});

function EvaluarVacios() {
    if ($('#nombre').val() == '') {
        Materialize.toast('El campo nombre no puede estar vacio!', 8000);
        return false;
    }
    if ($('#marca').val() == '') {
        Materialize.toast('El campo marca no puede estar vacio!', 8000);
        return false;
    }
    if ($('#tipo').val() == '') {
        Materialize.toast('El campo tipo no puede estar vacio!', 8000);
        return false;
    }
    if ($('#diametro').val() == '') {
        Materialize.toast('El campo diametro no puede estar vacio!', 8000);
        return false;
    }
    if ($('#dis_focal').val() == '') {
        Materialize.toast('El campo dis_focal no puede estar vacio!', 8000);
        return false;
    }
    if ($('#montura').val() == '') {
        Materialize.toast('El campo montura no puede estar vacio!', 8000);
        return false;
    }
    else {
        return true;
    }
};

function Guardar() {
    var i = $('#id').val();
    var nom = $('#nombre').val();
    var mar = $('#marca').val();
    var tip = $('#tipo').val();
    var dia = $('#diametro').val();
    var dis_f = $('#dis_focal').val();
    var mon = $('#montura').val();

    $.getJSON("/Telescopio/GuardarTelescopio", { id: i, nombre: nom, marca: mar, tipo: tip, diametro: dia, dis_focal: dis_f, montura: mon, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalTelescopio').modal('close');
            ListarTelescopios();
        }
    });
    ListarTelescopios();
};

function Editar(id) {
    var o = { id: id };
    $.getJSON("/Telescopio/GetTelescopio", o, function (obj) {

        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR TELESCOPIO ' + obj.nom + '</p>';
        $('#cabeceraModal').html(codigo);

        $("#id").val(id);
        $('#nombre').val(obj.nom);
        $('#marca').val(obj.mar);
        $('#tipo').val(obj.tip);
        $('#diametro').val(obj.dia);
        $('#dis_focal').val(obj.dis_f);
        $('#montura').val(obj.mon);
        est = obj.estado;
        CargarEstadoEnChck(est);
        Materialize.updateTextFields();
    });
    $('#campo_estado').show();
    $('#modalTelescopio').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#nombre').val('');
    $('#marca').val('');
    $('#tipo').val('');
    $('#diametro').val('');
    $('#dis_focal').val('');
    $('#montura').val('');
};

function ListarTelescopios() {
    $.getJSON("/Telescopio/ListarTelescopio", null, function (cadena) {
        $("#tabla").html(cadena);
        CrearDataTable();
    });
    $('#btnListar').show();
};

function ModalConfirmar(id) {
    $('#idEliminar').val(id);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">¿Está seguro que desea eliminar el Telescopio?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('El Telescopio fue eliminado exitosamente!', 8000);
    ListarTelescopios();
});

$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Telescopio/DeleteTelescopio", o, function (e) { });
};
