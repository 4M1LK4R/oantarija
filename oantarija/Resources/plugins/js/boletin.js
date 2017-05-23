var est = true;
//Restringir Numeros
$(document).ready(function () {
    ListarBoletines();
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
    $('#modalDatos').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVO BOLETIN</p>';
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
function EvaluarVacios() {
    if ($('#nombre').val() == '') {
        Materialize.toast('El campo nombre no puede estar vacio!', 8000);
        return false;
    }
    else {
        return true;
    }
};
function Guardar() {
    var i = $('#id').val();
    var nom = $('#nombre').val();
    var ur = $('#url').val();
    var des = $('#descripcion').val();
    $.getJSON("/Boletin/GuardarBoletin", { id: i, nombre: nom, descripcion:des, url: ur, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalDatos').modal('close');
            ListarBoletines();
        }
    });
    ListarBoletines();
};
function Editar(id) {
    var o = { id: id };
    $.getJSON("/Boletin/GetBoletin", o, function (obj) {

        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR BOLETIN ' + obj.nombre + '</p>';
        $('#cabeceraModal').html(codigo);
        $("#id").val(id);
        $("#nombre").val(obj.nombre);
        $("#descripcion").val(obj.descripcion);
        $("#url").val(obj.url);
        est = obj.estado;
        CargarEstadoEnChck(est);
        //Activar Campos
        Materialize.updateTextFields();
    });

    $('#campo_estado').show();
    $('#modalDatos').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#nombre').val('');
    $('#descripcion').val('');
    $('#url').val('');
};
function ListarBoletines() {
    $.getJSON("/Boletin/ListarBoletines", { usu : $('#usu').val()}, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
    //$('#datatable').datatable();
};



//Funciones para eliminar
function ModalConfirmar(id, nom) {
    //alert(id + nom);
    $('#idEliminar').val(id);
    $('#nomEliminar').val(nom);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">¿Está seguro que desea Eliminar la sala ' + nom + '?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('El tipo de grupo fue eliminado exitosamente!', 8000);
    ListarBoletines();
});
$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Boletin/DeleteBoletin", o, function (e) {
        ListarBoletines();
    });
};
