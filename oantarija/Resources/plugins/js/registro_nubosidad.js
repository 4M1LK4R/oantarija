var est = true;
//Restringir Numeros
$(document).ready(function () {
    ListarRegisNub();
})

$('#activo').click(function () {
    est = true;
    //alert(est);

});
$('#inactivo').click(function () {
    est = false;
    //alert(est);
});




function CargarEstadoEnChck(flag) {
    if (flag) {
        //alert('Cargando estado true');
        $('#activo').prop('checked', true);
    }
    else {
        //alert('Cargando estado false');
        $('#inactivo').prop('checked', true);
    }

}



function Nuevo() {
    LimpiarCampos();
    est = true;   
    CargarEstadoEnChck(est);
    $('#campo_estado').hide();
    $('#modalRegisNub').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVO REGISTOS DE NUBOSIDAD</p>';
    $('#cabeceraModal').html(codigo);
};


$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});
$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalRegisNub').modal('close');
});
function EvaluarVacios() {
    if ($('#nubosidad').val() == '') {
        Materialize.toast('El campo nubosidad no puede estar vacio!', 8000);
        return false;
    }
    if ($('#temperatura').val() == '') {
        Materialize.toast('El campo temperatura no puede estar vacio!', 8000);
        return false;
    }
    if ($('#observaciones').val() == '') {
        Materialize.toast('El campo observaciones no puede estar vacio!', 8000);
        return false;
    }
    else {
        return true;
    }
};
function Guardar() {
    var i = $('#id').val();
    var nub = $('#nubosidad').val();
    var tem = $('#temperatura').val();
    var obs = $('#observaciones').val();
    $.getJSON("/RegistroNubosidad/GuardarRegisNub", { id: i, nubosidad: nub, temperatura: tem, observaciones: obs, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalRegisNub').modal('close');
            ListarRegisNub();
        }
    });
    ListarRegisNub();
};
function Editar(id) {
    var o = { id: id };
    $.getJSON("/RegistroNubosidad/GetRegisNub", o, function (obj) {

        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR REGISTRO DE NUBOSIDAD ' + obj.fech + '</p>';
        $('#cabeceraModal').html(codigo);

        $("#id").val(id);
        $("#nubosidad").val(obj.nub);
        $("#temperatura").val(obj.tem);
        $("#observaciones").val(obj.obs);
        est = obj.estado;
        CargarEstadoEnChck(est);
        //Activar Campos
        Materialize.updateTextFields();
    });

    $('#campo_estado').show();
    $('#modalRegisNub').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $("#nubosidad").val('');
    $("#temperatura").val('');
    $("#observaciones").val('');
};
function ListarRegisNub() {
    $.getJSON("/RegistroNubosidad/ListarRegisNub", null, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
    //$('#datatable').datatable();
};
//Funciones para eliminar
function ModalConfirmar(id) {
    //alert(id + nom);
    $('#idEliminar').val(id);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">¿Está seguro que desea el Registro de Nubosidad?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('El registro de nubosidad fue eliminado exitosamente!', 8000);
    ListarRegisNub();
});
$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/RegistroNubosidad/DeleteRegisNub", o, function (e) { });
};
