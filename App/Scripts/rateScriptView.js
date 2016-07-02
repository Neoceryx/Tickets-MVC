var rateModule = (function () {
    var CONSTANTS = {
        TYPE: 'POST',
        CONTENTTYPE: 'application/json; charset=utf-8',
        DATATYPE: 'json'
    }

    var variables = {
        rateUrl: ''
    }

    function privateUpddateRate(rate) {
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url: variables.rateUrl,
            data: JSON.stringify(rate),
            success: function (data) {
                console.log(data);
            },
            error: function (err) {
                console.error(err);
            }
        })
    }

    return {
        setRateUrl: function (url) {
            variables.rateUrl = url;
        },

        UpdateRate: function (ticket) {
            if (variables.rateUrl) {
                privateUpddateRate(ticket);
            }
            else {
                console.error('Update rate has not been set');
            }
        }
    }
})();

// JQuery code
$(function () {

    //Star Ranking.
    var ratecurrentval = $(".js-rate").data('rate')
    $(".js-rate").rateYo({
        fullStar: true,
        starWidth: "50px",
        rating: ratecurrentval,
        onSet: function (rate, rateYoInstance) {
            var ticket = {
                Id: $('.js-ticket').data('id'),
                Rate: rate,
            }
            rateModule.UpdateRate(ticket)

            swal({
                title: "¡Gracias!",
                text: "Gracias por tu opinion:",
                type: "success",
                showCancelButton: false,
                confirmButtonText: "Okay!",
                //cancelButtonText: "Cancelar",
                closeOnConfirm: false,
                closeOnCancel: false
            },

        function (isConfirm) {
            if (isConfirm) {
                window.location = '/Ticket/All';
            } 
        });




        }
    });
});
