var addModule = (function () {
    var CONSTANTS = {
        TYPE: 'POST',
        CONTENTTYPE: 'application/json; charset=utf-8',
        DATATYPE: 'json'
    }

    var variables = {
        solutionUrl: '',
        helperUrl:''
    }

    function privateClearImageError()
    {
        $('#fileUpload').on('click', function () {
            $('#js-file-error').text(' ');
        });

    }

    function privateGetHelper(id)
    {
        var params = { id: id };
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType:CONSTANTS.DATATYPE,
            url: variables.helperUrl,
            data:JSON.stringify(params),
            success: function(data){
                if (data && data.Status){
                    $('.js-helper-request').text(data.Status);
                }
            },
            error: function (err) {
                console.error(err);
            }
        });
    }

    function privateGetResolution(solution) {
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url: variables.solutionUrl,
            data: JSON.stringify(solution),
            success: function (data) {
                if (data && data.Status) {
                    $('#Js-days').text(data.Status);
                }
            },
            error: function (err) {
                console.error(err);
            }
        });
    }

    function privateSelectMenuItem() {
        $('#Add').addClass('active2');
    }

    function privateValidateSubmit() {
        var radios = $('input[name="Priority"]');
        var description = $('.js-ticket-description').val();
        var file = $('#fileUpload').val();
        var radioValid = false;
        var descriptionValid = false;
        var fileValid = true;
        var priority = $('#Js-days').text().trim();

        // validate at least a radio is checked
        radios.each(function (index, element) {
            if (radios.eq(index).prop('checked')) {
                radioValid = true;                
            }
        });
        
        // validate a description is typed
        descriptionValid = description.trim().length > 0 ? true : false;

        // validate if file is choosen it is of a correct type
        if (file.length > 0) {
            if (file.indexOf('.png') > 0 || file.indexOf('.gif') > 0 || file.indexOf('.jpg') > 0) {
                fileValid = true;
            } else {
                fileValid = false;
            }
        }
        var val = $('form').validate();
        val.checkForm();
         if (!fileValid)
            {
                $('#js-file-error').text('El archivo solo puede ser .jpg, .gif o .png');
            }
        if (!val.valid()) {
            val.showErrors();
           
            
        }
        if (radioValid && descriptionValid && fileValid) {
            swal({
                title: 'Ticket creado!',
                text: priority,
                type: 'success',
                allowEscapeKey: false
            },
            function (isConfirm) {
                console.log(isConfirm);
                $('form').submit();
            });
        }
    }

    return {
        setSolutionUrl: function (url) {
            variables.solutionUrl = url;
        },
        getResolution: function (solution) {
            if (variables.solutionUrl) {
                privateGetResolution(solution);
            }
            else {
                console.error('no solution url has been set');
            }
        },
        setHelperUrl:function(url)
        {
            variables.helperUrl=url;
        },
        getHelper: function (id){
            if (variables.helperUrl) {
                privateGetHelper(id);
            }
            else{
                console.error('no solution url has been set');
            }
        },
        submit: function() {
            privateValidateSubmit();
        },
        init: function () {
            privateSelectMenuItem();
            privateClearImageError();
        }
    }
})();


$(function () {
    $('.js-radio').on('click', function () {
        var id = $(this).val();
        var solution = { id: $(this).val() };
        addModule.getResolution(solution);
    });

    $('.js-select-request').on('change', function () {
        var id = $(this).val();
        addModule.getHelper(id);
    });

    $(document).ready(function () {
        var requestid = $('select>option').val();
        addModule.getHelper(requestid);
    });


    $('#js-submit-ticket').on('click', function (e) {
        
        addModule.submit();
    });

    $('js-submit-ticket').on('click', function () {
        if($('#js-file-error').val()!=null){
            $('.js-ticket-description').val('');
            $('.radio').prop('checked', false);
            $("#RequestType").val($("#RequestType option:first").val());
        }
    });

    $('.navbar-nav > li').on('click', function () {
        var $this = $(this);
        $this.addClass('active2');
        $this.siblings().removeClass('active2');
    });

});




