var est = true;
//Restringir Numeros
$(document).ready(function () {
    ListarTemas();
    ListarDisertantes();
    ListarSalas();
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
    ListarDisertantes();
    ListarSalas();
    est = true;   
    CargarEstadoEnChck(est);
    $('#modalDatos').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVO TEMA</p>';
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
    if ($('#descripcionTema').val() == '') {
        Materialize.toast('El campo de descripcion no puede estar vacio!', 8000);
        return false;
    }
    if ($('#selectDisertante').val() == null) {
        Materialize.toast('Debe seleccionar un disertante!', 8000);
        return false;
    }
    if ($('#selectSala').val() == null) {
        Materialize.toast('Debe seleccionar una sala!', 8000);
        return false;
    }
    else {
        return true;
    }
};
function Guardar() {
    var i = $('#id').val();
    var nom = $('#nombre').val();
    var des = $('#descripcionTema').val();
    var dis = $('#selectDisertante').val();
    var sal = $('#selectSala').val();
    $.getJSON("/Tema/GuardarTema", { id: i, nombre: nom, descripcion:des, disertante:dis, sala:sal, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalDatos').modal('close');
            ListarTemas();
        }
    });
    ListarTemas();
};
function Editar(id) {
    ListarDisertantes();
    ListarSalas();
    var o = { id: id };
    $.getJSON("/Tema/GetTema", o, function (obj) {

        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR TEMA ' + obj.nombre + '</p>';
        $('#cabeceraModal').html(codigo);
        $("#id").val(id);
        $("#nombre").val(obj.nombre);
        $("#descripcionTema").val(obj.descripcion);
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
    $('#descripcionTema').val('');
    $('select').material_select();
};
function ListarTemas() {
    $.getJSON("/Tema/ListarTemas", null, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
};
function ListarDisertantes() {
    $.getJSON("/Tema/ListarDisertantes", function (cadena) {
        $("#disertantes").html(cadena);
        $('select').material_select();
    });
};
function ListarSalas() {
    $.getJSON("/Tema/ListarSalas", function (cadena) {
        $("#salas").html(cadena);
        $('select').material_select();
    });
};

//Funciones para eliminar
function ModalConfirmar(id,nom) {
    //alert(id + nom);
    $('#idEliminar').val(id);
    $('#nomEliminar').val(nom);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">¿Está seguro que desea Eliminar el Tema' + nom + '?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('El tema fue eliminado exitosamente!', 8000);
    ListarTemas();
});
$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Tema/DeleteTema", o, function (e) { });
};
