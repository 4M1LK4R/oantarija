$(document).ready(function () {
    LimpiarCampos();
});
var est = true;


function Nuevo() {
    LimpiarCampos();
    est = true;
    //CargarEstadoEnChck(est);
    $('#modalRegistro').modal('open');
};


$('#aceptar').click(function () {
    if (EvaluarVacios()) {
        Guardar();
    }
});
$('#cancelar').click(function () {
    LimpiarCampos();
    $('#modalRegistro').modal('close');
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
    var nom = $('#nombreRe').val();
    var ape = $('#apellidoRe').val();
    var cor = $('#correoRe').val();
    var pas = $('#passRe').val();
    //CrearCuenta(string nombre, string apellido, string correo, string pass)
    $.getJSON("/Cuenta/CrearCuenta", { nombre: nom, apellido:ape, correo: cor,pass:pas}, function (e) {
        if (e != "") {
            Materialize.toast(e, 8000);
        }
        else {
            Materialize.toast('Registro exitoso!', 8000);
            LimpiarCampos();
            $('#modalRegistro').modal('close');
            ListarBoletines();
        }
    });
    ListarBoletines();
};

function LimpiarCampos() {
    $('#nik').val('');
    $('#pass').val('');
    $('#correoRe').val('');
    $('#nombreRe').val('');
    $('#apellidoRe').val('');
    $('#passRe').val('');
  //  alert('Limpiando');
};
