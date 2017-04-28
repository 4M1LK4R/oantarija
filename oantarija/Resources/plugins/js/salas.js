var est = true;
//Restringir Numeros
$(document).ready(function () {
    ListarSalas();
    $('#capacidad').numeric();
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
    $('#modalSala').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVA SALA</p>';
    $('#cabeceraModal').html(codigo);
};


$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});
$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalSala').modal('close');
});
function EvaluarVacios() {
    if ($('#nombre').val() == '') {
        Materialize.toast('El campo nombre no puede estar vacio!', 8000);
        return false;
    }
    if ($('#capacidad').val() == '') {
        Materialize.toast('El campo capacidad no puede estar vacio!', 8000);
        return false;
    }
    else {
        return true;
    }
};
function Guardar() {
    var i = $('#id').val();
    var nom = $('#nombre').val();
    var cap = $('#capacidad').val();
    $.getJSON("/Sala/GuardarSala", { id: i, nombre: nom, capacidad: cap, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalSala').modal('close');
            ListarSalas();
        }
    });
    ListarSalas();
};
function Editar(id) {
    var o = { id: id };
    $.getJSON("/Sala/GetSala", o, function (obj) {

        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR SALA ' + obj.nombre + '</p>';
        $('#cabeceraModal').html(codigo);

        $("#id").val(id);
        $("#nombre").val(obj.nombre);
        $("#capacidad").val(obj.capacidad);
        est = obj.estado;
        CargarEstadoEnChck(est);
        //Activar Campos
        Materialize.updateTextFields();
    });

    $('#campo_estado').show();
    $('#modalSala').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#nombre').val('');
    $('#capacidad').val('');
};
function ListarSalas() {
    $.getJSON("/Sala/ListarSalas", null, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
    //$('#datatable').datatable();
};



//Funciones para eliminar
function ModalConfirmar(id,nom) {
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
    Materialize.toast('La sala fue eliminada exitosamente!', 8000);
    ListarSalas();
});
$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Sala/DeleteSala", o, function (e) { });


};
