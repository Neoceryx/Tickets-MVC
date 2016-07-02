$(function () {
    $('#Reports').addClass('active2');

    $('#datatable1').DataTable({
        dom: 'lfrtip',
        language: {
            lengthMenu: 'Mostrar _MENU_ registros',
            search: 'Buscar',
            emptyTable: 'No existen registros',
            zeroRecords: 'No existen coincidencias',
            info: 'Mostrando del ticket _START_ al _END_ de un total de _TOTAL_ tickets',
            infoFiltered: '(filtrado de un total de _MAX_ tickets)',
            infoEmpty: 'No existen tickets',
            paginate: {
                first: 'Inicio',
                previous: 'Anterior',
                next: 'Siguiente',
                last: 'Final'
            }
        }
    });
});

//te Manda a la vista de detalle.
$('.js-clickable').on('click', function () {
    var $this = $(this);
    var id = $this.data("id");
    var url = 'Detail/' + id;
    window.location = url;    
});


$(function () {
    $('#datatable1').tooltip({
        close: function (event, ui) {
            $('.ui-helper-hidden-accessible').children().remove();
            console.log(event);
            console.log(ui);
        }
    });
});