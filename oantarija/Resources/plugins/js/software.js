var est = true;
$(document).ready(function () {
    ListarSoftware();
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
    $('#modalSoftware').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVA Software</p>';
    $('#cabeceraModal').html(codigo);
};

$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});

$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalSoftware').modal('close');
});

function EvaluarVacios() {
    if ($('#nombre').val() == '') {
        Materialize.toast('El campo nombre no puede estar vacio!', 8000);
        return false;
    }
    if ($('#objetivo').val() == '') {
        Materialize.toast('El campo objetivo no puede estar vacio!', 8000);
        return false;
    }
    if ($('#descripcion').val() == '') {
        Materialize.toast('El campo descripcion no puede estar vacio!', 8000);
        return false;
    }
    else {
        return true;
    }
};

function Guardar() {
    var i = $('#id').val();
    var nom = $('#nombre').val();
    var obje = $('#objetivo').val();
    var des = $('#descripcion').val();
    $.getJSON("/Software/GuardarSoftware", { id: i, nombre: nom, objetivo: obje, descripcion: des, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalSoftware').modal('close');
            ListarSoftware();
        }
    });
    ListarSoftware();
};

function Editar(id) {
    var o = { id: id };
    $.getJSON("/Software/GetSoftware", o, function (obj) {
        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR SOFTWARE ' + obj.nom + '</p>';
        $('#cabeceraModal').html(codigo);
        $("#id").val(id);
        $('#nombre').val(obj.nom);
        $('#objetivo').val(obj.obje);
        $('#descripcion').val(obj.des);        
        est = obj.estado;
        CargarEstadoEnChck(est);
        Materialize.updateTextFields();
    });
    $('#modalSoftware').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#nombre').val('');
    $('#objetivo').val('');
    $('#descripcion').val('');
};

function ListarSoftware() {
    $.getJSON("/Software/ListarSoftware", null, function (cadena) {
        $("#tabla").html(cadena);
        CrearDataTable();
    });
    $('#btnListar').show();
};

function ModalConfirmar(id) {
    $('#idEliminar').val(id);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">Esta seguro que desea eliminar el Software?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
};

$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('El Software fue eliminado exitosamente!', 8000);
    ListarSoftware();
});

$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Software/DeleteSoftware", o, function (e) {
        ListarSoftware();
    });
};
