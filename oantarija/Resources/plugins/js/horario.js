var est = true;
//Restringir Numeros
$(document).ready(function () {
    ListarHorarios();
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
    $('select').material_select('destroy');
    $('select').material_select();
    LimpiarCampos();
    est = true;   
    CargarEstadoEnChck(est);
    $('#modalDatos').modal('open');
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">CREAR NUEVO HORARIO</p>';
    $('#cabeceraModal').html(codigo);
};



//Select Horarios
$('#hInicio').change(function () {
    //alert($('#hInicio').val());
});
$('#hFin').change(function () {
    //alert($('#hFin').val());
});

//Checkboxea
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
    if ($('#hInicio').val() == null) {
        Materialize.toast('Seleciones una hora de inicio!', 8000);
        return false;
    }
    if ($('#hFin').val() == null) {
        Materialize.toast('Seleciones una hora de fin!', 8000);
        return false;
    }
    else {
        return true;
    }
};
function Guardar() {
    var i = $('#id').val();
    var nom = $('#nombre').val();
    var hi = $('#hInicio').val();
    var hf = $('#hFin').val();
    $.getJSON("/Horario/GuardarHorario", { id: i, nombre: nom, h_inicio: hi, h_fin:hf, estado: est }, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalDatos').modal('close');
            ListarHorarios()
        }
    });    
    ListarHorarios();
};
function Editar(id) {
    var o = { id: id };
    $.getJSON("/Horario/GetHorario", o, function (obj) {
        var codigo = '<p class="light-blue-text text-darken-4 flow-text">EDITAR HORARIO ' + obj.nombre + '</p>';
        //console.log(obj);

        $('#cabeceraModal').html(codigo);
        $("#id").val(id);
        $("#nombre").val(obj.nombre);

        $('select').material_select('destroy');
        $('select').material_select();
        //$('#hInicio').val(obj.h_inicio);
        //$('#hFin').val(obj.h_fin);
        est = obj.estado;
        CargarEstadoEnChck(est);
        //Activar Campos
        //$('select').material_select();
        Materialize.updateTextFields();
    });

    $('#campo_estado').show();
    $('#modalDatos').modal('open');
};

function LimpiarCampos() {
    $('#id').val(0);
    $('#nombre').val('');
 
};
function ListarHorarios() {
    $.getJSON("/Horario/ListarHorarios", null, function (cadena) {
        $("#tabla").html(cadena);
    });
    $('#btnListar').show();
    //$('#datatable').datatable();
};



//Funciones para eliminar
function ModalConfirmar(id, nom) {
    $('#idEliminar').val(id);
    $('#nomEliminar').val(nom);
    var codigo = '<p class="light-blue-text text-darken-4 flow-text">¿Está seguro que desea Eliminar el horario ' + nom + '?</p>';
    $('#cabeceraModalEliminar').html(codigo);
    $('#modalEliminar').modal('open');
}
$('#aceptarEliminar').click(function () {
    Eliminar($('#idEliminar').val());
    $('#modalEliminar').modal('close');
    Materialize.toast('El horario fue eliminado exitosamente!', 8000);
    ListarHorarios();
});
$('#cancelarEliminar').click(function () {
    $('#idEliminar').val('');
    $('#nomEliminar').val('');
    $('#modalEliminar').modal('close');
});

function Eliminar(id) {
    var o = { id: id };
    $.getJSON("/Horario/DeleteHorario", o, function (e) { });
};
