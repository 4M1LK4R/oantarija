var est = true;
//Restringir Numeros
$(document).ready(function () {
    ListarCamaras();
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
    $('#modalCamara').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVA CAMARA</p>';
    $('#cabeceraModal').html(codigo);
};


$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});
$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalCamara').modal('close');
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
    if ($('#dim_chip').val() == '') {
        Materialize.toast('El campo dimension de chip no puede estar vacio!', 8000);
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
    var dim = $('#dim_chip').val();
    $.getJSON("/Camara/GuardarCamara", { id: i, nombre: nom, marca: mar, dim_chip: dim, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalCamara').modal('close');
            ListarCamaras();
        }
    });
    ListarCamaras();
};
function Editar(id) {
    var o = { id: id };
    $.getJSON("/Camara/GetCamara", o, function (obj) {

        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR CAMARA ' + obj.nom + '</p>';
        $('#cabeceraModal').html(codigo);

        $("#id").val(id);
        $('#nombre').val(obj.nom);
        $('#marca').val(obj.mar);
        $('#dim_chip').val(obj.dim_ch);        
        est = obj.estado;
        CargarEstadoEnChck(est);
        //Activar Campos
        Materialize.updateTextFields();
    });
    $('#modalCamara').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#nombre').val('');
    $('#marca').val('');
    $('#dim_chip').val('');
};
function ListarCamaras() {
    $.getJSON("/Camara/ListarCamara", null, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
    //$('#datatable').datatable();
};
//Funciones para eliminar
function ModalConfirmar(id) {
    //alert(id + nom);
    $('#idEliminar').val(id);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">¿Está seguro que desea eliminar la Camara?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('La Camara fue eliminado exitosamente!', 8000);
    ListarCamaras();
});
$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Camara/DeleteCamara", o, function (e) { });
};
