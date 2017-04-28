function CerrarSideNav() {
    $('button-collapse').sideNav('hide');
};
$("#AbriMenu").click(function () {
    $('.button-collapse').sideNav('show');
});
$(document).ready(function () {
    console.log('Init Materialize');
    $('.modal').modal();
    $('.parallax').parallax();
    $('.button-collapse').sideNav();
    $('.tooltipped').tooltip({ delay: 50 });
    $('.slider').slider();
    $('.collapsible').collapsible();
    $('.materialboxed').materialbox();
    $('select').material_select();
    $('.scrollspy').scrollSpy();
    $('input#input_text, textarea#descripcionTema').characterCounter();
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 2 // Creates a dropdown of 15 years to control year
    });
});

console.log('init.js');
